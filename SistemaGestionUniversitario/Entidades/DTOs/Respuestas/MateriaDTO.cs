using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTOs.Respuestas
{
    public class MateriaDTO
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public int Anio { get; set; }
        public string Modalidad { get; set; }
        public List<string> NombresProfesores { get; set; }
        public List<string> DescripcionDiasHorarios { get; set; }
    }
}