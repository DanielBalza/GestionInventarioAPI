using Microsoft.AspNetCore.Mvc;
using GestionInventarioAPI.Models;
using System.Diagnostics.CodeAnalysis;
using GestionInventarioAPI.Data;
using Microsoft.EntityFrameworkCore;
using GestionInventarioAPI.DTOs;// Importamos nuestro modelo de producto

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionInventarioAPI.Controllers
{
    [Route("api/[controller]")] //La ruta sera: api/productos
    [ApiController]
    public class ProductosController : ControllerBase
    {

        // El constructor recibe el contexto de la base de datos
        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        // A Partir de aqui usaremos _context.productos para todo
        /// </summary>


        // GET: api/<ProductosController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> Get()
        {
            return await _context.Productos.Include(p => p.Categoria).ToListAsync(); // Obtenemos todos los productos de la base de datos de forma asincrona
        }

        /// <summary>
        /// Obtiene un producto especifico por su ID. Si el producto no existe, devuelve un código 404 Not Found.
        /// </summary>

        // GET api/<ProductosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> Get(int id)
        {
            var producto = await _context.Productos.FindAsync(id); // Busca el producto por ID de forma asincrona

            if (producto == null)
            {
                return NotFound(); // Buena practica: si no existe, avisamos al usuario (Error 404)
            }
            return Ok(producto);
        }

        /// <summary>
        /// crea un nuevo producto
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Producto>> Post([FromBody] ProductoDTO ProductoDto)
        {
            // Esta linea es le "guardia de seguridad"
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // si el modelo no es valido, devolvemos un error 400 Bad Request con los detalles del error
            }

             //Convertimos el DTO a una entidad Producto
            var nuevoProducto = new Producto
            {
                Nombre = ProductoDto.Nombre,
                Precio = ProductoDto.Precio,
                Stock = ProductoDto.Stock,
                CategoriaId = ProductoDto.CategoriaId
            };
            _context.Productos.Add(nuevoProducto);
            await _context.SaveChangesAsync(); // Guardamos los cambios de forma asincrona

            return CreatedAtAction(nameof(Get), new { id = nuevoProducto.Id }, nuevoProducto);
        }

        [HttpPut("{id}")]
        public async  Task<IActionResult> Put(int id, [FromBody] Producto productoActualizado)
        {
            var productoDB = await _context.Productos.FindAsync(id); // Busca el producto por ID de forma asincrona
            if (productoDB == null) return NotFound(); // retorna un código 404 Not Found si el producto no existe, para avisar al usuario

            // Actualizamos las propiedades del producto existente
            productoDB.Nombre = productoActualizado.Nombre;
            productoDB.Precio = productoActualizado.Precio;
            productoDB.Stock = productoActualizado.Stock;
            await _context.SaveChangesAsync(); // Guardamos los cambios de forma asincrona
            return NoContent(); // Retorna un código 204 No Content para indicar que la actualización fue exitosa
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        { 
            var productoDB = await _context.Productos.FindAsync(id); // Busca el producto por ID de forma asincrona
            if (productoDB == null) return NotFound(); // Si no existe, avisamos al usuario (Error 404)

            _context.Productos.Remove(productoDB);
            await _context.SaveChangesAsync(); // Guardamos los cambios de forma asincrona
            return NoContent(); // Retorna un código 204 No Content para indicar que la eliminación fue exitosa
        }

    }
}
