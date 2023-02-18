using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTienda
{
    public class ProductoVendido
    {
        //Atributos
        public long id;
        public int stock;
        public long idProducto;
        public long idVenta;

        //Constructor
        public ProductoVendido(long id, int stock, long idProducto, long idVenta)
        {
            this.Id = id;
            this.Stock = stock;
            this.IdProducto = idProducto;
            this.IdVenta = idVenta;
        }
        public ProductoVendido() { }

        //Getters y setters
        public long Id { get => id; set => id = value; }
        public int Stock { get => stock; set => stock = value; }
        public long IdProducto { get => idProducto; set => idProducto = value; }
        public long IdVenta { get => idVenta; set => idVenta = value; }

    }


}
