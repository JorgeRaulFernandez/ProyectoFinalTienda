using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoTienda.ADO.NET;

namespace ProyectoTienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        [HttpPost("{idUsuario}")]
        public void CargarVenta(long idUsuario, List<Producto> nuevosProductosVendidos)
        {
            VentaHandler.CargarVenta(idUsuario, nuevosProductosVendidos);
        }
        [HttpGet("{idUsuario}")]
        public List<Venta> TraerVentas(long idUsuario)
        {
            return ADO.NET.VentaHandler.TraerVentas(idUsuario);
        }
    } 
}
