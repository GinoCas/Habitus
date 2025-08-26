namespace Habitus.Modelos
{
    public class ReporteProgreso
    {
        public DateTime FechaGeneracion { get; set; }
        public string TipoReporte { get; set; } // Diario, Semanal, Mensual
        public Resumen ResumenPeriodo { get; set; }
        public List<Tendencia> Tendencias { get; set; }
        public List<Objetivo> ObjetivosEvaluados { get; set; }
        public List<Reto> RetosActivos { get; set; }
        public string Recomendaciones { get; set; }
        public int NivelActual { get; set; }
        public int PuntosActuales { get; set; }

        public ReporteProgreso()
        {
            Tendencias = new List<Tendencia>();
            ObjetivosEvaluados = new List<Objetivo>();
            RetosActivos = new List<Reto>();
        }
    }
}