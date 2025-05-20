using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTOs
{
    public class ModificarUsuarioPasswordDTO
    {
        public string ActualPassword { get; set; } = string.Empty;
        public string NuevaPassword { get; set; } = string.Empty;
    }

}
