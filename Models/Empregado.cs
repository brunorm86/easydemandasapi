// Models/Empregado.cs

using System.ComponentModel.DataAnnotations.Schema;

namespace easydemandasapi.Models;

[Table("Empregados")]
public class Empregado
{
    public int Id { get; set; }

    // --- Dados pessoais (antes em Pessoa) ---
    public required string Nome { get; set; }
    public required string Sobrenome { get; set; }
    public required string Email { get; set; }
    public required string Telefone { get; set; }
    public required string Endereco { get; set; }
    public required string Cpf { get; set; }
    public required DateOnly DataNascimento { get; set; }

    // --- Dados de vínculo empregatício ---
    public int CargoId { get; set; }
    public Cargo? Cargo { get; set; }

    public required DateOnly DataContratacao { get; set; }

    public int? DepartamentoId { get; set; }
    public Departamento? Departamento { get; set; }

    // --- Dados de Autenticação e Autorização ---
    public string SenhaHash { get; set; } = string.Empty;
    public string Perfil { get; set; } = string.Empty;
}
