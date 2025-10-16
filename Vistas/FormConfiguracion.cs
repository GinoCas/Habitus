using Habitus.Controladores;
using Habitus.Modelos;
using Habitus.Modelos.Enums;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Habitus.Vistas
{
    public partial class FormConfiguracion : Form
    {
        private ControladorPerfilUsuario _controladorUsuario;
        private ControladorNiveles _controladorNiveles;
        private PerfilUsuario _usuario;



        public FormConfiguracion()
        {
            InitializeComponent();
            _controladorUsuario = new ControladorPerfilUsuario();
            _controladorNiveles = new ControladorNiveles();
            _usuario = _controladorUsuario.ObtenerUsuario();
            CargarDatosUsuario();
        }


        // TODO: No se guarda al cambiar los valores
        private void CrearTabPerfil()
        {
            var tabPerfil = new TabPage("👤 Perfil")
            {
                BackColor = Color.White
            };
            tabControl.TabPages.Add(tabPerfil);

            var panelNivel = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(580, 80),
                BackColor = Color.FromArgb(52, 152, 219),
                BorderStyle = BorderStyle.None
            };
            tabPerfil.Controls.Add(panelNivel);

            var lblNivelTitulo = new Label
            {
                Text = "🏆 Información del Nivel",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 10),
                Size = new Size(200, 25)
            };
            panelNivel.Controls.Add(lblNivelTitulo);

            var lblNivelInfo = new Label
            {
                Text = "Cargando información del nivel...",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                Location = new Point(20, 35),
                Size = new Size(540, 40),
                Name = "lblNivelInfo"
            };
            panelNivel.Controls.Add(lblNivelInfo);

            var lblDatosPersonales = new Label
            {
                Text = "📝 Datos Personales",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(20, 120),
                Size = new Size(200, 25)
            };
            tabPerfil.Controls.Add(lblDatosPersonales);

            var lblNombre = new Label
            {
                Text = "Nombre:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(40, 160),
                Size = new Size(80, 20)
            };
            tabPerfil.Controls.Add(lblNombre);

            txtNombre = new TextBox
            {
                Location = new Point(130, 158),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10)
            };
            tabPerfil.Controls.Add(txtNombre);

            var lblEdad = new Label
            {
                Text = "Edad:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(350, 160),
                Size = new Size(50, 20)
            };
            tabPerfil.Controls.Add(lblEdad);

            nudEdad = new NumericUpDown
            {
                Location = new Point(410, 158),
                Size = new Size(80, 25),
                Font = new Font("Segoe UI", 10),
                Minimum = 10,
                Maximum = 120,
                Value = 25
            };
            tabPerfil.Controls.Add(nudEdad);

            var lblGenero = new Label
            {
                Text = "Género:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(40, 200),
                Size = new Size(80, 20)
            };
            tabPerfil.Controls.Add(lblGenero);

            cmbGenero = new ComboBox
            {
                Location = new Point(130, 198),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbGenero.Items.AddRange(new[] { "Masculino", "Femenino", "Otro" });
            tabPerfil.Controls.Add(cmbGenero);

            var lblPeso = new Label
            {
                Text = "Peso (kg):",
                Font = new Font("Segoe UI", 10),
                Location = new Point(40, 240),
                Size = new Size(80, 20)
            };
            tabPerfil.Controls.Add(lblPeso);

            nudPeso = new NumericUpDown
            {
                Location = new Point(130, 238),
                Size = new Size(80, 25),
                Font = new Font("Segoe UI", 10),
                Minimum = 30,
                Maximum = 300,
                DecimalPlaces = 1,
                Value = 70
            };
            tabPerfil.Controls.Add(nudPeso);

            var lblAltura = new Label
            {
                Text = "Altura (cm):",
                Font = new Font("Segoe UI", 10),
                Location = new Point(250, 240),
                Size = new Size(80, 20)
            };
            tabPerfil.Controls.Add(lblAltura);

            nudAltura = new NumericUpDown
            {
                Location = new Point(340, 238),
                Size = new Size(80, 25),
                Font = new Font("Segoe UI", 10),
                Minimum = 100,
                Maximum = 250,
                Value = 170
            };
            tabPerfil.Controls.Add(nudAltura);

            var lblNivelActividad = new Label
            {
                Text = "Nivel de Actividad:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(40, 280),
                Size = new Size(120, 20)
            };
            tabPerfil.Controls.Add(lblNivelActividad);

            cmbNivelActividad = new ComboBox
            {
                Location = new Point(170, 278),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbNivelActividad.Items.AddRange(new[] { "Sedentario", "Ligero", "Moderado", "Activo", "Muy Activo" });
            tabPerfil.Controls.Add(cmbNivelActividad);

            var lblIMC = new Label
            {
                Text = "IMC: Calculando...",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(127, 140, 141),
                Location = new Point(40, 320),
                Size = new Size(200, 20),
                Name = "lblIMC"
            };
            tabPerfil.Controls.Add(lblIMC);

            nudPeso.ValueChanged += CalcularIMC;
            nudAltura.ValueChanged += CalcularIMC;
        }
        // TODO: Hacer funcionar este tab -> Baja prioridad
        private void CrearTabDatos()
        {
            var tabDatos = new TabPage("💾 Datos")
            {
                BackColor = Color.White
            };
            tabControl.TabPages.Add(tabDatos);

            // Gestión de datos
            var lblGestionDatos = new Label
            {
                Text = "📁 Gestión de Datos",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(20, 20),
                Size = new Size(200, 25)
            };
            tabDatos.Controls.Add(lblGestionDatos);

            btnExportarDatos = new Button
            {
                Text = "📤 Exportar Datos",
                Location = new Point(40, 60),
                Size = new Size(150, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnExportarDatos.FlatAppearance.BorderSize = 0;
            btnExportarDatos.Click += BtnExportarDatos_Click;
            tabDatos.Controls.Add(btnExportarDatos);

            btnImportarDatos = new Button
            {
                Text = "📥 Importar Datos",
                Location = new Point(200, 60),
                Size = new Size(150, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnImportarDatos.FlatAppearance.BorderSize = 0;
            btnImportarDatos.Click += BtnImportarDatos_Click;
            tabDatos.Controls.Add(btnImportarDatos);

            // Información de datos
            var lblInfoDatos = new Label
            {
                Text = "ℹ️ Información de Datos:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(40, 120),
                Size = new Size(200, 20)
            };
            tabDatos.Controls.Add(lblInfoDatos);

            var lblInfoTexto = new Label
            {
                Text = "• Los datos se almacenan localmente en tu dispositivo\n• Puedes exportar tus datos para crear respaldos\n• La importación sobrescribirá los datos actuales",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(127, 140, 141),
                Location = new Point(40, 150),
                Size = new Size(500, 60)
            };
            tabDatos.Controls.Add(lblInfoTexto);

            // Opciones avanzadas
            var lblOpcionesAvanzadas = new Label
            {
                Text = "⚠️ Opciones Avanzadas",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(231, 76, 60),
                Location = new Point(20, 240),
                Size = new Size(200, 25)
            };
            tabDatos.Controls.Add(lblOpcionesAvanzadas);

            btnReiniciarApp = new Button
            {
                Text = "🔄 Reiniciar Aplicación",
                Location = new Point(40, 280),
                Size = new Size(180, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnReiniciarApp.FlatAppearance.BorderSize = 0;
            btnReiniciarApp.Click += BtnReiniciarApp_Click;
            tabDatos.Controls.Add(btnReiniciarApp);

            var lblAdvertencia = new Label
            {
                Text = "⚠️ ADVERTENCIA: Reiniciar la aplicación eliminará\ntodos los datos y configuraciones actuales.",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(231, 76, 60),
                Location = new Point(40, 325),
                Size = new Size(400, 40)
            };
            tabDatos.Controls.Add(lblAdvertencia);
        }

        private void CargarDatosUsuario()
        {
            if (_usuario != null)
            {
                txtNombre.Text = _usuario.Nombre;
                nudEdad.Value = _usuario.Edad;
                cmbGenero.SelectedItem = _usuario.Genero;
                nudPeso.Value = (decimal)_usuario.Peso;
                nudAltura.Value = (decimal)_usuario.Altura;
                cmbNivelActividad.SelectedItem = _usuario.NivelActividad;

                // Cargar información del nivel
                var nivel = _controladorNiveles.ObtenerNivelActual(_usuario.Puntos);
                var siguienteNivel = _controladorNiveles.ObtenerSiguienteNivel(_usuario.Puntos);
                var puntosParaSiguiente = _controladorNiveles.CalcularPuntosParaSiguienteNivel(_usuario.Puntos);
                
                var lblNivelInfo = this.Controls.Find("lblNivelInfo", true)[0] as Label;
                if (lblNivelInfo != null)
                {
                    lblNivelInfo.Text = $"Nivel {nivel.Numero}: {nivel.Nombre} | Puntos: {_usuario.Puntos}\n" +
                                       $"Faltan {puntosParaSiguiente} puntos para el siguiente nivel";
                }

                CalcularIMC(null, null);
            }
        }

        public void CalcularIMC(object sender, EventArgs e)
        {
            try
            {
                var peso = (double)nudPeso.Value;
                var altura = (double)nudAltura.Value / 100; // Convertir cm a metros
                var imc = peso / (altura * altura);

                string categoria;
                Color color;

                if (imc < 18.5)
                {
                    categoria = "Bajo peso";
                    color = Color.FromArgb(52, 152, 219);
                }
                else if (imc < 25)
                {
                    categoria = "Peso normal";
                    color = Color.FromArgb(39, 174, 96);
                }
                else if (imc < 30)
                {
                    categoria = "Sobrepeso";
                    color = Color.FromArgb(230, 126, 34);
                }
                else
                {
                    categoria = "Obesidad";
                    color = Color.FromArgb(231, 76, 60);
                }

                var lblIMC = this.Controls.Find("lblIMC", true)[0] as Label;
                if (lblIMC != null)
                {
                    lblIMC.Text = $"IMC: {imc:F1} ({categoria})";
                    lblIMC.ForeColor = color;
                }
            }
            catch
            {
                var lblIMC = this.Controls.Find("lblIMC", true)[0] as Label;
                if (lblIMC != null)
                {
                    lblIMC.Text = "IMC: Error en el cálculo";
                    lblIMC.ForeColor = Color.Red;
                }
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarDatos())
                {
                    if (_usuario == null)
                    {
                        _usuario = new PerfilUsuario();
                    }

                    _controladorUsuario.ActualizarNombre(txtNombre.Text.Trim());
                    _controladorUsuario.ActualizarEdad((int)nudEdad.Value);

                    var generoTexto = cmbGenero.SelectedItem?.ToString() ?? "NoEspecificado";
                    
                    if (Enum.TryParse<Genero>(generoTexto, out var genero))
                    {
                        _controladorUsuario.ActualizarGenero(genero);
                    }
                    else
                    {
                        MessageBox.Show("Género no válido.");
                    }
                    _controladorUsuario.ActualizarPeso( (double)nudPeso.Value);
                    _controladorUsuario.ActualizarAltura((double)nudAltura.Value);

                    var nivelTexto = (cmbNivelActividad.SelectedItem?.ToString() ?? "Moderado")
                     .Trim()
                     .Replace(" ", "");

                    if (Enum.TryParse<NivelActividad>(nivelTexto, ignoreCase: true, out var nivelActividad))
                    {
                        _controladorUsuario.ActualizarNivelActividad(nivelActividad);
                    }
                    else
                    {
                        MessageBox.Show("Nivel de actividad no válido.");
                    }


                    if (_controladorUsuario.ObtenerUsuario() == null)
                    {
                        Genero genero1 = Enum.TryParse<Genero>(_usuario.Genero, out var g) ? g : Genero.Masculino;

                        _controladorUsuario.CrearUsuario(_usuario.Nombre, _usuario.Edad, _usuario.Peso, 
                                                        _usuario.Altura, genero, nivelActividad, 0);
                    }
                    else
                    {
                        // TODO: Implementar un método para actualizar el usuario completo -> Baja prioridad
                        _controladorUsuario.ActualizarPeso(_usuario.Peso);
                    }

                    MessageBox.Show("Configuración guardada exitosamente.", "Configuración", 
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la configuración: {ex.Message}", "Error", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (cmbGenero.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona un género.", "Validación", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbGenero.Focus();
                return false;
            }

            if (cmbNivelActividad.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona un nivel de actividad.", "Validación", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbNivelActividad.Focus();
                return false;
            }

            return true;
        }

        private void BtnRestaurar_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Deseas restaurar los valores originales?", "Restaurar", 
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                CargarDatosUsuario();
                MessageBox.Show("Valores restaurados.", "Restaurar", 
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnExportarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                var saveDialog = new SaveFileDialog
                {
                    Filter = "Archivo JSON (*.json)|*.json|Todos los archivos (*.*)|*.*",
                    Title = "Exportar Datos de Habitus",
                    FileName = $"Habitus_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.json"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Aquí se implementaría la lógica de exportación
                    var datosExportacion = new
                    {
                        Usuario = _usuario,
                        FechaExportacion = DateTime.Now,
                        Version = "1.0"
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(datosExportacion, Newtonsoft.Json.Formatting.Indented);
                    System.IO.File.WriteAllText(saveDialog.FileName, json);

                    MessageBox.Show($"Datos exportados exitosamente a:\n{saveDialog.FileName}", 
                                   "Exportación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar datos: {ex.Message}", "Error", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnImportarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                var openDialog = new OpenFileDialog
                {
                    Filter = "Archivo JSON (*.json)|*.json|Todos los archivos (*.*)|*.*",
                    Title = "Importar Datos de Habitus"
                };

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    var result = MessageBox.Show("¿Estás seguro de que deseas importar estos datos?\n\n" +
                                                 "Esta acción sobrescribirá todos los datos actuales.", 
                                                 "Confirmar Importación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    
                    if (result == DialogResult.Yes)
                    {
                        // Aquí se implementaría la lógica de importación
                        MessageBox.Show("Función de importación en desarrollo.", "Información", 
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al importar datos: {ex.Message}", "Error", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReiniciarApp_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("⚠️ ADVERTENCIA ⚠️\n\n" +
                                        "Esta acción eliminará TODOS los datos de la aplicación:\n" +
                                        "• Perfil de usuario\n" +
                                        "• Actividades registradas\n" +
                                        "• Comidas registradas\n" +
                                        "• Progreso y estadísticas\n" +
                                        "• Retos y puntos\n\n" +
                                        "¿Estás COMPLETAMENTE seguro de que deseas continuar?", 
                                        "Confirmar Reinicio", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                var confirmacion = MessageBox.Show("Esta es tu última oportunidad para cancelar.\n\n" +
                                                   "¿Proceder con el reinicio completo?", 
                                                   "Confirmación Final", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                
                if (confirmacion == DialogResult.Yes)
                {
                    try
                    {
                        string rutaCarpeta = "C:\\Users\\Expedientes\\AppData\\Roaming\\Habitus\\Datos";

                        if (Directory.Exists(rutaCarpeta))
                        {
                            Directory.Delete(rutaCarpeta, true); // Elimina todo el contenido recursivamente
                        }
                        string[] archivosJSON = new string[]
                        {
                          "C:\\Users\\Expedientes\\AppData\\Roaming\\Habitus\\configuracion.json"

                         };
                        foreach (string archivo in archivosJSON)
                        {
                            if (File.Exists(archivo))
                            {
                                // Opcional: Eliminar el archivo completo
                                File.Delete(archivo);

                                // O restablecer el contenido del archivo a vacío o con un objeto vacío
                                // Esto lo haces solo si prefieres mantener el archivo pero vacío
                                // var objetoVacio = new { }; // Un objeto vacío o con valores predeterminados
                                // File.WriteAllText(archivo, JsonConvert.SerializeObject(objetoVacio));

                                // O incluso podrías borrar el contenido con un texto vacío
                                // File.WriteAllText(archivo, string.Empty);
                            }
                        }

                        MessageBox.Show("Aplicación reiniciada. La aplicación se cerrará.", "Reinicio Completo", 
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);

                        
                        Application.Exit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al reiniciar la aplicación: {ex.Message}", "Error", 
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}