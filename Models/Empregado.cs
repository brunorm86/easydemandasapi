// Models/Empregado.cs

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace easydemandasapi.Models;

[Table("Empregados")]
public class Empregado
{
    public int Id { get; set; }

    // Chave estrangeira para Pessoa
    public int PessoaId { get; set; }
    public Pessoa? Pessoa { get; set; }

    // Chave Estrangeira para Cargo
    public int CargoId { get; set; }
    public Cargo? Cargo { get; set; }

    // Data em que o empregado foi contratado
    public required DateOnly DataContratacao { get; set; }

    [JsonIgnore]
    public ICollection<Dependente> Dependentes { get; set; } = new List<Dependente>();

    // Chave Estrangeira para o Departamento em que o Empregado trabalha
    public int? DepartamentoId { get; set; }
    
    public Departamento? Departamento { get; set; }
}
