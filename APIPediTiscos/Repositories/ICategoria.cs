using APIPediTiscos.Entities;

namespace APIPediTiscos.Repositories;

public interface ICategoria{

    Task<IEnumerable<Categorias>> GetAllCategoriasAsync();
}
