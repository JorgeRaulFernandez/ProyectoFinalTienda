using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTienda
{
    public class Venta
    {
        //Atributos
        public long id;
        public string comentarios;
        public long idUsuario;

        //Constructor
        public Venta(long id, string comentarios, long idUsuario)
        {
            this.Id = id;
            this.Comentarios = comentarios;
            this.IdUsuario = idUsuario;
        }
        public Venta() { }
        //Getters y setters

        public long Id { get => id; set => id = value; }
        public string Comentarios { get => comentarios; set => comentarios = value; }
        public long IdUsuario { get => idUsuario; set => idUsuario = value; }
    }
}
