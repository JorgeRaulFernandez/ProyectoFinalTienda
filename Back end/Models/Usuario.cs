using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTienda
{
    public class Usuario
    {
        //Atributos
        public long id;
        public string nombre;
        public string apellido;
        public string nombreUsuario;
        public string contraseña;
        public string mail;

        //Constructor
        public Usuario(long id, string nombre, string apellido, string nombreUsuario, string contraseña, string mail)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.NombreUsuario = nombreUsuario;
            this.Contraseña = contraseña;
            this.Mail = mail;
        }
        public Usuario() { }
        //Getters y setters
        public long Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
        public string Contraseña { get => contraseña; set => contraseña = value; }
        public string Mail { get => mail; set => mail = value; }
    }
}
