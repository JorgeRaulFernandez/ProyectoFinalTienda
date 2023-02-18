using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTienda
{
    public class Producto
    {
        //Atributos
        public long id;
        public string descripciones;
        public decimal costo;
        public decimal precioVenta;
        public int stock;
        public long idUsuario;

        //Constructor
        public Producto(long id, string descripciones, decimal costo, decimal precioVenta, int stock, long idUsuario)
        {
            this.Id = id;
            this.Descripciones = descripciones;
            this.Costo = costo;
            this.PrecioVenta = precioVenta;
            this.Stock = stock;
            this.IdUsuario = idUsuario;
        }
        public Producto() { 
        }

        //Getters y setters
        public long Id { get => id; set => id = value; }
        public string Descripciones { get => descripciones; set => descripciones = value; }
        public decimal Costo { get => costo; set => costo = value; }
        public decimal PrecioVenta { get => precioVenta; set => precioVenta = value; }
        public int Stock { get => stock; set => stock = value; }
        public long IdUsuario { get => idUsuario; set => idUsuario = value; }
    }
}
