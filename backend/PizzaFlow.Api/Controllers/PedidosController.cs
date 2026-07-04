using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaFlow.Api.Dtos;
using PizzaFlow.Api.Models;
using PizzaFlow.Api.Repositories;

namespace PizzaFlow.Api.Controllers;

[ApiController]
[Route("api/pedidos")]
[Authorize]
public class PedidosController : ControllerBase
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IRepository<Pizza> _pizzaRepository;
    private readonly IMapper _mapper;

    public PedidosController(IPedidoRepository pedidoRepository, IRepository<Pizza> pizzaRepository, IMapper mapper)
    {
        _pedidoRepository = pedidoRepository;
        _pizzaRepository = pizzaRepository;
        _mapper = mapper;
    }

    private int UsuarioIdLogado =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    // POST /api/pedidos -> processa o carrinho e cria o pedido
    [HttpPost]
    public async Task<ActionResult<PedidoDto>> Create([FromBody] PedidoCreateDto dto)
    {
        if (dto.Itens == null || dto.Itens.Count == 0)
            return BadRequest("O pedido precisa ter ao menos um item.");

        var pedido = new Pedido
        {
            UsuarioId = UsuarioIdLogado,
            DataPedido = DateTime.UtcNow,
            Status = StatusPedido.Recebido
        };

        decimal total = 0;
        foreach (var item in dto.Itens)
        {
            var pizza = await _pizzaRepository.GetByIdAsync(item.PizzaId);
            if (pizza == null || !pizza.Disponivel)
                return BadRequest($"Pizza {item.PizzaId} indisponível.");

            var itemPedido = new ItemPedido
            {
                PizzaId = pizza.Id,
                Quantidade = item.Quantidade,
                PrecoUnitario = pizza.Preco
            };
            total += pizza.Preco * item.Quantidade;
            pedido.Itens.Add(itemPedido);
        }
        pedido.ValorTotal = total;

        await _pedidoRepository.AddAsync(pedido);
        await _pedidoRepository.SaveChangesAsync();

        var completo = await _pedidoRepository.GetComItensAsync(pedido.Id);
        return Ok(_mapper.Map<PedidoDto>(completo));
    }

    // GET /api/pedidos/meus -> histórico do usuário logado
    [HttpGet("meus")]
    public async Task<ActionResult<List<PedidoResumoDto>>> MeusPedidos()
    {
        var pedidos = await _pedidoRepository.GetByUsuarioIdAsync(UsuarioIdLogado);
        return Ok(_mapper.Map<List<PedidoResumoDto>>(pedidos));
    }

    // GET /api/pedidos/{id} -> detalhe de um pedido específico
    [HttpGet("{id}")]
    public async Task<ActionResult<PedidoDto>> GetById(int id)
    {
        var pedido = await _pedidoRepository.GetComItensAsync(id);
        if (pedido == null) return NotFound();
        if (pedido.UsuarioId != UsuarioIdLogado && !User.IsInRole("Gestor"))
            return Forbid();

        return Ok(_mapper.Map<PedidoDto>(pedido));
    }
}
