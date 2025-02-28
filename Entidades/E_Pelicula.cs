using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class E_Pelicula
    {
        //Propiedades simples
        public int IDPelicula { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public string nombreImagen { get; set; }
    }
}
