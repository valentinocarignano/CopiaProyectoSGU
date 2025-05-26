namespace Entidades.DTOs.Respuestas
{
    public class UsuarioDTO
    {
        public int ID { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string CaracteristicaTelefono { get; set; }
        public string NumeroTelefono { get; set; }

        public string Localidad { get; set; }
        public string Direccion { get; set; }

        public string RolUsuarioDescripcion { get; set; }
    }
}