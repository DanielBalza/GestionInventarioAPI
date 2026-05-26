import React from 'react';

export const TablaProductos = ({ productos, onEliminar, onEditar }) => {
    return (
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
                {/* Si no hay productos, mostramos un mensaje amigable en la tabla */}
                {productos.length === 0 ? (
                    <tr>
                        <td colSpan="4" style={{ padding: '20px', textAlign: 'center', color: '#888' }}>
                            No hay productos registrados en el inventario.
                        </td>
                    </tr>
                ) : (
                    productos.map(p => (
                        <tr key={p.id} style={{ borderBottom: '1px solid #ddd' }}>
                            <td style={{ padding: '12px' }}>{p.id}</td>
                            <td style={{ padding: '12px' }}>{p.nombre}</td>
                            <td style={{ padding: '12px' }}>${p.precio}</td>
                            <td style={{ padding: '12px', textAlign: 'center' }}>
                                <button 
                                    onClick={() => onEliminar(p.id)} 
                                    style={{ background: '#dc3545', color: 'white', border: 'none', padding: '5px 10px', borderRadius: '4px', cursor: 'pointer', marginRight: '5px' }}
                                >
                                    Eliminar
                                </button>
                                <button 
                                    onClick={() => onEditar(p)} 
                                    style={{ background: '#007bff', color: 'white', border: 'none', padding: '5px 10px', borderRadius: '4px', cursor: 'pointer' }}
                                >
                                    ✏️ Editar
                                </button>
                            </td>
                        </tr>
                    ))
                )}
            </tbody>
        </table>
    )
}