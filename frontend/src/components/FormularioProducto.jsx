import { useState, useEffect } from 'react';

export const FormularioProducto = ({ onSubmit, editando, productoAEditar}) => {
    const [nombre, setNombre] = useState('');
    const [precio, setPrecio] = useState(0);

    useEffect(() => {
        if (editando && productoAEditar) {
            setNombre(productoAEditar.nombre);
            setPrecio(productoAEditar.precio);
        } else {
            setNombre('');
            setPrecio(0);
        }
    }, [editando, productoAEditar]);

    const alEnviar = (e) => {
        e.preventDefault();
        onSubmit({ nombre, precio: parseFloat(precio) });

    };

    return (
        <div style={{ background: '#f8f9fa', padding: '20px', borderRadius: '8px', marginBottom: '20px' }}>
            <h3 style={{ color: '#6c757d', textAlign: 'center' }}>
                {editando ? 'Modificar Producto' : 'Registrar Nuevo Producto'}
            </h3>
            
            <form onSubmit={alEnviar} style={{ display: 'flex', gap: '10px', justifyContent: 'center' }}>
                <input 
                    type="text" 
                    placeholder="Nombre" 
                    value={nombre} 
                    onChange={(e) => setNombre(e.target.value)}
                    required
                    style={{ padding: '8px', background: '#333', color: 'white', border: 'none', borderRadius: '4px' }}
                />
                <input 
                    type="number" 
                    placeholder="Precio" 
                    value={precio} 
                    onChange={(e) => setPrecio(e.target.value)}
                    required
                    style={{ padding: '8px', background: '#333', color: 'white', border: 'none', borderRadius: '4px' }}
                />
                <button 
                    type="submit" 
                    style={{ 
                        padding: '8px 20px', 
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
    );
}