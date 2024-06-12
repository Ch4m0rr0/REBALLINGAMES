using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Almacen
    {
        private static CD_Almacen _instancia = null;

        private CD_Almacen() { }

        public static CD_Almacen Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Almacen();
                }
                return _instancia;
            }
        }

        public List<Almacen> ObtenerAlmacenes()
        {
            List<Almacen> listaAlmacenes = new List<Almacen>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_AlmacenObtener", oConexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Almacen almacen = new Almacen()
                        {
                            id_Almacen = Convert.ToInt32(dr["id_Almacen"]),
                            Nombre_Almacen = dr["Nombre_Almacen"].ToString(),
                            Ubicacion = dr["Ubicacion"].ToString(),
                            Descripcion = dr["Descripcion"].ToString(),
                           
                        };
                        listaAlmacenes.Add(almacen);
                    }

                    dr.Close();
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    Console.WriteLine(ex.Message);
                }
            }
            return listaAlmacenes;
        }

        public bool RegistrarAlmacen(Almacen almacen)
        {
            bool respuesta = false;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_RegistrarAlmacen", oConexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Nombre_Almacen", almacen.Nombre_Almacen);
                cmd.Parameters.AddWithValue("@Ubicacion", almacen.Ubicacion);
                cmd.Parameters.AddWithValue("Descripcion", almacen.Descripcion = (almacen.Descripcion != null ?

                    almacen.Descripcion : ""));


                cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                try
                {
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    Console.WriteLine(ex.Message);
                }
            }
            return respuesta;
        }

        public bool ModificarAlmacen(Almacen almacen)
        {
            bool respuesta = false;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_ModificarAlmacen", oConexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@id_Almacen", almacen.id_Almacen);
                cmd.Parameters.AddWithValue("@Nombre_Almacen", almacen.Nombre_Almacen);
                cmd.Parameters.AddWithValue("@Ubicacion", almacen.Ubicacion);
                cmd.Parameters.AddWithValue("Descripcion", almacen.Descripcion = (almacen.Descripcion != null ?

                    almacen.Descripcion : ""));

                cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                try
                {
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    Console.WriteLine(ex.Message);
                }
            }
            return respuesta;
        }

        public bool EliminarAlmacen(int id_Almacen)
        {
            bool respuesta = false;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_AlmacenEliminar", oConexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("Cod", id_Almacen);
                cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                try
                {
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    Console.WriteLine(ex.Message);
                }
            }
            return respuesta;
        }
    }
}
