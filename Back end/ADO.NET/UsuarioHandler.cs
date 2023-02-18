using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoTienda;

namespace ProyectoTienda.ADO.NET
{
    internal static class UsuarioHandler
    {

        //Planteo la cadena de conexion y la declaro como una variable de clase para no redundar codigo
        public static string cadenaConexion = "Data Source=DESKTOP-37MP24G;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static Usuario Login(string nombreUsuario, string pass)
        {
            Usuario usuarioEncontrado = new Usuario();
            //Realizo la conexion a la BBDD. Utilizo el using para que la conexion se autogestione
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                //Planteo el comando, escribiendo la sentencia SQL y le paso la conexion
                SqlCommand comando = new SqlCommand("SELECT * FROM Usuario WHERE (NombreUsuario =@nombreUsuario AND Contraseña=@pass)", conn);
                //Creo el SQL parameter 1
                SqlParameter nombreUsuarioParametro = new SqlParameter();
                nombreUsuarioParametro.Value = nombreUsuario;
                nombreUsuarioParametro.SqlDbType = SqlDbType.VarChar;
                nombreUsuarioParametro.ParameterName = "nombreUsuario";
                //Creo el SQL parameter 2
                SqlParameter passParametro = new SqlParameter();
                passParametro.Value = pass;
                passParametro.SqlDbType = SqlDbType.VarChar;
                passParametro.ParameterName = "pass";
                //Añado los parametros a mi comando
                comando.Parameters.Add(nombreUsuarioParametro);
                comando.Parameters.Add(passParametro);
                //Abro la conexion
                conn.Open();
                //Uso el objeto SqlDataReader para tomar lo que viene de la consulta. No uso el data adapter dado que serviría para traer todas las filas y resulta innecesario para el caso
                SqlDataReader reader = comando.ExecuteReader();
                //Valido si el objeto reader tiene filas
                if (reader.HasRows)
                {
                    //Creo un usuario temporal para ir sumando en cada iteracion a los usuarios a la lista de usuarios

                    reader.Read();
                    usuarioEncontrado.Id = reader.GetInt64(0);
                    usuarioEncontrado.Nombre = reader.GetString(1);
                    usuarioEncontrado.Apellido = reader.GetString(2);
                    usuarioEncontrado.NombreUsuario = reader.GetString(3);
                    usuarioEncontrado.Contraseña = reader.GetString(4);
                    usuarioEncontrado.Mail = reader.GetString(5);
                    //Retorno mi lista de usuarios
                    return usuarioEncontrado;
                }
                else return null;
            }
        }
        public static int CrearUsuario(Usuario usuario)
        {
            //Realizo la conexion a la BBDD. Utilizo el using para que la conexion se autogestione
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                //Planteo el comando, escribiendo la sentencia SQL y le paso la conexion
                SqlCommand comando = new SqlCommand("INSERT INTO dbo.Usuario (Nombre,Apellido,NombreUsuario,Contraseña,Mail) VALUES (@nombre, @apellido, @nombreUsuario, @pass, @mail)", conn);
                //Creo los SQL parameters
                SqlParameter nombreParam = new SqlParameter();
                nombreParam.Value = usuario.Nombre;
                nombreParam.SqlDbType = SqlDbType.VarChar;
                nombreParam.ParameterName = "nombre";

                SqlParameter apellidoParam = new SqlParameter();
                apellidoParam.Value = usuario.Apellido;
                apellidoParam.SqlDbType = SqlDbType.VarChar;
                apellidoParam.ParameterName = "apellido";

                SqlParameter nombreUsuarioParam = new SqlParameter();
                nombreUsuarioParam.Value = usuario.NombreUsuario;
                nombreUsuarioParam.SqlDbType = SqlDbType.VarChar;
                nombreUsuarioParam.ParameterName = "nombreUsuario";

                SqlParameter passParam = new SqlParameter();
                passParam.Value = usuario.Contraseña;
                passParam.SqlDbType = SqlDbType.VarChar;
                passParam.ParameterName = "pass";

                SqlParameter mailParam = new SqlParameter();
                mailParam.Value = usuario.Mail;
                mailParam.SqlDbType = SqlDbType.VarChar;
                mailParam.ParameterName = "mail";
                //Añado los parametros a mi comando
                comando.Parameters.Add(nombreParam);
                comando.Parameters.Add(apellidoParam);
                comando.Parameters.Add(nombreUsuarioParam);
                comando.Parameters.Add(passParam);
                comando.Parameters.Add(mailParam);
                //Abro la conexion
                conn.Open();
                //El objeto SqlCommand tiene un metodo que nos devuelve la cantidad de filas afectadas
                int cantUsuariosMismoNombreUsuario = ExistenciaUsuario(usuario);
                if (cantUsuariosMismoNombreUsuario == 0)
                {
                    return comando.ExecuteNonQuery();
                }
                return 0;
            }
        }
        public static int ModificarUsuario(Usuario usuario)
        {
            //Realizo la conexion a la BBDD. Utilizo el using para que la conexion se autogestione
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                //Planteo el comando, escribiendo la sentencia SQL y le paso la conexion
                SqlCommand comando = new SqlCommand("UPDATE dbo.Usuario SET Contraseña = @pass , Nombre = @nombre, Apellido = @apellido, NombreUsuario = @nombreUsuario, Mail=@mail WHERE Id = @idUser", conn);
                //Creo los SQL parameters
                SqlParameter nombreParam = new SqlParameter();
                nombreParam.Value = usuario.Nombre;
                nombreParam.SqlDbType = SqlDbType.VarChar;
                nombreParam.ParameterName = "nombre";

                SqlParameter apellidoParam = new SqlParameter();
                apellidoParam.Value = usuario.Apellido;
                apellidoParam.SqlDbType = SqlDbType.VarChar;
                apellidoParam.ParameterName = "apellido";

                SqlParameter nombreUsuarioParam = new SqlParameter();
                nombreUsuarioParam.Value = usuario.NombreUsuario;
                nombreUsuarioParam.SqlDbType = SqlDbType.VarChar;
                nombreUsuarioParam.ParameterName = "nombreUsuario";

                SqlParameter passParam = new SqlParameter();
                passParam.Value = usuario.Contraseña;
                passParam.SqlDbType = SqlDbType.VarChar;
                passParam.ParameterName = "pass";

                SqlParameter mailParam = new SqlParameter();
                mailParam.Value = usuario.Mail;
                mailParam.SqlDbType = SqlDbType.VarChar;
                mailParam.ParameterName = "mail";

                SqlParameter idParam = new SqlParameter();
                idParam.Value = usuario.Id;
                idParam.SqlDbType = SqlDbType.BigInt;
                idParam.ParameterName = "idUser";
                //Añado los parametros a mi comando
                comando.Parameters.Add(nombreParam);
                comando.Parameters.Add(apellidoParam);
                comando.Parameters.Add(nombreUsuarioParam);
                comando.Parameters.Add(passParam);
                comando.Parameters.Add(mailParam);
                comando.Parameters.Add(idParam);
                //Abro la conexion
                conn.Open();
                //El objeto SqlCommand tiene un metodo que nos devuelve la cantidad de filas afectadas
                return comando.ExecuteNonQuery();
            }
        }
        public static Usuario TraerUsuario(string nombreUsuario)
        {
            Usuario usuarioSeleccionado = new Usuario();
            //Realizo la conexion a la BBDD. Utilizo el using para que la conexion se autogestione
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                //Planteo el comando, escribiendo la sentencia SQL y le paso la conexion
                SqlCommand comando = new SqlCommand("SELECT * FROM Usuario WHERE NombreUsuario =@nombreUsuario", conn);
                //Creo el SQL parameter
                SqlParameter nombreUserParametro = new SqlParameter();
                nombreUserParametro.Value = nombreUsuario;
                nombreUserParametro.SqlDbType = SqlDbType.VarChar;
                nombreUserParametro.ParameterName = "nombreUsuario";
                //Añado el parametro a mi comando
                comando.Parameters.Add(nombreUserParametro);
                //Abro la conexion
                conn.Open();
                //Uso el objeto SqlDataReader para tomar lo que viene de la consulta. No uso el data adapter dado que serviría para traer todas las filas y resulta innecesario para el caso
                SqlDataReader reader = comando.ExecuteReader();
                //Valido si el objeto reader tiene filas
                if (reader.HasRows)
                {
                    //Creo un usuario temporal para ir sumando en cada iteracion a los usuarios a la lista de usuarios

                    reader.Read();
                    usuarioSeleccionado.Id = reader.GetInt64(0);
                    usuarioSeleccionado.Nombre = reader.GetString(1);
                    usuarioSeleccionado.Apellido = reader.GetString(2);
                    usuarioSeleccionado.NombreUsuario = reader.GetString(3);
                    usuarioSeleccionado.Contraseña = reader.GetString(4);
                    usuarioSeleccionado.Mail = reader.GetString(5);
                }
                //Retorno mi lista de usuarios
                return usuarioSeleccionado;
            }
        }
        public static void EliminarUsuario(long idUsuarioEliminar)
        {
            ProductoVendidoHandler.EliminarProductosVendidosPorUsuarioVenta(idUsuarioEliminar);
            VentaHandler.EliminarVentasPorUsuarioVentas(idUsuarioEliminar);
            ProductoVendidoHandler.EliminarProductosVendidosPorUsuarioCarga(idUsuarioEliminar);
            ProductoHandler.EliminarProductoPorUsuarioCarga(idUsuarioEliminar);
            EliminarUsuarioPorId(idUsuarioEliminar);

        }

        //Funciones auxiliares
        public static int ExistenciaUsuario(Usuario usuario)
        {
            //Realizo la conexion a la BBDD. Utilizo el using para que la conexion se autogestione
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                //Planteo el comando, escribiendo la sentencia SQL y le paso la conexion
                SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM dbo.Usuario WHERE NombreUsuario=@nombre", conn);
                //Creo el SQL parameters
                SqlParameter nombreParam = new SqlParameter();
                nombreParam.Value = usuario.NombreUsuario;
                nombreParam.SqlDbType = SqlDbType.VarChar;
                nombreParam.ParameterName = "nombre";
                //Añado los parametros a mi comando
                comando.Parameters.Add(nombreParam);
                //Abro la conexion
                conn.Open();
                //Uso ExecuteScalar para devolver la cantidad de usuarios
                int cantidadUsuarios = (int)comando.ExecuteScalar();
                return cantidadUsuarios;

            }
        }
        public static int EliminarUsuarioPorId(long idUsuarioEliminar)
        {
            //Realizo la conexion a la BBDD. Utilizo el using para que la conexion se autogestione
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                //Planteo el comando, escribiendo la sentencia SQL y le paso la conexion
                //Planteo un comando que me servira para eliminar el usuario luego de haber eliminado los productos vendidos, productos y ventas
                SqlCommand comando = new SqlCommand("DELETE FROM Usuario WHERE Id=@idUsuarioEliminar", conn);
                //Creo el SQL parameter para el comando 
                SqlParameter idUsuarioParam = new SqlParameter();
                idUsuarioParam.Value = idUsuarioEliminar;
                idUsuarioParam.SqlDbType = SqlDbType.BigInt;
                idUsuarioParam.ParameterName = "idUsuarioEliminar";
                //Añado el parametro a mi comando
                comando.Parameters.Add(idUsuarioParam);
                //Abro la conexion
                conn.Open();
                //El objeto SqlCommand tiene un metodo que nos devuelve la cantidad de filas afectadas
                return comando.ExecuteNonQuery();
            }
        }
    }
}

