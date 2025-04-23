using Entidades.DTOs;

namespace Logica.Contracts
{
    public interface IRolUsuarioLogic
    {
        Task<List<RolUsuarioDTO>> ObtenerRoles();
    }
}
