namespace GestionInventarioAPI.Models
{
    public class Producto
    {
        // El ID es vital ara identificar cada registro de forma unica
        public int Id { get; set; }
        // El nombre del producto es esencial para su identificación y descripción
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}
