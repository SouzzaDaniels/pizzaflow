using System.ComponentModel.DataAnnotations;

namespace PizzaFlow.Api.Models;

public class Usuario
{
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string NomeCompleto { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    public string Telefone { get; set; } = string.Empty; // chave de autenticação primária

    [MaxLength(14)]
    public string? Cpf { get; set; }

    [MaxLength(250)]
    public string? Endereco { get; set; }

    [Required]
    public string SenhaHash { get; set; } = string.Empty;

    public TipoUsuario TipoUsuario { get; set; } = TipoUsuario.Cliente;

    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
