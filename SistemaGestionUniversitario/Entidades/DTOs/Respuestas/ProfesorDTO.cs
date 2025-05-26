namespace Entidades.DTOs.Respuestas
{
    public class ProfesorDTO
    {
        public int ID { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaInicioContrato { get; set; }
    }
}