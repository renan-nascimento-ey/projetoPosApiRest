using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoFinalApi.Models.Data;

namespace ProjetoFinalApi.Context.Configurations;

public class PartidaEntityTypeConfiguration : IEntityTypeConfiguration<Partida>
{
    public void Configure(EntityTypeBuilder<Partida> builder)
    {
        builder.ToTable("Partidas");

        // Relacionamentos
        builder
            .HasOne(p => p.Torneio)
            .WithMany(t => t.Partidas)
            .HasForeignKey(p => p.TorneioId)
            .IsRequired();

        builder
            .HasOne(p => p.TimeCasa)
            .WithMany(time => time.PartidasCasa)
            .HasForeignKey(p => p.TimeCasaId)
            .IsRequired();

        builder
            .HasOne(p => p.TimeVisitante)
            .WithMany(time => time.PartidasVisitante)
            .HasForeignKey(p => p.TimeVisitanteId)
            .IsRequired();
    }
}
