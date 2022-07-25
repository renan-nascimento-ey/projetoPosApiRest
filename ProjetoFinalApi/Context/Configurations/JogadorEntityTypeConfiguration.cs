using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoFinalApi.Models.Data;

namespace ProjetoFinalApi.Context.Configurations;

public class JogadorEntityTypeConfiguration : IEntityTypeConfiguration<Jogador>
{
    public void Configure(EntityTypeBuilder<Jogador> builder)
    {
        builder.ToTable("Jogadores");

        // Relacionamentos
        builder
            .HasOne(j => j.Time)
            .WithMany(t => t.Jogadores)
            .HasForeignKey(j => j.TimeId); 
    }
}
