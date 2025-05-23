using Entidades.DTOs.Respuestas;
using Logica.Contracts;
using Microsoft.AspNetCore.Mvc;
using Entidades.Entities;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private readonly IAlumnoLogic _alumnoLogic;
        public AlumnoController(IAlumnoLogic alumnoLogic)
        {
            _alumnoLogic = alumnoLogic;
        }
        // GET: api/Alumnos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumnos()
        {
            List<AlumnoDTO> alumnos = await _alumnoLogic.ObtenerAlumnos();

            return Ok(alumnos);
        }
        [HttpGet]
        [Route("DNI/{dni}")]
        public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumnoPorDNI(string dni)
        {
            AlumnoDTO alumno = await _alumnoLogic.ObtenerAlumnoDNI(dni);

            return Ok(alumno);
        }
    }
}
