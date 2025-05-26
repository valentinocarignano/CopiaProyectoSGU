namespace Entidades.DTOs.Crear
{
    public class CrearMateriaDTO
    {
        public string Nombre { get; set; }
        public string Anio { get; set; }
        public string Modalidad { get; set; }
        public List<int> ProfesoresIDs { get; set; }
        public List<int> DiasHorariosIDs { get; set; }
    }
}