using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public interface Igeneradorhash

    {
        byte[] GenerarHash(string texto);
    }
}
