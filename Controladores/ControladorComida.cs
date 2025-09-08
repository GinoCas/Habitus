using Habitus.Modelos;
using Habitus.Modelos.Enums;
using Habitus.Utilidades;

namespace Habitus.Controladores
{
    public class ControladorComida
    {
        private readonly GestorJson<Comida> _catalogoComidas;
        private readonly GestorJson<Comida> _comidasConsumidas;

        public ControladorComida()
        {
            _catalogoComidas = new GestorJson<Comida>("catalogoComidas.json");
            _comidasConsumidas = new GestorJson<Comida>("comidasConsumidas.json");
        }

        public void RegistrarComida(string nombre, double cantidad, TipoComida tipo)
        {
            /*var alimento = _catalogoComidas.FirstOrDefault(a => a.Nombre.ToLower() == nombreAlimento.ToLower());
            double calorias = 0;

            if (alimento != null)
            {
                calorias = (alimento.CaloriasPor100g * cantidad) / 100;
            }
            else
            {
                // Si no está en la base de datos, permitir ingreso manual
                calorias = cantidad; // Asumiendo que el usuario ingresa las calorías directamente
            }

            var comida = new Comida
            {
                Nombre = nombreAlimento,
                Calorias = calorias,
                Tipo = tipoComida,
                Fecha = DateTime.Now
            };

            _comidas.Add(comida);
            GuardarComidas();*/
        }

        public List<Comida> ObtenerComidasPorFecha(DateTime fecha)
        {
            return _comidasConsumidas.GetAll().Where(c => c.Fecha.Date == fecha.Date).ToList();
        }

        public List<Comida> ObtenerComidasPorTipo(DateTime fecha, TipoComida tipoComida)
        {
            return _comidasConsumidas.GetAll().Where(c => c.Fecha.Date == fecha.Date && c.Tipo == tipoComida).ToList();
        }

        public List<Comida> ObtenerComidasPorPeriodo(DateTime fechaInicio, DateTime fechaFin)
        {
            return _comidasConsumidas.GetAll().Where(c => c.Fecha.Date >= fechaInicio.Date && c.Fecha.Date <= fechaFin.Date).ToList();
        }

        public double ObtenerTotalCaloriasConsumidas(DateTime fecha)
        {
            return _comidasConsumidas.GetAll().Where(c => c.Fecha.Date == fecha.Date)
                          .Sum(c => c.Calorias);
        }

        public List<Comida> BuscarComidaPorTermino(string termino)
        {
            /*return _baseDatosAlimentos.Where(a => a.Nombre.ToLower().Contains(termino.ToLower()))
                                     .Take(10)
                                     .ToList();*/
            return new List<Comida>();
        }

        /*private List<Alimento> InicializarAlimentosBasicos()
        {
            return new List<Alimento>
            {
                new Alimento { Nombre = "Manzana", CaloriasPor100g = 52, Proteinas = 0.3, Carbohidratos = 14, Grasas = 0.2, Fibra = 2.4, Categoria = "Frutas" },
                new Alimento { Nombre = "Plátano", CaloriasPor100g = 89, Proteinas = 1.1, Carbohidratos = 23, Grasas = 0.3, Fibra = 2.6, Categoria = "Frutas" },
                new Alimento { Nombre = "Pollo (pechuga)", CaloriasPor100g = 165, Proteinas = 31, Carbohidratos = 0, Grasas = 3.6, Fibra = 0, Categoria = "Carnes" },
                new Alimento { Nombre = "Arroz blanco", CaloriasPor100g = 130, Proteinas = 2.7, Carbohidratos = 28, Grasas = 0.3, Fibra = 0.4, Categoria = "Cereales" },
                new Alimento { Nombre = "Brócoli", CaloriasPor100g = 34, Proteinas = 2.8, Carbohidratos = 7, Grasas = 0.4, Fibra = 2.6, Categoria = "Verduras" }
            };
        }*/
    }
}