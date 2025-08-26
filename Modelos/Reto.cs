namespace Habitus.Modelos
{
    public class Reto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int PuntosRecompensa { get; set; }
        public int NivelRequerido { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Completado { get; set; }
        public string TipoReto { get; set; } // Ejercicio, Alimentacion, Habito
        public string Dificultad { get; set; } // Facil, Medio, Dificil
    }
}