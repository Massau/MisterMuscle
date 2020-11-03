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
public class NotaFiscalController : Controller
{
    private readonly AppDbContext db;

    public NotaFiscalController(AppDbContext db)
    {
        this.db = db;
    }

    [HttpGet]
    [Route("List")]
    public async Task<IActionResult> Get()
    {
        var notas = await db.NotaFiscais.ToListAsync();
        return Ok(notas);
    }

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult<NotaFiscal>> Post([FromBody] NotaFiscal notaFiscal)
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
            var newNotaFiscal = new NotaFiscal
            {
                cpf_comprador = usuario.Cpf,
                data = DateTime.Now,
                numeroCartao = notaFiscal.numeroCartao,
                dataVencimento = notaFiscal.dataVencimento,
                cvv = notaFiscal.cvv,
                nome = notaFiscal.nome,
            };
            db.Add(newNotaFiscal);
            await db.SaveChangesAsync();
            return Ok(newNotaFiscal);
        }
        catch (Exception e)
        {
            return View(e);
        }
    }
}