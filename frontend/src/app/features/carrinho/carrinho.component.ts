import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { PedidoService } from '../../core/services/pedido.service';

@Component({
  selector: 'app-carrinho',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatToolbarModule],
  template: `
    <mat-toolbar color="warn">
      <button mat-icon-button (click)="voltar()"><mat-icon>arrow_back</mat-icon></button>
      <span>Seu carrinho</span>
    </mat-toolbar>

    <div class="pf-container">
      <p *ngIf="itens().length === 0">Seu carrinho está vazio.</p>

      <mat-card *ngFor="let item of itens()" style="margin-bottom: 12px">
        <div style="display: flex; justify-content: space-between; align-items: center">
          <div>
            <strong>{{ item.pizza.nome }}</strong>
            <p>Qtd: {{ item.quantidade }} x R$ {{ item.pizza.preco.toFixed(2) }}</p>
          </div>
          <button mat-icon-button color="warn" (click)="remover(item.pizza.id)">
            <mat-icon>delete</mat-icon>
          </button>
        </div>
      </mat-card>

      <h2 *ngIf="itens().length > 0">Total: R$ {{ total().toFixed(2) }}</h2>

      <button mat-raised-button color="warn" *ngIf="itens().length > 0" (click)="finalizar()" [disabled]="enviando">
        Finalizar pedido (pagamento na entrega)
      </button>

      <p *ngIf="erro" style="color: red">{{ erro }}</p>
    </div>
  `
})
export class CarrinhoComponent {
  enviando = false;
  erro = '';

  constructor(private pedidoService: PedidoService, private router: Router) {}

  itens = () => this.pedidoService.carrinho();
  total = () => this.pedidoService.totalCarrinho();

  remover(pizzaId: number): void {
    this.pedidoService.removerDoCarrinho(pizzaId);
  }

  finalizar(): void {
    this.enviando = true;
    this.erro = '';
    this.pedidoService.finalizarPedido().subscribe({
      next: (pedido) => {
        this.enviando = false;
        this.pedidoService.limparCarrinho();
        this.router.navigate(['/pedidos', pedido.id]);
      },
      error: () => {
        this.enviando = false;
        this.erro = 'Não foi possível finalizar o pedido. Tente novamente.';
      }
    });
  }

  voltar(): void {
    this.router.navigate(['/cardapio']);
  }
}
