using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entidades.Entities;
using Datos.Contexts;
using System.Data.Entity;
using Datos.Repositories.Implementations;
using Entidades.DTOs;
using Datos.Repositories.Contracts;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HorarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Horario>>> GetHorarios()
        {
            var horarios = await _context.Horario.ToListAsync();



            return Ok(horarios); 
        }

        // GET: api/horarios/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Horario>> GetHorarioById(int id)
        {
            var horario = await _context.Horario.FirstOrDefaultAsync(d => d.ID == id);

            if (horario == null)
            {
                return NotFound();
            }


            return Ok(horario);
        }
    }
}
