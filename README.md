# 🍕 PizzaFlow

Plataforma de pedidos para pizzarias — cliente web (Angular) + API (.NET 8) + app do gestor (Flutter).

**Comece pelo arquivo [`TUTORIAL.md`](./TUTORIAL.md)** — ele contém o passo a passo completo, do zero até o projeto rodando na internet e publicado no seu GitHub.

## Estrutura do repositório

```
pizzaflow/
├── backend/    → API REST em .NET 8 (PostgreSQL/Supabase, JWT, EF Core)
├── frontend/   → Aplicação web em Angular 20 (cliente da pizzaria)
├── mobile/     → App Flutter do gestor (fila de pedidos, status)
└── TUTORIAL.md → passo a passo completo (comece aqui)
```

## Stack

| Camada | Tecnologia | Hospedagem |
|---|---|---|
| Banco de dados | PostgreSQL | Supabase |
| Backend | .NET 8 + EF Core + JWT | Render (Docker) |
| Frontend | Angular 20 + Angular Material | Netlify |
| App do gestor | Flutter (.apk) | Instalação direta no celular |
