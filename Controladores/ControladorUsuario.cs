using Habitus.Modelos;

namespace Habitus.Controladores
{
    public class ControladorUsuario
    {
        private Usuario _usuario;
        private string _rutaArchivo = "usuario.json";

        public ControladorUsuario()
        {
            CargarUsuario();
        }

        public Usuario ObtenerUsuario()
        {
            return _usuario;
        }

        public void CrearUsuario(string nombre, int edad, double peso, double altura, Genero genero, NivelActividad nivelActividad, int puntos)
        {
            _usuario = new Usuario
            {
                Nombre = nombre,
                Edad = edad,
                Peso = peso,
                Altura = altura,
                Genero = genero.ToString(),
                NivelActividad = nivelActividad.ToString(),
                Puntos = puntos
            };
            GuardarUsuario();
        }

        public void ActualizarPuntos(int puntos)
        {
            if (_usuario != null)
            {
                _usuario.Puntos += puntos;
                GuardarUsuario();
            }
        }

        public void ActualizarPeso(double nuevoPeso)
        {
            if (_usuario != null)
            {
                _usuario.Peso = nuevoPeso;
                GuardarUsuario();
            }
        }

        public double CalcularIMC()
        {
            if (_usuario != null && _usuario.Altura > 0)
            {
                double alturaEnMetros = _usuario.Altura / 100;
                return _usuario.Peso / (alturaEnMetros * alturaEnMetros);
            }
            return 0;
        }

        private void CargarUsuario()
        {
            try
            {
                if (File.Exists(_rutaArchivo))
                {
                    string json = File.ReadAllText(_rutaArchivo);
                    _usuario = System.Text.Json.JsonSerializer.Deserialize<Usuario>(json);
                }
            }
            catch (Exception ex)
            {
                // Log error
                _usuario = null;
            }
        }

        private void GuardarUsuario()
        {
            try
            {
                if (_usuario != null)
                {
                    string json = System.Text.Json.JsonSerializer.Serialize(_usuario, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(_rutaArchivo, json);
                }
            }
            catch (Exception ex)
            {
                // Log error
            }
        }

        public bool UsuarioExiste()
        {
            return _usuario != null;
        }
    }
}