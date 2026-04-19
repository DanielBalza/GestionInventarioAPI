using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestionInventarioAPI.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoria es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        // Propiedad de navegacion: una categoria tiene una Lista de productos
        [JsonIgnore] // esto evita un error de "Bucle infinito" al monstrar los datos en swagger
        public List<Producto> Productos { get; set; } = new List<Producto>();

    }
}
