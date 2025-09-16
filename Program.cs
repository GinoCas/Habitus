using System;
using System.IO;
using System.Windows.Forms;
using Habitus.Vistas;
using Habitus.Controladores;
using Habitus.Modelos;

namespace Habitus
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var formPrincipal = new FormPrincipal();
            Application.Run(formPrincipal);

            /*try
            {
                if (EsPrimeraEjecucion())
                {
                    // Mostrar cuestionario inicial 
                    var formCuestionario = new FormCuestionario();
                    Application.Run(formCuestionario);
                }
                else
                {
                    // Verificar si el usuario existe
                    var controladorUsuario = new ControladorPerfilUsuario();
                    PerfilUsuario usuario = null;
                    //MessageBox.Show("test");
                    //var usuario = controladorUsuario.ObtenerUsuario();
                    //MessageBox.Show(usuario.Nombre);

                    if (usuario == null)
                    {
                        // Si no hay usuario, mostrar cuestionario
                        MessageBox.Show("TEST");
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
            }*/
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
}
}