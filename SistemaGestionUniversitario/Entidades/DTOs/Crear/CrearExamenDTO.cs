using Entidades.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTOs.Crear
{
    public class CrearExamenDTO
    {
        public string TipoExamen { get; set; }
        public string NombreMateria { get; set; }
        public string DescripcionDiaHorario { get; set; }
    }
}