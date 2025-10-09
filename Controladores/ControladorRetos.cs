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
            _retosDisponibles = new GestorJson<Reto>("retos.json", true); // check: se pueden agregar un poco mas
            _retosActivos = new GestorJson<Reto>("retosActivos.json", false); 
        }

        public List<Reto> ObtenerRetosDisponibles(int nivelUsuario, int puntosUsuario)
        {
            return _retosDisponibles.GetAll().Where(r => r.NivelRequerido <= nivelUsuario && !_retosActivos.GetAll().Any(ra => ra.Nombre == r.Nombre))
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
                FechaFin = DateTime.Now.AddDays(7),
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
            var reto = _retosActivos.GetAll().FirstOrDefault(r => r.Id == idReto && !r.Completado);
            if (reto != null)
            {
                reto.Completado = true;
                _retosActivos.Delete(r => r.Id == reto.Id);
                return true;
            }
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
    }
}