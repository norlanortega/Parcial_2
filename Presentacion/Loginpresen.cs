using Datos;
using entidade;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion
{
    internal class Loginpresen
    {
        private readonly ILoginVista _vista;
        private readonly N_usuarios _negocio;

        public Loginpresen(ILoginVista vista, N_usuarios negocio)
        {
            _vista = vista;
            _negocio = negocio;
            _vista.IniciarSesion += OnIniciarSesion;
        }

        /// <summary>Valida las credenciales al presionar "Iniciar sesión".</summary>
        private void OnIniciarSesion(object sender, EventArgs e)
        {
            try
            {
                E_usuarios usuario = _negocio.Login(_vista.Usuario, _vista.Password);

                if (usuario == null)
                {
                    _vista.MostrarError("Usuario o contraseña incorrectos.");
                    return;
                }

                _vista.AbrirPrincipal(usuario);
            }
            catch (Exception ex)
            {
                _vista.MostrarError("No se pudo conectar con la base de datos.\n\n" + ex.Message);

            }
        }
    }
}
