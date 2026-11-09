using entidade;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class D_usuarios:D_conexion
    {
        public static E_usuarios Login(string strUsuario, string strPassword)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ValidarLoginHash", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@dsUsuario", SqlDbType.VarChar, 50).Value = strUsuario;
                        cmd.Parameters.Add("@dsPassword", SqlDbType.VarChar, 50).Value = strPassword;

                        using (SqlDataReader fReader = cmd.ExecuteReader())
                        {
                            E_usuarios usuario = new E_usuarios();
                            while (fReader.Read())
                            {
                                usuario.id = fReader.GetInt32(fReader.GetOrdinal("id"));
                                usuario.dsUsuario = fReader.GetString(fReader.GetOrdinal("dsUsuario"));
                            }
                            return usuario;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
