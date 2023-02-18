using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTienda.ADO.NET
{
    internal static class ProductoVendidoHandler
    {
        //Planteo la cadena de conexion y la declaro como una variable de clase para no redundar codigo
        public static string cadenaConexion = "Data Source=DESKTOP-37MP24G;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static List<ProductoVendido> TraerProductosVendidos(long idUsuario)
        {
            //Creo una lista de productos y la instancio
            List<ProductoVendido> productosVendidos = new List<ProductoVendido>();
            //Realizo la conexion a la BBDD. Utilizo el using para que la conexion se autogestione
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                //Planteo el comando, escribiendo la sentencia SQL y le paso la conexion
                SqlCommand comando = new SqlCommand("SELECT * FROM ProductoVendido INNER JOIN Venta ON ProductoVendido.IdVenta=Venta.Id", conn);
                //Abro la conexion
                conn.Open();
                //Uso el objeto SqlDataReader para tomar lo que viene de la consulta. No uso el data adapter dado que serviría para traer todas las filas y resulta innecesario para el caso
                SqlDataReader reader = comando.ExecuteReader();
                //Valido si el objeto reader tiene filas
                if (reader.HasRows)
                {
                    //Uso un bucle while para ver leer las filas
                    while (reader.Read() && idUsuario == reader.GetInt64(6))
                    {
                        //Creo un producto temporal para ir sumando los productos a la lista de productos vendidos
                        ProductoVendido productoVendidoTemp = new ProductoVendido();
                        productoVendidoTemp.Id = reader.GetInt64(0);
                        productoVendidoTemp.Stock = reader.GetInt32(1);
                        productoVendidoTemp.IdProducto = reader.GetInt64(2);
                        productoVendidoTemp.IdVenta = reader.GetInt64(3);
                        //Agrego el producto a mi lista de productos
                        productosVendidos.Add(productoVendidoTemp);
                    }
                }
                //Terminado el bucle, retorno mi lista de productos
                return productosVendidos;
            }
        }

        //Funciones auxiliares
        public static int InsertarProductoVendido(ProductoVendido nuevoProductoVendido)
        {
            //Realizo la conexion a la BBDD. Utilizo el using para que la conexion se autogestione
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                //Planteo el comando, escribiendo la sentencia SQL y le paso la conexion
                SqlCommand comando = new SqlCommand("INSERT INTO ProductoVendido (Stock, IdProducto, IdVenta) VALUES (@stock, @idProducto,@idVenta)", conn);
                //Creo los SQL parameters
                SqlParameter stockParam = new SqlParameter();
                stockParam.Value = nuevoProductoVendido.Stock;
                stockParam.SqlDbType = SqlDbType.Int;
                stockParam.ParameterName = "stock";

                SqlParameter idProductoParam = new SqlParameter();
                idProductoParam.Value = nuevoProductoVendido.Id;
                idProductoParam.SqlDbType = SqlDbType.BigInt;
                idProductoParam.ParameterName = "idProducto";

                SqlParameter idVentaParam = new SqlParameter();
                idVentaParam.Value = nuevoProductoVendido.IdVenta;
                idVentaParam.SqlDbType = SqlDbType.BigInt;
                idVentaParam.ParameterName = "idVenta";
                //Añado los parametros a mi comando
                comando.Parameters.Add(stockParam);
                comando.Parameters.Add(idProductoParam);
                comando.Parameters.Add(idVentaParam);
                //Abro la conexion
                conn.Open();
                //El objeto SqlCommand tiene un metodo que nos devuelve la cantidad de filas afectadas
                return comando.ExecuteNonQuery();
            }
        }
        public static void EliminarProductosVendidosPorUsuarioVenta(long idUsuarioEliminar)
        {
            //Creo una lista donde voy a almacenar los productos vendidos por esos usuarios que van a ser eliminados
            List<long> listaIdProductosVendidos = new List<long>();
            //Realizo la conexion a la BBDD. Utilizo el using para que la conexion se autogestione
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                //Planteo el comando, escribiendo la sentencia SQL y le paso la conexion
                SqlCommand comando1 = new SqlCommand("SELECT * FROM ProductoVendido INNER JOIN Venta ON ProductoVendido.IdVenta=Venta.Id WHERE Venta.IdUsuario= @idUsuario", conn);

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
                    long idProductoVendido;
                    //Uso un bucle while para ver leer las filas
                    while (reader.Read())
                    {
                        idProductoVendido = reader.GetInt64(0);
                        //Agrego el producto a mi lista de productos vendidos, a la de productos y a la de ventas
                        if (!listaIdProductosVendidos.Contains(idProductoVendido))
                        {
                            listaIdProductosVendidos.Add(idProductoVendido);
                        };
                    }
                    EliminarListaProductosVendidos(listaIdProductosVendidos);
                };
            }
        }
        public static void EliminarProductosVendidosPorUsuarioCarga(long idUsuarioEliminar)
        {
            //Creo una lista donde voy a almacenar los productos vendidos por esos usuarios que van a ser eliminados
            List<long> listaIdProductosVendidos = new List<long>();
            //Realizo la conexion a la BBDD. Utilizo el using para que la conexion se autogestione
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                //Planteo el comando, escribiendo la sentencia SQL y le paso la conexion
                SqlCommand comando1 = new SqlCommand("SELECT * FROM ProductoVendido INNER JOIN Producto ON ProductoVendido.IdProducto=Producto.Id WHERE Producto.IdUsuario= @idUsuario", conn);

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
                    long idProductoVendido;
                    //Uso un bucle while para ver leer las filas
                    while (reader.Read())
                    {
                        idProductoVendido = reader.GetInt64(0);
                        //Agrego el producto a mi lista de productos vendidos, a la de productos y a la de ventas
                        if (!listaIdProductosVendidos.Contains(idProductoVendido))
                        {
                            listaIdProductosVendidos.Add(idProductoVendido);
                        };
                    }
                    EliminarListaProductosVendidos(listaIdProductosVendidos);
                };
            }
        }
        public static int EliminarProductoVendido(long idProductoVendido)
        {
            //Realizo la conexion a la BBDD. Utilizo el using para que la conexion se autogestione
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                //Planteo el comando, escribiendo la sentencia SQL y le paso la conexion
                SqlCommand comando = new SqlCommand("DELETE FROM ProductoVendido WHERE Id=@idProductoVendido", conn);
                //Creo el SQL parameter
                SqlParameter idProductoParam = new SqlParameter();
                idProductoParam.Value = idProductoVendido;
                idProductoParam.SqlDbType = SqlDbType.BigInt;
                idProductoParam.ParameterName = "idProductoVendido";
                comando.Parameters.Add(idProductoParam);
                //Abro la conexion
                conn.Open();
                //El objeto SqlCommand tiene un metodo que nos devuelve la cantidad de filas afectadas
                return comando.ExecuteNonQuery();
            }
        }
        public static void EliminarListaProductosVendidos(List<long> idsProductosVendidos)
        {
            foreach (long idProductoVendido in idsProductosVendidos)
            {
                EliminarProductoVendido(idProductoVendido);
            }
        }
    }
}
