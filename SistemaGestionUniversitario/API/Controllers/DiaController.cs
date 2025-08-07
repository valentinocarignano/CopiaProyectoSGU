using Entidades.DTOs.Respuestas;
using Logica.Contracts;
using Logica.Implementations;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaController : ControllerBase
    {
        private readonly IDiaLogic _diaLogic;
        public DiaController(IDiaLogic diaLogic)
        {
            _diaLogic = diaLogic;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDias()
        {
            List<DiaDTO> diaDTO = await _diaLogic.ObtenerDias();

            if (diaDTO.Count == 0)
            {
                return NoContent();
            }
            
            return Ok(diaDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerDiaPorID(int id)
        {
            try
            {
                DiaDTO diaDTO = await _diaLogic.ObtenerDiaId(id);

                if (diaDTO == null)
                {
                    return NotFound();
                }

                return Ok(diaDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }      
        }
    }
}