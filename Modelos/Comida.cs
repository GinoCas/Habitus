namespace Habitus.Modelos
{
    public class Comida
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public double Calorias { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public TipoComida TipoComida { get; set; }
        public string AlimentoId { get; set; } = string.Empty;
        public string NombreAlimento { get; set; } = string.Empty;
        public double Cantidad { get; set; }
        public double CaloriasConsumidas { get; set; }
        public List<Alimento> Alimentos { get; set; } = new List<Alimento>();
        
        public Comida()
        {
            Id = Guid.NewGuid().ToString();
            Fecha = DateTime.Now;
        }
    }
}