import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { PedidoService } from '../../core/services/pedido.service';
import { PedidoResumo } from '../../core/models/models';

@Component({
  selector: 'app-meus-pedidos',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatToolbarModule, MatIconModule],
  template: `
    <mat-toolbar color="warn">
      <button mat-icon-button (click)="voltar()"><mat-icon>arrow_back</mat-icon></button>
      <span>Meus pedidos</span>
    </mat-toolbar>

    <div class="pf-container">
      <p *ngIf="pedidos.length === 0">Você ainda não fez nenhum pedido.</p>

      <mat-card
        *ngFor="let pedido of pedidos"
        style="margin-bottom: 12px; cursor: pointer"
        (click)="verDetalhe(pedido.id)"
      >
        <div style="display: flex; justify-content: space-between">
          <span>Pedido #{{ pedido.id }}</span>
          <span>{{ pedido.status }}</span>
        </div>
        <p>{{ pedido.dataPedido | date: 'dd/MM/yyyy HH:mm' }} — R$ {{ pedido.valorTotal.toFixed(2) }}</p>
      </mat-card>
    </div>
  `
})
export class MeusPedidosComponent implements OnInit {
  pedidos: PedidoResumo[] = [];

  constructor(private pedidoService: PedidoService, private router: Router) {}

  ngOnInit(): void {
    this.pedidoService.meusPedidos().subscribe((res) => (this.pedidos = res));
  }

  verDetalhe(id: number): void {
    this.router.navigate(['/pedidos', id]);
  }

  voltar(): void {
    this.router.navigate(['/cardapio']);
  }
}
