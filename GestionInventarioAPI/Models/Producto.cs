using System.ComponentModel.DataAnnotations; // Necesario para las validaciones de datos

namespace GestionInventarioAPI.Models

{
    public class Producto
    {
        // El ID es vital ara identificar cada registro de forma unica
        public int Id { get; set; }
        // El nombre del producto es esencial para su identificación y descripción
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Range(0.01, 10000, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Range(0, 1000, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; }


        // clave foranea para relacionar el producto con su categoria
        public int CategoriaId { get; set; }

        // propiedad de navegacion para acceder a los datos de la categoria a la que pertenece el producto
        public Categoria? Categoria { get; set; }
    }
}
