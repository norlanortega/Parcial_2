using Datos;
using entidade;

namespace Negocio
{
    /// <summary>
    /// Reglas de negocio del usuario. Intermediario entre Presentación y Datos.
    /// </summary>
    public class N_usuarios
    {
        private readonly IVerificadorHash _verificadorHash;

        /// <summary>Usa SHA-512 como verificador por defecto.</summary>
        public N_usuarios() : this(new HashSHA512())
        {
        }

        /// <summary>Permite usar otro verificador de hash.</summary>
        public N_usuarios(IVerificadorHash verificadorHash)
        {
            _verificadorHash = verificadorHash;
        }

        /// <summary>
        /// Valida usuario y contraseña. Devuelve el usuario o null si falla.
        /// </summary>
        public E_usuarios Login(string strUsuario, string strPassword)
        {
            if (string.IsNullOrWhiteSpace(strUsuario) || string.IsNullOrEmpty(strPassword))
            {
                return null;
            }

            E_usuarios usuario = D_Usuario.ObtenerPorUsuario(strUsuario);
            if (usuario == null)
            {
                return null;
            }

            if (!_verificadorHash.VerificarHash(strPassword, usuario.dsPassword))
            {
                return null;
            }

            usuario.dsPassword = null;
            return usuario;
        }
    }
}