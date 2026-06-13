import { useState, useEffect } from 'react'
import './App.css'

function App() {
  const [productos, setProductos] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(null)

  useEffect(() => {
    const fetchProductos = async () => {
      try {
        // Asumiendo que la API corre en localhost:5001 (HTTPS)
        // Se usa la URL con los parámetros de paginación
        const response = await fetch('https://localhost:5001/api/productos?page=1&pageSize=20')
        if (!response.ok) {
          throw new Error('Error al cargar los productos')
        }
        const data = await response.json()
        setProductos(data)
      } catch (err) {
        setError(err.message)
      } finally {
        setLoading(false)
      }
    }

    fetchProductos()
  }, [])

  if (loading) return <div className="loading">Cargando productos...</div>
  if (error) return <div className="error">Error: {error}</div>

  return (
    <div className="App">
      <header className="App-header">
        <h1>NikiShop Ecommerce - React</h1>
      </header>
      <main>
        <h2>Lista de Productos</h2>
        <table className="product-table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Nombre</th>
              <th>Precio</th>
              <th>Stock</th>
            </tr>
          </thead>
          <tbody>
            {productos.map((producto) => (
              <tr key={producto.idProducto}>
                <td>{producto.idProducto}</td>
                <td>{producto.nombre}</td>
                <td>${producto.precioVenta.toLocaleString()}</td>
                <td>{producto.stockActual}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </main>
    </div>
  )
}

export default App
