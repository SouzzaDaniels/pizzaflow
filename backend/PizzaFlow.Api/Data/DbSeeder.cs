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


        if (!context.Usuarios.Any(u => u.TipoUsuario == TipoUsuario.Gestor))
        {
            context.Usuarios.Add(new Usuario
            {
                NomeCompleto = "Gestor PizzaFlow",
                Telefone = "16997645021",
                SenhaHash = BCrypt.Net.BCrypt.HashPassword("Admin123"),
                TipoUsuario = TipoUsuario.Gestor,
                DataCadastro = DateTime.UtcNow
            });
        }
        else
        {
            // Atualiza senha se o usuário já existe
            var gestor = context.Usuarios.FirstOrDefault(u => u.TipoUsuario == TipoUsuario.Gestor);
            if (gestor != null)
            {
                gestor.SenhaHash = BCrypt.Net.BCrypt.HashPassword("Admin123");
                gestor.Telefone = "16997645021";   // atualiza telefone também
            }
        }

        context.SaveChanges();
    }
}
