// Models/Empregado.cs

namespace easydemandasapi.Models;

public class Empregado : Pessoa
{
    // Cargo do empregado
    public required string Cargo { get; set; }

    // Data em que o empregado foi contratado
    public required DateOnly DataContratacao { get; set; }
}
