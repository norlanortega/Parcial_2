using entidade;
using System;

namespace Presentacion
{
    /// <summary>
    /// Presentador del formulario principal (MVP).
    /// </summary>
    public class PrincipalPresentador
    {
        private readonly IPrincipalVista _vista;
        private readonly E_usuarios _usuario;

        public PrincipalPresentador(IPrincipalVista vista, E_usuarios usuario)
        {
            _vista = vista;
            _usuario = usuario;
        }

        /// <summary>Muestra quién inició sesión y la hora.</summary>
        public void Iniciar()
        {
            _vista.MostrarUsuario("Sesión iniciada por: " + _usuario.dsUsuario);
            ActualizarHora();
        }

        /// <summary>Envía la hora actual a la vista.</summary>
        public void ActualizarHora()
        {
            _vista.MostrarHora(DateTime.Now.ToString("dd/MM/yyyy  hh:mm:ss tt"));
        }
    }
}
