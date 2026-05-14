// Data/AppDbContext.cs

using Microsoft.EntityFrameworkCore;
using easydemandasapi.Models;

namespace easydemandasapi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Empregado> Empregados { get; set; }
    public DbSet<Departamento> Departamentos { get; set; }
    public DbSet<Cargo> Cargos { get; set; }
    public DbSet<Chamado> Chamados { get; set; }
    public DbSet<DetalhesChamado> DetalhesChamados { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relação 1: Empregado trabalha em um Departamento
        modelBuilder.Entity<Empregado>()
            .HasOne(e => e.Departamento)
            .WithMany()
            .HasForeignKey(e => e.DepartamentoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relação 2: Departamento tem um Empregado como Responsável
        modelBuilder.Entity<Departamento>()
            .HasOne(d => d.Responsavel)
            .WithMany()
            .HasForeignKey(d => d.ResponsavelId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relação 3: Chamado é solicitado por um Empregado
        modelBuilder.Entity<Chamado>()
            .HasOne(c => c.Solicitante)
            .WithMany()
            .HasForeignKey(c => c.SolicitanteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relação 4: Chamado 1:1 DetalhesChamado
        modelBuilder.Entity<DetalhesChamado>()
            .HasOne(d => d.Chamado)
            .WithOne(c => c.Detalhes)
            .HasForeignKey<DetalhesChamado>(d => d.ChamadoId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relação 5: DetalhesChamado pertence a um Centro de Custo (Departamento)
        modelBuilder.Entity<DetalhesChamado>()
            .HasOne(d => d.Departamento)
            .WithMany()
            .HasForeignKey(d => d.DepartamentoId)
            .OnDelete(DeleteBehavior.Restrict);

        // --- Seed de Dados Iniciais ---
        modelBuilder.Entity<Cargo>().HasData(new Cargo
        {
            Id = 9999,
            Nome = "CARGO INDETERMINADO"
        });

        modelBuilder.Entity<Empregado>().HasData(new Empregado
        {
            Id = 9999,
            Nome = "EMPREGADO",
            Sobrenome = "INDETERMINADO",
            Email = "indeterminado@easydemandas.com",
            Telefone = "00000000000",
            Endereco = "Indeterminado",
            Cpf = "00000000000",
            DataNascimento = new DateOnly(1900, 1, 1),
            CargoId = 9999,
            DataContratacao = new DateOnly(1900, 1, 1),
            DepartamentoId = null
        });

        modelBuilder.Entity<Departamento>().HasData(new Departamento
        {
            Id = 9999,
            Nome = "DEPARTAMENTO INDETERMINADO",
            Sigla = "IND",
            ResponsavelId = 9999
        });
    }
}