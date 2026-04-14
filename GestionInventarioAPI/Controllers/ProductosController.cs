using Microsoft.AspNetCore.Mvc;
using GestionInventarioAPI.Models; // Importamos nuestro modelo de producto

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionInventarioAPI.Controllers
{
    [Route("api/[controller]")] //La ruta sera: api/productos
    [ApiController]
    public class ProductosController : ControllerBase
    {
        //creamos una lista estatica para simular una base de datos por ahora
        private static List<Producto> productos = new List<Producto>
        {
            new Producto { Id = 1, Nombre = "Laptop Gamer", Precio = 1200, Stock = 5 },
            new Producto { Id = 2, Nombre = "mause Óptico", Precio = 25, Stock = 50 }
        };

        /// <summary>
        ///    obtiene la lista completa de productos
        /// </summary>
        // GET: api/<ProductosController>
        [HttpGet]
        public ActionResult<IEnumerable<Producto>> Get()
        {
            return Ok(productos); // Devuelve la lista de productos con un código 200 OK
        }

        /// <summary>
        /// Obtiene un producto especifico por su ID. Si el producto no existe, devuelve un código 404 Not Found.
        /// </summary>

        // GET api/<ProductosController>/5
        [HttpGet("{id}")]
        public ActionResult<Producto> Get (int id)
        {
            var producto = productos.FirstOrDefault(p => p.Id == id); // Busca el producto por ID

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
        public ActionResult<Producto> Post([FromBody] Producto nuevoProducto)
        {
            // Generamos un ID simple basado en el ultimo
            nuevoProducto.Id = productos.Max(p => p.Id) + 1;

            productos.Add(nuevoProducto);

            // Retornar el recurso creado y la ruta donde encontrarlo (status 201)
            return CreatedAtAction(nameof(Get), new { id = nuevoProducto.Id }, nuevoProducto);
        }
    }
}
