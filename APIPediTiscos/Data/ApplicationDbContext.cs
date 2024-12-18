using APIPediTiscos.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIPediTiscos.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options){

    public DbSet<Categorias> Categorias { get; set; }
    public DbSet<SubCategorias> SubCategorias { get; set; }
    public DbSet<Produtos> Produtos { get; set; }
    public DbSet<Encomendas> Encomendas { get; set; }
    public DbSet<Envios> Envios { get; set; }
    public DbSet<ItensEncomendados> ItensEncomendados { get; set; }
    public DbSet<ModoDispo> ModoDispo { get; set; }
    public DbSet<Pagamentos> Pagamentos { get; set; }
    public DbSet<Promocoes> Promocoes { get; set; }

}
