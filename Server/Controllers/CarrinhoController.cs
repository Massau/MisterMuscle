using ProjetoIntegrador.Server;
using Microsoft.AspNetCore.Mvc;
using ProjetoIntegrador.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

[ApiController]
[Route("[controller]")]
public class CarrinhoController : Controller
{
    private readonly AppDbContext db;

    public CarrinhoController(AppDbContext db)
    {
        this.db = db;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult> Post([FromBody] Produto produto)
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
            var novoCarrinho = new Carrinho
            {
                UsuarioId = usuario.Id,
                ProdutoId = produto.Id,
                Quantidade = 1,

            };

            db.Add(novoCarrinho);
            await db.SaveChangesAsync();//INSERT INTO
            return Ok();
        }
        catch (Exception e)
        {
            return View(e);
        }
    }

    [HttpGet]
    [Route("GetId/{id}")]
    public IActionResult Get([FromQuery] int id)
    {
        var carrinho = db.Carrinhos.Include("Produtos").ToList();
        return Ok(carrinho);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> Put([FromBody] Carrinho carrinho)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        db.Entry(carrinho).State = EntityState.Modified;
        try
        {
            await db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw (ex);
        }
        return NoContent();
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<ActionResult<Carrinho>> Delete(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var carrinho = await db.Carrinhos.SingleAsync(carrinho => carrinho.ProdutoId == id && carrinho.UsuarioId == 1);
        if (carrinho == null)
        {
            return NotFound();
        }

        db.Carrinhos.Remove(carrinho);
        await db.SaveChangesAsync();
        return Ok(carrinho);
    }

}