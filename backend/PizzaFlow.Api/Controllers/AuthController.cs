using Microsoft.AspNetCore.Mvc;
using PizzaFlow.Api.Dtos;
using PizzaFlow.Api.Models;
using PizzaFlow.Api.Repositories;
using PizzaFlow.Api.Services;

namespace PizzaFlow.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IJwtService _jwtService;

    public AuthController(IUsuarioRepository usuarioRepository, IJwtService jwtService)
    {
        _usuarioRepository = usuarioRepository;
        _jwtService = jwtService;
    }

    [HttpPost("verificar-telefone")]
    public async Task<ActionResult<VerificarTelefoneResponse>> VerificarTelefone([FromBody] VerificarTelefoneRequest request)
    {
        var usuario = await _usuarioRepository.GetByTelefoneAsync(request.Telefone);
        return Ok(new VerificarTelefoneResponse { Existe = usuario != null });
    }

    [HttpPost("cadastro")]
    public async Task<ActionResult<AuthResponse>> Cadastro([FromBody] CadastroRequest request)
    {
        var existente = await _usuarioRepository.GetByTelefoneAsync(request.Telefone);
        if (existente != null)
            return Conflict("Já existe um usuário cadastrado com esse telefone.");

        var usuario = new Usuario
        {
            NomeCompleto = request.NomeCompleto,
            Telefone = request.Telefone,
            Cpf = request.Cpf,
            Endereco = request.Endereco,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha),
            TipoUsuario = TipoUsuario.Cliente
        };

        await _usuarioRepository.AddAsync(usuario);
        await _usuarioRepository.SaveChangesAsync();

        var token = _jwtService.GerarToken(usuario);
        return Ok(new AuthResponse
        {
            Token = token,
            NomeCompleto = usuario.NomeCompleto,
            TipoUsuario = usuario.TipoUsuario.ToString()
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
    {
        var usuario = await _usuarioRepository.GetByTelefoneAsync(request.Telefone);
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash))
            return Unauthorized("Telefone ou senha inválidos.");

        var token = _jwtService.GerarToken(usuario);
        return Ok(new AuthResponse
        {
            Token = token,
            NomeCompleto = usuario.NomeCompleto,
            TipoUsuario = usuario.TipoUsuario.ToString()
        });
    }
}
