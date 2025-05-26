namespace Entidades.DTOs.Modificar
{
    public class ModificarUsuarioDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string CaracteristicaTelefono { get; set; }
        public string NumeroTelefono { get; set; }

        public string Localidad { get; set; }
        public string Direccion { get; set; }
    }
}