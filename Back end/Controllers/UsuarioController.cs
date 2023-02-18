using ProyectoTienda.ADO.NET;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoTienda;

namespace ProyectoTienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet("{nombreUsuario}/{contrasena}")]
        public Usuario Login(string nombreUsuario, string contrasena) {
            return UsuarioHandler.Login(nombreUsuario, contrasena);
        }
        [HttpGet("{nombreUsuario}")]
        public Usuario TraerUsuario(string nombreUsuario)
        {
            return UsuarioHandler.TraerUsuario(nombreUsuario);
        }
        [HttpPost]
        public void CrearUsuario(Usuario usuarioNuevo)
        {
            UsuarioHandler.CrearUsuario(usuarioNuevo);
        }
        [HttpPut]
        public void ModificarUsuario(Usuario usuarioModificado)
        {
            UsuarioHandler.ModificarUsuario(usuarioModificado);
        }
        [HttpDelete("{idUsuario}")]
        public void EliminarUsuario(long idUsuario)
        {
            UsuarioHandler.EliminarUsuario(idUsuario);
        }

    }
}
