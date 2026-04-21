namespace GestionInventarioAPI.DTOs
{
    public class ProductoDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int CategoriaId { get; set; } // Solo necesitamos el ID de la categoria
    }
}
