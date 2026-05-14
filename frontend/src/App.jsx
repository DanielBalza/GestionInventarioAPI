import './App.css'
import { useEffect, useState } from 'react'
import { getProductos, crearProducto, eliminarProducto, actualizarProducto } from './services/api'

function App() {
  const [productos, setProductos] = useState([]);
  const [nombre, setNombre] = useState(['']);
  const [precio, setPrecio] = useState([0]);
  const [editando, setEditando] = useState(false); // estado para controlar si estamos editando o creando un producto
  const [idEditar, setIdEditar] = useState(null);// estado para guardar el id del producto que se esta editando, si es null significa que no estamos editando ningun producto

  useEffect(() => {
    cargarProductos();
  }, []);

  const cargarProductos = async () => {
    const datos = await getProductos();
    setProductos(datos);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const objetoProducto = { nombre, precio: parseFloat(precio), stock: 10, categoriaId: 1 }; // datos temporales

    if (editando) {
      // logica para actualizar
      await actualizarProducto(idEditar, { ...objetoProducto, id: idEditar });
      setEditando(false);
      setIdEditar(null);
    } else {
      // logica para crear
      await crearProducto(objetoProducto);
    }

    // Limpiar y refrescar la lista
    setNombre('');
    setPrecio(0);
    cargarProductos();
  };

  const handleEliminar = async (id) => {
    if (window.confirm("deseas eliminar este producto?")) { // pregunta de confirmacion para evitar eliminaciones accidentales
      await eliminarProducto(id); // llama a la funcion que elimina el producto por id
      cargarProductos(); // refresca la tabla para ver el cambio
    }
  };

  const prepararEdicion = (producto) => {
    setEditando(true);
    setIdEditar(producto.id);
    setNombre(producto.nombre);
    setPrecio(producto.precio);
  };

  return (
    <div style={{ padding: '20px', maxWidth: '800px', margin: '0 auto', fontFamily: 'Arial' }}>
      <h1 style={{ textAlign: 'center' }}>Gestión de Inventario</h1>

      {/* Sección del Formulario */}
      <div style={{ background: '#f4f4f4', padding: '20px', borderRadius: '8px', marginBottom: '30px' }}>
        <h3>Registrar Nuevo Producto</h3>
        <form onSubmit={handleSubmit} style={{ display: 'flex', gap: '10px' }}>
          <input
            style={{ padding: '8px', flex: 1 }}
            type="text"
            placeholder="Nombre del producto"
            value={nombre}
            onChange={(e) => setNombre(e.target.value)}
            required
          />
          <input
            style={{ padding: '8px', width: '100px' }}
            type="number"
            placeholder="Precio"
            value={precio}
            onChange={(e) => setPrecio(e.target.value)}
            required
          />
          <button
            type="submit"
            style={{
              padding: '8px 20px',
              // Si edita es amarillo, si guarda es verde
              background: editando ? '#ffc107' : '#28a745',
              color: editando ? 'black' : 'white',
              border: 'none',
              borderRadius: '4px',
              cursor: 'pointer'
            }}
          >
            {editando ? 'Actualizar Producto' : 'Guardar Producto'}
          </button>
        </form>
      </div>

      {/* Sección de la Tabla */}
      <table style={{ width: '100%', borderCollapse: 'collapse', boxShadow: '0 2px 10px rgba(0,0,0,0.1)' }}>
        <thead>
          <tr style={{ background: '#333', color: 'white' }}>
            <th style={{ padding: '12px', textAlign: 'left' }}>ID</th>
            <th style={{ padding: '12px', textAlign: 'left' }}>Nombre</th>
            <th style={{ padding: '12px', textAlign: 'left' }}>Precio</th>
            <th style={{ padding: '12px', textAlign: 'center' }}>Acciones</th>
          </tr>
        </thead>
        <tbody>
          {productos.map(p => (
            <tr key={p.id} style={{ borderBottom: '1px solid #ddd' }}>
              <td style={{ padding: '12px' }}>{p.id}</td>
              <td style={{ padding: '12px' }}>{p.nombre}</td>
              <td style={{ padding: '12px' }}>${p.precio}</td>
              <td style={{ padding: '12px', textAlign: 'center' }}>
                <button onClick={() => handleEliminar(p.id)} style={{ background: '#dc3545', color: 'white', border: 'none', padding: '5px 10px', borderRadius: '4px', cursor: 'pointer' }}>
                  Eliminar
                </button>
                <button onClick={() => prepararEdicion(p)} style={{ background: '#007bff', color: 'white', border: 'none', padding: '5px 10px', borderRadius: '4px', cursor: 'pointer' }}>
                  ✏️ Editar
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
export default App;