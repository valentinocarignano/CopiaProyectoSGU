namespace Entidades.Entities
{
    public class Dia : EntityBase
    {
        public string Descripcion {  get; set; }

        public ICollection<Horario> Horarios { get; set; }

        public override string ToString()
        {
            return $"{Descripcion}";
        }
    }
}