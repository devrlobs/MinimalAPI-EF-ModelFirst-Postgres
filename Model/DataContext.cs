using Microsoft.EntityFrameworkCore;

namespace MinimalAPI_EF_Postgres.Model;

public class DataContext : DbContext
{
    private readonly string? _connstring;
    public DataContext(string connstring)
    {
        _connstring = connstring;
    }

    public DataContext(DbContextOptions<DataContext> options)
    : base(options)
    {
    }


    public DbSet<Todo> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost:15432;Database=postgres;Username=postgres;Password=test1234");
    }
}