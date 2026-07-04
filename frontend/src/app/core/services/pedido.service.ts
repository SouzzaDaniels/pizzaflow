import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URL } from '../config/api.config';
import { ItemCarrinho, Pedido, PedidoResumo, Pizza } from '../models/models';

@Injectable({ providedIn: 'root' })
export class PedidoService {
  // Carrinho mantido em memória (sem persistência, conforme escopo do MVP)
  carrinho = signal<ItemCarrinho[]>([]);

  constructor(private http: HttpClient) {}

  adicionarAoCarrinho(pizza: Pizza): void {
    const atual = this.carrinho();
    const existente = atual.find((i) => i.pizza.id === pizza.id);
    if (existente) {
      existente.quantidade++;
      this.carrinho.set([...atual]);
    } else {
      this.carrinho.set([...atual, { pizza, quantidade: 1 }]);
    }
  }

  removerDoCarrinho(pizzaId: number): void {
    this.carrinho.set(this.carrinho().filter((i) => i.pizza.id !== pizzaId));
  }

  limparCarrinho(): void {
    this.carrinho.set([]);
  }

  totalCarrinho(): number {
    return this.carrinho().reduce((soma, item) => soma + item.pizza.preco * item.quantidade, 0);
  }

  finalizarPedido(): Observable<Pedido> {
    const itens = this.carrinho().map((i) => ({ pizzaId: i.pizza.id, quantidade: i.quantidade }));
    return this.http.post<Pedido>(`${API_URL}/pedidos`, { itens });
  }

  meusPedidos(): Observable<PedidoResumo[]> {
    return this.http.get<PedidoResumo[]>(`${API_URL}/pedidos/meus`);
  }

  detalhe(id: number): Observable<Pedido> {
    return this.http.get<Pedido>(`${API_URL}/pedidos/${id}`);
  }
}
