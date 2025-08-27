using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Habitus.Controladores;
using Habitus.Modelos;

namespace Habitus.Vistas
{
    public partial class FormRetos : Form
    {
        private ControladorRetos _controladorRetos;
        private ControladorUsuario _controladorUsuario;
        private ControladorNiveles _controladorNiveles;
        private TabControl tabControl;
        private ListView lstRetosDisponibles;
        private ListView lstRetosActivos;
        private Button btnAsignarReto;
        private Button btnCompletarReto;
        private Button btnGenerarRetos;
        private Label lblInfoReto;
        private Usuario _usuario;

        public FormRetos()
        {
            InitializeComponent();
            _controladorRetos = new ControladorRetos();
            _controladorUsuario = new ControladorUsuario();
            _controladorNiveles = new ControladorNiveles();
            _usuario = _controladorUsuario.ObtenerUsuario();
            InicializarComponentes();
            CargarRetos();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // FormRetos
            // 
            BackColor = Color.FromArgb(240, 248, 255);
            ClientSize = new Size(782, 753);
            Name = "FormRetos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Retos - Habitus";
            ResumeLayout(false);
        }

        private void InicializarComponentes()
        {
            // Título
            var lblTitulo = new Label
            {
                Text = "Centro de Retos",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                Location = new Point(50, 20),
                Size = new Size(700, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblTitulo);

            // Información del usuario
            var lblInfoUsuario = new Label
            {
                Text = _usuario != null ? $"Nivel: {_controladorNiveles.ObtenerNivelActual(_usuario.Puntos).NumeroNivel} | Puntos: {_usuario.Puntos}" : "Usuario no encontrado",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(50, 55),
                Size = new Size(700, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblInfoUsuario);

            // TabControl
            tabControl = new TabControl
            {
                Location = new Point(20, 90),
                Size = new Size(740, 400),
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(tabControl);

            // Tab Retos Disponibles
            var tabDisponibles = new TabPage("Retos Disponibles")
            {
                BackColor = Color.White
            };
            tabControl.TabPages.Add(tabDisponibles);

            // Tab Retos Activos
            var tabActivos = new TabPage("Mis Retos Activos")
            {
                BackColor = Color.White
            };
            tabControl.TabPages.Add(tabActivos);

            CrearTabRetosDisponibles(tabDisponibles);
            CrearTabRetosActivos(tabActivos);

            // Panel de información del reto
            var panelInfo = new Panel
            {
                Location = new Point(20, 500),
                Size = new Size(740, 230),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(panelInfo);

            var lblTituloInfo = new Label
            {
                Text = "Información del Reto",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(150, 20)
            };
            panelInfo.Controls.Add(lblTituloInfo);

            lblInfoReto = new Label
            {
                Text = "Selecciona un reto para ver más información",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.Gray,
                Padding = new Padding(10),
                Size = new Size(720, 200),
                Dock = DockStyle.Bottom
            };
            panelInfo.Controls.Add(lblInfoReto);
        }

        private void CrearTabRetosDisponibles(TabPage tab)
        {
            // Lista de retos disponibles
            lstRetosDisponibles = new ListView
            {
                Location = new Point(10, 10),
                Size = new Size(500, 320),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Font = new Font("Segoe UI", 9)
            };
            
            lstRetosDisponibles.Columns.Add("Nombre", 150);
            lstRetosDisponibles.Columns.Add("Dificultad", 80);
            lstRetosDisponibles.Columns.Add("Puntos", 60);
            lstRetosDisponibles.Columns.Add("Nivel Req.", 70);
            lstRetosDisponibles.Columns.Add("Duración", 100);
            
            lstRetosDisponibles.SelectedIndexChanged += LstRetosDisponibles_SelectedIndexChanged;
            tab.Controls.Add(lstRetosDisponibles);

            // Botones
            btnAsignarReto = new Button
            {
                Text = "Asignar Reto",
                Location = new Point(530, 50),
                Size = new Size(120, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };
            btnAsignarReto.FlatAppearance.BorderSize = 0;
            btnAsignarReto.Click += BtnAsignarReto_Click;
            tab.Controls.Add(btnAsignarReto);

            btnGenerarRetos = new Button
            {
                Text = "Generar Nuevos\nRetos",
                Location = new Point(530, 100),
                Size = new Size(120, 50),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnGenerarRetos.FlatAppearance.BorderSize = 0;
            btnGenerarRetos.Click += BtnGenerarRetos_Click;
            tab.Controls.Add(btnGenerarRetos);

            // Información adicional
            var lblInfo = new Label
            {
                Text = "💡 Los retos se generan automáticamente\nbasados en tu nivel actual.\n\n🏆 Completa retos para ganar puntos\ny subir de nivel.",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(127, 140, 141),
                Location = new Point(530, 170),
                Size = new Size(180, 100)
            };
            tab.Controls.Add(lblInfo);
        }

        private void CrearTabRetosActivos(TabPage tab)
        {
            // Lista de retos activos
            lstRetosActivos = new ListView
            {
                Location = new Point(10, 10),
                Size = new Size(500, 320),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Font = new Font("Segoe UI", 9)
            };
            
            lstRetosActivos.Columns.Add("Nombre", 150);
            lstRetosActivos.Columns.Add("Estado", 80);
            lstRetosActivos.Columns.Add("Puntos", 60);
            lstRetosActivos.Columns.Add("Fecha Inicio", 90);
            lstRetosActivos.Columns.Add("Fecha Fin", 90);
            
            lstRetosActivos.SelectedIndexChanged += LstRetosActivos_SelectedIndexChanged;
            tab.Controls.Add(lstRetosActivos);

            // Botón completar reto
            btnCompletarReto = new Button
            {
                Text = "Completar Reto",
                Location = new Point(530, 50),
                Size = new Size(120, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(230, 126, 34),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };
            btnCompletarReto.FlatAppearance.BorderSize = 0;
            btnCompletarReto.Click += BtnCompletarReto_Click;
            tab.Controls.Add(btnCompletarReto);

            // Estadísticas de retos
            var lblEstadisticas = new Label
            {
                Text = "📊 Estadísticas de Retos",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(530, 120),
                Size = new Size(180, 20)
            };
            tab.Controls.Add(lblEstadisticas);

            var lblStats = new Label
            {
                Text = "Cargando estadísticas...",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(127, 140, 141),
                Location = new Point(530, 150),
                Size = new Size(180, 100)
            };
            tab.Controls.Add(lblStats);

            // Actualizar estadísticas
            ActualizarEstadisticasRetos(lblStats);
        }

        private void CargarRetos()
        {
            CargarRetosDisponibles();
            CargarRetosActivos();
        }

        private void CargarRetosDisponibles()
        {
            lstRetosDisponibles.Items.Clear();
            var nivelUsuario = _usuario != null ? _controladorNiveles.ObtenerNivelActual(_usuario.Puntos).NumeroNivel : 1;
            var puntosUsuario = _usuario != null ? _usuario.Puntos : 0;
            var retosDisponibles = _controladorRetos.ObtenerRetosDisponibles(nivelUsuario, puntosUsuario);

            foreach (var reto in retosDisponibles)
            {
                if (reto.NivelRequerido <= nivelUsuario)
                {
                    var item = new ListViewItem(reto.Nombre);
                    item.SubItems.Add(reto.Dificultad.ToString());
                    item.SubItems.Add(reto.PuntosRecompensa.ToString());
                    item.SubItems.Add(reto.NivelRequerido.ToString());
                    item.SubItems.Add($"{reto.FechaInicio:dd/MM} - {reto.FechaFin:dd/MM}");
                    item.Tag = reto;
                    
                    // Colorear según dificultad
                    switch (reto.Dificultad.ToLower())
                    {
                        case "fácil":
                            item.BackColor = Color.FromArgb(230, 255, 230);
                            break;
                        case "medio":
                            item.BackColor = Color.FromArgb(255, 255, 230);
                            break;
                        case "difícil":
                            item.BackColor = Color.FromArgb(255, 230, 230);
                            break;
                    }
                    
                    lstRetosDisponibles.Items.Add(item);
                }
            }
        }

        private void CargarRetosActivos()
        {
            lstRetosActivos.Items.Clear();
            var retosActivos = _controladorRetos.ObtenerRetosActivos();

            foreach (var reto in retosActivos)
            {
                var item = new ListViewItem(reto.Nombre);
                item.SubItems.Add(reto.Completado ? "Completado" : "En Progreso");
                item.SubItems.Add(reto.PuntosRecompensa.ToString());
                item.SubItems.Add(reto.FechaInicio.ToString("dd/MM/yyyy"));
                item.SubItems.Add(reto.FechaFin.ToString("dd/MM/yyyy"));
                item.Tag = reto;
                
                // Colorear según estado
                if (reto.Completado)
                {
                    item.BackColor = Color.FromArgb(230, 255, 230);
                    item.ForeColor = Color.FromArgb(39, 174, 96);
                }
                else if (reto.FechaFin < DateTime.Today)
                {
                    item.BackColor = Color.FromArgb(255, 230, 230);
                    item.ForeColor = Color.FromArgb(231, 76, 60);
                }
                
                lstRetosActivos.Items.Add(item);
            }
        }

        private void LstRetosDisponibles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRetosDisponibles.SelectedItems.Count > 0)
            {
                var reto = (Reto)lstRetosDisponibles.SelectedItems[0].Tag;
                MostrarInformacionReto(reto);
                btnAsignarReto.Enabled = true;
            }
            else
            {
                btnAsignarReto.Enabled = false;
            }
        }

        private void LstRetosActivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRetosActivos.SelectedItems.Count > 0)
            {
                var reto = (Reto)lstRetosActivos.SelectedItems[0].Tag;
                MostrarInformacionReto(reto);
                btnCompletarReto.Enabled = !reto.Completado && reto.FechaFin >= DateTime.Today;
            }
            else
            {
                btnCompletarReto.Enabled = false;
            }
        }

        private void MostrarInformacionReto(Reto reto)
        {
            var estado = reto.Completado ? "✅ Completado" : 
                        reto.FechaFin < DateTime.Today ? "❌ Expirado" : "🔄 En Progreso";
            
            lblInfoReto.Text = $"📝 {reto.Nombre}\n" +
                              $"📖 {reto.Descripcion}\n" +
                              $"🏆 Puntos: {reto.PuntosRecompensa} | 📊 Dificultad: {reto.Dificultad} | 🎯 Nivel: {reto.NivelRequerido} | {estado}";
        }

        private void BtnAsignarReto_Click(object sender, EventArgs e)
        {
            if (lstRetosDisponibles.SelectedItems.Count > 0)
            {
                var reto = (Reto)lstRetosDisponibles.SelectedItems[0].Tag;
                
                var result = MessageBox.Show($"¿Deseas asignarte el reto '{reto.Nombre}'?\n\n" +
                                           $"Descripción: {reto.Descripcion}\n" +
                                           $"Puntos de recompensa: {reto.PuntosRecompensa}\n" +
                                           $"Dificultad: {reto.Dificultad}", 
                                           "Confirmar Asignación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        _controladorRetos.AsignarReto(reto);
                        MessageBox.Show($"¡Reto '{reto.Nombre}' asignado exitosamente!\n\n" +
                                       "Puedes ver tu progreso en la pestaña 'Mis Retos Activos'.", 
                                       "Reto Asignado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        CargarRetos(); // Actualizar listas
                        tabControl.SelectedIndex = 1; // Cambiar a pestaña de retos activos
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al asignar el reto: {ex.Message}", "Error", 
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnCompletarReto_Click(object sender, EventArgs e)
        {
            if (lstRetosActivos.SelectedItems.Count > 0)
            {
                var reto = (Reto)lstRetosActivos.SelectedItems[0].Tag;
                
                if (reto.Completado)
                {
                    MessageBox.Show("Este reto ya ha sido completado.", "Reto Completado", 
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                var result = MessageBox.Show($"¿Has completado el reto '{reto.Nombre}'?\n\n" +
                                           $"Al confirmar, ganarás {reto.PuntosRecompensa} puntos.", 
                                           "Confirmar Completación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        _controladorRetos.CompletarReto(reto.Id);
                        _controladorUsuario.ActualizarPuntos(reto.PuntosRecompensa);
                        
                        MessageBox.Show($"¡Felicidades! Has completado el reto '{reto.Nombre}'.\n\n" +
                                       $"Has ganado {reto.PuntosRecompensa} puntos.", 
                                       "¡Reto Completado!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        CargarRetos(); // Actualizar listas
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al completar el reto: {ex.Message}", "Error", 
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnGenerarRetos_Click(object sender, EventArgs e)
        {
            try
            {
                var nivelUsuario = _usuario != null ? _controladorNiveles.ObtenerNivelActual(_usuario.Puntos).NumeroNivel : 1;
                _controladorRetos.GenerarRetosAutomaticos(nivelUsuario);
                
                MessageBox.Show("¡Nuevos retos generados exitosamente!\n\n" +
                               "Los retos se han adaptado a tu nivel actual.", 
                               "Retos Generados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                CargarRetosDisponibles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar retos: {ex.Message}", "Error", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarEstadisticasRetos(Label lblStats)
        {
            try
            {
                var retosActivos = _controladorRetos.ObtenerRetosActivos();
                var retosCompletados = retosActivos.Count(r => r.Completado);
                var retosEnProgreso = retosActivos.Count(r => !r.Completado && r.FechaFin >= DateTime.Today);
                var retosExpirados = retosActivos.Count(r => !r.Completado && r.FechaFin < DateTime.Today);
                var puntosGanados = _controladorRetos.ObtenerPuntosRetos();
                
                lblStats.Text = $"✅ Completados: {retosCompletados}\n" +
                               $"🔄 En Progreso: {retosEnProgreso}\n" +
                               $"❌ Expirados: {retosExpirados}\n" +
                               $"🏆 Puntos Ganados: {puntosGanados}";
            }
            catch (Exception ex)
            {
                lblStats.Text = $"Error al cargar estadísticas:\n{ex.Message}";
            }
        }
    }
}