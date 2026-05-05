// Models/Departamento.cs

namespace easydemandasapi.Models;

public class Departamento
{
    // Chave primária do departamento
    public int Id { get; set; }

    // Nome do departamento
    public required string Nome { get; set; }

    // Sigla do departamento (opcional)
    public string? Sigla { get; set; }

    // Chave estrangeira para o Empregado responsável
    public int ResponsavelId { get; set; }

    // Propriedade de navegação para o responsável
    public Empregado? Responsavel { get; set; }
}
