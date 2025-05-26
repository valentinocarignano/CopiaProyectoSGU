namespace Entidades.DTOs.Respuestas
{
    public class AlumnoDTO
    {
        public int ID { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}