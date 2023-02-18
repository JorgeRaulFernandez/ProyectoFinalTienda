using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoTienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet("{idUsuario}")]
        public List<ProductoVendido> TraerProductosVendidos(long idUsuario)
        {
            return ADO.NET.ProductoVendidoHandler.TraerProductosVendidos(idUsuario);
        }
    }
}
