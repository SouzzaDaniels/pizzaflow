using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaFlow.Api.Repositories;

namespace PizzaFlow.Api.Controllers;

// ⚠️ CONTROLLER TEMPORÁRIO — use uma vez para resetar uma senha e DEPOIS APAGUE este arquivo.
[ApiController]
[Route("api/reset-senha")]
[AllowAnonymous]
public class DevToolsController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IConfiguration _config;

    public DevToolsController(IUsuarioRepository usuarioRepository, IConfiguration config)
    {
        _usuarioRepository = usuarioRepository;
        _config = config;
    }

    // Exemplo de uso no navegador:

    [HttpGet("reset-senha")]
    public async Task<IActionResult> ResetSenha(
        [FromQuery] string secret,
        [FromQuery] string telefone,
        [FromQuery] string novaSenha)
    {
        var chaveEsperada = _config["ResetSenha:Secret"];

        if (string.IsNullOrEmpty(chaveEsperada) || secret != chaveEsperada)
            return Unauthorized("Chave secreta inválida.");

        if (string.IsNullOrWhiteSpace(telefone) || string.IsNullOrWhiteSpace(novaSenha))
            return BadRequest("Informe telefone e novaSenha.");

        if (novaSenha.Length < 6)
            return BadRequest("A nova senha precisa ter pelo menos 6 caracteres.");

        var usuario = await _usuarioRepository.GetByTelefoneAsync(telefone);
        if (usuario == null)
            return NotFound("Nenhum usuário encontrado com esse telefone.");

        usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(novaSenha);
        _usuarioRepository.Update(usuario);
        await _usuarioRepository.SaveChangesAsync();

        return Ok($"Senha do usuário '{usuario.NomeCompleto}' ({usuario.Telefone}) foi resetada com sucesso.");
    }
}