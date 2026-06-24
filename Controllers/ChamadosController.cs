using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using easydemandasapi.Data;
using easydemandasapi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace easydemandasapi.Controllers;

[Authorize(Roles = "RH,Suporte,Usuario,Gestor")]
[ApiController]
[Route("api/[controller]")]
public class ChamadosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ChamadosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Chamado>>> GetChamados([FromQuery] bool todos = false)
    {
        var query = _context.Chamados.Include(c => c.Detalhes).AsQueryable();

        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        bool shouldFilter = true;

        if (todos)
        {
            var roleClaim = User.FindFirstValue(ClaimTypes.Role);
            if (roleClaim == "Suporte" || roleClaim == "Gestor")
            {
                shouldFilter = false;
            }
        }

        if (shouldFilter)
        {
            if (int.TryParse(userIdClaim, out int userId))
            {
                query = query.Where(c => c.SolicitanteId == userId);
            }
        }

        return await query.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Chamado>> GetChamado(int id)
    {
        var chamado = await _context.Chamados
            .Include(c => c.Detalhes)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (chamado == null)
        {
            return NotFound();
        }

        var roleClaim = User.FindFirstValue(ClaimTypes.Role);
        if (roleClaim != "Suporte" && roleClaim != "Gestor")
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userIdClaim, out int userId))
            {
                if (chamado.SolicitanteId != userId)
                {
                    return Forbid();
                }
            }
        }

        return chamado;
    }

    [HttpPost]
    public async Task<ActionResult<Chamado>> PostChamado(Chamado chamado)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (int.TryParse(userIdClaim, out int userId))
        {
            chamado.SolicitanteId = userId;
        }

        chamado.Status = "Aberto";

        _context.Chamados.Add(chamado);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetChamado), new { id = chamado.Id }, chamado);
    }

    [Authorize(Roles = "Suporte,Gestor")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutChamado(int id, Chamado chamado)
    {
        if (id != chamado.Id)
        {
            return BadRequest();
        }

        _context.Entry(chamado).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ChamadoExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { mensagem = "Erro de integridade ao atualizar chamado." });
        }

        return NoContent();
    }

    [Authorize(Roles = "Suporte,Gestor")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteChamado(int id)
    {
        var chamado = await _context.Chamados.FindAsync(id);
        if (chamado == null)
        {
            return NotFound();
        }

        _context.Chamados.Remove(chamado);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { mensagem = "Não é possível excluir o chamado pois ele está vinculado a outros registros." });
        }

        return NoContent();
    }

    private bool ChamadoExists(int id)
    {
        return _context.Chamados.Any(e => e.Id == id);
    }
}
