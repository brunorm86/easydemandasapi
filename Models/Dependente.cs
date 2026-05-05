// Models/Dependente.cs
using System.Text.Json.Serialization;

namespace easydemandasapi.Models;

public class Dependente
{
    public int Id { get; set; }

    // Relação com o Empregado (responsável)
    public int EmpregadoId { get; set; }
    
    [JsonIgnore]
    public Empregado? Empregado { get; set; }

    // Relação com a Pessoa (o dependente físico)
    public int PessoaId { get; set; }
    public Pessoa? Pessoa { get; set; }

    // Atributo adicional da associação
    public required string Parentesco { get; set; }
}
