using Entidades.DTOs.Respuestas;
using Entidades.Entities;
using Logica.Contracts;
using Logica.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaHorarioController : ControllerBase
    {
        private readonly IDiaHorarioLogic _diaHorarioLogic;
        
        public DiaHorarioController(IDiaHorarioLogic diaHorarioLogic)
        {
            _diaHorarioLogic = diaHorarioLogic;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<DiaHorarioDTO> diaHorarios = await _diaHorarioLogic.ObtenerDiasHorarios();

            if(diaHorarios.Count == 0)
            {
                return NoContent();
            }

            return Ok(diaHorarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                DiaHorarioDTO diaHorario = await _diaHorarioLogic.ObtenerDiaHorarioID(id);
                
                if (diaHorario == null)
                {
                    return NotFound();
                }
                return Ok(diaHorario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }         
        }
    }
}