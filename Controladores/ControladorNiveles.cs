using Habitus.Modelos;
using Habitus.Utilidades;

namespace Habitus.Controladores
{
    public class ControladorNiveles
    {
        private GestorJson<Nivel> _niveles;

        public ControladorNiveles()
        {
            _niveles = new GestorJson<Nivel>("niveles.json", true);
        }

        public Nivel ObtenerNivelActual(int puntos)
        {
            var niveles = _niveles.GetAll();
            return niveles.Where(n => n.PuntosRequeridos <= puntos)
                          .OrderByDescending(n => n.PuntosRequeridos)
                          .FirstOrDefault() ?? niveles.First();
        }

        public Nivel ObtenerSiguienteNivel(int puntos)
        {
            return _niveles.GetAll().Where(n => n.PuntosRequeridos > puntos)
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

            if (siguienteNivel == null) return 100;

            int puntosEnNivelActual = puntosActuales - nivelActual.PuntosRequeridos;
            int puntosNecesariosParaSiguiente = siguienteNivel.PuntosRequeridos - nivelActual.PuntosRequeridos;

            return (double)puntosEnNivelActual / puntosNecesariosParaSiguiente * 100;
        }

        public List<string> ObtenerBeneficiosDesbloqueados(int puntos)
        {
            var nivelActual = ObtenerNivelActual(puntos);
            var beneficios = new List<string>();

            foreach (var nivel in _niveles.GetAll().Where(n => n.PuntosRequeridos <= puntos))
            {
                beneficios.AddRange(nivel.BeneficiosDesbloqueados);
            }

            return beneficios.Distinct().ToList();
        }

        public bool HaSubidoDeNivel(int puntosAnteriores, int puntosNuevos)
        {
            var nivelAnterior = ObtenerNivelActual(puntosAnteriores);
            var nivelNuevo = ObtenerNivelActual(puntosNuevos);

            return nivelNuevo.Numero > nivelAnterior.Numero;
        }
    }
}