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

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult<Pedido>> Post([FromBody] Pedido pedido)
    {
        Usuario usuario = new Usuario
        {
            Id = 1,
            Nome = "Heron",
            Email = "heronze@gmail.com",
            Cpf = "46609031843",
            Senha = "123456",
            Celular = "99999999999",
            Confirmarsenha = "123456",
        };
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