using APIPediTiscos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APIPediTiscos.Controllers;

[Route("api/[controller]")]
[ApiController]

//[Authorize]
public class SubCategoriasController : ControllerBase {

    private readonly ISubCategoria _subCategoriaRepository;

    public SubCategoriasController(ISubCategoria subCategoriaRepository){
        _subCategoriaRepository = subCategoriaRepository;
    }

    // GET: api/SubCategorias
    [HttpGet]
    public async Task<IActionResult> GetAllSubCategorias(){
        try{
            var subCategorias = await _subCategoriaRepository.GetAllSubCategoriasAsync();
            return Ok(subCategorias);
        }
        catch (Exception ex){
            return StatusCode(500, new { Message = "Erro ao buscar subcategorias.", Details = ex.Message });
        }
    }

    // GET: api/SubCategorias/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubCategoria(int id){
        try{
            var subCategoria = await _subCategoriaRepository.GetSubCategoriaAsync(id);
            if (subCategoria == null)
                return NotFound(new { Message = "Subcategoria não encontrada." });

            return Ok(subCategoria);
        }
        catch (Exception ex){
            return StatusCode(500, new { Message = "Erro ao buscar subcategoria.", Details = ex.Message });
        }
    }

    // GET: api/SubCategorias/categoria/{categoriaId}
    [HttpGet("categoria/{categoriaId}")]
    public async Task<IActionResult> GetSubCategoriasByCategoria(int categoriaId){
        try{
            var subCategorias = await _subCategoriaRepository.GetSubCategoriasByCategoriaAsync(categoriaId);
            if (subCategorias == null || !subCategorias.Any())
                return NotFound(new { Message = "Nenhuma subcategoria encontrada para esta categoria." });

            return Ok(subCategorias);
        }
        catch (Exception ex){
            return StatusCode(500, new { Message = "Erro ao buscar subcategorias da categoria.", Details = ex.Message });
        }
    }

}
