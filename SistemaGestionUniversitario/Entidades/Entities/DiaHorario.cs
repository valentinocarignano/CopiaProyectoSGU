namespace Entidades.Entities
{
    public class DiaHorario : EntityBase
    {
        public int IdDia { get; set; }
        public int IdHorario { get; set; }


        public ICollection<Examen> Examenes { get; set; }
        public ICollection<Materia> Materias { get; set; }

        #region campos descripcionDia y descripcionHorario
        public string _descripcionDia;
        public string _descripcionHorario;

        public string GetDescripcionDia()
        {
            return _descripcionDia; 
        }

        public void SetDescripcionDia(string descripcion)
        {
            _descripcionDia = descripcion;
        }

        public string GetDescripcionHorario()
        {
            return _descripcionHorario;
        }

        public void SetDescripcionHorario(string descripcion)
        {
            _descripcionHorario = descripcion;
        }
        public void SetDescripcionDiaHorario(DiaHorario diaHorario, string diaDescripcion, string horarioDescripcion)
        {
            diaHorario.SetDescripcionDia(diaDescripcion);
            diaHorario.SetDescripcionHorario(horarioDescripcion);
        }

        #endregion

        public override string ToString()
        {
            return $"{_descripcionDia} {_descripcionHorario}";
        }
    }
}