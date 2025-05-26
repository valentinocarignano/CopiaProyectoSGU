namespace Entidades.DTOs
{
    public class ModificarUsuarioPasswordDTO
    {
        public string ActualPassword { get; set; } = string.Empty;
        public string NuevaPassword { get; set; } = string.Empty;
    }
}