using Habitus.Modelos;
using Habitus.Modelos.Enums;
using Habitus.Utilidades;

namespace Habitus.Controladores
{
    public class ControladorComida
    {
        private readonly GestorJson<Comida> _catalogoComida;
        private readonly GestorJson<Comida> _comidasConsumidas;

        public ControladorComida()
        {
            _catalogoComida = new GestorJson<Comida>("catalogoComida.json", true);
            _comidasConsumidas = new GestorJson<Comida>("comidasConsumidas.json", false);
        }

        public void RegistrarComida(string nombre, double cantidad, TipoComida tipo, DateTime fecha)
        {
            var comida = _catalogoComida.GetAll().FirstOrDefault(a => a.Nombre.ToLower() == nombre.ToLower());
            double calorias = 0;

            if (comida != null)
            {
                calorias = (comida.Calorias * cantidad) / 100;
            }
            else
            {
                // Si no está en la base de datos, permitir ingreso manual
                calorias = cantidad; // Asumiendo que el usuario ingresa las calorías directamente
            }
			var comidaRegistrada = new Comida
			{
				Id = Guid.NewGuid().ToString(),
				Nombre = nombre,
				Cantidad = cantidad,
				Calorias = calorias,
				Tipo = tipo,
				Fecha = fecha
			};
			_comidasConsumidas.Add(comidaRegistrada);
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
            return _catalogoComida.GetAll().Where(a => a.Nombre.ToLower().Contains(termino.ToLower()))
                                     .Take(10)
                                     .ToList();
        }
    }
}