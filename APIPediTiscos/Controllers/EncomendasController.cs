using APIPediTiscos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APIPediTiscos.Controllers;

[Route("api/[controller]")]
[ApiController]

//[Authorize]
public class EncomendasController : ControllerBase {

    private readonly IEncomenda _encomendaRepository;

    public EncomendasController(IEncomenda encomendaRepository){
        _encomendaRepository = encomendaRepository;
    }

    // GET: api/Encomendas/{clientId}
    [HttpGet("{clientId}")]
    public async Task<IActionResult> GetAllEncomendasByCliente(string clientId){
        try{
            var encomendas = await _encomendaRepository.GetAllEncomendasByClienteAsync(clientId);
            if (encomendas == null || !encomendas.Any())
                return NotFound(new { Message = "Nenhuma encomenda confirmada encontrada para este cliente." });

            return Ok(encomendas);
        }
        catch (Exception ex){
            return StatusCode(500, new { Message = "Erro ao buscar encomendas do cliente.", Details = ex.Message });
        }
    }

    // POST: api/Encomendas/{clientId}
    [HttpPost("{clientId}")]
    public async Task<IActionResult> StartNewEncomenda(string clientId){
        try{
            var encomenda = await _encomendaRepository.StartNewEncomendaAsync(clientId);
            return CreatedAtAction(nameof(GetAllEncomendasByCliente), new { clientId = encomenda.ClienteId }, encomenda);
        }
        catch (Exception ex){
            return StatusCode(500, new { Message = "Erro ao iniciar uma nova encomenda.", Details = ex.Message });
        }
    }

}
