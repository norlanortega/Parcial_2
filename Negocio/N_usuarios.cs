using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entidade;
using Datos;

namespace Negocio
{
    public class N_usuarios
    {
        public static E_usuarios Login(string strUsuario, string strPassword)
        {
            return D_usuarios.Login(strUsuario, strPassword);
        }
    }
}
