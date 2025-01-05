using APIPediTiscos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APIPediTiscos.Controllers;


[Route("api/[controller]")]
[ApiController]

//[Authorize]
public class PagamentosController : ControllerBase {

    private readonly IPagamento _pagamentoRepository;

    public PagamentosController(IPagamento pagamentoRepository){
        _pagamentoRepository = pagamentoRepository;
    }

    // POST: api/Pagamentos/{encomendaId}
    [HttpPost("{encomendaId}")]
    public async Task<IActionResult> AddPagamentoToEncomenda(int encomendaId){

        try{

            var pagamento = await _pagamentoRepository.AddPagamentoToEncomendaAsync(encomendaId);
            if (pagamento == null)
                return NotFound(new { Message = "Encomenda não encontrada." });

            return CreatedAtAction(nameof(GetPagamentoByEncomenda), new { encomendaId = pagamento.EncomendaId }, pagamento);
        }
        catch (Exception ex){
            return StatusCode(500, new { Message = "Ocorreu um erro ao criar o pagamento.", Details = ex.Message });
        }
    }

    // PUT: api/Pagamentos/{encomendaId}
    [HttpPut("{encomendaId}")]
    public async Task<IActionResult> ReattempPagamento(int encomendaId){
        try
        {
            var pagamento = await _pagamentoRepository.ReattempPagamentoAsync(encomendaId);
            if (pagamento == null)
                return NotFound(new { Message = "Pagamento não encontrado." });

            return Ok(pagamento);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Ocorreu um erro ao reativar o pagamento.", Details = ex.Message });
        }
    }

    // GET: api/Pagamentos/{encomendaId}
    [HttpGet("{encomendaId}")]
    public async Task<IActionResult> GetPagamentoByEncomenda(int encomendaId){

        try{

            var pagamento = await _pagamentoRepository.GetPagamentoByEncomendaAsync(encomendaId);
            if (pagamento == null)
                return NotFound(new { Message = "Pagamento não encontrado." });


            return Ok(pagamento);
        }
        catch (Exception ex){
            return StatusCode(500, new { Message = "Ocorreu um erro ao buscar o pagamento.", Details = ex.Message });
        }
    }

}
