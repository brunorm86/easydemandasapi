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

        // --- Seed de Dados Iniciais (Programático) ---

        // 1. Cargos (5 Cargos + Cargo Indeterminado)
        var cargos = new List<Cargo>
        {
            new Cargo { Id = 1, Nome = "Diretor" },
            new Cargo { Id = 2, Nome = "Gerente" },
            new Cargo { Id = 3, Nome = "Analista" },
            new Cargo { Id = 4, Nome = "Técnico" },
            new Cargo { Id = 5, Nome = "Assistente" },
            new Cargo { Id = 9999, Nome = "CARGO INDETERMINADO" }
        };
        modelBuilder.Entity<Cargo>().HasData(cargos.ToArray());

        // 2. Empregados (30 Empregados + Empregado Indeterminado)
        var empregados = new List<Empregado>();
        
        // Empregado Indeterminado
        empregados.Add(new Empregado
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
            DepartamentoId = null,
            SenhaHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", // "123456"
            Perfil = "Gestor"
        });

        // 30 Empregados Diversos
        var nomes = new[] { "Ana", "Bruno", "Carla", "Daniel", "Elena", "Felipe", "Gabriela", "Henrique", "Isabela", "João", "Julia", "Lucas", "Mariana", "Nicolas", "Olivia", "Pedro", "Sara", "Tiago", "Ursula", "Vitor", "Zeca", "Fernanda", "Gabriel", "Igor", "Diana", "Eduardo", "Quintino", "Zélia", "Heitor", "Clarice" };
        var sobrenomes = new[] { "Santos", "Oliveira", "Pereira", "Costa", "Ferreira", "Lima", "Souza", "Rodrigues", "Martins", "Alves", "Almeida", "Silva", "Barbosa", "Soares", "Carvalho", "Vieira", "Ribeiro", "Gomes", "Fernandes", "Lopes", "Nascimento", "Cardoso", "Teixeira", "Araujo", "Melo", "Rocha", "Pinto", "Batista", "Montes", "Teves" };
        var perfis = new[] { "Gestor", "RH", "Suporte", "Usuario" };

        for (int i = 1; i <= 30; i++)
        {
            var perfil = perfis[(i - 1) % 4];
            var cargoId = ((i - 1) % 5) + 1; // 1 a 5

            empregados.Add(new Empregado
            {
                Id = i,
                Nome = nomes[i - 1],
                Sobrenome = sobrenomes[i - 1],
                Email = $"{nomes[i - 1].ToLower()}.{sobrenomes[i - 1].ToLower()}@easydemandas.com",
                Telefone = $"119{i:D8}",
                Endereco = $"Rua das Flores, {100 + i}",
                Cpf = $"{i:D11}",
                DataNascimento = new DateOnly(1980 + (i % 20), (i % 12) + 1, (i % 28) + 1),
                CargoId = cargoId,
                DataContratacao = new DateOnly(2015 + (i % 10), (i % 12) + 1, (i % 28) + 1),
                DepartamentoId = i <= 7 ? null : ((i - 1) % 7) + 1, // i <= 7 são gestores, quebrando o ciclo de dependência circular
                SenhaHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", // "123456"
                Perfil = perfil
            });
        }
        modelBuilder.Entity<Empregado>().HasData(empregados.ToArray());

        // 3. Departamentos (7 Departamentos + Departamento Indeterminado)
        var departamentos = new List<Departamento>();
        
        // Departamento Indeterminado
        departamentos.Add(new Departamento
        {
            Id = 9999,
            Nome = "DEPARTAMENTO INDETERMINADO",
            Sigla = "IND",
            ResponsavelId = 9999
        });

        var nomesDepts = new[] { "Tecnologia da Informação", "Recursos Humanos", "Financeiro", "Vendas", "Marketing", "Operações", "Jurídico" };
        var siglasDepts = new[] { "TI", "RH", "FIN", "VEN", "MKT", "OPE", "JUR" };
        for (int i = 1; i <= 7; i++)
        {
            departamentos.Add(new Departamento
            {
                Id = i,
                Nome = nomesDepts[i - 1],
                Sigla = siglasDepts[i - 1],
                ResponsavelId = i // Empregado 'i' é o responsável
            });
        }
        modelBuilder.Entity<Departamento>().HasData(departamentos.ToArray());

        // 4. Chamados (50 Chamados)
        var chamados = new List<Chamado>();
        var detalhesChamados = new List<DetalhesChamado>();

        var titulos = new[] { "Erro no ERP", "Reset de Senha", "Problema com VPN", "Impressora Não Funciona", "Lentidão na Estação", "Falha de Conectividade", "Instalação de Office", "Acesso ao Servidor", "Ajuste de Permissões", "Solicitação de Upgrade" };
        var criticidades = new[] { "Baixo", "Medio", "Alto", "Critico" };
        var statuses = new[] { "Aberto", "Em Andamento", "Concluído", "Cancelado" };

        for (int i = 1; i <= 50; i++)
        {
            var solicitanteId = ((i - 1) % 30) + 1; // 1 a 30
            var titulo = $"{titulos[(i - 1) % 10]} #{i}";
            var status = statuses[(i - 1) % 4];

            chamados.Add(new Chamado
            {
                Id = i,
                Titulo = titulo,
                Descricao = $"Chamado número {i} aberto para resolução de {titulo.ToLower()}. Favor analisar com atenção.",
                Status = status,
                DataAbertura = new DateTime(2026, 1 + ((i - 1) % 5), ((i - 1) % 28) + 1, 0, 0, 0, DateTimeKind.Utc),
                SolicitanteId = solicitanteId
            });

            detalhesChamados.Add(new DetalhesChamado
            {
                Id = i,
                ChamadoId = i,
                DepartamentoId = ((i - 1) % 7) + 1, // 1 a 7
                Custo = (i % 7 == 0) ? null : (decimal?)(75.50m * i),
                NivelCriticidade = criticidades[(i - 1) % 4],
                Observacoes = $"Observações do ticket {i} anexadas no histórico.",
                Encaminhamentos = $"Direcionamento e encaminhamentos para chamado {i}."
            });
        }

        modelBuilder.Entity<Chamado>().HasData(chamados.ToArray());
        modelBuilder.Entity<DetalhesChamado>().HasData(detalhesChamados.ToArray());
    }
}