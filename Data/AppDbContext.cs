using Gerenciamento_Financeiro.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Tabelas
    public DbSet <Categorias>  TCategorias { get; set; }
    public DbSet <Usuarios>    TUsuarios   { get; set; }
    public DbSet <Lancamentos> TLancamentos { get; set; }


    // Populando registros padrões nas tabelas
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Categorias>().HasData(
            new Categorias { Id = 1, Nome = "Contas" },
            new Categorias { Id = 2, Nome = "Estudos" },
            new Categorias { Id = 3, Nome = "Alimentação" },
            new Categorias { Id = 4, Nome = "Lazer" },
            new Categorias { Id = 5, Nome = "Transporte" },
            new Categorias { Id = 6, Nome = "Assinaturas" }
        );
    }
}
