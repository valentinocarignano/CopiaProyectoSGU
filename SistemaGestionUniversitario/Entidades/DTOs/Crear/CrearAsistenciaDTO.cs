using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTOs.Crear
{
    public class CrearAsistenciaDTO
    {
        public int idInscripcion { get; set; }
        public int idDiaHorarioMateria { get; set; }
        public bool Estado { get; set; }
        public DateTime Fecha { get; set; }
    }
}
