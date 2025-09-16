using Habitus.Modelos;
using Habitus.Modelos.Enums;
using Habitus.Utilidades;

namespace Habitus.Controladores
{
    public class ControladorActividad
    {
        private readonly GestorJson<Actividad> _actividades;
        public ControladorActividad()
        {
            _actividades = new GestorJson<Actividad>("actividadesRealizadas.json", false);
        }

        public void RegistrarActividad(TipoActividad tipo, int duracion, ActividadIntensidad intensidad)
        {
            var actividad = new Actividad
            {
                Tipo = tipo,
                DuracionMinutos = duracion,
                Intensidad = intensidad,
                Fecha = DateTime.Now,
                //CaloriasQuemadas = CalcularCaloriasQuemadas(tipo, duracion, intensidad)
            };
            _actividades.Add(actividad);
        }

        public List<Actividad> ObtenerActividades()
        {
            
            return _actividades.GetAll();
        }

        public List<Actividad> ObtenerActividadesPorFecha(DateTime fecha)
        {
            return _actividades.GetAll().Where(a => a.Fecha.Date == fecha.Date).ToList();
        }

        public List<Actividad> ObtenerActividadesPorPeriodo(DateTime fechaInicio, DateTime fechaFin)
        {
            return _actividades.GetAll().Where(a => a.Fecha.Date >= fechaInicio.Date && a.Fecha.Date <= fechaFin.Date).ToList();
        }

        public double ObtenerTotalCaloriasQuemadas(DateTime fecha)
        {
            return _actividades.GetAll().Where(a => a.Fecha.Date == fecha.Date)
                              .Sum(a => a.CaloriasQuemadas);
        }

        public double ObtenerTotalMinutosActividad(DateTime fecha)
        {
            return _actividades.GetAll().Where(a => a.Fecha.Date == fecha.Date)
                              .Sum(a => a.DuracionMinutos);
        }

        public double CalcularCaloriasQuemadas(TipoActividad tipo, ActividadIntensidad intensidad, int duracion, double peso)
        {
            // Cálculo básico de calorías quemadas por minuto según tipo e intensidad
            double caloriasPorMinuto = tipo switch
            {
                TipoActividad.Cardio => intensidad switch
                {
                    ActividadIntensidad.Baja => 5,
                    ActividadIntensidad.Moderada => 8,
                    ActividadIntensidad.Alta => 12,
                    _ => 6
                },
                TipoActividad.Fuerza => intensidad switch
                {
                    ActividadIntensidad.Baja => 4,
                    ActividadIntensidad.Moderada => 6,
                    ActividadIntensidad.Alta => 9,
                    _ => 5
                },
                TipoActividad.Caminar => 3.5,
                TipoActividad.Natacion => 10,
                TipoActividad.Ciclismo => 7,
                TipoActividad.Yoga => 2.5,
                _ => 4
            };

            // Ajustar por peso del usuario (factor básico)
            double factorPeso = peso / 70.0; // 70kg como peso base
            return caloriasPorMinuto * duracion * factorPeso;
        }
    }
}