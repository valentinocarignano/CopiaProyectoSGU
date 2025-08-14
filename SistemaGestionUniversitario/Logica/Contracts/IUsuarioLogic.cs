using Entidades.DTOs.Respuestas;

namespace Logica.Contracts
{
    public interface IUsuarioLogic
    {
        Task AltaUsuario(string dni, string password, string nombre, string apellido, string caracteristicaTelefono, string numeroTelefono, string localidad, string direccion, string rolUsuario, DateTime? fechaContratoIngreso);
        Task BajaUsuario(string documento);
        Task<UsuarioDTO> ActualizacionUsuario(string documento, string nombre, string apellido, string caracteristicaTelefono, string numeroTelefono, string localidad, string direccion);
        Task ActualizacionPassword(string dni, string actualPassword, string nuevaPassword);
        Task<List<UsuarioDTO>> ObtenerUsuarios();
        Task<UsuarioDTO> ObtenerUsuarioPorDNI(string dni);
        Task<UsuarioLogInDTO> ValidarUsuario(string usuario, string password);
    }
}