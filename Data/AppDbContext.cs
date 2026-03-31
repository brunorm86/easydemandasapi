// Data/AppDbContext.cs

// Importações necessárias:
// - Microsoft.EntityFrameworkCore: namespace do EF Core (DbContext, DbSet, etc.)
// - easydemandasapi.Models: namespace onde estão nossas classes de modelo
using Microsoft.EntityFrameworkCore;
using easydemandasapi.Models;

namespace easydemandasapi.Data;

// AppDbContext herda de DbContext (classe base do EF Core).
// Herdando de DbContext, nossa classe ganha todos os poderes do EF:
// consultas, inserções, atualizações, deleções, migrations, etc.
public class AppDbContext : DbContext
{
    // Construtor que recebe as opções de configuração.
    // Essas opções (qual banco usar, connection string, etc.)
    // são injetadas pelo sistema de Injeção de Dependência do .NET.
    // Você não chama esse construtor manualmente — o .NET faz isso.
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        // Repassa as opções para o construtor da classe pai (DbContext)
    }

    // DbSet<Pessoa> representa a tabela "Pessoas" no banco de dados.
    //
    // O que é um DbSet?
    // É uma coleção que o EF mapeia diretamente para uma tabela.
    // Através do DbSet, você pode:
    //   _context.Pessoas.ToListAsync()         → SELECT * FROM "Pessoas"
    //   _context.Pessoas.FindAsync(id)         → SELECT * FROM "Pessoas" WHERE Id = @id
    //   _context.Pessoas.Add(pessoa)          → prepara um INSERT
    //   _context.Pessoas.Remove(pessoa)       → prepara um DELETE
    //   _context.SaveChangesAsync()             → executa as operações pendentes no banco
    //
    // O nome da propriedade ("Pessoas") define o nome da tabela no banco.
    public DbSet<Pessoa> Pessoas { get; set; }
}