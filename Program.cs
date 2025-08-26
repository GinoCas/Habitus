using System;
using System.IO;
using System.Windows.Forms;
using Habitus.Vistas;
using Habitus.Controladores;

namespace Habitus
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                CrearDirectoriosDatos();
                if (EsPrimeraEjecucion())
                {
                    // Mostrar cuestionario inicial
                    var formCuestionario = new FormCuestionario();
                    Application.Run(formCuestionario);
                }
                else
                {
                    // Verificar si el usuario existe
                    var controladorUsuario = new ControladorUsuario();
                    var usuario = controladorUsuario.ObtenerUsuario();

                    if (usuario == null)
                    {
                        // Si no hay usuario, mostrar cuestionario
                        var formCuestionario = new FormCuestionario();
                        Application.Run(formCuestionario);
                    }
                    else
                    {
                        // Usuario existe, ir directamente al formulario principal
                        var formPrincipal = new FormPrincipal();
                        Application.Run(formPrincipal);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar errores críticos de la aplicación
                MessageBox.Show($"Error crítico al iniciar la aplicación:\n\n{ex.Message}\n\nLa aplicación se cerrará.", 
                               "Error Crítico - Habitus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private static void CrearDirectoriosDatos()
        {
            try
            {
                var directorioBase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Habitus");
                
                if (!Directory.Exists(directorioBase))
                {
                    Directory.CreateDirectory(directorioBase);
                }

                // Crear subdirectorios para diferentes tipos de datos
                var subdirectorios = new[]
                {
                    "Usuarios",
                    "Actividades", 
                    "Comidas",
                    "Progreso",
                    "Retos",
                    "Niveles",
                    "Cuestionarios",
                    "Respaldos"
                };

                foreach (var subdirectorio in subdirectorios)
                {
                    var ruta = Path.Combine(directorioBase, subdirectorio);
                    if (!Directory.Exists(ruta))
                    {
                        Directory.CreateDirectory(ruta);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear directorios de datos:\n{ex.Message}\n\nLa aplicación podría no funcionar correctamente.", 
                               "Advertencia - Habitus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private static bool EsPrimeraEjecucion()
        {
            try
            {
                var directorioBase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Habitus");
                var archivoConfiguracion = Path.Combine(directorioBase, "configuracion.json");
                
                if (!File.Exists(archivoConfiguracion))
                {
                    // Crear archivo de configuración inicial
                    var configuracionInicial = new
                    {
                        PrimeraEjecucion = false,
                        FechaInstalacion = DateTime.Now,
                        Version = "1.0.0",
                        UltimaEjecucion = DateTime.Now
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(configuracionInicial, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(archivoConfiguracion, json);
                    
                    return true;
                }
                else
                {
                    // Actualizar última ejecución
                    try
                    {
                        var contenido = File.ReadAllText(archivoConfiguracion);
                        dynamic configuracion = Newtonsoft.Json.JsonConvert.DeserializeObject(contenido);
                        configuracion.UltimaEjecucion = DateTime.Now;
                        
                        var jsonActualizado = Newtonsoft.Json.JsonConvert.SerializeObject(configuracion, Newtonsoft.Json.Formatting.Indented);
                        File.WriteAllText(archivoConfiguracion, jsonActualizado);
                    }
                    catch
                    {
                        // Si hay error al actualizar, continuar sin problemas
                    }
                    
                    return false;
                }
            }
            catch
            {
                // En caso de error, asumir que es primera ejecución
                return true;
            }
        }
        public static string ObtenerRutaDatos()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Habitus");
        }

        public static string ObtenerRutaSubdirectorio(string subdirectorio)
        {
            return Path.Combine(ObtenerRutaDatos(), subdirectorio);
        }

        public static bool VerificarPermisos()
        {
            try
            {
                var rutaPrueba = Path.Combine(ObtenerRutaDatos(), "test_permisos.tmp");
                File.WriteAllText(rutaPrueba, "test");
                File.Delete(rutaPrueba);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}