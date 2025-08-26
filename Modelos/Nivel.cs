namespace Habitus.Modelos
{
    public class Nivel
    {
        public int NumeroNivel { get; set; }
        public string Nombre { get; set; }
        public int PuntosRequeridos { get; set; }
        public string Descripcion { get; set; }
        public List<string> BeneficiosDesbloqueados { get; set; }
        public string Insignia { get; set; }

        public Nivel()
        {
            BeneficiosDesbloqueados = new List<string>();
        }
    }
}