using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using easydemandasapi.Data;
using easydemandasapi.Models;
using Microsoft.AspNetCore.Authorization;

namespace easydemandasapi.Controllers;

[Authorize(Roles = "RH,Gestor")]
[ApiController]
[Route("api/[controller]")]
public class CargosController : ControllerBase
{
    private readonly AppDbContext _context;

    public CargosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cargo>>> GetCargos()
    {
        return await _context.Cargos.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cargo>> GetCargo(int id)
    {
        var cargo = await _context.Cargos.FindAsync(id);

        if (cargo == null)
        {
            return NotFound();
        }

        return cargo;
    }

    [Authorize(Roles = "Gestor")]
    [HttpPost]
    public async Task<ActionResult<Cargo>> PostCargo(Cargo cargo)
    {
        _context.Cargos.Add(cargo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCargo), new { id = cargo.Id }, cargo);
    }

    [Authorize(Roles = "Gestor")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCargo(int id, Cargo cargo)
    {
        if (id != cargo.Id)
        {
            return BadRequest();
        }

        _context.Entry(cargo).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CargoExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [Authorize(Roles = "Gestor")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCargo(int id)
    {
        if (id == 9999) return BadRequest(new { mensagem = "Não é possível excluir o cargo padrão do sistema." });

        var cargo = await _context.Cargos.FindAsync(id);
        if (cargo == null)
        {
            return NotFound();
        }

        var empregados = await _context.Empregados.Where(e => e.CargoId == id).ToListAsync();
        foreach(var e in empregados) e.CargoId = 9999;

        _context.Cargos.Remove(cargo);
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { mensagem = "Não é possível excluir este cargo pois ele está vinculado a um ou mais empregados." });
        }

        return NoContent();
    }

    private bool CargoExists(int id)
    {
        return _context.Cargos.Any(e => e.Id == id);
    }
}
