import axios from 'axios';

// aqui configuramos la base de nuestra comunicacion.
const api = axios.create({
    baseURL: 'http://localhost:5050/api', // ahi que verificar que este sea el puesto de swagger
    headers: {
        'Content-Type': 'application/json'
    }
});

// funcion para obtener todos los productos
export const getProductos = async () => {
    try { 
        const response = await api.get('/productos');
        return response.data;
    } catch (error) {
        console.error("Error al conectar con la API:", error);
        throw error;
    }
};

export const crearProducto = async (nuevoProducto) => {
    try {
        const response = await api.post('/productos', nuevoProducto);
        return response.data;
    } catch (error) {
        console.error("Error al crear el producto:", error);
        throw error;
    }
};
// hacer_publico Constante Nombre_mensajero = async (dato) => {
export const eliminarProducto = async (id) => {
    try { // intenta hacer esto
        // espera a que la api elimine el producto con esta dirreccion especifica y el id del producto
        await api.delete(`/productos/${id}`);
    }
    catch (error){ // Si hay un error, atrapalo y haz esto
        console.error("hubo un problema al eliminar el producto:", error);// muestra el error en la consola para que el desarrollador pueda ver que paso
        throw error; // avisa al que llamo esta funcion que hubo un error para que pueda manejarlo
    }
    
};

export const actualizarProducto = async (id, productoEditado) => {
    try {
        await api.put(`/productos/${id}`, productoEditado);
    } catch (error) {
        console.error("Error al actualizar:", error);
        throw error;
    }
};

export default api;