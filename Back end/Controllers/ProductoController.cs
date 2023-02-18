using ProyectoTienda.ADO.NET;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoTienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpGet("/producto/{descripcion}")] //Si lo que viene es del metodo get con el template indicado, indico que se gestione con el siguiente metodo:
        public List<Producto> ObtenerProductoByDescripciones(string descripcion)
        {
            return ADO.NET.ProductoHandler.TraerProductosPorDescripcion(descripcion);
        }
        [HttpGet("/productos")]
        public List<Producto> ObtenerProductos()
        {
            return ADO.NET.ProductoHandler.TraerTodosProductos();
        }
        [HttpGet("{idUsuario}")]
        public List<Producto> TraerProductos(long idUsuario)
        {
            return ADO.NET.ProductoHandler.TraerProductos(idUsuario);
        }
        [HttpPost]
        public void CrearProducto(Producto productoNuevo)
        {
            ProductoHandler.CrearProducto(productoNuevo);
        }
        [HttpPut]
        public void ModificarProducto(Producto productoModificado)
        {
            ProductoHandler.ModificarProducto(productoModificado);
        }
        [HttpDelete("{id}")]
        public void EliminarProducto(long id) {
            ProductoHandler.EliminarProducto(id);
        }
    }
}
