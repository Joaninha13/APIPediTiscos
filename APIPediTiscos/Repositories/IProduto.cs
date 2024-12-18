using APIPediTiscos.Entities;

namespace APIPediTiscos.Repositories;
public interface IProduto{

    Task<IEnumerable<Produtos>> GetProdutosByCategoriaAsync(int categoriaId);
    Task<IEnumerable<Produtos>> GetProdutosBySubCategoriaAsync(int subCategoriaId);
    Task<IEnumerable<Produtos>> GetProdutosWithPromotionAsync();
    Task<IEnumerable<Produtos>> GetProdutosWithMoreSalesAsync();
    Task<Produtos> GetDetailsFromProdutoAsync(int id);
    Task<IEnumerable<Produtos>> GetAllProdutosAsync();

}
