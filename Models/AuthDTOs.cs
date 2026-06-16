// Models/AuthDTOs.cs

namespace easydemandasapi.Models;

public class LoginDto
{
    public required string Email { get; set; }
    public required string Senha { get; set; }
}

public class AuthResponseDto
{
    public required string Token { get; set; }
    public required int UsuarioId { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required string Perfil { get; set; }
    public string? Cargo { get; set; }
    public string? Departamento { get; set; }
    public string? Cpf { get; set; }
}
