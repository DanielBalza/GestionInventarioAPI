using System.ComponentModel.DataAnnotations;

namespace GestionInventarioAPI.DTOs
{
    public class ProductoDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Range(0.01, 1000000, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }
        [Range(0, 10000, ErrorMessage = "El stock debe ser un valor positivo")]
        public int Stock { get; set; }
        public int CategoriaId { get; set; } // Solo necesitamos el ID de la categoria
    }
}
