using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CapaModelo;
using CapaDatos;

namespace CapaDatos
{
    public class CD_Producto
    {
        public static CD_Producto _instancia = null;
        public static CD_Producto Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Producto();
                }
                return _instancia;
            }
        }

        public List<Producto> ObtenerProducto()
        {
            var rptListaProducto = new List<Producto>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_ProductoObtener", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())

                    {

                        rptListaProducto.Add(new Producto()
                        {

                            id_Producto = Convert.ToInt32(dr["id_Producto"].ToString()),
                            Nombre_Producto = dr["Nombre_Producto"].ToString(),
                            Descripcion = dr["Descripcion"].ToString(),
                            id_Categoria = Convert.ToInt32(dr["id_Categoria"].ToString()),
                            objCategoria=new Categoria() { Nombre_Categoria = dr["Nombre_Categoria"].ToString()},
                            Estado = !string.IsNullOrEmpty(dr["Estado"].ToString()) && Convert.ToBoolean(dr["Estado"].ToString())
                        });
                    }

                    dr.Close();

                    return rptListaProducto;
                }
                catch
                {
                    rptListaProducto = null;
                    return rptListaProducto;

                }
            }
        }
        public bool RegistrarProducto(Producto oProducto)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_RegistrarProducto", oConexion);
                    cmd.Parameters.AddWithValue("Nombre", oProducto.Nombre_Producto);

                    cmd.Parameters.AddWithValue("Descripcion", oProducto.Descripcion = (oProducto.Descripcion != null ?

                    oProducto.Descripcion : ""));
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.Parameters.AddWithValue("Categoria", oProducto.id_Categoria);

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
        public bool ModificarProducto(Producto oProducto)

        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ModificarProducto", oConexion);
                    cmd.Parameters.AddWithValue("IdProducto", oProducto.id_Producto);
                    cmd.Parameters.AddWithValue("NombreProd", oProducto.Nombre_Producto);
                    cmd.Parameters.AddWithValue("Descripcion", oProducto.Descripcion = (oProducto.Descripcion != null ?

                    oProducto.Descripcion : ""));
                    cmd.Parameters.AddWithValue("Categoria", oProducto.id_Categoria);
                    cmd.Parameters.AddWithValue("Activo", oProducto.Estado);

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
        public bool EliminarProducto(int IdProducto)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ProductoEliminar", oConexion);
                    cmd.Parameters.AddWithValue("cod", IdProducto);

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
