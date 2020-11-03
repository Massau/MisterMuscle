using ProjetoIntegrador.Server;
using Microsoft.AspNetCore.Mvc;
using ProjetoIntegrador.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using System.Collections.Generic;

[ApiController]
[Route("[controller]")]
public class ProdutoPedidoController : Controller
{
    private readonly AppDbContext db;

    public ProdutoPedidoController(AppDbContext db)
    {
        this.db = db;
    }

    [HttpGet]
    [Route("List")]
    public async Task<IActionResult> Get()
    {
        var produtoPedidos = await db.ProdutoPedidos.ToListAsync();
        return Ok(produtoPedidos);
    }

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult<ProdutoPedido>> Post([FromBody] List<ProdutoPedido> produtoPedido)
    {
        foreach (var item in produtoPedido)
        {
            try
            {
                db.Add(item);
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return View(e);
            }
        }
        return Ok();
    }
}