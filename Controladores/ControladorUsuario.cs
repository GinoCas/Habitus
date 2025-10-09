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

        // Crear un nuevo usuario
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

        // Actualizar puntos
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

        // Actualizar peso
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

        // Actualizar altura
        public void ActualizarAltura(double nuevaAltura)
        {
            var usuario = ObtenerUsuario();
            if (usuario != null)
            {
                _perfilUsuario.Update(
                    user => user.Nombre == usuario.Nombre,
                    user => user.Altura = nuevaAltura
                );
            }
        }

        // Actualizar nombre
        public void ActualizarNombre(string nuevoNombre)
        {
            var usuario = ObtenerUsuario();
            if (usuario != null)
            {
                _perfilUsuario.Update(
                    user => user.Nombre == usuario.Nombre,
                    user => user.Nombre = nuevoNombre
                );
            }
        }

        // Actualizar edad
        public void ActualizarEdad(int nuevaEdad)
        {
            var usuario = ObtenerUsuario();
            if (usuario != null)
            {
                _perfilUsuario.Update(
                    user => user.Nombre == usuario.Nombre,
                    user => user.Edad = nuevaEdad
                );
            }
        }

        // Actualizar genero
        public void ActualizarGenero(Genero nuevoGenero)
        {
            var usuario = ObtenerUsuario();
            if (usuario != null)
            {
                _perfilUsuario.Update(
                    user => user.Nombre == usuario.Nombre,
                    user => user.Genero = nuevoGenero.ToString()
                );
            }
        }

        // Actualizar nivel de actividad
        public void ActualizarNivelActividad(NivelActividad nuevoNivelActividad)
        {
            var usuario = ObtenerUsuario();
            if (usuario != null)
            {
                _perfilUsuario.Update(
                    user => user.Nombre == usuario.Nombre,
                    user => user.NivelActividad = nuevoNivelActividad.ToString()
                );
            }
        }

        // Actualizar comida
        public void AgregarComida(Comida comida)
        {
            var usuario = ObtenerUsuario();
            if (usuario != null && comida != null)
            {
                usuario.Comidas.Add(comida);
                _perfilUsuario.Update(
                    user => user.Nombre == usuario.Nombre,
                    user => user.Comidas = usuario.Comidas
                );
            }
        }

        // Eliminar comida
        public void EliminarComida(Comida comida)
        {
            var usuario = ObtenerUsuario();
            if (usuario != null && comida != null && usuario.Comidas.Contains(comida))
            {
                usuario.Comidas.Remove(comida);
                _perfilUsuario.Update(
                    user => user.Nombre == usuario.Nombre,
                    user => user.Comidas = usuario.Comidas
                );
            }
        }

        // Agregar actividad
        public void AgregarActividad(Actividad actividad)
        {
            var usuario = ObtenerUsuario();
            if (usuario != null && actividad != null)
            {
                usuario.Actividades.Add(actividad);
                _perfilUsuario.Update(
                    user => user.Nombre == usuario.Nombre,
                    user => user.Actividades = usuario.Actividades
                );
            }
        }

        // Eliminar actividad
        public void EliminarActividad(Actividad actividad)
        {
            var usuario = ObtenerUsuario();
            if (usuario != null && actividad != null && usuario.Actividades.Contains(actividad))
            {
                usuario.Actividades.Remove(actividad);
                _perfilUsuario.Update(
                    user => user.Nombre == usuario.Nombre,
                    user => user.Actividades = usuario.Actividades
                );
            }
        }

        // Calcular IMC
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
