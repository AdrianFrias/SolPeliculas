using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class N_Pelicula
    {
        /// <summary>
        /// Obtiene todos los datos de una base de datos
        /// </summary>
        /// <returns>Lista de peliculas</returns>
        public List<E_Pelicula> ObtenerTodos()
        {
            D_Pelicula datos = new D_Pelicula();//Variable tipo datos(Los metodos para la comunicacion con la base de datos)
            List<E_Pelicula> peliculas = new List<E_Pelicula>();//Variable para mis enetidades
            peliculas = datos.ReadTodos();
            return peliculas;
        }
        public E_Pelicula ObtenerPelicula(int ID)
        {
            E_Pelicula pelicula = new E_Pelicula();
            D_Pelicula datos = new D_Pelicula();
            pelicula = datos.ReadPelicula(ID);
            return pelicula;
        }
        public void AgregarPelicula(E_Pelicula pelicula)
        {
            D_Pelicula datos = new D_Pelicula();
            if (datos.ValidarPelicula(pelicula.Nombre) == 1)
            {
                //Mandara esta excepcion al controlador
                throw new Exception($"{pelicula.Nombre} ya existe en la base de datos");
            }
            datos.CreatePelicula(pelicula);
        }
        public void ActualizarPelicula(E_Pelicula pelicula)
        {
            D_Pelicula datos = new D_Pelicula();
            datos.UpdatePelicula(pelicula);
        }
        public void EliminarPelicula(E_Pelicula pelicula)
        {
            D_Pelicula datos = new D_Pelicula();
            datos.DeletePelicula(pelicula.IDPelicula);
        }
    }
}
