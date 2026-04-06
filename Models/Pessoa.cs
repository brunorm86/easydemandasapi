// Models/Pessoa.cs

namespace easydemandasapi.Models;

// A classe Pessoa representa a tabela "Pessoas" no banco de dados.
// O Entity Framework usa esta classe para:
//   1. Criar a tabela via Migration
//   2. Ler e escrever dados na tabela
//   3. Mapear resultados de queries para objetos C#
//
// Convenção de nomenclatura do EF:
//   - O nome da tabela no banco será o nome da classe no plural: "Produtos"
//   - A propriedade "Id" é automaticamente reconhecida como chave primária
public class Pessoa
{
    // Chave primária — identificador único da pessoa.
    // O EF reconhece "Id" automaticamente como PK.
    // No banco: coluna "Id" INTEGER PRIMARY KEY AUTOINCREMENT
    public int Id { get; set; }

    // Nome da pessoa — campo obrigatório.
    // "required" garante que o C# não permite criar uma Pessoa sem Nome.
    // No banco: coluna "Nome" TEXT NOT NULL
    public required string Nome { get; set; }

    // Sobrenome da pessoa — campo opcional.
    // "?" indica que a propriedade pode ser nula (nullable).
    // No banco: coluna "Sobrenome" TEXT NULL
    public required string Sobrenome { get; set; }

    // Email da pessoa — campo opcional.
    // No banco: coluna "Email" TEXT NULL
    public required string Email { get; set; }

    // Telefone da pessoa — campo opcional.
    // No banco: coluna "Telefone" TEXT NULL
    public required string Telefone { get; set; }
   
    // Endereço da pessoa — campo opcional.
    // No banco: coluna "Endereco" TEXT NULL
    public required string Endereco { get; set; }

    // CPF da pessoa — campo opcional.
    // No banco: coluna "CPF" TEXT NULL
    public required string Cpf { get; set; }

 // Data de nascimento da pessoa.
    // Apenas dia, mês e ano.
    // No banco: coluna "DataNascimento" DATE
    public required DateOnly DataNascimento { get; set; }

    
    public required int NumDependentes { get; set; }
}