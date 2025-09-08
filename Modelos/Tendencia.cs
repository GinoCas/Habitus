namespace Habitus.Modelos
{
    public class Tendencia
    {
        public string TipoMetrica { get; set; } // Peso, Calorias, Actividad TODO: Enum
        public List<double> Valores { get; set; }
        public List<DateTime> Fechas { get; set; }
        public string TendenciaGeneral { get; set; } // Ascendente, Descendente, Estable TODO: Enum
        public double PorcentajeCambio { get; set; }
        public string Periodo { get; set; } // Semanal, Mensual, Trimestral TODO: Enum

        public Tendencia()
        {
            Valores = new List<double>();
            Fechas = new List<DateTime>();
        }
    }
}