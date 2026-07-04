namespace PizzaFlow.Api.Dtos;

public class ItemPedidoCreateDto
{
    public int PizzaId { get; set; }
    public int Quantidade { get; set; }
}

public class PedidoCreateDto
{
    public List<ItemPedidoCreateDto> Itens { get; set; } = new();
}

public class ItemPedidoDto
{
    public int PizzaId { get; set; }
    public string PizzaNome { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
}

public class PedidoDto
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public DateTime DataPedido { get; set; }
    public decimal ValorTotal { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<ItemPedidoDto> Itens { get; set; } = new();
}

public class PedidoResumoDto
{
    public int Id { get; set; }
    public DateTime DataPedido { get; set; }
    public decimal ValorTotal { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class AtualizarStatusRequest
{
    public string Status { get; set; } = string.Empty; // Recebido, EmPreparo, SaiuParaEntrega, Entregue
}

public class PedidoAdminDto
{
    public int Id { get; set; }
    public string ClienteNome { get; set; } = string.Empty;
    public string ClienteTelefone { get; set; } = string.Empty;
    public string ClienteEndereco { get; set; } = string.Empty;
    public DateTime DataPedido { get; set; }
    public decimal ValorTotal { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<ItemPedidoDto> Itens { get; set; } = new();
}
