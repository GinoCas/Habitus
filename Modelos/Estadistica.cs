namespace Habitus.Modelos
{
    public class Estadistica
    {
        public DateTime Fecha { get; set; }
        public string TipoEstadistica { get; set; } // Peso, Calorias, Actividad, etc. TODO: Enum
        public double Valor { get; set; }
        public string Unidad { get; set; } // kg, cal, min, etc.
        public string Periodo { get; set; } // Diario, Semanal, Mensual
    }
}