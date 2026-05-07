using GestionInventarioAPI.DTOs;
using GestionInventarioAPI.Models;
using GestionInventarioAPI.Repositories;

namespace GestionInventarioAPI.Services
{
    public class ServicioProducto : IServicioProducto
    {
        private readonly IProductoRepository _repo;

        public ServicioProducto(IProductoRepository repo) { _repo = repo; }

        public async Task<IEnumerable<Producto>> ObtenerTodosAsync() => await _repo.GetAllAsync();

        public async Task<Producto?> ObtenerPorIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<Producto> CrearProductoAsync(ProductoDTO dto)
        {
            var nuevoProducto = new Producto { Nombre = dto.Nombre, Precio = dto.Precio, Stock = dto.Stock, CategoriaId = dto.CategoriaId };
            await _repo.AddAsync(nuevoProducto);
            await _repo.SaveChangesAsync(); // Guardar los cambios después de agregar el nuevo producto
            return nuevoProducto;
        }

        // Aqui se agrega Logicas Extras para actualizar un producto o una alerta
        public async Task ActualizarProductoAsync(int id, ProductoDTO dto)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p != null)
            {
                p.Nombre = dto.Nombre; p.Precio = dto.Precio; p.Stock = dto.Stock;
                await _repo.UpdateAsync(p);
                await _repo.SaveChangesAsync(); // Guardar los cambios después de actualizar el producto
            }
        }

        public async Task EliminarProductoAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p != null)
            {
                await _repo.DeleteAsync(p);
                await _repo.SaveChangesAsync(); // Guardar los cambios después de eliminar el producto
            }

        }

    }
}
