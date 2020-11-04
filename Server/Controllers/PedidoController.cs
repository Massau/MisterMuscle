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
public class PedidoController : Controller
{
    private readonly AppDbContext db;

    public PedidoController(AppDbContext db)
    {
        this.db = db;
    }

    [HttpGet]
    [Route("List")]
    public async Task<IActionResult> Get()
    {
        var pedidos = await db.Pedidos.ToListAsync();
        return Ok(pedidos);
    }

    [HttpGet]
    [Route("GetId/{id}")]
    public IActionResult Get(int id)
    {
        var pedido = db.ProdutoPedidos.Include("Produto").Include("Pedido").Where(p => p.Pedido.UsuarioId == id).ToList();
        return Ok(pedido);
    }
    
    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult<Pedido>> Post([FromBody] Pedido pedido)
    {
        try
        {
            db.Add(pedido);
            await db.SaveChangesAsync();
            return Ok(pedido);
        }
        catch (Exception e)
        {
            return View(e);
        }
    }
}