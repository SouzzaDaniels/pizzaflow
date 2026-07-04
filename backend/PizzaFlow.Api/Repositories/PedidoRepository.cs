using Microsoft.EntityFrameworkCore;
using PizzaFlow.Api.Data;
using PizzaFlow.Api.Models;

namespace PizzaFlow.Api.Repositories;

public interface IPedidoRepository : IRepository<Pedido>
{
    Task<List<Pedido>> GetByUsuarioIdAsync(int usuarioId);
    Task<Pedido?> GetComItensAsync(int id);
    Task<List<Pedido>> GetAtivosParaGestorAsync();
}

public class PedidoRepository : Repository<Pedido>, IPedidoRepository
{
    public PedidoRepository(AppDbContext context) : base(context) { }

    public async Task<List<Pedido>> GetByUsuarioIdAsync(int usuarioId) =>
        await DbSet.Where(p => p.UsuarioId == usuarioId)
                   .OrderByDescending(p => p.DataPedido)
                   .ToListAsync();

    public async Task<Pedido?> GetComItensAsync(int id) =>
        await DbSet.Include(p => p.Itens)
                    .ThenInclude(i => i.Pizza)
                   .Include(p => p.Usuario)
                   .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<List<Pedido>> GetAtivosParaGestorAsync() =>
        await DbSet.Where(p => p.Status != Models.StatusPedido.Entregue)
                   .Include(p => p.Itens).ThenInclude(i => i.Pizza)
                   .Include(p => p.Usuario)
                   .OrderBy(p => p.DataPedido)
                   .ToListAsync();
}
