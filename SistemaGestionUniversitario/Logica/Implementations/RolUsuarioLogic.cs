using Datos.Repositories.Contracts;
using Entidades.DTOs.Respuestas;
using Entidades.Entities;
using Logica.Contracts;

namespace Logica.Implementations
{
    public class RolUsuarioLogic : IRolUsuarioLogic
    {
        private IRolUsuarioRepository _rolUsuarioRepository;
        
        public RolUsuarioLogic(IRolUsuarioRepository rolUsuarioRepository)
        {
            _rolUsuarioRepository = rolUsuarioRepository;
        }

        public async Task<List<RolUsuarioDTO>> ObtenerRoles()
        {
            try
            {
                List<RolUsuario> listaRoles = (await _rolUsuarioRepository.FindAllAsync()).ToList();

                if (listaRoles == null)
                {
                    return null;
                }

                List<RolUsuarioDTO> listaRolesUsuarioDTO = listaRoles.Select(t => new RolUsuarioDTO
                {
                    ID = t.ID,
                    Descripcion = t.Descripcion,

                }).ToList();

                return listaRolesUsuarioDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            };
        }
    }
}