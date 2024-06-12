using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Descartado
    {
        public static CD_Descartado _instancia = null;

        private CD_Descartado()
        {
        }

        public static CD_Descartado Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Descartado();
                }
                return _instancia;
            }
        }

        public List<Descartado> ObtenerDescartados()
        {
            var rptListaDescartado = new List<Descartado>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_ObtenerDescartados", oConexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaDescartado.Add(new Descartado()
                        {
                            id_Descartado = Convert.ToInt32(dr["id_Descartado"].ToString()),
                            Nombre = dr["Nombre"].ToString()
                        });
                    }

                    dr.Close();
                    return rptListaDescartado;
                }
                catch
                {
                    rptListaDescartado = null;
                    return rptListaDescartado;
                }
            }
        }

        public bool RegistrarDescartado(Descartado oDescartado)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_RegistrarDescartado", oConexion);
                    cmd.Parameters.AddWithValue("Nombre", oDescartado.Nombre);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool ModificarDescartado(Descartado oDescartado)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ModificarDescartado", oConexion);
                    cmd.Parameters.AddWithValue("id_Descartado", oDescartado.id_Descartado);
                    cmd.Parameters.AddWithValue("Nombre", oDescartado.Nombre);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool EliminarDescartado(int id_Descartado)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_EliminarDescartado", oConexion);
                    cmd.Parameters.AddWithValue("id_Descartado", id_Descartado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}
