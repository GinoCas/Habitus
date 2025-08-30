using Habitus.Modelos;

namespace Habitus.Controladores
{
    public class ControladorActividad
    {
        private List<Actividad> _actividades;
        private string _rutaArchivo = "actividades.json";

        public ControladorActividad()
        {
            CargarActividades();
        }

        public void RegistrarActividad(string tipo, double duracion, string intensidad)
        {
            var actividad = new Actividad
            {
                Tipo = tipo,
                Duracion = duracion,
                Intensidad = intensidad,
                Fecha = DateTime.Now,
                CaloriasQuemadas = CalcularCaloriasQuemadas(tipo, duracion, intensidad)
            };

            _actividades.Add(actividad);
            GuardarActividades();
        }

        public List<Actividad> ObtenerActividades()
        {
            CargarActividades();
            return _actividades;
        }

        public List<Actividad> ObtenerActividadesPorFecha(DateTime fecha)
        {
            return _actividades.Where(a => a.Fecha.Date == fecha.Date).ToList();
        }

        public List<Actividad> ObtenerActividadesPorPeriodo(DateTime fechaInicio, DateTime fechaFin)
        {
            return _actividades.Where(a => a.Fecha.Date >= fechaInicio.Date && a.Fecha.Date <= fechaFin.Date).ToList();
        }

        public double ObtenerTotalCaloriasQuemadas(DateTime fecha)
        {
            return _actividades.Where(a => a.Fecha.Date == fecha.Date)
                              .Sum(a => a.CaloriasQuemadas);
        }

        public double ObtenerTotalMinutosActividad(DateTime fecha)
        {
            return _actividades.Where(a => a.Fecha.Date == fecha.Date)
                              .Sum(a => a.Duracion);
        }

        public double CalcularCaloriasQuemadas(string tipo, double duracion, string intensidad)
        {
            // Cálculo básico de calorías quemadas por minuto según tipo e intensidad
            double caloriasPorMinuto = tipo.ToLower() switch
            {
                "cardio" => intensidad.ToLower() switch
                {
                    "baja" => 5,
                    "media" => 8,
                    "alta" => 12,
                    _ => 6
                },
                "fuerza" => intensidad.ToLower() switch
                {
                    "baja" => 4,
                    "media" => 6,
                    "alta" => 9,
                    _ => 5
                },
                "caminata" => 3.5,
                "natacion" => 10,
                "ciclismo" => 7,
                "yoga" => 2.5,
                _ => 4
            };

            return caloriasPorMinuto * duracion;
        }

        public double CalcularCaloriasQuemadas(TipoActividad tipo, Intensidad intensidad, double duracion, double peso)
        {
            // Cálculo básico de calorías quemadas por minuto según tipo e intensidad
            double caloriasPorMinuto = tipo switch
            {
                TipoActividad.Cardio => intensidad switch
                {
                    Intensidad.Baja => 5,
                    Intensidad.Moderada => 8,
                    Intensidad.Alta => 12,
                    _ => 6
                },
                TipoActividad.Fuerza => intensidad switch
                {
                    Intensidad.Baja => 4,
                    Intensidad.Moderada => 6,
                    Intensidad.Alta => 9,
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

        private void CargarActividades()
        {
            try
            {
                if (File.Exists(_rutaArchivo))
                {
                    string json = File.ReadAllText(_rutaArchivo);
                    _actividades = System.Text.Json.JsonSerializer.Deserialize<List<Actividad>>(json) ?? new List<Actividad>();
                }
                else
                {
                    _actividades = new List<Actividad>();
                }
            }
            catch (Exception ex)
            {
                _actividades = new List<Actividad>();
            }
        }

        private void GuardarActividades()
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(_actividades, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_rutaArchivo, json);
            }
            catch (Exception ex)
            {
                // Log error
            }
        }
    }
}