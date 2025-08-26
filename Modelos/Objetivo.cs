namespace Habitus.Modelos
{
    public class Objetivo
    {
        public string Descripcion { get; set; }
        public double ValorObjetivo { get; set; }
        public double ValorActual { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Completado { get; set; }
        public string TipoObjetivo { get; set; } // Peso, Calorias, Actividad, etc.

        public double PorcentajeCompletado
        {
            get
            {
                if (ValorObjetivo == 0) return 0;
                return (ValorActual / ValorObjetivo) * 100;
            }
        }
    }
}