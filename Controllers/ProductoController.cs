using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroservicioDemo2.Data;
using MicroservicioDemo2.Models;

namespace MicroservicioDemo2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoContext dBConexion;

        public ProductoController(ProductoContext dBConexion)
        {
            this.dBConexion = dBConexion;
        }

        // GET: api/Producto/Listar
        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            var productos = await dBConexion.Productos.ToListAsync();
            return Ok(productos);
        }

        // GET: api/Producto/Buscar/5
        [HttpGet("Buscar/{id:int}")]
        public async Task<ActionResult<Producto>> GetProductoById(int id)
        {
            var producto = await dBConexion.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return producto;
        }

        // POST: api/Producto
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto product)
        {
            dBConexion.Productos.Add(product);
            await dBConexion.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductoById), new { id = product.Id }, product);
        }
    }
}
