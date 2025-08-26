using Habitus.Modelos;

namespace Habitus.Controladores
{
    public class ControladorRetos
    {
        private List<Reto> _retosDisponibles;
        private List<Reto> _retosActivos;
        private string _rutaArchivoRetos = "retos.json";
        private string _rutaArchivoRetosActivos = "retos_activos.json";

        public ControladorRetos()
        {
            CargarRetos();
            CargarRetosActivos();
        }

        public List<Reto> ObtenerRetosDisponibles(int nivelUsuario, int puntosUsuario)
        {
            return _retosDisponibles.Where(r => r.NivelRequerido <= nivelUsuario && !_retosActivos.Any(ra => ra.Nombre == r.Nombre))
                                   .Take(5)
                                   .ToList();
        }

        public void AsignarReto(Reto reto)
        {
            var retoActivo = new Reto
            {
                Nombre = reto.Nombre,
                Descripcion = reto.Descripcion,
                PuntosRecompensa = reto.PuntosRecompensa,
                NivelRequerido = reto.NivelRequerido,
                TipoReto = reto.TipoReto,
                Dificultad = reto.Dificultad,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(7), // Retos de 7 días por defecto
                Completado = false
            };

            _retosActivos.Add(retoActivo);
            GuardarRetosActivos();
        }

        public List<Reto> ObtenerRetosActivos()
        {
            return _retosActivos.Where(r => !r.Completado && r.FechaFin >= DateTime.Now).ToList();
        }

        public bool CompletarReto(string idReto)
        {
            var reto = _retosActivos.FirstOrDefault(r => r.Id == idReto && !r.Completado);
            if (reto != null)
            {
                reto.Completado = true;
                GuardarRetosActivos();
                return true;
            }
            return false;
        }

        public int ObtenerPuntosReto(string nombreReto)
        {
            var reto = _retosActivos.FirstOrDefault(r => r.Nombre == nombreReto && r.Completado);
            return reto?.PuntosRecompensa ?? 0;
        }

        public int ObtenerPuntosRetos()
        {
            return _retosActivos.Where(r => r.Completado).Sum(r => r.PuntosRecompensa);
        }

        public void GenerarRetosAutomaticos(int nivelUsuario)
        {
            // Generar retos automáticos basados en el nivel del usuario
            var retosAutomaticos = new List<Reto>
            {
                new Reto
                {
                    Nombre = "Camina 30 minutos",
                    Descripcion = "Realiza una caminata de al menos 30 minutos",
                    PuntosRecompensa = 50,
                    NivelRequerido = 1,
                    TipoReto = "Ejercicio",
                    Dificultad = "Fácil"
                },
                new Reto
                {
                    Nombre = "Bebe 8 vasos de agua",
                    Descripcion = "Consume al menos 2 litros de agua durante el día",
                    PuntosRecompensa = 30,
                    NivelRequerido = 1,
                    TipoReto = "Hábito",
                    Dificultad = "Fácil"
                },
                new Reto
                {
                    Nombre = "Come 5 porciones de frutas y verduras",
                    Descripcion = "Incluye al menos 5 porciones de frutas y verduras en tu día",
                    PuntosRecompensa = 40,
                    NivelRequerido = 2,
                    TipoReto = "Alimentación",
                    Dificultad = "Medio"
                }
            };

            foreach (var reto in retosAutomaticos.Where(r => r.NivelRequerido <= nivelUsuario))
            {
                if (!_retosDisponibles.Any(rd => rd.Nombre == reto.Nombre))
                {
                    _retosDisponibles.Add(reto);
                }
            }

            GuardarRetos();
        }

        private void CargarRetos()
        {
            try
            {
                if (File.Exists(_rutaArchivoRetos))
                {
                    string json = File.ReadAllText(_rutaArchivoRetos);
                    _retosDisponibles = System.Text.Json.JsonSerializer.Deserialize<List<Reto>>(json) ?? new List<Reto>();
                }
                else
                {
                    _retosDisponibles = new List<Reto>();
                    GenerarRetosAutomaticos(1);
                }
            }
            catch (Exception ex)
            {
                _retosDisponibles = new List<Reto>();
            }
        }

        private void GuardarRetos()
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(_retosDisponibles, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_rutaArchivoRetos, json);
            }
            catch (Exception ex)
            {
                // Log error
            }
        }

        private void CargarRetosActivos()
        {
            try
            {
                if (File.Exists(_rutaArchivoRetosActivos))
                {
                    string json = File.ReadAllText(_rutaArchivoRetosActivos);
                    _retosActivos = System.Text.Json.JsonSerializer.Deserialize<List<Reto>>(json) ?? new List<Reto>();
                }
                else
                {
                    _retosActivos = new List<Reto>();
                }
            }
            catch (Exception ex)
            {
                _retosActivos = new List<Reto>();
            }
        }

        private void GuardarRetosActivos()
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(_retosActivos, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_rutaArchivoRetosActivos, json);
            }
            catch (Exception ex)
            {
                // Log error
            }
        }
    }
}