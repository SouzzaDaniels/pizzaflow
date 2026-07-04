using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaFlow.Api.Models;

public class ItemPedido
{
    public int Id { get; set; }

    public int PedidoId { get; set; }
    public Pedido? Pedido { get; set; }

    public int PizzaId { get; set; }
    public Pizza? Pizza { get; set; }

    public int Quantidade { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal PrecoUnitario { get; set; }
}
