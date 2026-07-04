import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'entrar', pathMatch: 'full' },
  {
    path: 'entrar',
    loadComponent: () => import('./features/telefone/telefone.component').then((m) => m.TelefoneComponent)
  },
  {
    path: 'login',
    loadComponent: () => import('./features/login/login.component').then((m) => m.LoginComponent)
  },
  {
    path: 'cadastro',
    loadComponent: () => import('./features/cadastro/cadastro.component').then((m) => m.CadastroComponent)
  },
  {
    path: 'cardapio',
    canActivate: [authGuard],
    loadComponent: () => import('./features/cardapio/cardapio.component').then((m) => m.CardapioComponent)
  },
  {
    path: 'carrinho',
    canActivate: [authGuard],
    loadComponent: () => import('./features/carrinho/carrinho.component').then((m) => m.CarrinhoComponent)
  },
  {
    path: 'meus-pedidos',
    canActivate: [authGuard],
    loadComponent: () => import('./features/pedidos/meus-pedidos.component').then((m) => m.MeusPedidosComponent)
  },
  {
    path: 'pedidos/:id',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./features/pedido-detalhe/pedido-detalhe.component').then((m) => m.PedidoDetalheComponent)
  },
  { path: '**', redirectTo: 'entrar' }
];
