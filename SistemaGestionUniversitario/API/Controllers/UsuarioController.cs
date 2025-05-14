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
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuario.ToListAsync();
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuarioPorId(long? id)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // GET: api/Usuario/DNI
        [HttpGet("GetDNI")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetDNI()
        {
            var usuarios = await _context.Usuario.ToListAsync();

            var usuariosResponse = usuarios.Select(usuario => new
            {
                usuario.DNI,
            });

            return Ok(usuariosResponse);
        }

        //GET: api/Usuario
        public async Task<IActionResult> GetUsuarios([FromQuery] string? filtroSeleccionado)
        {
            List<UsuarioDTO> listaUsuarios = new List<UsuarioDTO>();
            var usuarios = await _context.Usuario.ToListAsync();

            if (filtroSeleccionado == "Administrador")
            {
                listaUsuarios = usuarios
                    .Where(a => a.RolUsuario.Descripcion == "Administrador")
                    .Select(a => new UsuarioDTO
                    {
                        RolUsuarioDescripcion = a.RolUsuario.Descripcion,
                        Nombre = $"{a.Nombre} {a.Apellido}",
                        DNI = a.DNI
                    }).ToList();
            }
            else if (filtroSeleccionado == "Alumno")
            {
                listaUsuarios = usuarios
                    .Where(a => a.RolUsuario.Descripcion == "Alumno")
                    .Select(a => new UsuarioDTO
                    {
                        RolUsuarioDescripcion = a.RolUsuario.Descripcion,
                        Nombre = $"{a.Nombre} {a.Apellido}",
                        DNI = a.DNI
                    }).ToList();
            }
            else if (filtroSeleccionado == "Profesor")
            {
                listaUsuarios = usuarios
                    .Where(a => a.RolUsuario.Descripcion == "Profesor")
                    .Select(a => new UsuarioDTO
                    {
                        RolUsuarioDescripcion = a.RolUsuario.Descripcion,
                        Nombre = $"{a.Nombre} {a.Apellido}",
                        DNI = a.DNI
                    }).ToList();
            }
            else if (filtroSeleccionado == "Z - A")
            {
                listaUsuarios = usuarios
                    .Select(a => new UsuarioDTO
                    {
                        RolUsuarioDescripcion = a.RolUsuario.Descripcion,
                        Nombre = $"{a.Nombre} {a.Apellido}",
                        DNI = a.DNI
                    }).OrderByDescending(u => u.Nombre).ToList();
            }
            else if (filtroSeleccionado == "A - Z")
            {
                listaUsuarios = usuarios
                    .Select(a => new UsuarioDTO
                    {
                        RolUsuarioDescripcion = a.RolUsuario.Descripcion,
                        Nombre = $"{a.Nombre} {a.Apellido}",
                        DNI = a.DNI
                    }).OrderBy(u => u.Nombre).ToList();
            }
            else if (!string.IsNullOrEmpty(filtroSeleccionado))
            {
                listaUsuarios = usuarios
                    .Where(a => a.Nombre.Contains(filtroSeleccionado) || a.Apellido.Contains(filtroSeleccionado))
                    .Select(a => new UsuarioDTO
                    {
                        RolUsuarioDescripcion = a.RolUsuario.Descripcion,
                        Nombre = $"{a.Nombre} {a.Apellido}",
                        DNI = a.DNI
                    }).ToList();
            }
            else
            {
                listaUsuarios = usuarios
                    .Select(a => new UsuarioDTO
                    {
                        RolUsuarioDescripcion = a.RolUsuario.Descripcion,
                        Nombre = $"{a.Nombre} {a.Apellido}",
                        DNI = a.DNI
                    }).ToList();
            }

            return Ok(listaUsuarios);
        }
        // PUT: api/Usuario/dni
        [HttpPut("{dni}")]
        public async Task<ActionResult<Usuario>> PutUsuario(string dni, UsuarioDTO usuario)
        {
            Usuario? usuarioEntity = _context.Usuario.FirstOrDefault(u => u.DNI == dni);

            if (usuarioEntity == null)
            {
                return NotFound($"Usuario con DNI {dni} no encontrado.");
            }
            // Actualizar las propiedades del usuario existente
            usuarioEntity.Nombre = usuario.Nombre;
            usuarioEntity.Apellido = usuario.Apellido;
            usuarioEntity.CaracteristicaTelefono = usuario.CaracteristicaTelefono;
            usuarioEntity.NumeroTelefono = usuario.NumeroTelefono;
            usuarioEntity.Localidad = usuario.Localidad;
            usuarioEntity.Direccion = usuario.Direccion;

            // Buscar el RolUsuario correspondiente
            RolUsuario? rolUsuarioEntity = _context.RolUsuario.FirstOrDefault(p => p.Descripcion == usuario.RolUsuarioDescripcion);

            if (rolUsuarioEntity == null)
            {
                return BadRequest("RolUsuario no válido.");
            }

            // Asignar el RolUsuario encontrado
            usuarioEntity.RolUsuario = rolUsuarioEntity;

            try
            {
                // Llamar al método para actualizar el usuario en el repositorio
                _context.Update(usuarioEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            // Crear un nuevo objeto con solo las propiedades que deseas devolver
            var usuarioResponse = new
            {
                usuario.DNI,
                usuario.Nombre,
                usuario.Apellido,
                usuario.CaracteristicaTelefono,
                usuario.NumeroTelefono,
                usuario.Localidad,
                usuario.Direccion,
                RolUsuario = new RolUsuarioDTO
                {
                    Descripcion = usuario.RolUsuarioDescripcion
                }
            };

            return Ok(usuarioResponse); // Devuelve el objeto como resultado
        }
        // POST: api/Usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioDTO usuario)
        {
            // Crear la entidad Usuario a partir del DTO
            Usuario usuarioEntity = new Usuario
            {
                DNI = usuario.DNI,
                Password = usuario.Password,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                CaracteristicaTelefono = usuario.CaracteristicaTelefono,
                NumeroTelefono = usuario.NumeroTelefono,
                Localidad = usuario.Localidad,
                Direccion = usuario.Direccion,
                RolUsuario = _context.RolUsuario.FirstOrDefault(p => p.Descripcion == usuario.RolUsuarioDescripcion)
            };

            // Llamar al método para crear el usuario en el repositorio
            await _context.Usuario.AddAsync(usuarioEntity);
            await _context.SaveChangesAsync();

            // Crear un nuevo objeto para devolver que no contenga relaciones cíclicas
            var usuarioResponse = new
            {
                usuarioEntity.DNI,
                usuarioEntity.Nombre,
                usuarioEntity.Apellido,
                usuarioEntity.CaracteristicaTelefono,
                usuarioEntity.NumeroTelefono,
                usuarioEntity.Localidad,
                usuarioEntity.Direccion,
                RolUsuarioDescripcion = new RolUsuarioDTO
                {
                    Descripcion = usuarioEntity.RolUsuario?.Descripcion // Usar el operador null-conditional para evitar NRE
                }
            };

            // Devolver el resultado de la creación
            return CreatedAtAction("GetUsuario", new { dni = usuarioEntity.DNI }, usuarioResponse);
        }
        // DELETE: api/Usuario/dni
        [HttpDelete("{dni}")]
        public async Task<IActionResult> DeleteUsuario(string dni)
        {
            try
            {
                Usuario? usuario = _context.Usuario.FirstOrDefault(u => u.DNI == dni);

                if (usuario == null)
                {
                    return NotFound();
                }

                _context.Remove(usuario);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Capturar el error y devolver un mensaje más detallado
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
    
}
