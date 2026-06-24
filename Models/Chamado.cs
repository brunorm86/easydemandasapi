// Models/Chamado.cs

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace easydemandasapi.Models;

[Table("Chamados")]
public class Chamado
{
    public int Id { get; set; }

    public required string Titulo { get; set; }

    public required string Descricao { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime DataAbertura { get; set; } = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)).DateTime;

    public bool Ativo { get; set; } = true;

    public DateTime? DataConclusao { get; set; }

    // Chave Estrangeira para o Empregado que solicitou o chamado
    public int SolicitanteId { get; set; }
    public Empregado? Solicitante { get; set; }

    // Propriedade de navegação para os detalhes (relação 1:1)
    public DetalhesChamado? Detalhes { get; set; }
}
