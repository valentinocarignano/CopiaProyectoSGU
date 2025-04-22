namespace Entidades.Entities
{
    public class DiaHorarioMateria : EntityBase
    {
        public int IdMateria {  get; set; }
        public int IdDiaHorario { get; set; }

        public ICollection<Inscripcion> Inscripciones { get; set; } 
    }
}