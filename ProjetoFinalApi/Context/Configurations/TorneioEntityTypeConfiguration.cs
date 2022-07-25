using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoFinalApi.Models.Data;

namespace ProjetoFinalApi.Context.Configurations;

public class TorneioEntityTypeConfiguration : IEntityTypeConfiguration<Torneio>
{
    public void Configure(EntityTypeBuilder<Torneio> builder)
    {
        builder.ToTable("Torneios");

        // Relacionamentos
        
    }
}
