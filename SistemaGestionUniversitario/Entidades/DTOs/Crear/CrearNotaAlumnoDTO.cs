using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTOs.Crear
{
    public class CrearNotaAlumnoDTO
    {
        public int Nota { get; set; }
        public int IDAlumno { get; set; }
        public int IDExamen { get; set; }
    }
}
