using Habitus.Modelos;
using Habitus.Modelos.Enums;
using Habitus.Utilidades;
using System.Security.Cryptography.X509Certificates;

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
            /*if (_usuario != null)
            {
                _usuario.Puntos += puntos;
                GuardarUsuario();
            }*/
        }

        public void ActualizarPeso(double nuevoPeso)
        {
            /*if (_usuario != null)
            {
                _usuario.Peso = nuevoPeso;
                GuardarUsuario();
            }*/
        }

        public double CalcularIMC()
        {
            /*if (_usuario != null && _usuario.Altura > 0)
            {
                double alturaEnMetros = _usuario.Altura / 100;
                return _usuario.Peso / (alturaEnMetros * alturaEnMetros);
            }*/
            return 0;
        }

        public bool UsuarioExiste()
        {
            return _perfilUsuario.GetAll().FirstOrDefault() != null;
        }
    }
}