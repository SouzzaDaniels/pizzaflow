import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { interval, Subscription, switchMap } from 'rxjs';
import { PedidoService } from '../../core/services/pedido.service';
import { Pedido } from '../../core/models/models';

const ETAPAS = ['Recebido', 'EmPreparo', 'SaiuParaEntrega', 'Entregue'];

@Component({
  selector: 'app-pedido-detalhe',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatToolbarModule, MatIconModule],
  template: `
    <mat-toolbar color="warn">
      <button mat-icon-button (click)="voltar()"><mat-icon>arrow_back</mat-icon></button>
      <span>Pedido #{{ pedidoId }}</span>
    </mat-toolbar>

    <div class="pf-container" *ngIf="pedido as p">
      <mat-card>
        <h2>Status: {{ p.status }}</h2>

        <div style="display: flex; justify-content: space-between; margin: 16px 0">
          <div *ngFor="let etapa of etapas" [style.font-weight]="etapa === p.status ? 'bold' : 'normal'">
            {{ etapa }}
          </div>
        </div>

        <h3>Itens</h3>
        <ul>
          <li *ngFor="let item of p.itens">
            {{ item.quantidade }}x {{ item.pizzaNome }} — R$ {{ item.precoUnitario.toFixed(2) }}
          </li>
        </ul>

        <h3>Total: R$ {{ p.valorTotal.toFixed(2) }}</h3>
        <p><em>O status é atualizado automaticamente a cada 15 segundos.</em></p>
      </mat-card>
    </div>
  `
})
export class PedidoDetalheComponent implements OnInit, OnDestroy {
  pedidoId!: number;
  pedido?: Pedido;
  etapas = ETAPAS;
  private assinatura?: Subscription;

  constructor(private route: ActivatedRoute, private router: Router, private pedidoService: PedidoService) {}

  ngOnInit(): void {
    this.pedidoId = Number(this.route.snapshot.paramMap.get('id'));

    // Long polling: consulta o backend a cada 15 segundos para refletir
    // as mudanças de status feitas pelo gestor no app Flutter.
    this.assinatura = interval(15000)
      .pipe(switchMap(() => this.pedidoService.detalhe(this.pedidoId)))
      .subscribe((p) => (this.pedido = p));

    this.pedidoService.detalhe(this.pedidoId).subscribe((p) => (this.pedido = p));
  }

  ngOnDestroy(): void {
    this.assinatura?.unsubscribe();
  }

  voltar(): void {
    this.router.navigate(['/meus-pedidos']);
  }
}
