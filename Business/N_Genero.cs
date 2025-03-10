using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class N_Genero
    {
        public List<E_Genero> ObtenerGeneros()
        {
            D_Genero datos = new D_Genero();
            List<E_Genero> generos = new List<E_Genero>();
            generos = datos.ReadTodos();
            return generos;
        }
        public E_Genero ObtenerGenero(int ID)
        {
            D_Genero datos = new D_Genero();
            return datos.ReadGenero(ID);
        }
        public void AgregarGenero(E_Genero genero)
        {
            D_Genero datos = new D_Genero();
            if (datos.ValidarGenero(genero.Genero) == 1)
            {
                throw new Exception($"{genero.Genero} ya existe en la base de datos");
            }
            //Queda pendiente la validacion
            datos.CreateGenero(genero);
        }
        public void ActualizarGenero(E_Genero genero)
        {
            D_Genero datos = new D_Genero();
            if (datos.ValidarGenero(genero.Genero) == 1)
            {
                throw new Exception($"{genero.Genero} ya existe en la base de datos");
            }
            datos.UpdateGenero(genero);
        }
        public void EliminarGenero(E_Genero genero)
        {
            D_Genero datos = new D_Genero();
            //Una validacion aqui si funcionaria
            datos.DelateGenero(genero);
        }
        

    }
}
