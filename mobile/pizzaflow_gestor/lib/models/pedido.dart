class ItemPedido {
  final int pizzaId;
  final String pizzaNome;
  final int quantidade;
  final double precoUnitario;

  ItemPedido({
    required this.pizzaId,
    required this.pizzaNome,
    required this.quantidade,
    required this.precoUnitario,
  });

  factory ItemPedido.fromJson(Map<String, dynamic> json) {
    return ItemPedido(
      pizzaId: json['pizzaId'],
      pizzaNome: json['pizzaNome'],
      quantidade: json['quantidade'],
      precoUnitario: (json['precoUnitario'] as num).toDouble(),
    );
  }
}

class Pedido {
  final int id;
  final String clienteNome;
  final String clienteTelefone;
  final String clienteEndereco;
  final DateTime dataPedido;
  final double valorTotal;
  final String status;
  final List<ItemPedido> itens;

  Pedido({
    required this.id,
    required this.clienteNome,
    required this.clienteTelefone,
    required this.clienteEndereco,
    required this.dataPedido,
    required this.valorTotal,
    required this.status,
    required this.itens,
  });

  factory Pedido.fromJson(Map<String, dynamic> json) {
    return Pedido(
      id: json['id'],
      clienteNome: json['clienteNome'] ?? '',
      clienteTelefone: json['clienteTelefone'] ?? '',
      clienteEndereco: json['clienteEndereco'] ?? '',
      dataPedido: DateTime.parse(json['dataPedido']),
      valorTotal: (json['valorTotal'] as num).toDouble(),
      status: json['status'],
      itens: (json['itens'] as List).map((i) => ItemPedido.fromJson(i)).toList(),
    );
  }
}

/// Ordem oficial do ciclo de vida do pedido.
const List<String> etapasPedido = ['Recebido', 'EmPreparo', 'SaiuParaEntrega', 'Entregue'];

String proximoStatus(String atual) {
  final indice = etapasPedido.indexOf(atual);
  if (indice == -1 || indice == etapasPedido.length - 1) return atual;
  return etapasPedido[indice + 1];
}
