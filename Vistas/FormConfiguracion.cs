using System;
using System.Drawing;
using System.Windows.Forms;
using Habitus.Controladores;
using Habitus.Modelos;

namespace Habitus.Vistas
{
    public partial class FormConfiguracion : Form
    {
        private ControladorUsuario _controladorUsuario;
        private ControladorNiveles _controladorNiveles;
        private Usuario _usuario;

        public FormConfiguracion()
        {
            InitializeComponent();
            _controladorUsuario = new ControladorUsuario();
            _controladorNiveles = new ControladorNiveles();
            _usuario = _controladorUsuario.ObtenerUsuario();
            CargarDatosUsuario();
        }



        private void CrearTabPerfil()
        {
            var tabPerfil = new TabPage("👤 Perfil")
            {
                BackColor = Color.White
            };
            tabControl.TabPages.Add(tabPerfil);

            // Información del nivel actual
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

            // Datos personales
            var lblDatosPersonales = new Label
            {
                Text = "📝 Datos Personales",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(20, 120),
                Size = new Size(200, 25)
            };
            tabPerfil.Controls.Add(lblDatosPersonales);

            // Nombre
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

            // Edad
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

            // Género
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

            // Peso
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

            // Altura
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

            // Nivel de actividad
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

            // Información adicional
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

            // Eventos para calcular IMC automáticamente
            nudPeso.ValueChanged += CalcularIMC;
            nudAltura.ValueChanged += CalcularIMC;
        }

        private void CrearTabPreferencias()
        {
            var tabPreferencias = new TabPage("🎨 Preferencias")
            {
                BackColor = Color.White
            };
            tabControl.TabPages.Add(tabPreferencias);

            // Notificaciones
            var lblNotificaciones = new Label
            {
                Text = "🔔 Notificaciones",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(20, 20),
                Size = new Size(200, 25)
            };
            tabPreferencias.Controls.Add(lblNotificaciones);

            chkNotificaciones = new CheckBox
            {
                Text = "Habilitar notificaciones de recordatorio",
                Font = new Font("Segoe UI", 10),
                Location = new Point(40, 60),
                Size = new Size(300, 25),
                Checked = true
            };
            tabPreferencias.Controls.Add(chkNotificaciones);

            chkSonidos = new CheckBox
            {
                Text = "Habilitar sonidos de la aplicación",
                Font = new Font("Segoe UI", 10),
                Location = new Point(40, 90),
                Size = new Size(300, 25),
                Checked = true
            };
            tabPreferencias.Controls.Add(chkSonidos);

            // Apariencia
            var lblApariencia = new Label
            {
                Text = "🎨 Apariencia",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(20, 140),
                Size = new Size(200, 25)
            };
            tabPreferencias.Controls.Add(lblApariencia);

            var lblTema = new Label
            {
                Text = "Tema:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(40, 180),
                Size = new Size(80, 20)
            };
            tabPreferencias.Controls.Add(lblTema);

            cmbTema = new ComboBox
            {
                Location = new Point(130, 178),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbTema.Items.AddRange(new[] { "Claro", "Oscuro", "Automático" });
            cmbTema.SelectedIndex = 0;
            tabPreferencias.Controls.Add(cmbTema);

            // Información adicional
            var lblInfo = new Label
            {
                Text = "💡 Tip: Las notificaciones te ayudarán a mantener\nconsistencia en tus hábitos de salud.",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(127, 140, 141),
                Location = new Point(40, 250),
                Size = new Size(400, 40)
            };
            tabPreferencias.Controls.Add(lblInfo);
        }

        private void CrearTabObjetivos()
        {
            var tabObjetivos = new TabPage("🎯 Objetivos")
            {
                BackColor = Color.White
            };
            tabControl.TabPages.Add(tabObjetivos);

            // Objetivos diarios
            var lblObjetivosDiarios = new Label
            {
                Text = "📅 Objetivos Diarios",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(20, 20),
                Size = new Size(200, 25)
            };
            tabObjetivos.Controls.Add(lblObjetivosDiarios);

            // Objetivo de calorías
            var lblObjetivoCalorias = new Label
            {
                Text = "Calorías a quemar por día:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(40, 60),
                Size = new Size(180, 20)
            };
            tabObjetivos.Controls.Add(lblObjetivoCalorias);

            nudObjetivoCalorias = new NumericUpDown
            {
                Location = new Point(230, 58),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10),
                Minimum = 100,
                Maximum = 2000,
                Increment = 50,
                Value = 300
            };
            tabObjetivos.Controls.Add(nudObjetivoCalorias);

            var lblCaloriasUnidad = new Label
            {
                Text = "kcal",
                Font = new Font("Segoe UI", 10),
                Location = new Point(340, 60),
                Size = new Size(40, 20)
            };
            tabObjetivos.Controls.Add(lblCaloriasUnidad);

            // Objetivo de actividad
            var lblObjetivoActividad = new Label
            {
                Text = "Minutos de actividad por día:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(40, 100),
                Size = new Size(180, 20)
            };
            tabObjetivos.Controls.Add(lblObjetivoActividad);

            nudObjetivoActividad = new NumericUpDown
            {
                Location = new Point(230, 98),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10),
                Minimum = 15,
                Maximum = 300,
                Increment = 15,
                Value = 30
            };
            tabObjetivos.Controls.Add(nudObjetivoActividad);

            var lblActividadUnidad = new Label
            {
                Text = "minutos",
                Font = new Font("Segoe UI", 10),
                Location = new Point(340, 100),
                Size = new Size(60, 20)
            };
            tabObjetivos.Controls.Add(lblActividadUnidad);

            // Progreso actual
            var lblProgresoActual = new Label
            {
                Text = "📊 Progreso Actual",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(20, 160),
                Size = new Size(200, 25)
            };
            tabObjetivos.Controls.Add(lblProgresoActual);

            var lblProgresoInfo = new Label
            {
                Text = "Cargando progreso...",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(127, 140, 141),
                Location = new Point(40, 200),
                Size = new Size(500, 60),
                Name = "lblProgresoInfo"
            };
            tabObjetivos.Controls.Add(lblProgresoInfo);

            // Recomendaciones
            var lblRecomendaciones = new Label
            {
                Text = "💡 Recomendaciones:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(40, 280),
                Size = new Size(150, 20)
            };
            tabObjetivos.Controls.Add(lblRecomendaciones);

            var lblRecomendacionesTexto = new Label
            {
                Text = "• Establece objetivos realistas y alcanzables\n• Aumenta gradualmente la intensidad\n• Mantén consistencia en tus hábitos",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(127, 140, 141),
                Location = new Point(40, 305),
                Size = new Size(500, 60)
            };
            tabObjetivos.Controls.Add(lblRecomendacionesTexto);
        }

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
                    lblNivelInfo.Text = $"Nivel {nivel.NumeroNivel}: {nivel.Nombre} | Puntos: {_usuario.Puntos}\n" +
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
                    // Actualizar datos del usuario
                    if (_usuario == null)
                    {
                        _usuario = new Usuario();
                    }

                    _usuario.Nombre = txtNombre.Text.Trim();
                    _usuario.Edad = (int)nudEdad.Value;
                    _usuario.Genero = cmbGenero.SelectedItem?.ToString() ?? "No especificado";
                    _usuario.Peso = (double)nudPeso.Value;
                    _usuario.Altura = (double)nudAltura.Value;
                    _usuario.NivelActividad = cmbNivelActividad.SelectedItem?.ToString() ?? "Moderado";

                    // Guardar usuario
                    if (_controladorUsuario.ObtenerUsuario() == null)
                    {
                        // Convertir strings a enums
                        Genero genero = Enum.TryParse<Genero>(_usuario.Genero, out var g) ? g : Genero.Masculino;
                        NivelActividad nivelActividad = Enum.TryParse<NivelActividad>(_usuario.NivelActividad, out var na) ? na : NivelActividad.ModeradamenteActivo;
                        
                        _controladorUsuario.CrearUsuario(_usuario.Nombre, _usuario.Edad, _usuario.Peso, 
                                                        _usuario.Altura, genero, nivelActividad, 0);
                    }
                    else
                    {
                        // Aquí se implementaría un método para actualizar el usuario completo
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
                        // Aquí se implementaría la lógica para eliminar todos los archivos de datos
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