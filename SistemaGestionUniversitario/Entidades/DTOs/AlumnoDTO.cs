using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTOs
{
    public class AlumnoDTO
    {
        public int ID { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}