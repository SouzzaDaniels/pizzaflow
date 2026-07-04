import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-cadastro',
  standalone: true,
  imports: [CommonModule, FormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatCardModule],
  template: `
    <div class="pf-container">
      <mat-card>
        <h1 class="pf-title">Criar conta</h1>
        <p>Telefone: {{ telefone }}</p>

        <mat-form-field appearance="outline" style="width: 100%">
          <mat-label>Nome completo</mat-label>
          <input matInput [(ngModel)]="nomeCompleto" />
        </mat-form-field>

        <mat-form-field appearance="outline" style="width: 100%">
          <mat-label>Endereço de entrega</mat-label>
          <input matInput [(ngModel)]="endereco" />
        </mat-form-field>

        <mat-form-field appearance="outline" style="width: 100%">
          <mat-label>Senha</mat-label>
          <input matInput type="password" [(ngModel)]="senha" />
        </mat-form-field>

        <button mat-raised-button color="warn" (click)="cadastrar()" [disabled]="carregando">
          Cadastrar
        </button>

        <p *ngIf="erro" style="color: red">{{ erro }}</p>
      </mat-card>
    </div>
  `
})
export class CadastroComponent implements OnInit {
  telefone = '';
  nomeCompleto = '';
  endereco = '';
  senha = '';
  carregando = false;
  erro = '';

  constructor(private route: ActivatedRoute, private router: Router, private authService: AuthService) {}

  ngOnInit(): void {
    this.telefone = this.route.snapshot.queryParamMap.get('telefone') ?? '';
  }

  cadastrar(): void {
    if (!this.nomeCompleto || !this.senha) {
      this.erro = 'Preencha nome e senha.';
      return;
    }
    this.carregando = true;
    this.erro = '';
    this.authService
      .cadastro({ nomeCompleto: this.nomeCompleto, telefone: this.telefone, endereco: this.endereco, senha: this.senha })
      .subscribe({
        next: () => {
          this.carregando = false;
          this.router.navigate(['/cardapio']);
        },
        error: () => {
          this.carregando = false;
          this.erro = 'Não foi possível concluir o cadastro.';
        }
      });
  }
}
