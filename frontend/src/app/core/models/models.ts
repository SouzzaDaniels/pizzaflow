export interface Pizza {
  id: number;
  nome: string;
  descricao?: string;
  preco: number;
  disponivel: boolean;
}

export interface ItemCarrinho {
  pizza: Pizza;
  quantidade: number;
}

export interface ItemPedidoDto {
  pizzaId: number;
  pizzaNome: string;
  quantidade: number;
  precoUnitario: number;
}

export interface Pedido {
  id: number;
  usuarioId: number;
  dataPedido: string;
  valorTotal: number;
  status: string;
  itens: ItemPedidoDto[];
}

export interface PedidoResumo {
  id: number;
  dataPedido: string;
  valorTotal: number;
  status: string;
}

export interface AuthResponse {
  token: string;
  nomeCompleto: string;
  tipoUsuario: string;
}
