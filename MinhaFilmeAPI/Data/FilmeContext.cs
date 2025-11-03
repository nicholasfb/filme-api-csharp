using Microsoft.EntityFrameworkCore;
using MinhaFilmeAPI.Models;
namespace MinhaFilmeAPI.Data;

public class FilmeContext : DbContext
{
    public FilmeContext(DbContextOptions<FilmeContext> opts)
        : base(opts)
    {
        
    }

    public DbSet<Filme> Filmes { get; set; }
}
