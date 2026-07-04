namespace PizzaFlow.Api.Dtos;

public class PizzaDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public bool Disponivel { get; set; }
}

public class PizzaCreateDto
{
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public bool Disponivel { get; set; } = true;
}
