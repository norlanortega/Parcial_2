using System.Security.Cryptography;
using System.Text;

namespace Negocio
{
    /// <summary>
    /// Implementa los dos contratos con SHA-512.
    /// Da el mismo resultado que HASHBYTES('SHA2_512', ...) de SQL Server.
    /// </summary>
    public class HashSHA512 : Igeneradorhash, IVerificadorHash
    {
        /// <summary>Genera el hash SHA-512 de un texto.</summary>
        public byte[] GenerarHash(string texto)
        {
            using (SHA512 sha = SHA512.Create())
            {
                return sha.ComputeHash(Encoding.UTF8.GetBytes(texto ?? string.Empty));
            }
        }

        /// <summary>Compara el hash del texto con el hash guardado.</summary>
        public bool VerificarHash(string texto, byte[] hashGuardado)
        {
            if (hashGuardado == null)
            {
                return false;
            }

            byte[] hashIngresado = GenerarHash(texto);

            if (hashIngresado.Length != hashGuardado.Length)
            {
                return false;
            }

            int diferencias = 0;
            for (int i = 0; i < hashIngresado.Length; i++)
            {
                diferencias |= hashIngresado[i] ^ hashGuardado[i];
            }
            return diferencias == 0;
        }
    }
}