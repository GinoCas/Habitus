namespace Habitus.Modelos
{
    public class Actividad
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Tipo { get; set; }
        public string TipoActividad { get; set; }
        public double Duracion { get; set; }
        public int DuracionMinutos { get; set; }
        public double CaloriasQuemadas { get; set; }
        public DateTime Fecha { get; set; }
        public string Intensidad { get; set; }
        public string Descripcion { get; set; }
    }
}