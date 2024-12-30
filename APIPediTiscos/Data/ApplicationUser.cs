using Microsoft.AspNetCore.Identity;

namespace APIPediTiscos.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser{

    public string? Nome { get; set; }
    public long? NIF { get; set; }
    public string? Morada { get; set; }
    public string? Codigo_Postal { get; set; }

}
