using Entidades.DTOs.Crear;
using Entidades.DTOs.Modificar;
using Entidades.DTOs.Respuestas;
using Logica.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly IProfesorLogic _profesorLogic;
        public ProfesorController(IProfesorLogic profesorLogic)
        {
            _profesorLogic = profesorLogic;
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerProfesores()
        {
            List<ProfesorDTO> profesorDTO = await _profesorLogic.ObtenerProfesores();

            return Ok(profesorDTO);
        }
        [HttpGet]
        [Route("{dni}")]
        public async Task<IActionResult> ObtenerProfesor(string dni)
        {
            ProfesorDTO profesorDTO = await _profesorLogic.ObtenerProfesorDNI(dni);

            return Ok(profesorDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CrearProfesor([FromBody] CrearProfesorDTO crearProfesorDTO)
        {
            await _profesorLogic.AltaProfesor(crearProfesorDTO.usuario, crearProfesorDTO.FechaInicioContrato);

            return Ok(crearProfesorDTO);
        }

        [HttpPut]
        public async Task<IActionResult> ModificarProfesor([FromBody] ModificarProfesorDTO modificarProfesorDTO)
        {
            await _profesorLogic.ActualizacionProfesor(modificarProfesorDTO.usuario);

            return Ok(modificarProfesorDTO);
        }
        [HttpDelete]
        [Route("{dni}")]
        public async Task<IActionResult> EliminarProfesor(string dni)
        {
            await _profesorLogic.BajaProfesor(dni);

            return Ok();
        }
    }
}
