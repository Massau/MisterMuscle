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

    [HttpGet]
    [Route("List")]
    public async Task<IActionResult> Get()
    {
        var carrinhos = await db.Carrinhos.ToListAsync();
        return Ok(carrinhos);

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
    [Route("GetId")]
    public async Task<IActionResult> Get([FromQuery] string id)
    {
        var carrinho = await db.Carrinhos.SingleOrDefaultAsync(carrinho => carrinho.UsuarioId == Convert.ToInt32(id));
        return Ok(carrinho);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<ActionResult<Carrinho>> Delete(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var carrinho = await db.Carrinhos.FindAsync(id);
        if (carrinho == null)
        {
            return NotFound();
        }

        db.Carrinhos.Remove(carrinho);
        await db.SaveChangesAsync();
        return Ok(carrinho);
    }

}