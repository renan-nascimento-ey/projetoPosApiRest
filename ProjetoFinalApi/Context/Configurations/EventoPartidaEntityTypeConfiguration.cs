using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoFinalApi.Models.Data;

namespace ProjetoFinalApi.Context.Configurations;

public class EventoPartidaEntityTypeConfiguration : IEntityTypeConfiguration<EventoPartida>
{
    public void Configure(EntityTypeBuilder<EventoPartida> builder)
    {
        builder.ToTable("EventosPartidas");

        // Relacionamentos
        builder
            .HasOne(ep => ep.Partida)
            .WithMany(p => p.EventosPartida)
            .HasForeignKey(ep => ep.PartidaId);
    }
}
