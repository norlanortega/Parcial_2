using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entidade;

namespace Presentacion
{
    public interface ILoginVista
    {
        string Usuario { get; }
        string Password { get; }
        event EventHandler IniciarSesion;
        void MostrarError(string mensaje);
        void AbrirPrincipal(E_usuarios usuario);
    }
}
