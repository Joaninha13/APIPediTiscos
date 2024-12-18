using APIPediTiscos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APIPediTiscos.Controllers;

[Route("api/[controller]")]
[ApiController]

//[Authorize]
public class ItensEncomendadosController : ControllerBase {

    private readonly IItensEncomendado _itensEncomendadosRepository;

    public ItensEncomendadosController(IItensEncomendado itensEncomendadosRepository){
        _itensEncomendadosRepository = itensEncomendadosRepository;
    }

    // POST: api/ItensEncomendados
    [HttpPost]
    public async Task<IActionResult> AddItemToEncomenda(int encomendaId, int produtoId, int quantidade){
        try{
            var item = await _itensEncomendadosRepository.AddItemToEncomendaAsync(encomendaId, produtoId, quantidade);
            return CreatedAtAction(nameof(GetItensEncomendadosByEncomenda), new { encomendaId = item.EncomendaId }, item);
        }
        catch (Exception ex){
            return StatusCode(500, new { Message = "Erro ao adicionar o item à encomenda.", Details = ex.Message });
        }
    }

    // DELETE: api/ItensEncomendados/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItemFromEncomenda(int id){
        try{
            var item = await _itensEncomendadosRepository.DeleteItemFromEncomenda(id);
            if (item == null)
                return NotFound(new { Message = "Item não encontrado." });

            return Ok(item);
        }
        catch (Exception ex){
            return StatusCode(500, new { Message = "Erro ao remover o item da encomenda.", Details = ex.Message });
        }
    }

    // GET: api/ItensEncomendados/{encomendaId}
    [HttpGet("{encomendaId}")]
    public async Task<IActionResult> GetItensEncomendadosByEncomenda(int encomendaId){
        try
        {
            var itens = await _itensEncomendadosRepository.GetItensEncomendadosByEncomendaAsync(encomendaId);
            if (itens == null)
                return NotFound(new { Message = "Itens não encontrados para a encomenda especificada." });

            return Ok(itens);
        }
        catch (Exception ex){
            return StatusCode(500, new { Message = "Erro ao buscar os itens encomendados.", Details = ex.Message });
        }
    }

    // PUT: api/ItensEncomendados
    [HttpPut]
    public async Task<IActionResult> RefreshItem(int encomendaId, int produtoId, int quantidade){
        try{
            var item = await _itensEncomendadosRepository.RefreshItemAsync(encomendaId, produtoId, quantidade);
            if (item == null)
                return NotFound(new { Message = "Item não encontrado para atualizar." });

            return Ok(item);
        }
        catch (Exception ex){
            return StatusCode(500, new { Message = "Erro ao atualizar o item da encomenda.", Details = ex.Message });
        }
    }

}
