using Microsoft.AspNetCore.Mvc;
using GestionInventarioAPI.Models;
using GestionInventarioAPI.Data;
using Microsoft.EntityFrameworkCore;// Importamos nuestro modelo de categoria

namespace GestionInventarioAPI.Controllers
{
    [Route("api/[controller]")] //La ruta sera: api/categorias
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ApplicationDbContext _context; // Inyectamos el contexto de la base de datos
        // El constructor recibe el contexto de la base de datos
        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<CategoriasController>
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            return _context.Categorias.ToList(); // Obtenemos todas las categorias de la base de datos
        }
        // GET api/<CategoriasController>/5
        [HttpGet("{id}")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _context.Categorias.Find(id); // Busca la categoria por ID
            if (categoria == null)
            {
                return NotFound(); // Buena practica: si no existe, avisamos al usuario (Error 404)
            }
            return Ok(categoria);
        }
        // POST api/<CategoriasController>
        [HttpPost]
        public ActionResult<Categoria> Post([FromBody] Categoria nuevaCategoria)
        {
            _context.Categorias.Add(nuevaCategoria); // Agregamos la nueva categoria a la base de datos
            _context.SaveChanges(); // Guardamos los cambios y se guarda fisicamente en Inventario.db
            return CreatedAtAction(nameof(Get), new { id = nuevaCategoria.Id }, nuevaCategoria); // Retornar el recurso creado y la ruta donde encontrarlo (status 201)
        }
    }
}