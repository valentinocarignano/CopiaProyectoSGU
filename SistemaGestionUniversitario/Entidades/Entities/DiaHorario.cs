namespace Entidades.Entities
{
    public class DiaHorario : EntityBase
    {
        public int IdDia { get; set; }
        public int IdHorario { get; set; }

        public ICollection<Examen> Examenes { get; set; }
        public ICollection<Materia> Materias { get; set; }
    }
}