using APIPediTiscos.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using APIPediTiscos.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIPediTiscos.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UtilizadoresController : ControllerBase
{
    private readonly IConfiguration _config;

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UtilizadoresController(IConfiguration config, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _config = config;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegistarUser([FromBody] Utilizador utilizador)
    {
        var utilizadorExiste = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == utilizador.Email);

        if (utilizadorExiste is not null)
        {
            return BadRequest("Já existe um utilizador com este email");
        }

        var novoUtilizador = new ApplicationUser
        {
            UserName = utilizador.Email,
            Email = utilizador.Email,
            Nome = utilizador.Nome,
            NIF = utilizador.NIF,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        await _userManager.CreateAsync(novoUtilizador, utilizador.Password);
        await _userManager.AddToRoleAsync(novoUtilizador, "Cliente");

        return StatusCode(StatusCodes.Status201Created);
    }

    //[HttpPost("[action]")]
    //public async Task<IActionResult> LoginUser([FromBody] Utilizador utilizador)
    //{
    //    var utilizadorAtual = await _userManager.Users.FirstOrDefaultAsync(u =>
    //                             u.Email == utilizador.Email);

    //    if (utilizadorAtual is null){
    //        return NotFound("Utilizador não encontrado");
    //    }

    //    // ************ Logar com Identity
    //    var result = await _signInManager.PasswordSignInAsync(utilizador.Email, utilizador.Password, false, lockoutOnFailure: false);
    //    if (result.Succeeded)
    //    {
    //        var tempUser = await _userManager.FindByEmailAsync(utilizador.Email);
    //        var userRoles = await _userManager.GetRolesAsync(tempUser);

    //        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
    //        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    //        var claims = new[]
    //        {
    //            new Claim(ClaimTypes.Email , utilizador.Email),
    //            new Claim(ClaimTypes.Role, userRoles[0]!)
    //        };

    //        var token = new JwtSecurityToken(
    //            issuer: _config["JWT:Issuer"],
    //            audience: _config["JWT:Audience"],
    //            claims: claims,
    //            expires: DateTime.Now.AddDays(30),
    //            signingCredentials: credentials);

    //        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

    //        return new ObjectResult(new
    //        {
    //            accesstoken = jwt,
    //            tokentype = "bearer",
    //            utilizadorid = utilizadorAtual.Id,
    //            utilizadornome = utilizadorAtual.Nome
    //        });
    //    }
    //    else
    //    {
    //        return BadRequest("Erro: Login Inválido!");
    //    }
    //}
    [HttpPost("[action]")]
    public async Task<IActionResult> LoginUser([FromBody] Utilizador utilizador)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(utilizador.Email);

            if (user is null)
            {
                return NotFound("Utilizador não encontrado");
            }

            var result = await _signInManager.PasswordSignInAsync(utilizador.Email, utilizador.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim(ClaimTypes.Email, utilizador.Email),
                new Claim(ClaimTypes.Role, userRoles.FirstOrDefault())
            };

                var token = new JwtSecurityToken(
                    issuer: _config["JWT:Issuer"],
                    audience: _config["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: credentials);

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new
                {
                    accessToken = jwt,
                    tokenType = "bearer",
                    userId = user.Id,
                    userName = user.UserName
                });
            }
            else
            {
                return BadRequest("Erro: Login Inválido!");
            }
        }
        catch (Exception ex)
        {
            // Log the exception here
            Console.WriteLine($"Login error: {ex.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

}

public class Utilizador{

    public string? Nome { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }
    public string? ConfirmPassword { get; set; }

    public long? NIF { get; set; }


}