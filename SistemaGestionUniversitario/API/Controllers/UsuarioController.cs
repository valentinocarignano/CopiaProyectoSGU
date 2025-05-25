using Entidades.DTOs;
using Entidades.DTOs.Crear;
using Entidades.DTOs.Modificar;
using Entidades.DTOs.Respuestas;
using Logica.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    namespace API.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class UsuarioController : ControllerBase
        {
            private readonly IUsuarioLogic _usuarioLogic;
            private readonly IRolUsuarioLogic _rolUsuarioLogic;

            public UsuarioController(IUsuarioLogic usuarioLogic, IRolUsuarioLogic rolUsuarioLogic)
            {
                _usuarioLogic = usuarioLogic;
                _rolUsuarioLogic = rolUsuarioLogic;
            }

            // GET: api/Usuarios
            [HttpGet]
            public async Task<IActionResult> GetUsuarios()
            {
                List<UsuarioDTO> usuarios = await _usuarioLogic.ObtenerUsuarios();

                return Ok(usuarios);
            }

            // GET: api/Usuario/DNI
            [HttpGet("{dni}")]
            public async Task<IActionResult> GetUsuarioPorDNI(string dni)
            {
                UsuarioDTO usuario = await _usuarioLogic.ObtenerUsuarioPorDNI(dni);
                
                return Ok(usuario);
            }

            // POST: api/Usuario
            [HttpPost]
            public async Task<IActionResult> PostUsuario([FromBody] CrearUsuarioDTO usuario)
            {
                await _usuarioLogic.AltaUsuario(
                    usuario.DNI, 
                    usuario.Password, 
                    usuario.Nombre, 
                    usuario.Apellido, 
                    usuario.CaracteristicaTelefono, 
                    usuario.NumeroTelefono, 
                    usuario.Localidad, 
                    usuario.Direccion, 
                    usuario.RolUsuarioDescripcion, 
                    usuario.FechaContratoIngreso);

                return Ok("Usuario creado correctamente.");
            }

            // PUT: api/Usuario/dni
            [HttpPut("{dni}")]
            public async Task<IActionResult> PutUsuario(string dni, [FromBody] ModificarUsuarioDTO usuario)
            {
                UsuarioDTO usuarioDTO = await _usuarioLogic.ActualizacionUsuario(
                    dni, 
                    usuario.Nombre, 
                    usuario.Apellido, 
                    usuario.CaracteristicaTelefono, 
                    usuario.NumeroTelefono, 
                    usuario.Localidad, 
                    usuario.Direccion);

                if (usuarioDTO == null)
                {
                    return NotFound();
                }

                return Ok(usuarioDTO);
            }

            //PUT: api/Usuario/actualizarPassword/dni
            [HttpPut("actualizarPassword/{dni}")]
            public async Task<IActionResult> PutUsuarioPassword(string dni, [FromBody] ModificarUsuarioPasswordDTO passwordDto)
            {
                await _usuarioLogic.ActualizacionPassword(dni, passwordDto.ActualPassword, passwordDto.NuevaPassword);
                
                return Ok("Contraseña actualizada correctamente.");
            }

            // DELETE: api/Usuario/dni
            [HttpDelete("{dni}")]
            public async Task<IActionResult> DeleteUsuario(string dni)
            {
                try
                {
                    await _usuarioLogic.BajaUsuario(dni);
                    
                    return Ok($"Usuario con DNI {dni} eliminado correctamente.");
                }
                catch(Exception ex)
                {
                    return BadRequest(ex);
                }
            }
        }
    }
}