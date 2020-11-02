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
    public async Task<ActionResult<Pedido>> Post([FromBody] List<Carrinho> carrinho)
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
        int idNotaFiscal = 0;
        try
        {
            var newNotaFiscal = new NotaFiscal
            {
                cpf_comprador = usuario.Cpf,
                data = DateTime.Now,
            };
            db.Add(newNotaFiscal);
            await db.SaveChangesAsync();
            idNotaFiscal = newNotaFiscal.Id;

        }
        catch (Exception e)
        {
            return View(e);
        }

        decimal subTotal = 0;
        int usuarioId = 1;
        foreach (var item in carrinho)
        {
            subTotal = subTotal + (item.Quantidade * item.Produtos.Preco);
            usuarioId = item.UsuarioId;
        }

        decimal total = -5 + 20 + subTotal;
        try
        {
            var newPedido = new Pedido
            {
                NotaFiscalId = idNotaFiscal,
                UsuarioId = 1,
                data = DateTime.Now,
                total = total,
            };

            db.Add(newPedido);
            await db.SaveChangesAsync();
            foreach (var item in carrinho)
            {
                try
                {
                    var newProdutoPedido = new ProdutoPedido
                    {
                        preco_unitario_produto = item.Produtos.Preco,
                        preco_total_produto = item.Quantidade * item.Produtos.Preco,
                        quantidade_produto = item.Quantidade,
                        ProdutoId = item.Produtos.Id,
                        PedidoId = newPedido.Id,
                    };

                    db.Add(newProdutoPedido);
                    await db.SaveChangesAsync();
                    item.Produtos.Quantidade -= item.Quantidade;
                    db.Entry(item.Produtos).State = EntityState.Modified;
                    try
                    {
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        throw (ex);
                    }
                }
                catch (Exception e)
                {
                    return View(e);
                }

                db.Carrinhos.Remove(item);
                await db.SaveChangesAsync();
            }
            return Ok();
        }
        catch (Exception e)
        {
            return View(e);
        }
    }
}