using Entidades.DTOs.Respuestas;

namespace Logica.Contracts
{
    public interface IRolUsuarioLogic
    {
        Task<List<RolUsuarioDTO>> ObtenerRoles();
    }
}