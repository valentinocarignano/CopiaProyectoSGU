namespace Front.Models.Respuestas
{
    public class AsistenciaFront
    {
        public int ID { get; set; }
        public string DniAlumno { get; set; }
        public string NombreMateria { get; set; }
        public bool Estado { get; set; }
        public DateTime Fecha { get; set; }
    }
}