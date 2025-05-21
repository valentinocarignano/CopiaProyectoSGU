using Entidades.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTOs.Crear
{
    public class CrearProfesorDTO
    {
        public string Id { get; set; }
        public Usuario usuario { get; set; }
        public DateTime? FechaInicioContrato { get; set; }
    }
}
