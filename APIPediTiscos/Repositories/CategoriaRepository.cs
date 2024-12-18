using Microsoft.EntityFrameworkCore;

using APIPediTiscos.Data;
using APIPediTiscos.Entities;

namespace APIPediTiscos.Repositories;

public class CategoriaRepository : ICategoria {

    private readonly ApplicationDbContext dbContext;
    public CategoriaRepository(ApplicationDbContext dbContext){
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Categorias>> GetAllCategoriasAsync() => await dbContext.Categorias.ToListAsync();
}
