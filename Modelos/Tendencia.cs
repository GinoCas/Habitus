namespace Habitus.Modelos
{
    public class Tendencia
    {
        public string TipoMetrica { get; set; } // Peso, Calorias, Actividad
        public List<double> Valores { get; set; }
        public List<DateTime> Fechas { get; set; }
        public string TendenciaGeneral { get; set; } // Ascendente, Descendente, Estable
        public double PorcentajeCambio { get; set; }
        public string Periodo { get; set; } // Semanal, Mensual, Trimestral

        public Tendencia()
        {
            Valores = new List<double>();
            Fechas = new List<DateTime>();
        }
    }
}