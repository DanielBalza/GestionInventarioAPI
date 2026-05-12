using Microsoft.AspNetCore.Mvc;
using GestionInventarioAPI.Models;
using System.Diagnostics.CodeAnalysis;
using GestionInventarioAPI.Data;
using Microsoft.EntityFrameworkCore;
using GestionInventarioAPI.DTOs;
using GestionInventarioAPI.Repositories;
using GestionInventarioAPI.Services;// Importamos nuestro modelo de producto

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionInventarioAPI.Controllers
{
    [Route("api/[controller]")] //La ruta sera: api/productos
    [ApiController]
    public class ProductosController : ControllerBase
    {


        private readonly IServicioProducto _repository; // Inyectamos el contexto de la base de datos

        // El constructor recibe el contexto de la base de datos
        public ProductosController(IServicioProducto servicioProducto)
        {
            _repository = servicioProducto;
        }
        /// <summary>
        // A Partir de aqui usaremos _context.productos para todo
        /// </summary>


        // GET: api/<ProductosController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> Get()
        {
            var productos = await _repository.ObtenerTodosAsync();
            return Ok(productos);// Obtenemos todos los productos de la base de datos de forma asincrona
        }

        /// <summary>
        /// Obtiene un producto especifico por su ID. Si el producto no existe, devuelve un código 404 Not Found.
        /// </summary>

        // GET api/<ProductosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> Get(int id)
        {
            var producto = await _repository.ObtenerPorIdAsync(id); // Busca el producto por ID de forma asincrona

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
            await _repository.CrearProductoAsync(ProductoDto);

            return CreatedAtAction(nameof(Get), new { id = nuevoProducto.Id }, nuevoProducto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductoDTO productoActualizado)
        {
            var productoDB = await _repository.ObtenerPorIdAsync(id); // Busca el producto por ID de forma asincrona
            if (productoDB == null) return NotFound(); // retorna un código 404 Not Found si el producto no existe, para avisar al usuario

            // Actualizamos las propiedades del producto existente
            productoDB.Nombre = productoActualizado.Nombre;
            productoDB.Precio = productoActualizado.Precio;
            productoDB.Stock = productoActualizado.Stock;
            await _repository.ActualizarProductoAsync(id,productoActualizado); // Guardamos los cambios de forma asincrona
            return NoContent(); // Retorna un código 204 No Content para indicar que la actualización fue exitosa
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var productoDB = await _repository.ObtenerPorIdAsync(id); // Busca el producto por ID de forma asincrona
            if (productoDB == null) return NotFound(); // Si no existe, avisamos al usuario (Error 404)
            
            await _repository.EliminarProductoAsync(id); // Eliminamos el producto de forma asincrona
            return NoContent(); // Retorna un código 204 No Content para indicar que la eliminación fue exitosa
        }

    }
}
