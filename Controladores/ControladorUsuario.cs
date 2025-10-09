using Habitus.Modelos;
using Habitus.Modelos.Enums;
using Habitus.Utilidades;

namespace Habitus.Controladores
{
    public class ControladorPerfilUsuario
    {
        private GestorJson<PerfilUsuario> _perfilUsuario;

        public ControladorPerfilUsuario()
        {
            _perfilUsuario = new GestorJson<PerfilUsuario>("perfil.json", false);
        }

        public PerfilUsuario? ObtenerUsuario()
        {
            return _perfilUsuario.GetAll().FirstOrDefault();
        }

        public void CrearUsuario(string nombre, int edad, double peso, double altura, Genero genero, NivelActividad nivelActividad, int puntos)
        {
            var _usuario = new PerfilUsuario
            {
                Nombre = nombre,
                Edad = edad,
                Peso = peso,
                Altura = altura,
                Genero = genero.ToString(),
                NivelActividad = nivelActividad.ToString(),
                Puntos = puntos
            };
            _perfilUsuario.Add(_usuario);
        }

        public void ActualizarPuntos(int puntos)
        {
            var usuario = ObtenerUsuario();
            if (usuario != null)
            {
                _perfilUsuario.Update(
                    user => user.Nombre == usuario.Nombre,
                    user => user.Puntos += puntos
                );
            }
        }

        public void ActualizarPeso(double nuevoPeso)
        {
            var usuario = ObtenerUsuario();
            if (usuario != null)
            {
                _perfilUsuario.Update(
                    user => user.Nombre == usuario.Nombre,
                    user => user.Peso = nuevoPeso
                );
            }
        }

        public double CalcularIMC()
        {
            var usuario = ObtenerUsuario();
            if (usuario != null && usuario.Altura > 0)
            {
                double alturaEnMetros = usuario.Altura / 100;
                return usuario.Peso / (alturaEnMetros * alturaEnMetros);
            }
            return 0;
        }
    }
}