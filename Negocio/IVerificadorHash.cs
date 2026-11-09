using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public interface IVerificadorHash
    {
        bool VerificarHash(string texto, byte[] hashGuardado);
    }
}
