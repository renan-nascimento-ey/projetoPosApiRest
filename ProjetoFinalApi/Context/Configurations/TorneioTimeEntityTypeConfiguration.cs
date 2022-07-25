using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoFinalApi.Models.Data;

namespace ProjetoFinalApi.Context.Configurations;

public class TorneioTimeEntityTypeConfiguration : IEntityTypeConfiguration<TorneioTime>
{
    public void Configure(EntityTypeBuilder<TorneioTime> builder)
    {
        builder.ToTable("TorneiosTimes");

        // propriedades

        builder
            .HasKey(tt => new { tt.TorneioId, tt.TimeId });

        // Relacionamentos

        builder
            .HasOne(tt => tt.Time)
            .WithMany(time => time.TimeTorneios)
            .HasForeignKey(tt => tt.TimeId);

        builder
            .HasOne(tt => tt.Torneio)
            .WithMany(torneio => torneio.TorneioTimes)
            .HasForeignKey(tt => tt.TorneioId);
    }
}
