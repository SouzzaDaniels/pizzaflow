using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaFlow.Api.Dtos;
using PizzaFlow.Api.Models;
using PizzaFlow.Api.Repositories;

namespace PizzaFlow.Api.Controllers;

[ApiController]
[Route("api/pizzas")]
public class PizzasController : ControllerBase
{
    private readonly IRepository<Pizza> _pizzaRepository;
    private readonly IMapper _mapper;

    public PizzasController(IRepository<Pizza> pizzaRepository, IMapper mapper)
    {
        _pizzaRepository = pizzaRepository;
        _mapper = mapper;
    }

    // GET /api/pizzas -> lista pública de pizzas disponíveis
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<PizzaDto>>> GetAll()
    {
        var pizzas = await _pizzaRepository.GetAllAsync();
        var disponiveis = pizzas.Where(p => p.Disponivel).ToList();
        return Ok(_mapper.Map<List<PizzaDto>>(disponiveis));
    }

    // POST /api/pizzas -> cadastro de novas pizzas pelo gestor (App Flutter)
    [HttpPost]
    [Authorize(Roles = "Gestor")]
    public async Task<ActionResult<PizzaDto>> Create([FromBody] PizzaCreateDto dto)
    {
        var pizza = _mapper.Map<Pizza>(dto);
        await _pizzaRepository.AddAsync(pizza);
        await _pizzaRepository.SaveChangesAsync();
        return Ok(_mapper.Map<PizzaDto>(pizza));
    }
}
