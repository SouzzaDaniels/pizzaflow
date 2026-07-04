using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaFlow.Api.Models;

public class Pizza
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Descricao { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }

    public bool Disponivel { get; set; } = true;
}
