using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaFlow.Api.Models;

public class Pedido
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }

    public DateTime DataPedido { get; set; } = DateTime.UtcNow;

    [Column(TypeName = "decimal(10,2)")]
    public decimal ValorTotal { get; set; }

    public StatusPedido Status { get; set; } = StatusPedido.Recebido;

    public ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
}
