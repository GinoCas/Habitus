using Habitus.Modelos;

namespace Habitus.Controladores
{
    public class ControladorNiveles
    {
        private List<Nivel> _niveles;
        private string _rutaArchivo = "niveles.json";

        public ControladorNiveles()
        {
            CargarNiveles();
        }

        public Nivel ObtenerNivelActual(int puntos)
        {
            return _niveles.Where(n => n.PuntosRequeridos <= puntos)
                          .OrderByDescending(n => n.PuntosRequeridos)
                          .FirstOrDefault() ?? _niveles.First();
        }

        public Nivel ObtenerSiguienteNivel(int puntos)
        {
            return _niveles.Where(n => n.PuntosRequeridos > puntos)
                          .OrderBy(n => n.PuntosRequeridos)
                          .FirstOrDefault();
        }

        public int CalcularPuntosParaSiguienteNivel(int puntosActuales)
        {
            var siguienteNivel = ObtenerSiguienteNivel(puntosActuales);
            return siguienteNivel?.PuntosRequeridos - puntosActuales ?? 0;
        }

        public double CalcularProgresoNivel(int puntosActuales)
        {
            var nivelActual = ObtenerNivelActual(puntosActuales);
            var siguienteNivel = ObtenerSiguienteNivel(puntosActuales);

            if (siguienteNivel == null) return 100; // Nivel máximo alcanzado

            int puntosEnNivelActual = puntosActuales - nivelActual.PuntosRequeridos;
            int puntosNecesariosParaSiguiente = siguienteNivel.PuntosRequeridos - nivelActual.PuntosRequeridos;

            return (double)puntosEnNivelActual / puntosNecesariosParaSiguiente * 100;
        }

        public List<string> ObtenerBeneficiosDesbloqueados(int puntos)
        {
            var nivelActual = ObtenerNivelActual(puntos);
            var beneficios = new List<string>();

            foreach (var nivel in _niveles.Where(n => n.PuntosRequeridos <= puntos))
            {
                beneficios.AddRange(nivel.BeneficiosDesbloqueados);
            }

            return beneficios.Distinct().ToList();
        }

        public bool HaSubidoDeNivel(int puntosAnteriores, int puntosNuevos)
        {
            var nivelAnterior = ObtenerNivelActual(puntosAnteriores);
            var nivelNuevo = ObtenerNivelActual(puntosNuevos);

            return nivelNuevo.NumeroNivel > nivelAnterior.NumeroNivel;
        }

        private void CargarNiveles()
        {
            try
            {
                if (File.Exists(_rutaArchivo))
                {
                    string json = File.ReadAllText(_rutaArchivo);
                    _niveles = System.Text.Json.JsonSerializer.Deserialize<List<Nivel>>(json) ?? new List<Nivel>();
                }
                else
                {
                    _niveles = InicializarNiveles();
                    GuardarNiveles();
                }
            }
            catch (Exception ex)
            {
                _niveles = InicializarNiveles();
            }
        }

        private void GuardarNiveles()
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(_niveles, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_rutaArchivo, json);
            }
            catch (Exception ex)
            {
                // Log error
            }
        }

        private List<Nivel> InicializarNiveles()
        {
            return new List<Nivel>
            {
                new Nivel
                {
                    NumeroNivel = 1,
                    Nombre = "Principiante",
                    PuntosRequeridos = 0,
                    Descripcion = "¡Bienvenido a tu viaje de salud!",
                    BeneficiosDesbloqueados = new List<string> { "Registro básico de actividades", "Retos simples" },
                    Insignia = "🌱"
                },
                new Nivel
                {
                    NumeroNivel = 2,
                    Nombre = "Explorador",
                    PuntosRequeridos = 100,
                    Descripcion = "Estás comenzando a formar hábitos saludables",
                    BeneficiosDesbloqueados = new List<string> { "Seguimiento de peso", "Retos de alimentación" },
                    Insignia = "🚀"
                },
                new Nivel
                {
                    NumeroNivel = 3,
                    Nombre = "Comprometido",
                    PuntosRequeridos = 300,
                    Descripcion = "Tu compromiso con la salud es evidente",
                    BeneficiosDesbloqueados = new List<string> { "Estadísticas avanzadas", "Retos personalizados" },
                    Insignia = "💪"
                },
                new Nivel
                {
                    NumeroNivel = 4,
                    Nombre = "Entusiasta",
                    PuntosRequeridos = 600,
                    Descripcion = "Eres un verdadero entusiasta del bienestar",
                    BeneficiosDesbloqueados = new List<string> { "Reportes detallados", "Retos de grupo" },
                    Insignia = "🔥"
                },
                new Nivel
                {
                    NumeroNivel = 5,
                    Nombre = "Experto",
                    PuntosRequeridos = 1000,
                    Descripcion = "Has alcanzado un nivel experto en salud",
                    BeneficiosDesbloqueados = new List<string> { "Análisis de tendencias", "Retos extremos" },
                    Insignia = "⭐"
                },
                new Nivel
                {
                    NumeroNivel = 6,
                    Nombre = "Maestro",
                    PuntosRequeridos = 1500,
                    Descripcion = "Eres un maestro del bienestar y la salud",
                    BeneficiosDesbloqueados = new List<string> { "Funciones premium", "Mentor de otros usuarios" },
                    Insignia = "👑"
                },
                new Nivel
                {
                    NumeroNivel = 7,
                    Nombre = "Leyenda",
                    PuntosRequeridos = 2500,
                    Descripcion = "¡Eres una leyenda viviente de la salud!",
                    BeneficiosDesbloqueados = new List<string> { "Acceso completo", "Reconocimiento especial" },
                    Insignia = "🏆"
                }
            };
        }
    }
}