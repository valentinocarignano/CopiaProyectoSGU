using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTOs
{
    public class AsistenciaDTO
    {
        public int ID { get; set; }
        public string NombreAlumno { get; set; } //idinscripcion
        public string Dia { get; set; }
        public string Horario { get; set; }
        public string Materia { get; set; }
        public bool Estado { get; set; }
        public DateTime Fecha { get; set; }
    }
}
