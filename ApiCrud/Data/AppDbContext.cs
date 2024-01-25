using ApiCrud.Bibliotecas;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Data;
public class AppDbContext : DbContext
{
    public DbSet<Biblioteca> Bibliotecas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Banco.sqlite");
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        optionsBuilder.EnableSensitiveDataLogging();

        base.OnConfiguring(optionsBuilder);
    }
}
