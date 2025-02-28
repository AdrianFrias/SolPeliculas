using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class D_Pelicula
    {
        private string CadenaConexion = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        //Crear los metodos de un CRUD(create, read, update,Delete)

        /// <summary>
        /// Read, Obtener todos los elementos de una base de datos
        /// </summary>
        /// <returns></returns>
        public List<E_Pelicula> ReadTodos()
        {
            List<E_Pelicula> lista = new List<E_Pelicula>();

            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                conexion.Open();
                SqlCommand Comando = new SqlCommand("spObtenerTodosPeliculas", conexion);
                Comando.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Comando.ExecuteReader();
                while (reader.Read())
                {
                    E_Pelicula pelicula = new E_Pelicula();
                    pelicula.IDPelicula = Convert.ToInt32(reader["idPelicula"]);
                    pelicula.Nombre = Convert.ToString(reader["nombre"]);
                    pelicula.Genero = Convert.ToString(reader["genero"]);
                    pelicula.FechaLanzamiento = Convert.ToDateTime(reader["fechalanzamiento"]);
                    pelicula.nombreImagen = Convert.ToString(reader["nombreImagen"]);
                    lista.Add(pelicula);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }

            return lista;
        }
        /// <summary>
        /// Read, Obtener una pelicual segun su ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>Pelicula</returns>
        public E_Pelicula ReadPelicula(int ID)
        {
            E_Pelicula pelicula = new E_Pelicula();
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("spObtenerPelicula", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("id", ID);

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    pelicula.IDPelicula = Convert.ToInt32(reader["idPelicula"]);
                    pelicula.Nombre = Convert.ToString(reader["nombre"]);
                    pelicula.Genero = Convert.ToString(reader["genero"]);
                    pelicula.FechaLanzamiento = Convert.ToDateTime(reader["fechalanzamiento"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return pelicula;
        }
        /// <summary>
        /// Create, Agrega peliculas a la base de datos
        /// </summary>
        /// <param name="pelicula"></param>
        public void CreatePelicula(E_Pelicula pelicula)
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("spAgregarPelicula", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@sp_nombre", pelicula.Nombre);
                comando.Parameters.AddWithValue("@sp_genero", pelicula.Genero);
                comando.Parameters.AddWithValue("@sp_fecha", pelicula.FechaLanzamiento);
                comando.Parameters.AddWithValue("@sp_nombreImagen", pelicula.nombreImagen);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }
        /// <summary>
        /// Update, Actualizar los datos de una pelicula
        /// </summary>
        /// <param name="pelicula"></param>
        public void UpdatePelicula(E_Pelicula pelicula)
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("spActualizarPelicula", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@sp_id", pelicula.IDPelicula);
                comando.Parameters.AddWithValue("@sp_nombre", pelicula.Nombre);
                comando.Parameters.AddWithValue("@sp_genero", pelicula.Genero);
                comando.Parameters.AddWithValue("@sp_fecha", pelicula.FechaLanzamiento);
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }
        /// <summary>
        /// Delete, Eliminar producto
        /// </summary>
        /// <param name="ID"></param>
        public void DeletePelicula(int ID)
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("spEliminarPelicula", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@sp_id", ID);
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }
        public uint ValidarPelicula(string nombre)
        {
            uint flag = new uint();
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("spValidarPelicula", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@sp_nombre", nombre);
                flag = Convert.ToUInt16(comando.ExecuteScalar());
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return flag;
        }
    }
}
