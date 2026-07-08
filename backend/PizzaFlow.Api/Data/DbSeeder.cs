using PizzaFlow.Api.Models;

namespace PizzaFlow.Api.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Pizzas.Any())
        {
            context.Pizzas.AddRange(
                new Pizza { Nome = "Margherita", Descricao = "Molho de tomate, mussarela e manjericão fresco", Preco = 39.90m, Disponivel = true },
                new Pizza { Nome = "Calabresa", Descricao = "Molho de tomate, mussarela, calabresa fatiada e cebola", Preco = 42.90m, Disponivel = true },
                new Pizza { Nome = "Quatro Queijos", Descricao = "Mussarela, provolone, parmesão e gorgonzola", Preco = 47.90m, Disponivel = true },
                new Pizza { Nome = "Portuguesa", Descricao = "Presunto, ovos, cebola, azeitona e mussarela", Preco = 45.90m, Disponivel = true },
                new Pizza { Nome = "Frango com Catupiry", Descricao = "Frango desfiado, catupiry e milho", Preco = 44.90m, Disponivel = true }
            );
        }

        // Gestor padrão, inserido manualmente conforme escopo do MVP.
        // Troque telefone/senha antes de subir para produção!
        if (!context.Usuarios.Any(u => u.TipoUsuario == TipoUsuario.Gestor))
        {
            context.Usuarios.Add(new Usuario
            {
                NomeCompleto = "Gestor PizzaFlow",
                Telefone = "11997645021",
                SenhaHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                TipoUsuario = TipoUsuario.Gestor,
                DataCadastro = DateTime.UtcNow
            });
        }

        context.SaveChanges();
    }
}
