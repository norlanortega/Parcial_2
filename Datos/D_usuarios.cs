using entidade;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
 
    public class D_Usuario : D_conexion
    {
     
        public static E_usuarios ObtenerPorUsuario(string strUsuario)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            using (SqlCommand cmd = new SqlCommand("sp_ObtenerUsuario", conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@dsUsuario", SqlDbType.VarChar, 50).Value = strUsuario;

                conexion.Open();

                using (SqlDataReader fReader = cmd.ExecuteReader())
                {
                    if (!fReader.Read())
                    {
                        return null;
                    }

                    return new E_usuarios
                    {
                        id = fReader.GetInt32(fReader.GetOrdinal("id")),
                        dsId = fReader.GetString(fReader.GetOrdinal("dsId")),
                        dsUsuario = fReader.GetString(fReader.GetOrdinal("dsUsuario")),
                        dsPassword = (byte[])fReader["dsPassword"],
                        dsEstado = Convert.ToChar(fReader["dsEstado"])
                    };
                }
            }
        }
    }
}