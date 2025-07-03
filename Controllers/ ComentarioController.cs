using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroservicioDemo2.Data;
using MicroservicioDemo2.Models;

namespace MicroservicioDemo2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComentarioController : ControllerBase
    {
        private readonly CalificacionContext _context;

        public ComentarioController(CalificacionContext context)
        {
            _context = context;
        }

        // GET: api/comentario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetComentarios()
        {
            return await _context.Comentarios.ToListAsync();
        }

        // POST: api/comentario
        [HttpPost]
        public async Task<ActionResult<Comentario>> PostComentario([FromBody] Comentario comentario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            comentario.Fecha = DateTime.UtcNow;

            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();

            // Cambiado de 201 Created a 200 OK
            return Ok(comentario);
        }

        // GET: api/comentario/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Comentario>> GetComentarioById(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario == null)
                return NotFound();

            return Ok(comentario);
        }

        // PUT: api/comentario/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComentario(int id, [FromBody] Comentario comentario)
        {
            if (id != comentario.Id)
                return BadRequest("El ID no coincide.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            comentario.Fecha = DateTime.UtcNow;

            _context.Entry(comentario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Comentarios.Any(e => e.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/comentario/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComentario(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario == null)
                return NotFound();

            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
