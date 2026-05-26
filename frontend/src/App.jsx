import './App.css'
import { FormularioProducto } from './components/FormularioProducto';
import { useEffect, useState } from 'react'
import { getProductos, crearProducto, eliminarProducto, actualizarProducto } from './services/api'
import { TablaProductos } from './components/TablaProductos';

function App() {
  const [productos, setProductos] = useState([]);
  const [editando, setEditando] = useState(false); // estado para controlar si estamos editando o creando un producto
  const [idEditar, setIdEditar] = useState(null);// estado para guardar el id del producto que se esta editando, si es null significa que no estamos editando ningun producto

  useEffect(() => {
    cargarProductos();
  }, []);

  const cargarProductos = async () => {
    const datos = await getProductos();
    setProductos(datos);
  };

  const handleGuardarProducto = async (datosFormulario) => {
    // 1. Armamos el objeto con la estructura exacta que tu ProductoDTO de C# necesita recibir
  const productoParaEnviar = {
    Nombre: datosFormulario.nombre,
    Precio: parseFloat(datosFormulario.precio),
    Stock: 10,        // Valor por defecto para evitar errores de validación
    CategoriaId: 1    // ⚠️ Reemplaza este 1 por un ID que sí exista en tu tabla de Categorías
  };

    if (editando) {
      // Modo Edición: Combinamos el ID que teníamos guardado con los nuevos datos
      await actualizarProducto(idEditar, { ...productoParaEnviar, id: idEditar, Id: idEditar }); // Asegúrate de enviar el ID correcto según tu API
      setEditando(false);
      setIdEditar(null);
    } else {
      // Modo Creación: Mandamos los datos limpios a la API
      await crearProducto(productoParaEnviar);
    }

    // Recargamos la tabla de inmediato
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
    setIdEditar(producto.id); // Guardamos el ID para que el formulario sepa a quién buscar
  };

  return (
    <div style={{ padding: '20px', maxWidth: '800px', margin: '0 auto', fontFamily: 'Arial' }}>
      <h1 style={{ textAlign: 'center' }}>Gestión de Inventario</h1>

      {/* Sección del Formulario */}
      <div style={{ background: '#f4f4f4', padding: '20px', borderRadius: '8px', marginBottom: '30px' }}>
        {/* ✅ Reemplaza todo tu formulario viejo por esto */}
        <FormularioProducto
          onSubmit={handleGuardarProducto}
          editando={editando}
          productoAEditar={productos.find(p => p.id === idEditar)}
        />
      </div>

      {/* Sección de la Tabla */}
      <TablaProductos
        productos={productos}
        onEliminar={handleEliminar}
        onEditar={prepararEdicion}
      />

    </div>
  );
};
export default App;