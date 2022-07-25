using Microsoft.EntityFrameworkCore;
using ProjetoFinalApi.Context.Configurations;
using ProjetoFinalApi.Models.Data;

namespace ProjetoFinalApi.Context;

public class ApiDbContext : DbContext
{
    public DbSet<Time>? Times { get; set; }

    public DbSet<Jogador>? Jogadores { get; set; }

    public DbSet<Transferencia>? Transferencias { get; set; }

    public DbSet<Torneio>? Torneios { get; set; }

    public DbSet<TorneioTime>? TorneiosTimes { get; set; }

    public DbSet<Partida>? Partidas { get; set; }

    public DbSet<EventoPartida>? EventosPartidas { get; set; }

    public ApiDbContext(DbContextOptions<ApiDbContext> options) 
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new TimeEntityTypeConfiguration().Configure(modelBuilder.Entity<Time>());
        new JogadorEntityTypeConfiguration().Configure(modelBuilder.Entity<Jogador>());
        new TransferenciaEntityTypeConfiguration().Configure(modelBuilder.Entity<Transferencia>());
        new TorneioEntityTypeConfiguration().Configure(modelBuilder.Entity<Torneio>());
        new TorneioTimeEntityTypeConfiguration().Configure(modelBuilder.Entity<TorneioTime>());
        new PartidaEntityTypeConfiguration().Configure(modelBuilder.Entity<Partida>());
        new EventoPartidaEntityTypeConfiguration().Configure(modelBuilder.Entity<EventoPartida>());
    }
}
