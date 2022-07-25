using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoFinalApi.Models.Data;

namespace ProjetoFinalApi.Context.Configurations;

public class TransferenciaEntityTypeConfiguration : IEntityTypeConfiguration<Transferencia>
{
    public void Configure(EntityTypeBuilder<Transferencia> builder)
    {
        builder.ToTable("Transferencias");

        // Relacionamentos
        builder
            .HasOne(t => t.Jogador)
            .WithMany(j => j.Transferencias)
            .HasForeignKey(t => t.JogadorId)
            .IsRequired();

        builder
            .HasOne(transf => transf.TimeOrigem)
            .WithMany(time => time.TransferenciasOrigem)
            .HasForeignKey(transf => transf.TimeOrigemId)
            .IsRequired();

        builder
            .HasOne(transf => transf.TimeDestisno)
            .WithMany(time => time.TransferenciasDestino)
            .HasForeignKey(transf => transf.TimeDestinoId)
            .IsRequired();
    }
}
