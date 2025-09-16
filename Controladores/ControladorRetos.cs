using Habitus.Modelos;
using Habitus.Utilidades;

namespace Habitus.Controladores
{
    public class ControladorRetos
    {
        private GestorJson<Reto> _retosDisponibles;
        private GestorJson<Reto> _retosActivos;

        public ControladorRetos()
        {
            _retosDisponibles = new GestorJson<Reto>("retos.json", true);
            _retosActivos = new GestorJson<Reto>("retosActivos.json", false);
        }

        public List<Reto> ObtenerRetosDisponibles(int nivelUsuario, int puntosUsuario)
        {
            /*CargarRetos();
            CargarRetosActivos();
            return _retosDisponibles.Where(r => r.NivelRequerido <= nivelUsuario && !_retosActivos.Any(ra => ra.Nombre == r.Nombre))
                                   .Take(5)
                                   .ToList();*/
            return new List<Reto>();
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
        }

        public List<Reto> ObtenerRetosActivos()
        {
            return _retosActivos.GetAll().Where(r => !r.Completado && r.FechaFin >= DateTime.Now).ToList();
        }

        public bool CompletarReto(string idReto)
        {
            /*var reto = _retosActivos.FirstOrDefault(r => r.Id == idReto && !r.Completado);
            if (reto != null)
            {
                reto.Completado = true;
                GuardarRetosActivos();
                return true;
            }*/
            return false;
        }

        public int ObtenerPuntosReto(string nombreReto)
        {
            var reto = _retosActivos.GetAll().FirstOrDefault(r => r.Nombre == nombreReto && r.Completado);
            return reto?.PuntosRecompensa ?? 0;
        }

        public int ObtenerPuntosRetos()
        {
            return _retosActivos.GetAll().Where(r => r.Completado).Sum(r => r.PuntosRecompensa);
        }

        /*public void GenerarRetosAutomaticos(int nivelUsuario)
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
        }*/
    }
}