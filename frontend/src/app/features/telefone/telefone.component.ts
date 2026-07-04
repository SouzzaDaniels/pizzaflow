import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-telefone',
  standalone: true,
  imports: [CommonModule, FormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatCardModule],
  template: `
    <div class="pf-container">
      <mat-card>
        <h1 class="pf-title">🍕 PizzaFlow</h1>
        <p>Informe seu telefone para continuar</p>

        <mat-form-field appearance="outline" style="width: 100%">
          <mat-label>Telefone</mat-label>
          <input matInput [(ngModel)]="telefone" placeholder="Ex: 11999999999" />
        </mat-form-field>

        <button mat-raised-button color="warn" (click)="continuar()" [disabled]="carregando">
          Continuar
        </button>

        <p *ngIf="erro" style="color: red">{{ erro }}</p>
      </mat-card>
    </div>
  `
})
export class TelefoneComponent {
  telefone = '';
  carregando = false;
  erro = '';

  constructor(private authService: AuthService, private router: Router) {}

  continuar(): void {
    if (!this.telefone) {
      this.erro = 'Informe o telefone.';
      return;
    }
    this.carregando = true;
    this.erro = '';
    this.authService.verificarTelefone(this.telefone).subscribe({
      next: (res) => {
        this.carregando = false;
        const rota = res.existe ? '/login' : '/cadastro';
        this.router.navigate([rota], { queryParams: { telefone: this.telefone } });
      },
      error: () => {
        this.carregando = false;
        this.erro = 'Não foi possível verificar o telefone. Tente novamente.';
      }
    });
  }
}
