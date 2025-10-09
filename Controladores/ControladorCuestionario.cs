using Habitus.Modelos;
using Habitus.Utilidades;
using System.IO;
using System.Linq;

namespace Habitus.Controladores
{
    public class ControladorCuestionario
    {
        private GestorJson<Cuestionario> _planillaCuestionario;
		private GestorJson<Cuestionario> _cuestionario;

        public ControladorCuestionario()
        {
			_cuestionario = new GestorJson<Cuestionario>("cuestionario.json", false);
			_planillaCuestionario = new GestorJson<Cuestionario>("cuestionario_plantilla.json", true);

            if(_cuestionario.GetAll().Count == 0)
            {
                var planilla = _planillaCuestionario.GetAll();
                _cuestionario.Add(planilla[0]);
            }
		}

        public Cuestionario? ObtenerCuestionario()
        {
            var result = _cuestionario.GetAll();
            if (result.Count == 0) {
                MessageBox.Show("El json del cuestionario esta vacio.");
                return null;
            }
            return result.FirstOrDefault();
        }
        public void ResponderPregunta(int indicePregunta, string respuesta)
        {
            _cuestionario.Update(c => true, c =>
            {
                if (indicePregunta >= 0 && indicePregunta < c.Preguntas.Count)
                {
                    var pregunta = c.Preguntas[indicePregunta];
                    pregunta.RespuestaSeleccionada = respuesta;
                    pregunta.PuntosAsignados = CalcularPuntosPregunta(indicePregunta, respuesta);
                }
            });
        }

        public int CalcularPuntosTotal()
        {
            int puntosTotal = 0;
            foreach (var pregunta in _cuestionario.GetAll()[0].Preguntas)
            {
                puntosTotal += pregunta.PuntosAsignados;
            }
            return puntosTotal;
        }

        public void CompletarCuestionario()
        {
            _cuestionario.Update(c => true, c =>
            {
                c.PuntosObtenidos = c.Preguntas.Sum(p => p.PuntosAsignados);
                c.Completado = true;
                c.FechaCompletado = DateTime.Now;
            });
        }

        public bool EstaCompletado()
        {
            return _cuestionario.GetAll()[0].Completado;
        }
        // check
        private int CalcularPuntosPregunta(int indicePregunta, string respuesta)
        {
            switch (indicePregunta)
            {
                case 0: // Objetivo principal
                    return respuesta switch
                    {
                        "Perder peso" => 20,
                        "Ganar músculo" => 25,
                        "Mantener peso" => 15,
                        "Mejorar salud general" => 30,
                        _ => 10
                    };
                case 1: // Frecuencia de ejercicio
                    return respuesta switch
                    {
                        "Nunca" => 5,
                        "1-2 veces por semana" => 15,
                        "3-4 veces por semana" => 25,
                        "5+ veces por semana" => 35,
                        _ => 10
                    };
                case 2: // Alimentación
                    return respuesta switch
                    {
                        "Muy mala" => 5,
                        "Regular" => 15,
                        "Buena" => 25,
                        "Excelente" => 35,
                        _ => 10
                    };
                case 3: // Horas de sueño
                    return respuesta switch
                    {
                        "Menos de 6 horas" => 5,
                        "6-7 horas" => 15,
                        "7-8 horas" => 30,
                        "Más de 8 horas" => 25,
                        _ => 10
                    };
                case 4: // Consumo de agua
                    return respuesta switch
                    {
                        "Menos de 1 litro" => 5,
                        "1-2 litros" => 15,
                        "2-3 litros" => 30,
                        "Más de 3 litros" => 25,
                        _ => 10
                    };
                case 5: // Motivación
                    return respuesta switch
                    {
                        "Poco motivado" => 10,
                        "Algo motivado" => 20,
                        "Muy motivado" => 30,
                        "Extremadamente motivado" => 40,
                        _ => 15
                    };
                default:
                    return 10;
            }
            return 0;
        }
    }
}