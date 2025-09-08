namespace Habitus.Modelos
{
    public class PerfilUsuario
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
        public string Genero { get; set; }
        public string NivelActividad { get; set; }
        public List<Comida> Comidas { get; set; }
        public List<Actividad> Actividades { get; set; }
        public int Puntos { get; set; }

        public PerfilUsuario()
        {
            Comidas = new List<Comida>();
            Actividades = new List<Actividad>();
        }
    }
}