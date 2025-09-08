namespace Habitus.Modelos
{
    public class Progreso
    {
        public DateTime Fecha { get; set; }
        public double Peso { get; set; }
        public double CaloriasConsumidas { get; set; }
        public double CaloriasQuemadas { get; set; }
        public int MinutosActividad { get; set; }
        public int PasosRealizados { get; set; }
        public string Notas { get; set; }
        public int PuntosGanados { get; set; }

        public double BalanceCalorico
        {
            get
            {
                return CaloriasConsumidas - CaloriasQuemadas;
            }
        }
    }
}