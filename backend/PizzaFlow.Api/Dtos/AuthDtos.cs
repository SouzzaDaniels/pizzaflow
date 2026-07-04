using System.ComponentModel.DataAnnotations;

namespace PizzaFlow.Api.Dtos;

public class VerificarTelefoneRequest
{
    [Required]
    public string Telefone { get; set; } = string.Empty;
}

public class VerificarTelefoneResponse
{
    public bool Existe { get; set; }
}

public class CadastroRequest
{
    [Required, MaxLength(150)]
    public string NomeCompleto { get; set; } = string.Empty;

    [Required]
    public string Telefone { get; set; } = string.Empty;

    public string? Cpf { get; set; }

    public string? Endereco { get; set; }

    [Required, MinLength(6)]
    public string Senha { get; set; } = string.Empty;
}

public class LoginRequest
{
    [Required]
    public string Telefone { get; set; } = string.Empty;

    [Required]
    public string Senha { get; set; } = string.Empty;
}

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public string NomeCompleto { get; set; } = string.Empty;
    public string TipoUsuario { get; set; } = string.Empty;
}
