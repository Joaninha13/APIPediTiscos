using Microsoft.EntityFrameworkCore;

using APIPediTiscos.Data;
using APIPediTiscos.Entities;

namespace APIPediTiscos.Repositories;

public class ProdutoRepository : IProduto{

    private readonly ApplicationDbContext dbContext;

    public ProdutoRepository(ApplicationDbContext dbContext){
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Produtos>> GetAllProdutosAsync(){

        return await dbContext.Produtos
            .Include("SubCategoria")
            .Include("ModoDispo")
            .Include("Promocoes")
            .ToListAsync();

    }

    public async Task<Produtos> GetDetailsFromProdutoAsync(int id){

        var produto = await dbContext.Produtos
            .Where(p => p.Id == id)
            .Include("SubCategoria")
            .Include("ModoDispo")
            .Include("Promocoes")
            .FirstOrDefaultAsync();

        if (produto == null)
            throw new Exception("Produto não encontrado");
        else
            return produto;
    }

    public async Task<IEnumerable<Produtos>> GetProdutosByCategoriaAsync(int categoriaId){

        return await dbContext.Produtos
            .Where(p => p.SubCategoria.CategoriaId == categoriaId)
            .Include("SubCategoria")
            .Include("ModoDispo")
            .Include("Promocoes")
            .ToListAsync();

    }

    public async Task<IEnumerable<Produtos>> GetProdutosBySubCategoriaAsync(int subCategoriaId){

        return await dbContext.Produtos
            .Where(p => p.SubCategoriaId == subCategoriaId)
            .Include("SubCategoria")
            .Include("ModoDispo")
            .Include("Promocoes")
            .ToListAsync();

    }

    public async Task<IEnumerable<Produtos>> GetProdutosWithMoreSalesAsync(){

        return await dbContext.Produtos
            .Include("SubCategoria")
            .Include("ModoDispo")
            .Include("Promocoes")
            .OrderByDescending(p => p.NVendas)
            .ToListAsync();

    }

    public async Task<IEnumerable<Produtos>> GetProdutosWithPromotionAsync(){

        return await dbContext.Produtos
            .Where(p => p.PromocoesId != null)
            .Include("SubCategoria")
            .Include("ModoDispo")
            .Include("Promocoes")
            .ToListAsync();

    }
}
