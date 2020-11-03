using ProjetoIntegrador.Server;
using Microsoft.AspNetCore.Mvc;
using ProjetoIntegrador.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

[ApiController]
[Route("[controller]")]
public class ViewController : Controller
{
    private readonly AppDbContext db;

    public ViewController(AppDbContext db)
    {
        this.db = db;
    }

    [HttpGet]
    [Route("GetId")]
    public async Task<IActionResult> Get([FromQuery] string id)
    {
        // procura no banco na tabela products se tem algum Id igual e retorna o produto com todas suas informações
        var produto = await db.Produtos.SingleOrDefaultAsync(p => p.Id == Convert.ToInt32(id));
        

        return Ok(produto);
    }

    

}