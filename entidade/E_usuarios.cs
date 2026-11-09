using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entidade
{
    public class E_usuarios
    {
        public int id { get; set; }
        public string dsId { get; set; }
        public string dsUsuario { get; set; }
        public string dsPassword { get; set; }
        public char dsEstado { get; set; }
        public string dsUsuarioCreacion { get; set; }
        public DateTime dtFechaCreacion { get; set; }
        public string idUsuarioModificacion { get; set; }
        public DateTime dtFechaModificacion { get; set; }
    }
}
