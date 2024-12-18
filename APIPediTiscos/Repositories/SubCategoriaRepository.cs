using Microsoft.EntityFrameworkCore;

using APIPediTiscos.Data;
using APIPediTiscos.Entities;

namespace APIPediTiscos.Repositories;

public class SubCategoriaRepository : ISubCategoria {

    private readonly ApplicationDbContext dbContext;
    public SubCategoriaRepository(ApplicationDbContext dbContext){
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<SubCategorias>> GetAllSubCategoriasAsync(){

        return await dbContext.SubCategorias
            .Include("Categoria")
            .OrderBy(o => o.Ordem)
            .ToListAsync();
    }

    public async Task<SubCategorias> GetSubCategoriaAsync(int id){

        return await dbContext.SubCategorias
            .Where(p => p.Id == id)
            .Include("Categoria")
            .FirstOrDefaultAsync();
    }
    public async Task<IEnumerable<SubCategorias>> GetSubCategoriasByCategoriaAsync(int categoriaId){

        return await dbContext.SubCategorias
            .Where(p => p.CategoriaId == categoriaId)
            .Where(x => x.Imagem.Length > 0)
            .Include("Categoria")
            .OrderBy(o => o.Ordem)
            .ToListAsync();
    }
}
