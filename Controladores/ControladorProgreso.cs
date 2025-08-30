using Habitus.Modelos;

namespace Habitus.Controladores
{
    public class ControladorProgreso
    {
        private List<Progreso> _registrosProgreso;
        private string _rutaArchivo = "progreso.json";

        public ControladorProgreso()
        {
            CargarProgreso();
        }

        public void RegistrarProgresoDiario(double peso, double caloriasConsumidas, double caloriasQuemadas, 
                                           int minutosActividad, int pasos, string estadoAnimo, string notas)
        {
            CargarProgreso();
            var progreso = new Progreso
            {
                Fecha = DateTime.Now.Date,
                Peso = peso,
                CaloriasConsumidas = caloriasConsumidas,
                CaloriasQuemadas = caloriasQuemadas,
                MinutosActividad = minutosActividad,
                PasosRealizados = pasos,
                EstadoAnimo = estadoAnimo,
                Notas = notas,
                PuntosGanados = CalcularPuntosDiarios(caloriasConsumidas, caloriasQuemadas, minutosActividad, pasos)
            };

            // Verificar si ya existe un registro para hoy
            var registroExistente = _registrosProgreso.FirstOrDefault(p => p.Fecha.Date == DateTime.Now.Date);
            if (registroExistente != null)
            {
                _registrosProgreso.Remove(registroExistente);
            }

            _registrosProgreso.Add(progreso);
            GuardarProgreso();
        }

        public void RegistrarConsumoCalorias(DateTime fecha, double calorias)
        {
            CargarProgreso();
            var registroExistente = _registrosProgreso.FirstOrDefault(p => p.Fecha.Date == fecha.Date);
            if (registroExistente != null)
            {
                registroExistente.CaloriasConsumidas += calorias;
            }
            else
            {
                var progreso = new Progreso
                {
                    Fecha = fecha.Date,
                    CaloriasConsumidas = calorias
                };
                _registrosProgreso.Add(progreso);
            }
            GuardarProgreso();
        }

        public void RegistrarCaloriasQuemadas(DateTime fecha, double calorias)
        {
            CargarProgreso();
            var registroExistente = _registrosProgreso.FirstOrDefault(p => p.Fecha.Date == fecha.Date);
            if (registroExistente != null)
            {
                registroExistente.CaloriasQuemadas += calorias;
            }
            else
            {
                var progreso = new Progreso
                {
                    Fecha = fecha.Date,
                    CaloriasQuemadas = calorias
                };
                _registrosProgreso.Add(progreso);
            }
            GuardarProgreso();
        }

        public void RegistrarMinutosActividad(DateTime fecha, int minutos)
        {
            CargarProgreso();
            var registroExistente = _registrosProgreso.FirstOrDefault(p => p.Fecha.Date == fecha.Date);
            if (registroExistente != null)
            {
                registroExistente.MinutosActividad += minutos;
            }
            else
            {
                var progreso = new Progreso
                {
                    Fecha = fecha.Date,
                    MinutosActividad = minutos
                };
                _registrosProgreso.Add(progreso);
            }
            GuardarProgreso();
        }

        public Progreso ObtenerProgresoPorFecha(DateTime fecha)
        {
            CargarProgreso();
            return _registrosProgreso.FirstOrDefault(p => p.Fecha.Date == fecha.Date);
        }

        public List<Progreso> ObtenerProgresoPorPeriodo(DateTime fechaInicio, DateTime fechaFin)
        {
            CargarProgreso();
            return _registrosProgreso.Where(p => p.Fecha.Date >= fechaInicio.Date && p.Fecha.Date <= fechaFin.Date)
                                    .OrderBy(p => p.Fecha)
                                    .ToList();
        }

        public Resumen GenerarResumen(DateTime fechaInicio, DateTime fechaFin)
        {
            var registros = ObtenerProgresoPorPeriodo(fechaInicio, fechaFin);
            
            if (!registros.Any())
                return new Resumen { FechaInicio = fechaInicio, FechaFin = fechaFin };

            var resumen = new Resumen
            {
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                PromedioCaloriasConsumidas = registros.Average(r => r.CaloriasConsumidas),
                PromedioCaloriasQuemadas = registros.Average(r => r.CaloriasQuemadas),
                TotalMinutosActividad = registros.Sum(r => r.MinutosActividad),
                PuntosGanados = registros.Sum(r => r.PuntosGanados)
            };

            // Calcular cambio de peso
            var primerRegistro = registros.OrderBy(r => r.Fecha).FirstOrDefault();
            var ultimoRegistro = registros.OrderByDescending(r => r.Fecha).FirstOrDefault();
            
            if (primerRegistro != null && ultimoRegistro != null)
            {
                resumen.CambioPeso = ultimoRegistro.Peso - primerRegistro.Peso;
            }

            return resumen;
        }

        public List<Tendencia> GenerarTendencias(DateTime fechaInicio, DateTime fechaFin)
        {
            var registros = ObtenerProgresoPorPeriodo(fechaInicio, fechaFin);
            var tendencias = new List<Tendencia>();

            if (registros.Count < 2)
                return tendencias;

            // Tendencia de peso
            var tendenciaPeso = new Tendencia
            {
                TipoMetrica = "Peso",
                Valores = registros.Select(r => r.Peso).ToList(),
                Fechas = registros.Select(r => r.Fecha).ToList(),
                Periodo = "Personalizado"
            };
            tendenciaPeso.TendenciaGeneral = CalcularTendenciaGeneral(tendenciaPeso.Valores);
            tendenciaPeso.PorcentajeCambio = CalcularPorcentajeCambio(tendenciaPeso.Valores.First(), tendenciaPeso.Valores.Last());
            tendencias.Add(tendenciaPeso);

            // Tendencia de calorías
            var tendenciaCalorias = new Tendencia
            {
                TipoMetrica = "Calorías Consumidas",
                Valores = registros.Select(r => r.CaloriasConsumidas).ToList(),
                Fechas = registros.Select(r => r.Fecha).ToList(),
                Periodo = "Personalizado"
            };
            tendenciaCalorias.TendenciaGeneral = CalcularTendenciaGeneral(tendenciaCalorias.Valores);
            tendenciaCalorias.PorcentajeCambio = CalcularPorcentajeCambio(tendenciaCalorias.Valores.First(), tendenciaCalorias.Valores.Last());
            tendencias.Add(tendenciaCalorias);

            // Tendencia de actividad
            var tendenciaActividad = new Tendencia
            {
                TipoMetrica = "Minutos de Actividad",
                Valores = registros.Select(r => (double)r.MinutosActividad).ToList(),
                Fechas = registros.Select(r => r.Fecha).ToList(),
                Periodo = "Personalizado"
            };
            tendenciaActividad.TendenciaGeneral = CalcularTendenciaGeneral(tendenciaActividad.Valores);
            tendenciaActividad.PorcentajeCambio = CalcularPorcentajeCambio(tendenciaActividad.Valores.First(), tendenciaActividad.Valores.Last());
            tendencias.Add(tendenciaActividad);

            return tendencias;
        }

        private int CalcularPuntosDiarios(double caloriasConsumidas, double caloriasQuemadas, int minutosActividad, int pasos)
        {
            int puntos = 0;

            // Puntos por balance calórico
            double balanceCalorico = caloriasConsumidas - caloriasQuemadas;
            if (balanceCalorico >= -500 && balanceCalorico <= 500) // Balance saludable
                puntos += 20;
            else if (balanceCalorico < -500) // Déficit muy alto
                puntos += 10;
            else // Superávit muy alto
                puntos += 5;

            // Puntos por actividad física
            if (minutosActividad >= 60) puntos += 30;
            else if (minutosActividad >= 30) puntos += 20;
            else if (minutosActividad >= 15) puntos += 10;

            // Puntos por pasos
            if (pasos >= 10000) puntos += 25;
            else if (pasos >= 7500) puntos += 20;
            else if (pasos >= 5000) puntos += 15;
            else if (pasos >= 2500) puntos += 10;

            return puntos;
        }

        private string CalcularTendenciaGeneral(List<double> valores)
        {
            if (valores.Count < 2) return "Estable";

            double primerValor = valores.First();
            double ultimoValor = valores.Last();
            double diferencia = ultimoValor - primerValor;
            double porcentajeCambio = Math.Abs(diferencia / primerValor) * 100;

            if (porcentajeCambio < 5) return "Estable";
            return diferencia > 0 ? "Ascendente" : "Descendente";
        }

        private double CalcularPorcentajeCambio(double valorInicial, double valorFinal)
        {
            if (valorInicial == 0) return 0;
            return ((valorFinal - valorInicial) / valorInicial) * 100;
        }

        private void CargarProgreso()
        {
            try
            {
                if (File.Exists(_rutaArchivo))
                {
                    string json = File.ReadAllText(_rutaArchivo);
                    _registrosProgreso = System.Text.Json.JsonSerializer.Deserialize<List<Progreso>>(json) ?? new List<Progreso>();
                }
                else
                {
                    _registrosProgreso = new List<Progreso>();
                }
            }
            catch (Exception ex)
            {
                _registrosProgreso = new List<Progreso>();
            }
        }

        private void GuardarProgreso()
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(_registrosProgreso, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_rutaArchivo, json);
            }
            catch (Exception ex)
            {
                // Log error
            }
        }

        public void RegistrarProgreso(string estadoAnimo, string notas)
        {
            var progreso = new Progreso
            {
                Fecha = DateTime.Now.Date,
                EstadoAnimo = estadoAnimo,
                Notas = notas
            };

            // Verificar si ya existe un registro para hoy
            var registroExistente = _registrosProgreso.FirstOrDefault(p => p.Fecha.Date == DateTime.Now.Date);
            if (registroExistente != null)
            {
                registroExistente.EstadoAnimo = estadoAnimo;
                registroExistente.Notas = notas;
            }
            else
            {
                _registrosProgreso.Add(progreso);
            }

            GuardarProgreso();
        }
    }
}