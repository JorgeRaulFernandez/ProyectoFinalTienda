using ProyectoTienda.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTienda.ADO.NET
{
    internal static class ProductoHandler
    {
        //Planteo la cadena de conexion y la declaro como una variable de clase para no redundar codigo
        public static string cadenaConexion = "Data Source=DESKTOP-37MP24G;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static int CrearProducto(Producto productoNuevo)
        {
            //Realizo la conexion a la BBDD. Utilizo el using para que la conexion se autogestione
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                //Planteo el comando, escribiendo la sentencia SQL y le paso la conexion
                SqlCommand comando = new SqlCommand("INSERT INTO dbo.Producto (Descripciones,Costo,PrecioVenta,Stock,IdUsuario) VALUES (@descripciones, @costo, @precioVenta, @stock, @idUsuario)", conn);
                //Creo los SQL parameters
                SqlParameter descripcionesParam = new SqlParameter();
                descripcionesParam.Value = productoNuevo.Descripciones;
                descripcionesParam.SqlDbType = SqlDbType.VarChar;
                descripcionesParam.ParameterName = "descripciones";

                SqlParameter costoParam = new SqlParameter();
                costoParam.Value = productoNuevo.Costo;
                costoParam.SqlDbType = SqlDbType.Money;
                costoParam.ParameterName = "costo";

                SqlParameter precioVentaParam = new SqlParameter();
                precioVentaParam.Value = productoNuevo.PrecioVenta;
                precioVentaParam.SqlDbType = SqlDbType.Money;
                precioVentaParam.ParameterName = "precioVenta";

                SqlParameter StockParam = new SqlParameter();
                StockParam.Value = productoNuevo.Stock;
                StockParam.SqlDbType = SqlDbType.Int;
                StockParam.ParameterName = "stock";

                SqlParameter idUsuarioParam = new SqlParameter();
                idUsuarioParam.Value = productoNuevo.IdUsuario;
                idUsuarioParam.SqlDbType = SqlDbType.BigInt;
                idUsuarioParam.ParameterName = "idUsuario";
                //Añado los parametros a mi comando
                comando.Parameters.Add(descripcionesParam);
                comando.Parameters.Add(costoParam);
                comando.Parameters.Add(precioVentaParam);
                comando.Parameters.Add(StockParam);
                comando.Parameters.Add(idUsuarioParam);
                //Abro la conexion
                conn.Open();
                //El objeto SqlCommand tiene un metodo que nos devuelve la cantidad de filas afectadas
                return comando.ExecuteNonQuery();
            }
        }
        public static int ModificarProducto(Producto productoModificado)
        {
            //Realizo la conexion a la BBDD. Utilizo el using para que la conexion se autogestione
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                //Planteo el comando, escribiendo la sentencia SQL y le paso la conexion
                SqlCommand comando = new SqlCommand("UPDATE dbo.Producto SET Descripciones = @descripcion , Costo = @costo, PrecioVenta = @precioVenta, Stock = @stock, IdUsuario=@idUsuario WHERE Id = @idProducto", conn);
                //Creo los SQL parameters
                SqlParameter descripcionParam = new SqlParameter();
                descripcionParam.Value = productoModificado.Descripciones;
                descripcionParam.SqlDbType = SqlDbType.VarChar;
                descripcionParam.ParameterName = "descripcion";

                SqlParameter costoParam = new SqlParameter();
                costoParam.Value = productoModificado.Costo;
                costoParam.SqlDbType = SqlDbType.Money;
                costoParam.ParameterName = "costo";

                SqlParameter precioVentaParam = new SqlParameter();
                precioVentaParam.Value = productoModificado.PrecioVenta;
                precioVentaParam.SqlDbType = SqlDbType.Money;
                precioVentaParam.ParameterName = "precioVenta";

                SqlParameter stockParam = new SqlParameter();
                stockParam.Value = productoModificado.Stock;
                stockParam.SqlDbType = SqlDbType.Int;
                stockParam.ParameterName = "stock";

                SqlParameter idUsuarioParam = new SqlParameter();
                idUsuarioParam.Value = productoModificado.IdUsuario;
                idUsuarioParam.SqlDbType = SqlDbType.BigInt;
                idUsuarioParam.ParameterName = "idusuario";

                SqlParameter idProductoParam = new SqlParameter();
                idProductoParam.Value = productoModificado.Id;
                idProductoParam.SqlDbType = SqlDbType.BigInt;
                idProductoParam.ParameterName = "idProducto";
                //Añado los parametros a mi comando
                comando.Parameters.Add(descripcionParam);
                comando.Parameters.Add(costoParam);
                comando.Parameters.Add(precioVentaParam);
                comando.Parameters.Add(stockParam);
                comando.Parameters.Add(idUsuarioParam);
                comando.Parameters.Add(idProductoParam);
                //Abro la conexion
                conn.Open();
                //El objeto SqlCommand tiene un metodo que nos devuelve la cantidad de filas afectadas
                return comando.ExecuteNonQuery();
            }
        }
        public static int EliminarProducto(long idProductoEliminar)
        {
            //Realizo la conexion a la BBDD. Utilizo el using para que la conexion se autogestione
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                //Planteo el comando, escribiendo la sentencia SQL y le paso la conexion
                SqlCommand comando = new SqlCommand("DELETE FROM Producto WHERE Id = @idProducto", conn);
                //Creo el SQL parameter
                SqlParameter idProductoParametro = new SqlParameter();
                idProductoParametro.Value = idProductoEliminar;
                idProductoParametro.SqlDbType = SqlDbType.BigInt;
                idProductoParametro.ParameterName = "idProducto";
                //Añado el parametro a mi comando
                comando.Parameters.Add(idProductoParametro);
                EliminarProductoVendido(idProductoEliminar);
                //Abro la conexion
                conn.Open();
                //El objeto SqlCommand tiene un metodo que nos devuelve la cantidad de filas afectadas
                return comando.ExecuteNonQuery();
            }
        }
        public static List<Producto> TraerProductos(long idUsuario)
        {
            //Creo una lista de productos y la instancio
            List<Producto> productos = new List<Producto>();
            //Realizo la conexion a la BBDD. Utilizo el using para que la conexion se autogestione
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                //Planteo el comando, escribiendo la sentencia SQL y le paso la conexion
                SqlCommand comando = new SqlCommand("SELECT * FROM Producto WHERE IdUsuario =@id", conn);
                //Creo el SQL parameter
                SqlParameter idParametro = new SqlParameter();
                idParametro.Value = idUsuario;
                idParametro.SqlDbType = SqlDbType.BigInt;
                idParametro.ParameterName = "id";
                //Añado el parametro a mi comando
                comando.Parameters.Add(idParametro);
                //Abro la conexion
                conn.Open();
                //Uso el objeto SqlDataReader para tomar lo que viene de la consulta. No uso el data adapter dado que serviría para traer todas las filas y resulta innecesario para el caso
                SqlDataReader reader = comando.ExecuteReader();
                //Valido si el objeto reader tiene filas
                if (reader.HasRows)
                {
                    //Uso un bucle while para ver leer las filas
                    while (reader.Read())
                    {
                        //Creo un producto temporal para ir sumando los productos a la lista de productos
                        Producto productoTemp = new Producto();
                        productoTemp.Id = reader.GetInt64(0);
                        productoTemp.Descripciones = reader.GetString(1);
                        productoTemp.Costo = reader.GetDecimal(2);
                        productoTemp.PrecioVenta = reader.GetDecimal(3);
                        productoTemp.Stock = reader.GetInt32(4);
                        productoTemp.IdUsuario = reader.GetInt64(5);
                        //Agrego el producto a mi lista de productos
                        productos.Add(productoTemp);
                    }
                }
                //Terminado el bucle, retorno mi lista de productos
                return productos;
            }
        }

        //Funciones Auxiliares
        public static List<Producto> TraerTodosProductos()
        {
            //Creo una lista de productos y la instancio
            List<Producto> productos = new List<Producto>();
            //Realizo la conexion a la BBDD. Utilizo el using para que la conexion se autogestione
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                //Planteo el comando, escribiendo la sentencia SQL y le paso la conexion
                SqlCommand comando = new SqlCommand("SELECT * FROM Producto", conn);
                //Abro la conexion
                conn.Open();
                //Uso el objeto SqlDataReader para tomar lo que viene de la consulta. No uso el data adapter dado que serviría para traer todas las filas y resulta innecesario para el caso
                SqlDataReader reader = comando.ExecuteReader();
                //Valido si el objeto reader tiene filas
                if (reader.HasRows)
                {
                    //Uso un bucle while para ver leer las filas
                    while (reader.Read())
                    {
                        //Creo un producto temporal para ir sumando los productos a la lista de productos
                        Producto productoTemp = new Producto();
                        productoTemp.Id = reader.GetInt64(0);
                        productoTemp.Descripciones = reader.GetString(1);
                        productoTemp.Costo = reader.GetDecimal(2);
                        productoTemp.PrecioVenta = reader.GetDecimal(3);
                        productoTemp.Stock = reader.GetInt32(4);
                        productoTemp.IdUsuario = reader.GetInt64(5);
                        //Agrego el producto a mi lista de productos
                        productos.Add(productoTemp);
                    }
                }
                //Terminado el bucle, retorno mi lista de productos
                return productos;
            }
        }
        public static Producto TraerProductoPorId(long idProducto)
        {
            //Creo una lista de productos y la instancio
            Producto producto = new Producto();
            //Realizo la conexion a la BBDD. Utilizo el using para que la conexion se autogestione
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                //Planteo el comando, escribiendo la sentencia SQL y le paso la conexion
                SqlCommand comando = new SqlCommand("SELECT * FROM Producto WHERE Id=@idProducto", conn);
                //Creo el SQL parameter
                SqlParameter idParametro = new SqlParameter();
                idParametro.Value = idProducto;
                idParametro.SqlDbType = SqlDbType.BigInt;
                idParametro.ParameterName = "idProducto";
                //Añado el parametro a mi comando
                comando.Parameters.Add(idParametro);
                //Abro la conexion
                conn.Open();
                //Uso el objeto SqlDataReader para tomar lo que viene de la consulta. No uso el data adapter dado que serviría para traer todas las filas y resulta innecesario para el caso
                SqlDataReader reader = comando.ExecuteReader();
                //Valido si el objeto reader tiene filas
                if (reader.HasRows)
                {
                    reader.Read();
                    producto.Id = reader.GetInt64(0);
                    producto.Descripciones = reader.GetString(1);
                    producto.Costo = reader.GetDecimal(2);
                    producto.PrecioVenta = reader.GetDecimal(3);
                    producto.Stock = reader.GetInt32(4);
                    producto.IdUsuario = reader.GetInt64(5);
                }
            }
            return producto;
        }
        public static List<Producto> TraerProductosPorDescripcion(string descripcion)
        {
            //Creo una lista de productos y la instancio
            List<Producto> productos = new List<Producto>();
            //Realizo la conexion a la BBDD. Utilizo el using para que la conexion se autogestione
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                //Planteo el comando, escribiendo la sentencia SQL y le paso la conexion
                SqlCommand comando = new SqlCommand("SELECT * FROM Producto WHERE Descripciones = @descripcion", conn);
                //Creo el SQL parameter
                SqlParameter descripcionParametro = new SqlParameter();
                descripcionParametro.Value = descripcion;
                descripcionParametro.SqlDbType = SqlDbType.VarChar;
                descripcionParametro.ParameterName = "descripcion";
                //Añado el parametro a mi comando
                comando.Parameters.Add(descripcionParametro);
                //Abro la conexion
                conn.Open();
                //Uso el objeto SqlDataReader para tomar lo que viene de la consulta. No uso el data adapter dado que serviría para traer todas las filas y resulta innecesario para el caso
                SqlDataReader reader = comando.ExecuteReader();
                //Valido si el objeto reader tiene filas
                if (reader.HasRows)
                {
                    //Uso un bucle while para ver leer las filas
                    while (reader.Read())
                    {
                        //Creo un producto temporal para ir sumando los productos a la lista de productos
                        Producto productoTemp = new Producto();
                        productoTemp.Id = reader.GetInt64(0);
                        productoTemp.Descripciones = reader.GetString(1);
                        productoTemp.Costo = reader.GetDecimal(2);
                        productoTemp.PrecioVenta = reader.GetDecimal(3);
                        productoTemp.Stock = reader.GetInt32(4);
                        productoTemp.IdUsuario = reader.GetInt64(5);
                        //Agrego el producto a mi lista de productos
                        productos.Add(productoTemp);
                    }
                }
                //Terminado el bucle, retorno mi lista de productos
                return productos;
            }
        }
        public static int EliminarProductoVendido(long idProductoEliminar)
        {
            //Realizo la conexion a la BBDD. Utilizo el using para que la conexion se autogestione
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                //Planteo el comando, escribiendo la sentencia SQL y le paso la conexion
                SqlCommand comando = new SqlCommand("DELETE FROM ProductoVendido WHERE IdProducto = @idProductoVendido", conn);
                //Creo el SQL parameter
                SqlParameter idProductoVendidoParametro = new SqlParameter();
                idProductoVendidoParametro.Value = idProductoEliminar;
                idProductoVendidoParametro.SqlDbType = SqlDbType.BigInt;
                idProductoVendidoParametro.ParameterName = "idProductoVendido";
                //Añado el parametro a mi comando
                comando.Parameters.Add(idProductoVendidoParametro);
                //Abro la conexion
                conn.Open();
                //El objeto SqlCommand tiene un metodo que nos devuelve la cantidad de filas afectadas
                return comando.ExecuteNonQuery();
            }
        }
        public static void EliminarProductoPorUsuarioCarga(long idUsuarioEliminar)
        {
            //Creo una lista donde voy a almacenar los productos cargados por esos usuarios que van a ser eliminados
            List<long> listaIdProductosCargados = new List<long>();
            //Realizo la conexion a la BBDD. Utilizo el using para que la conexion se autogestione
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                //Planteo el comando, escribiendo la sentencia SQL y le paso la conexion
                SqlCommand comando1 = new SqlCommand("SELECT * FROM Producto INNER JOIN Usuario ON Producto.IdUsuario=Usuario.Id WHERE Producto.IdUsuario= @idUsuario", conn);

                //Creo el SQL parameter para el comando 1
                SqlParameter idUsuarioParam = new SqlParameter();
                idUsuarioParam.Value = idUsuarioEliminar;
                idUsuarioParam.SqlDbType = SqlDbType.BigInt;
                idUsuarioParam.ParameterName = "idUsuario";

                //Añado el parametro a mi comando
                comando1.Parameters.Add(idUsuarioParam);

                //Abro la conexion
                conn.Open();
                //Uso el objeto SqlDataReader para tomar lo que viene de la consulta. No uso el data adapter dado que serviría para traer todas las filas y resulta innecesario para el caso
                SqlDataReader reader = comando1.ExecuteReader();
                //Valido si el objeto reader tiene filas
                if (reader.HasRows)
                {
                    //Creo un producto temporal para ir sumando los productos a la lista de productos
                    long idProductoCargado;
                    //Uso un bucle while para ver leer las filas
                    while (reader.Read())
                    {
                        idProductoCargado = reader.GetInt64(0);
                        //Agrego el producto a mi lista de productos vendidos, a la de productos y a la de ventas
                        if (!listaIdProductosCargados.Contains(idProductoCargado))
                        {
                            listaIdProductosCargados.Add(idProductoCargado);
                        };
                    }
                    EliminarProductos(listaIdProductosCargados);
                };
            }
        }
        public static void EliminarProductos(List<long> idsProductos)
        {
            foreach (long idProducto in idsProductos)
            {
                EliminarProducto(idProducto);
            }
        }
        public static int updateStockProducto(long id, int cantidadVendida)
        {
            //Realizo una consulta para saber que stock tengo y restarle lo vendido
            Producto producto = TraerProductoPorId(id);
            producto.Stock -= cantidadVendida;
            return ModificarProducto(producto);
        }
    }
}
