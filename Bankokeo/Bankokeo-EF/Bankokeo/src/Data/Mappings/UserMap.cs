// esta classe é responsável por criar o mapeamento da tabela de usuários no banco de dados

using Bankokeo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bankokeo.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
    {
    public void Configure(EntityTypeBuilder<User> builder)
        {
        // Tabela
        builder.ToTable("User");

        // Chave Primária
        builder.HasKey(x => x.Id);

        // Identity
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        // Propriedades
        builder.Property(x => x.Name);
        builder.Property(x => x.Login);
        builder.Property(x => x.Password);
        builder.Property(x => x.Account);
        builder.Property(x => x.Balance);

        }
    }

