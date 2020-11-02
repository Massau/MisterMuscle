using ProjetoIntegrador.Server;
using Microsoft.AspNetCore.Mvc;
using ProjetoIntegrador.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

[ApiController]
[Route("[controller]")]
public class FornecedorController : Controller
{
    private readonly AppDbContext db;

    public FornecedorController(AppDbContext db)
    {
        this.db = db;
    }

    [HttpGet]
    [Route("List")]
    public async Task<IActionResult> Get()
    {
        var fornecedores = await db.Fornecedores.ToListAsync();
        return Ok(fornecedores);
        
    }

    

    

}