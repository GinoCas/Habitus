using Habitus.Modelos;
using Habitus.Utilidades;

namespace Habitus.Controladores
{
    public class ControladorCuestionario
    {
        private GestorJson<Cuestionario> _cuestionario;

        public ControladorCuestionario()
        {
            _cuestionario = new GestorJson<Cuestionario>("cuestionario.json", true);
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
            var cuestionario = _cuestionario.GetAll()[0];
            if (indicePregunta >= 0 && indicePregunta < cuestionario.Preguntas.Count)
            {
                var pregunta = cuestionario.Preguntas[indicePregunta];
                pregunta.RespuestaSeleccionada = respuesta;
                pregunta.PuntosAsignados = CalcularPuntosPregunta(indicePregunta, respuesta);
            }
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
            var cuestionario = _cuestionario.GetAll()[0];
            cuestionario.Completado = true;
            cuestionario.FechaCompletado = DateTime.Now;
            cuestionario.PuntosObtenidos = CalcularPuntosTotal();
        }

        public bool EstaCompletado()
        {
            return _cuestionario.GetAll()[0].Completado;
        }

        private void CrearPreguntasIniciales()
        {
           /* _cuestionario.Preguntas = new List<Pregunta>
            {
                new Pregunta
                {
                    Texto = "¿Cuál es tu objetivo principal?",
                    Opciones = new List<string> { "Perder peso", "Ganar músculo", "Mantener peso", "Mejorar salud general" }
                },
                new Pregunta
                {
                    Texto = "¿Con qué frecuencia haces ejercicio actualmente?",
                    Opciones = new List<string> { "Nunca", "1-2 veces por semana", "3-4 veces por semana", "5+ veces por semana" }
                },
                new Pregunta
                {
                    Texto = "¿Cómo describirías tu alimentación actual?",
                    Opciones = new List<string> { "Muy mala", "Regular", "Buena", "Excelente" }
                },
                new Pregunta
                {
                    Texto = "¿Cuántas horas duermes por noche?",
                    Opciones = new List<string> { "Menos de 6 horas", "6-7 horas", "7-8 horas", "Más de 8 horas" }
                },
                new Pregunta
                {
                    Texto = "¿Cuánta agua bebes al día?",
                    Opciones = new List<string> { "Menos de 1 litro", "1-2 litros", "2-3 litros", "Más de 3 litros" }
                },
                new Pregunta
                {
                    Texto = "¿Qué tan motivado/a te sientes para cambiar tus hábitos?",
                    Opciones = new List<string> { "Poco motivado", "Algo motivado", "Muy motivado", "Extremadamente motivado" }
                }
            };*/
        }

        private int CalcularPuntosPregunta(int indicePregunta, string respuesta)
        {
            /*switch (indicePregunta)
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
            }*/
            return 0;
        }
    }
}