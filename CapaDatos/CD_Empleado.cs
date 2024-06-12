using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public  class CD_Empleado
    {
        public static CD_Empleado _instancia = null;

        private CD_Empleado()
        {

        }

        public static CD_Empleado Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Empleado();
                }
                return _instancia;
            }
        }

        public List<Empleado> ObtenerEmpleado()
        {
            var rptListaEmpleado = new List<Empleado>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_EmpleadoObtener", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())

                    {

                        rptListaEmpleado.Add(new Empleado()
                        {
                            id_Empleado = Convert.ToInt32(dr["id_Empleado"].ToString()),
                            Nombre_Empleado= dr["Nombre_Empleado"].ToString(),
                            Direccion= dr["Direccion"].ToString(),
                            Numero_Cedula= dr["Numero_Cedula"].ToString(),
                            Telefono= dr["Telefono"].ToString()
                        });
                    }

                    dr.Close();

                    return rptListaEmpleado;
                }
                catch
                {
                    rptListaEmpleado = null;
                    return rptListaEmpleado;

                }
            }
        }
        public bool RegistrarEmpleado(Empleado oEmpleado)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_RegistrarEmpleado", oConexion);

                    cmd.Parameters.AddWithValue("Nombre_Empleado", oEmpleado.Nombre_Empleado);
                    cmd.Parameters.AddWithValue("Direccion", oEmpleado.Direccion);
                    cmd.Parameters.AddWithValue("Numero_Cedula",oEmpleado.Numero_Cedula);
                    cmd.Parameters.AddWithValue("Telefono", oEmpleado.Telefono);
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
        public bool ModificarEmpleado(Empleado oEmpleado)

        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ModificarEmpleado", oConexion);
                    cmd.Parameters.AddWithValue("id_Empleado", oEmpleado.id_Empleado);
                    cmd.Parameters.AddWithValue("Nombre_Empleado", oEmpleado.Nombre_Empleado);
                    cmd.Parameters.AddWithValue("Direccion", oEmpleado.Direccion);
                    cmd.Parameters.AddWithValue("Numero_Cedula", oEmpleado.Numero_Cedula);
                    cmd.Parameters.AddWithValue("Telefono", oEmpleado.Telefono);

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
        public bool EliminarEmpleado(int Id_Empleado)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_EmpleadoEliminar", oConexion);
                    cmd.Parameters.AddWithValue("Cod", Id_Empleado);

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
