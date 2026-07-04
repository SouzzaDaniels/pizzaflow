using Microsoft.EntityFrameworkCore;
using PizzaFlow.Api.Models;

namespace PizzaFlow.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Pizza> Pizzas => Set<Pizza>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<ItemPedido> ItensPedido => Set<ItemPedido>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Telefone)
            .IsUnique();

        modelBuilder.Entity<Usuario>()
            .Property(u => u.TipoUsuario)
            .HasConversion<string>();

        modelBuilder.Entity<Pedido>()
            .Property(p => p.Status)
            .HasConversion<string>();

        modelBuilder.Entity<Pedido>()
            .HasOne(p => p.Usuario)
            .WithMany(u => u.Pedidos)
            .HasForeignKey(p => p.UsuarioId);

        modelBuilder.Entity<ItemPedido>()
            .HasOne(i => i.Pedido)
            .WithMany(p => p.Itens)
            .HasForeignKey(i => i.PedidoId);

        modelBuilder.Entity<ItemPedido>()
            .HasOne(i => i.Pizza)
            .WithMany()
            .HasForeignKey(i => i.PizzaId);

        base.OnModelCreating(modelBuilder);
    }
}
