namespace Entidades.Entities
{
    public class Alumno : EntityBase
    {
        public DateTime FechaIngreso { get; set; }

        public Usuario Usuario { get; set; }
        public ICollection<Materia> Materias { get; set; }
        public ICollection<Examen> Examenes { get; set; }
    }
}