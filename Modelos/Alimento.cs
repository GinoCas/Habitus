namespace Habitus.Modelos
{
    public class Alimento
    {
        public string Nombre { get; set; }
        public double CaloriasPor100g { get; set; }
        public double Proteinas { get; set; }
        public double Carbohidratos { get; set; }
        public double Grasas { get; set; }
        public double Fibra { get; set; }
        public string Categoria { get; set; } // Frutas, Verduras, Carnes, etc.
    }
}