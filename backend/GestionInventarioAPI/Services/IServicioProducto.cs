using GestionInventarioAPI.DTOs;
using GestionInventarioAPI.Models;

namespace GestionInventarioAPI.Services
{
    public interface IServicioProducto
    {
        Task<IEnumerable<Producto>> ObtenerTodosAsync();
        Task<Producto?> ObtenerPorIdAsync(int id);
        Task<Producto> CrearProductoAsync(ProductoDTO productoDto);
        Task ActualizarProductoAsync(int id, ProductoDTO productoDto);
        Task EliminarProductoAsync(int id);

    }
}
