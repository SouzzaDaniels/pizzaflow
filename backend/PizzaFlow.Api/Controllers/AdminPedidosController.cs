using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaFlow.Api.Dtos;
using PizzaFlow.Api.Models;
using PizzaFlow.Api.Repositories;

namespace PizzaFlow.Api.Controllers;

[ApiController]
[Route("api/admin/pedidos")]
[Authorize(Roles = "Gestor")]
public class AdminPedidosController : ControllerBase
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IMapper _mapper;

    public AdminPedidosController(IPedidoRepository pedidoRepository, IMapper mapper)
    {
        _pedidoRepository = pedidoRepository;
        _mapper = mapper;
    }

    // GET /api/admin/pedidos -> fila de pedidos ativos, consumida pelo App Flutter via long polling (15s)
    [HttpGet]
    public async Task<ActionResult<List<PedidoAdminDto>>> GetFila()
    {
        var pedidos = await _pedidoRepository.GetAtivosParaGestorAsync();
        return Ok(_mapper.Map<List<PedidoAdminDto>>(pedidos));
    }

    // PUT /api/admin/pedidos/{id}/status -> transição do ciclo de vida do pedido
    [HttpPut("{id}/status")]
    public async Task<ActionResult<PedidoAdminDto>> AtualizarStatus(int id, [FromBody] AtualizarStatusRequest request)
    {
        var pedido = await _pedidoRepository.GetComItensAsync(id);
        if (pedido == null) return NotFound();

        if (!Enum.TryParse<StatusPedido>(request.Status, true, out var novoStatus))
            return BadRequest("Status inválido. Use: Recebido, EmPreparo, SaiuParaEntrega, Entregue.");

        pedido.Status = novoStatus;
        _pedidoRepository.Update(pedido);
        await _pedidoRepository.SaveChangesAsync();

        return Ok(_mapper.Map<PedidoAdminDto>(pedido));
    }
}
