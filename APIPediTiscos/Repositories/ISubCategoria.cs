using APIPediTiscos.Entities;

namespace APIPediTiscos.Repositories;

public interface ISubCategoria{

    Task<IEnumerable<SubCategorias>> GetAllSubCategoriasAsync();

    Task<IEnumerable<SubCategorias>> GetSubCategoriasByCategoriaAsync(int categoriaId);

    Task<SubCategorias> GetSubCategoriaAsync(int id);

}
