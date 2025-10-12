using Habitus.Modelos;
using Habitus.Utilidades;
using Microsoft.Win32;
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

        public void RegistrarConsumoCalorias(DateTime fecha, double calorias)
        {
            var registros = _progreso.GetAll();
            var registroExistente = registros.FirstOrDefault(p => p.Fecha.Date == fecha.Date);

            if (registroExistente != null)
            {
                _progreso.Update(p => p.Fecha.Date == fecha.Date, p => p.CaloriasConsumidas += calorias);
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
                _progreso.Update(p => p.Fecha.Date == fecha.Date, p => p.CaloriasQuemadas += calorias);
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

        public void RegistrarPuntos(DateTime fecha, int puntos)
        {
            var registro = ObtenerProgresoPorFecha(fecha);
            if (registro != null)
            {
                _progreso.Update(p => p.Fecha.Date == fecha.Date, p => p.PuntosGanados += puntos);
            }
            else
            {
                MessageBox.Show("No existe un progreso para la fecha:", fecha.ToString());
            }
        }

        public void RegistrarMinutosActividad(DateTime fecha, int minutos)
        {
            var registros = _progreso.GetAll();
            var registroExistente = registros.FirstOrDefault(p => p.Fecha.Date == fecha.Date);

            if (registroExistente != null)
            {
                _progreso.Update(p => p.Fecha.Date == fecha.Date, p => p.MinutosActividad += minutos);
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
            tendencias.Add(CrearTendencia("Peso", registros.Select(r => r.Peso).ToList(), registros.Select(r => r.Fecha).ToList()));
            tendencias.Add(CrearTendencia("Calorías Consumidas", registros.Select(r => r.CaloriasConsumidas).ToList(), registros.Select(r => r.Fecha).ToList()));
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
