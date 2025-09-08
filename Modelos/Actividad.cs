namespace Habitus.Modelos
{
    public class Actividad
    {
        public int Id { get; set; }
        public TipoActividad Tipo { get; set; }
        public int DuracionMinutos { get; set; }
        public double CaloriasQuemadas { get; set; }
        public DateTime Fecha { get; set; }
        public ActividadIntensidad Intensidad { get; set; }
        public string Descripcion { get; set; }
    }
}