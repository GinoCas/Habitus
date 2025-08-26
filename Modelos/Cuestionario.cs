namespace Habitus.Modelos
{
    public class Cuestionario
    {
        public List<Pregunta> Preguntas { get; set; }
        public int PuntosObtenidos { get; set; }
        public DateTime FechaCompletado { get; set; }
        public bool Completado { get; set; }

        public Cuestionario()
        {
            Preguntas = new List<Pregunta>();
        }
    }

    public class Pregunta
    {
        public string Texto { get; set; }
        public List<string> Opciones { get; set; }
        public string RespuestaSeleccionada { get; set; }
        public int PuntosAsignados { get; set; }

        public Pregunta()
        {
            Opciones = new List<string>();
        }
    }
}