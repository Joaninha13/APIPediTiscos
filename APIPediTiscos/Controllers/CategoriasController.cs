﻿using APIPediTiscos.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIPediTiscos.Controllers;

[ApiController]
[Route("api/[controller]")]

//[Authorize]
public class CategoriasController : ControllerBase {

    private readonly ICategoria _categoriaRepository;

    public CategoriasController(ICategoria categoriaRepository){
        _categoriaRepository = categoriaRepository;
    }

    // GET: api/Categorias
    //[AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllCategorias(){
        try{
            var categorias = await _categoriaRepository.GetAllCategoriasAsync();
            if (categorias == null || !categorias.Any())
                return NotFound(new { Message = "Nenhuma categoria encontrada." });

            return Ok(categorias);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Erro ao buscar categorias.", Details = ex.Message });
        }
    }

}
