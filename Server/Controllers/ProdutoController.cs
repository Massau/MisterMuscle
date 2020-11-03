using ProjetoIntegrador.Server;
using Microsoft.AspNetCore.Mvc;
using ProjetoIntegrador.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

[ApiController]
[Route("[controller]")]
public class ProdutoController : Controller
{
    private readonly AppDbContext db;

    public ProdutoController(AppDbContext db)
    {
        this.db = db;
    }

    [HttpGet]
    [Route("List")]
    public async Task<IActionResult> Get()
    {
        var produtos = await db.Produtos.ToListAsync();
        return Ok(produtos);
        
    }

    

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult> Post([FromBody] ProdutoDto produto)
    {
        try
        {
            
            var novoProduto = new Produto
            {
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                ImagemProduto = produto.ImagemProduto,
                Quantidade = produto.Quantidade,
                FornecedorId = Convert.ToInt32(produto.FornecedorId),
                CategoriaId = Convert.ToInt32(produto.CategoriaId),
                
                
                  
            };

            db.Add(novoProduto);
            await db.SaveChangesAsync();
            var pegaId = novoProduto.Id;
            

            // salvar transação de cadastro de produto no estoque (entrada)
            var novoEstoque = new Estoque
            {
                tipo_transacao = "entrada",
                Quantidade = novoProduto.Quantidade,
                ProdutoId = pegaId
            };

            db.Add(novoEstoque);
            await db.SaveChangesAsync();

            
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
        // procura no banco na tabela products se tem algum Id igual e retorna o produto com todas suas informações
        var produto = await db.Produtos.SingleOrDefaultAsync(p => p.Id == Convert.ToInt32(id));
        var conversaoForm = new ProdutoDto()
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco,
            ImagemProduto = produto.ImagemProduto,
            Quantidade = produto.Quantidade,
            FornecedorId = Convert.ToString(produto.FornecedorId),
            CategoriaId = Convert.ToString(produto.CategoriaId)

        };

        return Ok(conversaoForm);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> Put([FromBody] ProdutoDto produto)
    {
        // if (!ModelState.IsValid){

        //     return BadRequest(ModelState);

        // }
        var produtoCerto = new Produto()
        {       Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                ImagemProduto = produto.ImagemProduto,
                Quantidade = produto.Quantidade,
                FornecedorId = Convert.ToInt32(produto.FornecedorId),
                CategoriaId = Convert.ToInt32(produto.CategoriaId),
            
        };

        db.Entry(produtoCerto).State = EntityState.Modified;
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
    public async Task<ActionResult <Usuario>> Delete(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var produto = await db.Produtos.FindAsync(id);
        if(produto == null)
        {
            return NotFound();
        }

        db.Produtos.Remove(produto);
        await db.SaveChangesAsync();
        return Ok(produto);
    }

}