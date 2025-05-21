using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTOs.Modificar
{
    public class ModificarUsuarioDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string CaracteristicaTelefono { get; set; }
        public string NumeroTelefono { get; set; }

        public string Localidad { get; set; }
        public string Direccion { get; set; }
    }
}
