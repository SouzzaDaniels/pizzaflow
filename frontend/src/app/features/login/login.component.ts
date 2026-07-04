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
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatCardModule],
  template: `
    <div class="pf-container">
      <mat-card>
        <h1 class="pf-title">Entrar</h1>
        <p>Telefone: {{ telefone }}</p>

        <mat-form-field appearance="outline" style="width: 100%">
          <mat-label>Senha</mat-label>
          <input matInput type="password" [(ngModel)]="senha" />
        </mat-form-field>

        <button mat-raised-button color="warn" (click)="entrar()" [disabled]="carregando">
          Entrar
        </button>

        <p *ngIf="erro" style="color: red">{{ erro }}</p>
      </mat-card>
    </div>
  `
})
export class LoginComponent implements OnInit {
  telefone = '';
  senha = '';
  carregando = false;
  erro = '';

  constructor(private route: ActivatedRoute, private router: Router, private authService: AuthService) {}

  ngOnInit(): void {
    this.telefone = this.route.snapshot.queryParamMap.get('telefone') ?? '';
  }

  entrar(): void {
    this.carregando = true;
    this.erro = '';
    this.authService.login(this.telefone, this.senha).subscribe({
      next: () => {
        this.carregando = false;
        this.router.navigate(['/cardapio']);
      },
      error: () => {
        this.carregando = false;
        this.erro = 'Telefone ou senha inválidos.';
      }
    });
  }
}
