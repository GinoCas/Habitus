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
            try
            {
                if (EsPrimeraEjecucion())
                { 
                    var formCuestionario = new FormCuestionario();
                    Application.Run(formCuestionario);
                }
                else
                {
                    var controladorUsuario = new ControladorPerfilUsuario();
                    var usuario = controladorUsuario.ObtenerUsuario();

                    if (usuario == null)
                    {
                        var formCuestionario = new FormCuestionario();
                        Application.Run(formCuestionario);
                    }
                    else
                    {
                        var formPrincipal = new FormPrincipal();
                        Application.Run(formPrincipal);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error crítico al iniciar la aplicación:\n\n{ex.Message}\n\nLa aplicación se cerrará.", 
                               "Error Crítico - Habitus", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    Directory.CreateDirectory(directorioBase);
                    CopiarArchivosDeDatos();
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
                    }
                    
                    return false;
                }
            }
            catch
            {
                return true;
            }
        }

        private static void CopiarArchivosDeDatos()
        {
            try
            {
                string directorioBaseApp = AppDomain.CurrentDomain.BaseDirectory;
                string directorioFuente = Path.Combine(directorioBaseApp, "Datos", "Sistema");

                string directorioDestino = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Habitus", "Datos", "Sistema");

                if (!Directory.Exists(directorioDestino))
                {
                    Directory.CreateDirectory(directorioDestino);
                }

                string[] archivosJson = Directory.GetFiles(directorioFuente, "*.json");

                foreach (string archivo in archivosJson)
                {
                    string nombreArchivo = Path.GetFileName(archivo);
                    string rutaDestino = Path.Combine(directorioDestino, nombreArchivo);
                    File.Copy(archivo, rutaDestino, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al copiar los archivos de datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
}
}