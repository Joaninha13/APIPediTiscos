using APIPediTiscos.Entities;
using APIPediTiscos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APIPediTiscos.Controllers;

[Route("api/[controller]")]
[ApiController]

//[Authorize]

public class FavoritosController : ControllerBase{

    private readonly IFavoritos _favoritoRepository;

    public FavoritosController(IFavoritos favoritoRepository){
        _favoritoRepository = favoritoRepository;
    }

    // GET: api/Favoritos/{clientId}
    [HttpGet("{clientId}")]
    public async Task<IActionResult> GetAllFavoritosFromClient(string clientId)
    {
        try
        {
            var favoritos = await _favoritoRepository.GetAllFavoritosFromCliendAsync(clientId);
            if (favoritos == null || !favoritos.Any())
                return NotFound(new { Message = "Nenhum favorito encontrado para este cliente." });

            return Ok(favoritos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Erro ao buscar os favoritos do cliente.", Details = ex.Message });
        }
    }

    // POST: api/Favoritos
    [HttpPost]
    public async Task<IActionResult> AddFavorito(string clienteId, int produtoId)
    {
        if (string.IsNullOrEmpty(clienteId) || produtoId <= 0)
            return BadRequest(new { Message = "Dados inválidos para adicionar favorito." });

        try
        {
            var novoFavorito = await _favoritoRepository.AddFavoritoAsync(clienteId, produtoId);
            return CreatedAtAction(nameof(GetAllFavoritosFromClient), new { clientId = novoFavorito.ClienteId }, novoFavorito);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Erro ao adicionar favorito.", Details = ex.Message });
        }
    }

    // DELETE: api/Favoritos/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFavorito(int id)
    {
        try
        {
            var favorito = await _favoritoRepository.DeleteFavoritosAsync(id);
            if (favorito == null)
                return NotFound(new { Message = "Favorito não encontrado." });

            return Ok(favorito);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Erro ao remover favorito.", Details = ex.Message });
        }
    }

}
