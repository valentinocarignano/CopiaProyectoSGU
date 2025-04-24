namespace Entidades.Entities
{
    public class Horario : EntityBase
    {
        public string Descripcion { get; set; }

        public ICollection<Dia> Dias { get; set; }

        public override string ToString()
        {
            return $"{Descripcion}";
        }
    }
}