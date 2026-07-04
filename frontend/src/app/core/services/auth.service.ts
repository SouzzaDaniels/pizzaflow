import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { API_URL } from '../config/api.config';
import { AuthResponse } from '../models/models';

const TOKEN_KEY = 'pizzaflow_token';
const NOME_KEY = 'pizzaflow_nome';
const TIPO_KEY = 'pizzaflow_tipo';

@Injectable({ providedIn: 'root' })
export class AuthService {
  logado = signal<boolean>(!!localStorage.getItem(TOKEN_KEY));

  constructor(private http: HttpClient) {}

  verificarTelefone(telefone: string): Observable<{ existe: boolean }> {
    return this.http.post<{ existe: boolean }>(`${API_URL}/auth/verificar-telefone`, { telefone });
  }

  cadastro(dados: {
    nomeCompleto: string;
    telefone: string;
    cpf?: string;
    endereco?: string;
    senha: string;
  }): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${API_URL}/auth/cadastro`, dados).pipe(tap((r) => this.salvarSessao(r)));
  }

  login(telefone: string, senha: string): Observable<AuthResponse> {
    return this.http
      .post<AuthResponse>(`${API_URL}/auth/login`, { telefone, senha })
      .pipe(tap((r) => this.salvarSessao(r)));
  }

  logout(): void {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(NOME_KEY);
    localStorage.removeItem(TIPO_KEY);
    this.logado.set(false);
  }

  getToken(): string | null {
    return localStorage.getItem(TOKEN_KEY);
  }

  getNome(): string | null {
    return localStorage.getItem(NOME_KEY);
  }

  private salvarSessao(resposta: AuthResponse): void {
    localStorage.setItem(TOKEN_KEY, resposta.token);
    localStorage.setItem(NOME_KEY, resposta.nomeCompleto);
    localStorage.setItem(TIPO_KEY, resposta.tipoUsuario);
    this.logado.set(true);
  }
}
