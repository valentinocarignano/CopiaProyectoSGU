using Entidades.Entities;

namespace Logica.Contracts
{
    public interface IUsuarioLogic
    {
        Task AltaUsuario(string dni, string password, string nombre, string apellido, string caracteristicaTelefono, string numeroTelefono, string localidad, string direccion, string rolUsuario, DateTime? fechaContratoIngreso);
        Task BajaUsuario(string documento);
        Task ActualizacionUsuario(string documento, string nombre, string apellido, string caracteristicaTelefono, string numeroTelefono, string localidad, string direccion, string rolUsuario);
        void ActualizacionPassword(Usuario usuario, string nuevaPassword);
        Task<List<Usuario>> ObtenerUsuarios();
        //Task<List<UsuarioListadoDTO>> ObtenerUsuariosParaListado(string? filtroSeleccionado);
        Task<Usuario> ObtenerUsuarioPorDNI(string dni);
        Task<Usuario> ObtenerUsuarioDNIConRelaciones(string dni);
    }
}
