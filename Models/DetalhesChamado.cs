// Models/DetalhesChamado.cs

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace easydemandasapi.Models;

[Table("DetalhesChamados")]
public class DetalhesChamado
{
    public int Id { get; set; }

    // Chave estrangeira para Chamado (Relação 1:1)
    public int ChamadoId { get; set; }
    
    [JsonIgnore]
    public Chamado? Chamado { get; set; }

    // Centro de Custo (Chave Estrangeira para Departamento)
    public int? DepartamentoId { get; set; }
    public Departamento? Departamento { get; set; }

    public decimal? Custo { get; set; }

    public required string NivelCriticidade { get; set; }

    public string? Observacoes { get; set; }

    public string? Encaminhamentos { get; set; }
}
