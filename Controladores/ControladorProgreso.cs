using Habitus.Modelos;
using Habitus.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Habitus.Controladores
{
    public class ControladorProgreso
    {
        private GestorJson<Progreso> _progreso;

        public ControladorProgreso()
        {
            _progreso = new GestorJson<Progreso>("progreso.json", false);
        }

        public void RegistrarProgresoDiario(double peso, double caloriasConsumidas, double caloriasQuemadas,
                                           int minutosActividad, int pasos, string notas)
        {
            var progreso = new Progreso
            {
                Fecha = DateTime.Now.Date,
                Peso = peso,
                CaloriasConsumidas = caloriasConsumidas,
                CaloriasQuemadas = caloriasQuemadas,
                MinutosActividad = minutosActividad,
                PasosRealizados = pasos,
                Notas = notas,
                PuntosGanados = CalcularPuntosDiarios(caloriasConsumidas, caloriasQuemadas, minutosActividad, pasos)
            };

            // Eliminar cualquier progreso existente del día
            _progreso.Delete(p => p.Fecha.Date == DateTime.Now.Date);

            // Agregar el nuevo progreso
            _progreso.Add(progreso);
        }

        public void RegistrarConsumoCalorias(DateTime fecha, double calorias)
        {
            var registros = _progreso.GetAll();
            var registroExistente = registros.FirstOrDefault(p => p.Fecha.Date == fecha.Date);

            if (registroExistente != null)
            {
                registroExistente.CaloriasConsumidas += calorias;
                _progreso.Delete(p => p.Fecha.Date == fecha.Date);
                _progreso.Add(registroExistente);
            }
            else
            {
                var nuevoProgreso = new Progreso
                {
                    Fecha = fecha.Date,
                    CaloriasConsumidas = calorias
                };
                _progreso.Add(nuevoProgreso);
            }
        }

        public void RegistrarCaloriasQuemadas(DateTime fecha, double calorias)
        {
            var registros = _progreso.GetAll();
            var registroExistente = registros.FirstOrDefault(p => p.Fecha.Date == fecha.Date);

            if (registroExistente != null)
            {
                registroExistente.CaloriasQuemadas += calorias;
                _progreso.Delete(p => p.Fecha.Date == fecha.Date);
                _progreso.Add(registroExistente);
            }
            else
            {
                var nuevoProgreso = new Progreso
                {
                    Fecha = fecha.Date,
                    CaloriasQuemadas = calorias
                };
                _progreso.Add(nuevoProgreso);
            }
        }

        public void RegistrarMinutosActividad(DateTime fecha, int minutos)
        {
            var registros = _progreso.GetAll();
            var registroExistente = registros.FirstOrDefault(p => p.Fecha.Date == fecha.Date);

            if (registroExistente != null)
            {
                registroExistente.MinutosActividad += minutos;
                _progreso.Delete(p => p.Fecha.Date == fecha.Date);
                _progreso.Add(registroExistente);
            }
            else
            {
                var nuevoProgreso = new Progreso
                {
                    Fecha = fecha.Date,
                    MinutosActividad = minutos
                };
                _progreso.Add(nuevoProgreso);
            }
        }

        public Progreso ObtenerProgresoPorFecha(DateTime fecha)
        {
            return _progreso.GetAll().FirstOrDefault(p => p.Fecha.Date == fecha.Date);
        }

        public List<Progreso> ObtenerProgresoPorPeriodo(DateTime fechaInicio, DateTime fechaFin)
        {
            return _progreso.GetAll()
                            .Where(p => p.Fecha.Date >= fechaInicio.Date && p.Fecha.Date <= fechaFin.Date)
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
            tendencias.Add(CrearTendencia("Peso", registros.Select(r => r.Peso).ToList(), registros.Select(r => r.Fecha).ToList()));

            // Tendencia de calorías consumidas
            tendencias.Add(CrearTendencia("Calorías Consumidas", registros.Select(r => r.CaloriasConsumidas).ToList(), registros.Select(r => r.Fecha).ToList()));

            // Tendencia de minutos de actividad
            tendencias.Add(CrearTendencia("Minutos de Actividad", registros.Select(r => (double)r.MinutosActividad).ToList(), registros.Select(r => r.Fecha).ToList()));

            return tendencias;
        }

        private Tendencia CrearTendencia(string tipo, List<double> valores, List<DateTime> fechas)
        {
            var tendencia = new Tendencia
            {
                TipoMetrica = tipo,
                Valores = valores,
                Fechas = fechas,
                Periodo = "Personalizado",
                TendenciaGeneral = CalcularTendenciaGeneral(valores),
                PorcentajeCambio = CalcularPorcentajeCambio(valores.First(), valores.Last())
            };
            return tendencia;
        }

        private int CalcularPuntosDiarios(double caloriasConsumidas, double caloriasQuemadas, int minutosActividad, int pasos)
        {
            int puntos = 0;

            // Puntos por balance calórico
            double balanceCalorico = caloriasConsumidas - caloriasQuemadas;
            if (balanceCalorico >= -500 && balanceCalorico <= 500) puntos += 20;
            else if (balanceCalorico < -500) puntos += 10;
            else puntos += 5;

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
    }
}
