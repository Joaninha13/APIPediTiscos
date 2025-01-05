using APIPediTiscos.Data;
using APIPediTiscos.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIPediTiscos.Repositories;

public class FavoritoRepository : IFavoritos{

    private readonly ApplicationDbContext dbContext;
    public FavoritoRepository(ApplicationDbContext dbContext){
        this.dbContext = dbContext;
    }

    public async Task<Favoritos> AddFavoritoAsync(string clientId, int produtoId){

        var favorito = new Favoritos{
            ClienteId = clientId,
            ProdutoId = produtoId
        };
        dbContext.Favoritos.Add(favorito);
        await dbContext.SaveChangesAsync();

        return favorito;
    }

    public async Task<Favoritos> DeleteFavoritosAsync(int id){

        var favorito = await dbContext.Favoritos.FindAsync(id);


        if (favorito != null){
            dbContext.Favoritos.Remove(favorito);
            await dbContext.SaveChangesAsync();
            return favorito;
        }

        return null;

    }

    public async Task<IEnumerable<Favoritos>> GetAllFavoritosFromCliendAsync(string clientId){

        return await dbContext.Favoritos.Where(p => p.ClienteId == clientId).Include("Cliente").Include("Produto").ToListAsync();

    }
}
