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
    public class D_Genero
    {
        private string CadenaConexion = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        public List<E_Genero> ReadTodos()
        {
            List<E_Genero> lista = new List<E_Genero>();

            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                conexion.Open();
                SqlCommand Comando = new SqlCommand("spObtenerGenerosPeliculas", conexion);
                Comando.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = Comando.ExecuteReader();
                while (reader.Read())
                {
                    //E_Genero genero = new E_Genero();
                    //genero.IDGeneroPelicula = Convert.ToInt32(reader["idPelicula"]);
                    //genero.Genero = Convert.ToString(reader["genero"]);
                    //lista.Add(genero);

                    //Forma en una sola linea
                    //E_Genero genero = new E_Genero
                    //{
                    //    IDGeneroPelicula = Convert.ToInt32(reader["idGeneroPelicula"]),
                    //    Genero = Convert.ToString(reader["genero"])
                    //};
                    lista.Add(new E_Genero
                    {
                        IDGeneroPelicula = Convert.ToInt32(reader["idGeneroPelicula"]),
                        Genero = Convert.ToString(reader["genero"])
                    });
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
       
        public E_Genero ReadGenero(int ID)
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            E_Genero genero = new E_Genero();
            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("spGeneroObtener", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@sp_idgenero", ID);
                SqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    genero.Genero = Convert.ToString(reader["genero"]);
                    genero.IDGeneroPelicula = ID;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return genero;
        }
        public void CreateGenero(E_Genero genero)
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("spGeneroAgregar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@sp_nombregenero", genero.Genero);

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
        public void UpdateGenero(E_Genero genero)
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("spGeneroActualizar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@sp_idgenero", genero.IDGeneroPelicula);
                comando.Parameters.AddWithValue("@sp_genero", genero.Genero);
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
        public void DelateGenero(E_Genero genero)
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("spGeneroEliminar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@sp_idgenero", genero.IDGeneroPelicula);
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
        public uint ValidarGenero(string nombre)
        {
            uint flag = new uint();
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("spGeneroValidar", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@sp_genero", nombre);
                flag = Convert.ToUInt16(comando.ExecuteScalar());
            }
            catch (Exception ex)
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
