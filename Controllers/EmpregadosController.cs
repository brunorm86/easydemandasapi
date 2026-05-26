// Controllers/EmpregadosController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using easydemandasapi.Data;
using easydemandasapi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Text;

namespace easydemandasapi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EmpregadosController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmpregadosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Empregado>>> GetEmpregados()
    {
        var empregados = await _context.Empregados
            .Include(e => e.Cargo)
            .Include(e => e.Departamento)
            .ToListAsync();
        return Ok(empregados);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Empregado>> GetEmpregado(int id)
    {
        var empregado = await _context.Empregados
            .Include(e => e.Cargo)
            .Include(e => e.Departamento)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (empregado == null)
        {
            return NotFound(new { mensagem = $"Empregado com ID {id} não encontrado." });
        }

        return Ok(empregado);
    }

    [Authorize(Roles = "RH,Gestor")]
    [HttpPost]
    public async Task<ActionResult<Empregado>> PostEmpregado(Empregado empregado)
    {
        if (string.IsNullOrEmpty(empregado.SenhaHash))
        {
            empregado.SenhaHash = "8d969eee76d92476d701125b16222e1785c874291147139c74c09044b1551139"; // 123456
        }
        else if (empregado.SenhaHash.Length != 64)
        {
            empregado.SenhaHash = HashPassword(empregado.SenhaHash);
        }

        if (string.IsNullOrEmpty(empregado.Perfil))
        {
            empregado.Perfil = "Usuario";
        }

        _context.Empregados.Add(empregado);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEmpregado), new { id = empregado.Id }, empregado);
    }

    [Authorize(Roles = "RH,Gestor")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmpregado(int id, Empregado empregado)
    {
        if (id != empregado.Id)
        {
            return BadRequest(new { mensagem = "O ID da URL não corresponde ao ID do empregado no body." });
        }

        var existente = await _context.Empregados.FindAsync(id);

        if (existente == null)
        {
            return NotFound(new { mensagem = $"Empregado com ID {id} não encontrado." });
        }

        existente.Nome = empregado.Nome;
        existente.Sobrenome = empregado.Sobrenome;
        existente.Email = empregado.Email;
        existente.Telefone = empregado.Telefone;
        existente.Endereco = empregado.Endereco;
        existente.Cpf = empregado.Cpf;
        existente.DataNascimento = empregado.DataNascimento;
        existente.CargoId = empregado.CargoId;
        existente.DataContratacao = empregado.DataContratacao;
        existente.DepartamentoId = empregado.DepartamentoId;
        
        if (!string.IsNullOrEmpty(empregado.Perfil))
        {
            existente.Perfil = empregado.Perfil;
        }

        if (!string.IsNullOrEmpty(empregado.SenhaHash) && empregado.SenhaHash != existente.SenhaHash)
        {
            existente.SenhaHash = empregado.SenhaHash.Length == 64 ? empregado.SenhaHash : HashPassword(empregado.SenhaHash);
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [Authorize(Roles = "RH,Gestor")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmpregado(int id)
    {
        var empregado = await _context.Empregados.FindAsync(id);

        if (empregado == null)
        {
            return NotFound(new { mensagem = $"Empregado com ID {id} não encontrado." });
        }

        _context.Empregados.Remove(empregado);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static string HashPassword(string password)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes).ToLower();
    }
}
