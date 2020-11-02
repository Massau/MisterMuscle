using ProjetoIntegrador.Server;
using Microsoft.AspNetCore.Mvc;
using ProjetoIntegrador.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;

[ApiController]
[Route("[controller]")]
public class UsuarioController : Controller
{
    private readonly AppDbContext db;

    public UsuarioController(AppDbContext db)
    {
        this.db = db;
    }

    [HttpGet]
    [Route("List")]
    public async Task<IActionResult> Get()
    {
        var usuarios = await db.Usuarios.ToListAsync();
        return Ok(usuarios);
    }

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult> Post([FromBody] Usuario usuario)
    {
        try
        {
            var newUsuario = new Usuario
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Cpf = usuario.Cpf,
                Senha = usuario.Senha,
                Celular = usuario.Celular,
                Confirmarsenha = usuario.Confirmarsenha,
            };

            db.Add(newUsuario);
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
        var usuario = await db.Usuarios.SingleOrDefaultAsync(u => u.Id == Convert.ToInt32(id));

        var conversaoForm = new UsuarioDto()
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Cpf = usuario.Cpf,
            Celular = usuario.Celular,
            Senha = usuario.Senha
        };

        return Ok(conversaoForm);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> Put([FromBody] UsuarioDto usuario)
    {

        var usuarioCerto = new Usuario()
        {       
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Cpf = usuario.Cpf,
            Celular = usuario.Celular,
            Senha = usuario.Senha            
        };

        db.Entry(usuarioCerto).State = EntityState.Modified;
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

        var usuario = await db.Usuarios.FindAsync(id);
        if(usuario == null)
        {
            return NotFound();
        }

        db.Usuarios.Remove(usuario);
        await db.SaveChangesAsync();
        return Ok(usuario);
    }
}