namespace Habitus.Modelos
{
    public class Comida
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Calorias { get; set; }
        public TipoComida Tipo { get; set; }
        public DateTime Fecha { get; set; }
        public double Cantidad { get; set; }
        
        /*public Comida()
        {
            Id = Guid.NewGuid().ToString();
            Fecha = DateTime.Now;
        }*/
    }
}