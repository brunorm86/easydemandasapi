// Controllers/AuthController.cs

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using easydemandasapi.Data;
using easydemandasapi.Models;

namespace easydemandasapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Senha))
        {
            return BadRequest("Email e senha são obrigatórios.");
        }

        var emailLower = loginDto.Email.ToLower().Trim();

        var empregado = await _context.Empregados
            .Include(e => e.Cargo)
            .Include(e => e.Departamento)
            .FirstOrDefaultAsync(e => e.Email.ToLower() == emailLower);

        if (empregado == null)
        {
            return Unauthorized("Credenciais inválidas.");
        }

        var senhaHash = HashPassword(loginDto.Senha);

        if (empregado.SenhaHash != senhaHash)
        {
            return Unauthorized("Credenciais inválidas.");
        }

        var token = GerarJwtToken(empregado);

        return Ok(new AuthResponseDto
        {
            Token = token,
            UsuarioId = empregado.Id,
            Nome = $"{empregado.Nome} {empregado.Sobrenome}",
            Email = empregado.Email,
            Perfil = empregado.Perfil,
            Cargo = empregado.Cargo?.Nome,
            Departamento = empregado.Departamento?.Nome,
            Cpf = empregado.Cpf
        });
    }

    private string GerarJwtToken(Empregado empregado)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? "super_secret_easydemandas_token_key_123456!");
        
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, empregado.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{empregado.Nome} {empregado.Sobrenome}"),
            new Claim(ClaimTypes.Email, empregado.Email),
            new Claim(ClaimTypes.Role, empregado.Perfil)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"] ?? "easydemandasapi",
            Audience = _configuration["Jwt:Audience"] ?? "easydemandasfront"
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private static string HashPassword(string password)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes).ToLower();
    }
}
