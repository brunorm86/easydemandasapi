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
        var query = _context.Chamados.Include(c => c.Detalhes).Where(c => c.Ativo).AsQueryable();

        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        bool shouldFilter = true;

        if (todos)
        {
            if (User.IsInRole("Suporte") || User.IsInRole("Gestor") || 
                User.HasClaim(ClaimTypes.Role, "Suporte") || User.HasClaim(ClaimTypes.Role, "Gestor") ||
                User.HasClaim("role", "Suporte") || User.HasClaim("role", "Gestor"))
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
            .FirstOrDefaultAsync(c => c.Id == id && c.Ativo);

        if (chamado == null)
        {
            return NotFound();
        }

        if (!User.IsInRole("Suporte") && !User.IsInRole("Gestor") && 
            !User.HasClaim(ClaimTypes.Role, "Suporte") && !User.HasClaim(ClaimTypes.Role, "Gestor") &&
            !User.HasClaim("role", "Suporte") && !User.HasClaim("role", "Gestor"))
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
        chamado.Ativo = true;

        if (chamado.Detalhes == null) chamado.Detalhes = new DetalhesChamado { NivelCriticidade = "Baixo" };
        
        var log = await GerarLog("Chamado criado.");
        chamado.Detalhes.Encaminhamentos = string.IsNullOrEmpty(chamado.Detalhes.Encaminhamentos) 
            ? log 
            : $"{chamado.Detalhes.Encaminhamentos}\n{log}";

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

        var existente = await _context.Chamados.Include(c => c.Detalhes).FirstOrDefaultAsync(c => c.Id == id);
        if (existente == null) return NotFound();

        var alteracoes = new List<string>();

        if (existente.Titulo != chamado.Titulo) alteracoes.Add($"Título (de '{existente.Titulo}' para '{chamado.Titulo}')");
        if (existente.Descricao != chamado.Descricao) alteracoes.Add($"Descrição (de '{existente.Descricao}' para '{chamado.Descricao}')");
        if (chamado.SolicitanteId != 0 && existente.SolicitanteId != chamado.SolicitanteId) alteracoes.Add($"Solicitante (ID de {existente.SolicitanteId} para {chamado.SolicitanteId})");
        
        var existingDepto = existente.Detalhes?.DepartamentoId;
        var newDepto = chamado.Detalhes?.DepartamentoId;
        if (existingDepto != newDepto) alteracoes.Add($"Centro de Custo (ID de {existingDepto?.ToString() ?? "Nenhum"} para {newDepto?.ToString() ?? "Nenhum"})");

        var existingCusto = existente.Detalhes?.Custo;
        var newCusto = chamado.Detalhes?.Custo;
        if (existingCusto != newCusto) alteracoes.Add($"Custo Previsto (de '{existingCusto?.ToString() ?? "Vazio"}' para '{newCusto?.ToString() ?? "Vazio"}')");

        var existingObs = existente.Detalhes?.Observacoes ?? "";
        var newObs = chamado.Detalhes?.Observacoes ?? "";
        if (existingObs != newObs) alteracoes.Add($"Observações (de '{existingObs}' para '{newObs}')");

        var existingCrit = existente.Detalhes?.NivelCriticidade ?? "Baixo";
        var newCrit = chamado.Detalhes?.NivelCriticidade ?? "Baixo";
        if (existingCrit != newCrit) alteracoes.Add($"Criticidade (de '{existingCrit}' para '{newCrit}')");

        string acaoLog = "Chamado atualizado.";
        if (existente.Status == "Aberto" && chamado.Status == "Em Andamento")
        {
            acaoLog = "Chamado atendido.";
            if (alteracoes.Any()) acaoLog += $" Alterações adicionais: {string.Join(", ", alteracoes)}.";
        }
        else if (existente.Status != chamado.Status)
        {
            acaoLog = $"Status alterado de '{existente.Status}' para '{chamado.Status}'.";
            if (alteracoes.Any()) acaoLog += $" Outros campos alterados: {string.Join(", ", alteracoes)}.";
        }
        else if (alteracoes.Any())
        {
            acaoLog = $"Chamado atualizado. Campos modificados: {string.Join(", ", alteracoes)}.";
        }

        var existingLog = existente.Detalhes?.Encaminhamentos ?? "";
        var frontendLog = chamado.Detalhes?.Encaminhamentos ?? "";
        
        if (frontendLog.Length > existingLog.Length)
        {
            var newContent = frontendLog.Substring(existingLog.Length).Trim();
            if (!string.IsNullOrEmpty(newContent))
            {
                acaoLog += $" {newContent}";
            }
        }

        if (chamado.Status == "Concluído" && existente.Status != "Concluído" && !string.IsNullOrWhiteSpace(chamado.Detalhes?.Observacoes))
        {
            acaoLog += $" Observações: {chamado.Detalhes.Observacoes}";
        }

        string newLogLine = await GerarLog(acaoLog);

        existente.Titulo = chamado.Titulo;
        existente.Descricao = chamado.Descricao;
        existente.Status = chamado.Status;
        if (chamado.SolicitanteId != 0) existente.SolicitanteId = chamado.SolicitanteId;
        if (chamado.Status == "Concluído" && existente.DataConclusao == null) existente.DataConclusao = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)).DateTime;
        else if (chamado.Status != "Concluído") existente.DataConclusao = null;

        if (existente.Detalhes == null) existente.Detalhes = new DetalhesChamado { NivelCriticidade = "Baixo" };
        if (chamado.Detalhes != null)
        {
            existente.Detalhes.DepartamentoId = chamado.Detalhes.DepartamentoId;
            existente.Detalhes.Custo = chamado.Detalhes.Custo;
            existente.Detalhes.NivelCriticidade = chamado.Detalhes.NivelCriticidade;
            existente.Detalhes.Observacoes = chamado.Detalhes.Observacoes;
        }

        existente.Detalhes.Encaminhamentos = string.IsNullOrEmpty(existingLog) ? newLogLine : $"{existingLog}\n{newLogLine}";

        try
        {
            await _context.SaveChangesAsync();
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
        var chamado = await _context.Chamados.Include(c => c.Detalhes).FirstOrDefaultAsync(c => c.Id == id);
        if (chamado == null)
        {
            return NotFound();
        }

        chamado.Ativo = false;
        if (chamado.Detalhes == null) chamado.Detalhes = new DetalhesChamado { NivelCriticidade = "Baixo" };
        
        var log = await GerarLog("Chamado excluído.");
        chamado.Detalhes.Encaminhamentos = string.IsNullOrEmpty(chamado.Detalhes.Encaminhamentos) 
            ? log 
            : $"{chamado.Detalhes.Encaminhamentos}\n{log}";

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { mensagem = "Não é possível excluir o chamado no momento." });
        }

        return NoContent();
    }

    private bool ChamadoExists(int id)
    {
        return _context.Chamados.Any(e => e.Id == id);
    }

    private async Task<string> GerarLog(string acao)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (int.TryParse(userIdClaim, out int userId))
        {
            var emp = await _context.Empregados
                .Include(e => e.Cargo)
                .Include(e => e.Departamento)
                .FirstOrDefaultAsync(e => e.Id == userId);

            if (emp != null)
            {
                var cpf = string.IsNullOrEmpty(emp.Cpf) ? "00000000000" : emp.Cpf;
                var cargo = emp.Cargo?.Nome ?? "S/Cargo";
                var depto = emp.Departamento?.Nome ?? "S/Depto";
                var data = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)).ToString("dd/MM/yyyy, HH:mm:ss");
                return $"[{data}] - {emp.Nome} {emp.Sobrenome} (CPF: {cpf}) | {cargo} | {depto} - {acao}";
            }
        }
        var dataAlt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)).ToString("dd/MM/yyyy, HH:mm:ss");
        return $"[{dataAlt}] - Sistema - {acao}";
    }
}
