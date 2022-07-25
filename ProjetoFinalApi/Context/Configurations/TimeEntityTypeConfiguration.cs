using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoFinalApi.Models.Data;

namespace ProjetoFinalApi.Context.Configurations;

public class TimeEntityTypeConfiguration : IEntityTypeConfiguration<Time>
{
    public void Configure(EntityTypeBuilder<Time> builder)
    {
        builder.ToTable("Times");

        // Relacionamentos
    }
}
