import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatBadgeModule } from '@angular/material/badge';
import { PizzaService } from '../../core/services/pizza.service';
import { PedidoService } from '../../core/services/pedido.service';
import { Pizza } from '../../core/models/models';

@Component({
  selector: 'app-cardapio',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatToolbarModule, MatBadgeModule],
  template: `
    <mat-toolbar color="warn">
      <span>🍕 PizzaFlow</span>
      <span style="flex: 1 1 auto"></span>
      <button mat-icon-button (click)="irParaMeusPedidos()">
        <mat-icon>receipt_long</mat-icon>
      </button>
      <button mat-icon-button (click)="irParaCarrinho()" [matBadge]="totalItens()" matBadgeColor="accent">
        <mat-icon>shopping_cart</mat-icon>
      </button>
    </mat-toolbar>

    <div class="pf-container">
      <div *ngFor="let pizza of pizzas" style="margin-bottom: 16px">
        <mat-card>
          <h2>{{ pizza.nome }}</h2>
          <p>{{ pizza.descricao }}</p>
          <p><strong>R$ {{ pizza.preco.toFixed(2) }}</strong></p>
          <button mat-raised-button color="warn" (click)="adicionar(pizza)">Adicionar ao carrinho</button>
        </mat-card>
      </div>
    </div>
  `
})
export class CardapioComponent implements OnInit {
  pizzas: Pizza[] = [];

  constructor(private pizzaService: PizzaService, private pedidoService: PedidoService, private router: Router) {}

  ngOnInit(): void {
    this.pizzaService.listar().subscribe((res) => (this.pizzas = res));
  }

  adicionar(pizza: Pizza): void {
    this.pedidoService.adicionarAoCarrinho(pizza);
  }

  totalItens(): number {
    return this.pedidoService.carrinho().reduce((soma, i) => soma + i.quantidade, 0);
  }

  irParaCarrinho(): void {
    this.router.navigate(['/carrinho']);
  }

  irParaMeusPedidos(): void {
    this.router.navigate(['/meus-pedidos']);
  }
}
