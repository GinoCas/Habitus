namespace Habitus.Modelos
{
    public class Resumen
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public double PromedioCaloriasConsumidas { get; set; }
        public double PromedioCaloriasQuemadas { get; set; }
        public int TotalMinutosActividad { get; set; }
        public double CambioPeso { get; set; }
        public int RetosCompletados { get; set; }
        public int PuntosGanados { get; set; }
        public List<string> LogrosAlcanzados { get; set; }

        public Resumen()
        {
            LogrosAlcanzados = new List<string>();
        }
    }
}