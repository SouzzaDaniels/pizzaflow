namespace PizzaFlow.Api.Models;

public enum TipoUsuario
{
    Cliente = 0,
    Gestor = 1
}

public enum StatusPedido
{
    Recebido = 0,
    EmPreparo = 1,
    SaiuParaEntrega = 2,
    Entregue = 3
}
