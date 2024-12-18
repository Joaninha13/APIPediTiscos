using APIPediTiscos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APIPediTiscos.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ProdutosController : ControllerBase {

    private readonly IProduto _produtoRepository;

    public ProdutosController(IProduto produtoRepository){
        _produtoRepository = produtoRepository;
    }

    // GET: api/Produtos
    [HttpGet]
    public async Task<IActionResult> GetAllProdutos(){
        try{
            var produtos = await _produtoRepository.GetAllProdutosAsync();
            return Ok(produtos);
        }
        catch (Exception ex){
            return StatusCode(500, new { Message = "Erro ao buscar produtos.", Details = ex.Message });
        }
    }

    // GET: api/Produtos/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetailsFromProduto(int id){
        try{
            var produto = await _produtoRepository.GetDetailsFromProdutoAsync(id);
            if (produto == null)
                return NotFound(new { Message = "Produto não encontrado." });

            return Ok(produto);
        }
        catch (Exception ex){
            return StatusCode(500, new { Message = "Erro ao buscar detalhes do produto.", Details = ex.Message });
        }
    }

    // GET: api/Produtos/categoria/{categoriaId}
    [HttpGet("categoria/{categoriaId}")]
    public async Task<IActionResult> GetProdutosByCategoria(int categoriaId){
        try{
            var produtos = await _produtoRepository.GetProdutosByCategoriaAsync(categoriaId);
            if (!produtos.Any())
                return NotFound(new { Message = "Nenhum produto encontrado para esta categoria." });

            return Ok(produtos);
        }
        catch (Exception ex){
            return StatusCode(500, new { Message = "Erro ao buscar produtos por categoria.", Details = ex.Message });
        }
    }

    // GET: api/Produtos/subcategoria/{subCategoriaId}
    [HttpGet("subcategoria/{subCategoriaId}")]
    public async Task<IActionResult> GetProdutosBySubCategoria(int subCategoriaId){
        try{
            var produtos = await _produtoRepository.GetProdutosBySubCategoriaAsync(subCategoriaId);
            if (!produtos.Any())
                return NotFound(new { Message = "Nenhum produto encontrado para esta subcategoria." });

            return Ok(produtos);
        }
        catch (Exception ex){
            return StatusCode(500, new { Message = "Erro ao buscar produtos por subcategoria.", Details = ex.Message });
        }
    }

    // GET: api/Produtos/mais-vendidos
    [HttpGet("mais-vendidos")]
    public async Task<IActionResult> GetProdutosWithMoreSales(){
        try{
            var produtos = await _produtoRepository.GetProdutosWithMoreSalesAsync();
            return Ok(produtos);
        }
        catch (Exception ex){
            return StatusCode(500, new { Message = "Erro ao buscar produtos mais vendidos.", Details = ex.Message });
        }
    }

    // GET: api/Produtos/em-promocao
    [HttpGet("em-promocao")]
    public async Task<IActionResult> GetProdutosWithPromotion(){
        try{
            var produtos = await _produtoRepository.GetProdutosWithPromotionAsync();
            return Ok(produtos);
        }
        catch (Exception ex){
            return StatusCode(500, new { Message = "Erro ao buscar produtos em promoção.", Details = ex.Message });
        }
    }


}
