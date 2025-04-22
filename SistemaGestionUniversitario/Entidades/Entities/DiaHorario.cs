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

        public string ObtenerDescripcionDia()
        {
            return _descripcionDia; 
        }

        public void EstablecerDescripcionDia(string descripcion)
        {
            _descripcionDia = descripcion;
        }

        public string ObtenerDescripcionHorario()
        {
            return _descripcionHorario;
        }

        public void EstablecerDescripcionHorario(string descripcion)
        {
            _descripcionHorario = descripcion;
        }
        public void AsignarDescripcionDiaHorario(DiaHorario diaHorario, string diaDescripcion, string horarioDescripcion)
        {
            diaHorario.EstablecerDescripcionDia(diaDescripcion);
            diaHorario.EstablecerDescripcionHorario(horarioDescripcion);
        }

        #endregion

        public override string ToString()
        {
            return $"{_descripcionDia} {_descripcionHorario}";
        }
    }
}