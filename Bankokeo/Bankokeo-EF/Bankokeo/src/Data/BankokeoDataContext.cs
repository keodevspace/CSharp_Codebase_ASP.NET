// esta classe contem DbSet<User> Users { get; set; } que é responsável por criar a tabela de usuários no banco de dados
// também o método OnConfiguring que é responsável por configurar a conexão com o banco de dados
// e o método OnModelCreating que é responsável por criar o mapeamento da tabela de usuários no banco de dados

using Bankokeo.Models;
using Microsoft.EntityFrameworkCore;
using Bankokeo.Data.Mappings;

namespace Bankokeo.Data;

public class BankokeoDataContext : DbContext
    {
    public DbSet<User> Users { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlServer("Server=localhost,1433;Database=Bankokeo;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True",options => options.EnableRetryOnFailure()); 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.ApplyConfiguration(new UserMap());

        }

    }
