﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjetoFinalApi.Context;

#nullable disable

namespace ProjetoFinalApi.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    partial class ApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ProjetoFinalApi.Models.Data.EventoPartida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .HasMaxLength(280)
                        .HasColumnType("varchar(280)");

                    b.Property<int>("PartidaId")
                        .HasColumnType("int");

                    b.Property<int>("TipoEvento")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PartidaId");

                    b.ToTable("EventosPartidas", (string)null);
                });

            modelBuilder.Entity("ProjetoFinalApi.Models.Data.Jogador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float?>("Altura")
                        .HasColumnType("float");

                    b.Property<string>("Apelido")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<int>("Contrato")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LocalNascimento")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Nacionalidade")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Pe")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Posicao")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<int>("TimeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TimeId");

                    b.ToTable("Jogadores", (string)null);
                });

            modelBuilder.Entity("ProjetoFinalApi.Models.Data.Partida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataHoraFim")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataHoraInicio")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Local")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PlacarFinal")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<int>("TimeCasaId")
                        .HasColumnType("int");

                    b.Property<int?>("TimeVencedorId")
                        .HasColumnType("int");

                    b.Property<int>("TimeVisitanteId")
                        .HasColumnType("int");

                    b.Property<int>("TorneioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TimeCasaId");

                    b.HasIndex("TimeVisitanteId");

                    b.HasIndex("TorneioId");

                    b.ToTable("Partidas", (string)null);
                });

            modelBuilder.Entity("ProjetoFinalApi.Models.Data.Time", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Apelido")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Cores")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Estadio")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Fundacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Localidade")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Mascote")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.HasKey("Id");

                    b.ToTable("Times", (string)null);
                });

            modelBuilder.Entity("ProjetoFinalApi.Models.Data.Torneio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Apelido")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Edicao")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Organizacao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("PremiacaoCampeao")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("PremiacaoViceCampeao")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Serie")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<int>("Sistema")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Torneios", (string)null);
                });

            modelBuilder.Entity("ProjetoFinalApi.Models.Data.TorneioTime", b =>
                {
                    b.Property<int>("TorneioId")
                        .HasColumnType("int");

                    b.Property<int>("TimeId")
                        .HasColumnType("int");

                    b.HasKey("TorneioId", "TimeId");

                    b.HasIndex("TimeId");

                    b.ToTable("TorneiosTimes", (string)null);
                });

            modelBuilder.Entity("ProjetoFinalApi.Models.Data.Transferencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Contrato")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("JogadorId")
                        .HasColumnType("int");

                    b.Property<int>("TimeDestinoId")
                        .HasColumnType("int");

                    b.Property<int>("TimeOrigemId")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("JogadorId");

                    b.HasIndex("TimeDestinoId");

                    b.HasIndex("TimeOrigemId");

                    b.ToTable("Transferencias", (string)null);
                });

            modelBuilder.Entity("ProjetoFinalApi.Models.Data.EventoPartida", b =>
                {
                    b.HasOne("ProjetoFinalApi.Models.Data.Partida", "Partida")
                        .WithMany("EventosPartida")
                        .HasForeignKey("PartidaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Partida");
                });

            modelBuilder.Entity("ProjetoFinalApi.Models.Data.Jogador", b =>
                {
                    b.HasOne("ProjetoFinalApi.Models.Data.Time", "Time")
                        .WithMany("Jogadores")
                        .HasForeignKey("TimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Time");
                });

            modelBuilder.Entity("ProjetoFinalApi.Models.Data.Partida", b =>
                {
                    b.HasOne("ProjetoFinalApi.Models.Data.Time", "TimeCasa")
                        .WithMany("PartidasCasa")
                        .HasForeignKey("TimeCasaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetoFinalApi.Models.Data.Time", "TimeVisitante")
                        .WithMany("PartidasVisitante")
                        .HasForeignKey("TimeVisitanteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetoFinalApi.Models.Data.Torneio", "Torneio")
                        .WithMany("Partidas")
                        .HasForeignKey("TorneioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TimeCasa");

                    b.Navigation("TimeVisitante");

                    b.Navigation("Torneio");
                });

            modelBuilder.Entity("ProjetoFinalApi.Models.Data.TorneioTime", b =>
                {
                    b.HasOne("ProjetoFinalApi.Models.Data.Time", "Time")
                        .WithMany("TimeTorneios")
                        .HasForeignKey("TimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetoFinalApi.Models.Data.Torneio", "Torneio")
                        .WithMany("TorneioTimes")
                        .HasForeignKey("TorneioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Time");

                    b.Navigation("Torneio");
                });

            modelBuilder.Entity("ProjetoFinalApi.Models.Data.Transferencia", b =>
                {
                    b.HasOne("ProjetoFinalApi.Models.Data.Jogador", "Jogador")
                        .WithMany("Transferencias")
                        .HasForeignKey("JogadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetoFinalApi.Models.Data.Time", "TimeDestisno")
                        .WithMany("TransferenciasDestino")
                        .HasForeignKey("TimeDestinoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetoFinalApi.Models.Data.Time", "TimeOrigem")
                        .WithMany("TransferenciasOrigem")
                        .HasForeignKey("TimeOrigemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Jogador");

                    b.Navigation("TimeDestisno");

                    b.Navigation("TimeOrigem");
                });

            modelBuilder.Entity("ProjetoFinalApi.Models.Data.Jogador", b =>
                {
                    b.Navigation("Transferencias");
                });

            modelBuilder.Entity("ProjetoFinalApi.Models.Data.Partida", b =>
                {
                    b.Navigation("EventosPartida");
                });

            modelBuilder.Entity("ProjetoFinalApi.Models.Data.Time", b =>
                {
                    b.Navigation("Jogadores");

                    b.Navigation("PartidasCasa");

                    b.Navigation("PartidasVisitante");

                    b.Navigation("TimeTorneios");

                    b.Navigation("TransferenciasDestino");

                    b.Navigation("TransferenciasOrigem");
                });

            modelBuilder.Entity("ProjetoFinalApi.Models.Data.Torneio", b =>
                {
                    b.Navigation("Partidas");

                    b.Navigation("TorneioTimes");
                });
#pragma warning restore 612, 618
        }
    }
}