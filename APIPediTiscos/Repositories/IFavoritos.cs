using APIPediTiscos.Entities;

namespace APIPediTiscos.Repositories;

public interface IFavoritos{


    Task<IEnumerable<Favoritos>> GetAllFavoritosFromCliendAsync(string clientId);

    Task<Favoritos> AddFavoritoAsync(string clientId, int produtoId);

    Task<Favoritos> DeleteFavoritosAsync(int id);

}
