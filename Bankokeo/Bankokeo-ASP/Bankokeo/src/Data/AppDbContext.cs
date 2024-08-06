// DbContext é responsável por configurar o acesso ao banco de dados e mapear as entidades para tabelas no banco de dados.

using Microsoft.EntityFrameworkCore;
using Bankokeo.Models;

namespace Bankokeo.Data;

public class AppDbContext : DbContext
    {
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlServer("Server=localhost,1433;Database=Bankokeo;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True", options => options.EnableRetryOnFailure());

    }