using Microsoft.EntityFrameworkCore;
using PizzaFlow.Api.Data;
using PizzaFlow.Api.Models;

namespace PizzaFlow.Api.Repositories;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<Usuario?> GetByTelefoneAsync(string telefone);
}

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(AppDbContext context) : base(context) { }

    public async Task<Usuario?> GetByTelefoneAsync(string telefone) =>
        await DbSet.FirstOrDefaultAsync(u => u.Telefone == telefone);
}
