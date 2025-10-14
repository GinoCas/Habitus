using System;
using System.Drawing;
using System.Windows.Forms;
using Habitus.Controladores;
using Habitus.Modelos;

namespace Habitus.Vistas
{
    public partial class FormPrincipal : Form
    {
        private ControladorPerfilUsuario _controladorUsuario;
        private ControladorNiveles _controladorNiveles;
        private ControladorProgreso _controladorProgreso;
        private ControladorRetos _controladorRetos;
        private PerfilUsuario _usuario;
        private Panel panelResumenDia;

        public FormPrincipal()
        {
            InitializeComponent();
            InicializarControladores();
            CargarDatosUsuario();
        }

        private void InicializarControladores()
        {
            _controladorUsuario = new ControladorPerfilUsuario();
            _controladorNiveles = new ControladorNiveles();
            _controladorProgreso = new ControladorProgreso();
            _controladorRetos = new ControladorRetos();
        }



        private void CrearPanelSuperior()
        {
            // Bienvenida
            lblBienvenida = new Label
            {
                Text = "Bienvenido/a",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 15),
                Size = new Size(300, 25),
                AutoSize = false
            };
            panelSuperior.Controls.Add(lblBienvenida);

            // Nivel
            lblNivel = new Label
            {
                Text = "Nivel 1",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                Location = new Point(20, 45),
                Size = new Size(70, 20)
            };
            panelSuperior.Controls.Add(lblNivel);

            // Barra de progreso del nivel
            progressBarNivel = new ProgressBar
            {
                Location = new Point(95, 47),
                Size = new Size(150, 16),
                Style = ProgressBarStyle.Continuous
            };
            panelSuperior.Controls.Add(progressBarNivel);
        }

        private void CrearPanelLateral()
        {
            int yPos = 20;
            int spacing = 50;

            // Botón Registrar Actividad
            btnRegistrarActividad = CrearBotonMenu("Registrar Actividad", yPos);
            btnRegistrarActividad.Click += BtnRegistrarActividad_Click;
            panelLateral.Controls.Add(btnRegistrarActividad);
            yPos += spacing;

            // Botón Registrar Comida
            btnRegistrarComida = CrearBotonMenu("Registrar Comida", yPos);
            btnRegistrarComida.Click += BtnRegistrarComida_Click;
            panelLateral.Controls.Add(btnRegistrarComida);
            yPos += spacing;

            // Botón Retos
            btnRetos = CrearBotonMenu("Retos", yPos);
            btnRetos.Click += BtnRetos_Click;
            panelLateral.Controls.Add(btnRetos);
            yPos += spacing;

            // Botón Estadísticas
            btnEstadisticas = CrearBotonMenu("Estadísticas", yPos);
            btnEstadisticas.Click += BtnEstadisticas_Click;
            panelLateral.Controls.Add(btnEstadisticas);
            yPos += spacing;

            // Botón Configuración
            btnConfiguracion = CrearBotonMenu("Configuración", yPos);
            btnConfiguracion.Click += BtnConfiguracion_Click;
            panelLateral.Controls.Add(btnConfiguracion);
        }

        private Button CrearBotonMenu(string texto, int yPos)
        {
            return new Button
            {
                Text = texto,
                Location = new Point(10, yPos),
                Size = new Size(180, 40),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };
        }

        private Panel panelRetosActivos;
        private Panel panelProgresoReciente;
        
        private void CrearPanelContenido()
        {
            // Panel de resumen del día
            panelResumenDia = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(740, 200),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            panelContenido.Controls.Add(panelResumenDia);

            // Título del resumen
            var lblTituloResumen = new Label
            {
                Text = "Resumen del Día",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(20, 15),
                Size = new Size(200, 25)
            };
            panelResumenDia.Controls.Add(lblTituloResumen);

            // Crear tarjetas de resumen
            CrearTarjetasResumen(panelResumenDia);

            // Panel de retos activos
            panelRetosActivos = new Panel
            {
                Location = new Point(20, 240),
                Size = new Size(360, 300),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            panelContenido.Controls.Add(panelRetosActivos);

            var lblTituloRetos = new Label
            {
                Text = "Retos Activos",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(15, 15),
                Size = new Size(150, 25)
            };
            panelRetosActivos.Controls.Add(lblTituloRetos);

            // Panel de progreso reciente
            panelProgresoReciente = new Panel
            {
                Location = new Point(400, 240),
                Size = new Size(360, 300),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            panelContenido.Controls.Add(panelProgresoReciente);

            var lblTituloProgreso = new Label
            {
                Text = "Progreso Reciente",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(15, 15),
                Size = new Size(150, 25)
            };
            panelProgresoReciente.Controls.Add(lblTituloProgreso);
        }

        private void CrearTarjetasResumen(Panel panelPadre)
        {
            var tarjetas = new[]
            {
                new { Titulo = "Calorías\nConsumidas", Valor = "0", Color = Color.FromArgb(231, 76, 60) },
                new { Titulo = "Calorías\nQuemadas", Valor = "0", Color = Color.FromArgb(230, 126, 34) },
                new { Titulo = "Minutos de\nActividad", Valor = "0", Color = Color.FromArgb(39, 174, 96) },
               
            };

            for (int i = 0; i < tarjetas.Length; i++)
            {
                var tarjeta = new Panel
                {
                    Location = new Point(20 + (i * 170), 50),
                    Size = new Size(150, 120),
                    BackColor = tarjetas[i].Color
                };
                panelPadre.Controls.Add(tarjeta);

                var lblTitulo = new Label
                {
                    Text = tarjetas[i].Titulo,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.White,
                    Location = new Point(10, 15),
                    Size = new Size(130, 40),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                tarjeta.Controls.Add(lblTitulo);

                var lblValor = new Label
                {
                    Text = tarjetas[i].Valor,
                    Font = new Font("Segoe UI", 18, FontStyle.Bold),
                    ForeColor = Color.White,
                    Location = new Point(10, 60),
                    Size = new Size(130, 40),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                tarjeta.Controls.Add(lblValor);
            }
        }

        private void CargarDatosUsuario()
        {
            _usuario = _controladorUsuario.ObtenerUsuario();
            if (_usuario != null)
            {
                lblBienvenida.Text = $"Bienvenido/a, {_usuario.Nombre}";
                lblPuntos.Text = $"Puntos: {_usuario.Puntos}";
                
                var nivelActual = _controladorNiveles.ObtenerNivelActual(_usuario.Puntos);
                lblNivel.Text = $"Nivel {nivelActual.Numero}";
                
                var progresoNivel = _controladorNiveles.CalcularProgresoNivel(_usuario.Puntos);
                progressBarNivel.Value = Math.Min(100, (int)progresoNivel);
                ActualizarTarjetasResumen();
                CargarRetosActivos();
                CargarProgresoReciente();
            }
        }

        private void BtnRegistrarActividad_Click(object sender, EventArgs e)
        {
            var formActividad = new FormRegistrarActividad(_controladorProgreso);
            if (formActividad.ShowDialog() == DialogResult.OK)
            {
                CargarDatosUsuario(); // Actualizar datos después del registro
            }
        }

        private void BtnRegistrarComida_Click(object sender, EventArgs e)
        {
            var formComida = new FormRegistrarComida(_controladorProgreso);
            if (formComida.ShowDialog() == DialogResult.OK)
            {
                CargarDatosUsuario(); // Actualizar datos después del registro
            }
        }



        private void BtnRetos_Click(object sender, EventArgs e)
        {
            var formRetos = new FormRetos(_controladorProgreso);
            if (formRetos.ShowDialog() == DialogResult.OK)
            {
                CargarDatosUsuario(); // Actualizar datos después de cambios en retos
            }
        }

        private void BtnEstadisticas_Click(object sender, EventArgs e)
        {
            var formEstadisticas = new FormEstadisticas();
            formEstadisticas.ShowDialog();
        }

        private void BtnConfiguracion_Click(object sender, EventArgs e)
        {
            var formConfiguracion = new FormConfiguracion();
            if (formConfiguracion.ShowDialog() == DialogResult.OK)
            {
                CargarDatosUsuario(); // Actualizar datos después de cambios
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            var result = MessageBox.Show("¿Estás seguro de que deseas salir de Habitus?", 
                                       "Confirmar Salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            base.OnFormClosing(e);
        }

        private void CargarRetosActivos()
        {
            // Limpiar controles existentes excepto el título
            for (int i = panelRetosActivos.Controls.Count - 1; i >= 0; i--)
            {
                if (!(panelRetosActivos.Controls[i] is Label label && label.Text == "Retos Activos"))
                {
                    panelRetosActivos.Controls.RemoveAt(i);
                }
            }
            // Obtener retos activos
            var retosActivos = _controladorRetos.ObtenerRetosActivos();
            foreach(var reto in retosActivos)
            {
                MessageBox.Show(reto.ToString());
            }


            if (retosActivos.Count == 0)
            {
                var lblNoRetos = new Label
                {
                    Text = "No tienes retos activos. ¡Asigna uno nuevo desde la sección de Retos!",
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.FromArgb(52, 73, 94),
                    Location = new Point(15, 50),
                    Size = new Size(330, 40),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                panelRetosActivos.Controls.Add(lblNoRetos);
                return;
            }
            
            // Crear panel para contener los retos
            var panelListaRetos = new Panel
            {
                Location = new Point(10, 50),
                Size = new Size(340, 240),
                AutoScroll = true
            };
            panelRetosActivos.Controls.Add(panelListaRetos);
            
            // Mostrar retos activos
            int yPos = 5;
            foreach (var reto in retosActivos)
            {
                var panelReto = new Panel
                {
                    Location = new Point(5, yPos),
                    Size = new Size(310, 80),
                    BackColor = Color.FromArgb(240, 248, 255),
                    BorderStyle = BorderStyle.FixedSingle
                };
                
                var lblNombreReto = new Label
                {
                    Text = reto.Nombre,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(41, 128, 185),
                    Location = new Point(10, 5),
                    Size = new Size(290, 20)
                };
                panelReto.Controls.Add(lblNombreReto);
                
                var lblDescripcion = new Label
                {
                    Text = reto.Descripcion,
                    Font = new Font("Segoe UI", 8),
                    ForeColor = Color.FromArgb(52, 73, 94),
                    Location = new Point(10, 25),
                    Size = new Size(290, 30)
                };
                panelReto.Controls.Add(lblDescripcion);
                
                var lblFecha = new Label
                {
                    Text = $"Vence: {reto.FechaFin.ToShortDateString()}",
                    Font = new Font("Segoe UI", 8),
                    ForeColor = Color.FromArgb(231, 76, 60),
                    Location = new Point(10, 55),
                    Size = new Size(150, 15)
                };
                panelReto.Controls.Add(lblFecha);
                // TODO: arreglar que los puntos siempre son 0. -> Baja prioridad
                var lblPuntos = new Label
                {
                    Text = $"+{reto.PuntosRecompensa} pts",
                    Font = new Font("Segoe UI", 8, FontStyle.Bold),
                    ForeColor = Color.FromArgb(39, 174, 96),
                    Location = new Point(220, 55),
                    Size = new Size(80, 15),
                    TextAlign = ContentAlignment.MiddleRight
                };
                panelReto.Controls.Add(lblPuntos);
                
                panelListaRetos.Controls.Add(panelReto);
                yPos += 85;
            }
        }
        
        private void CargarProgresoReciente()
        {
            // Limpiar controles existentes excepto el título
            for (int i = panelProgresoReciente.Controls.Count - 1; i >= 0; i--)
            {
                if (!(panelProgresoReciente.Controls[i] is Label label && label.Text == "Progreso Reciente"))
                {
                    panelProgresoReciente.Controls.RemoveAt(i);
                }
            }
            
            // Obtener progreso reciente (últimos 7 días)
            var fechaFin = DateTime.Now.Date;
            var fechaInicio = fechaFin.AddDays(-7);
            var registrosProgreso = _controladorProgreso.ObtenerProgresoPorPeriodo(fechaInicio, fechaFin);
            
            if (registrosProgreso.Count == 0)
            {
                var lblNoProgreso = new Label
                {
                    Text = "No hay registros de progreso recientes. ¡Registra tu actividad diaria!",
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.FromArgb(52, 73, 94),
                    Location = new Point(15, 50),
                    Size = new Size(330, 40),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                panelProgresoReciente.Controls.Add(lblNoProgreso);
                return;
            }
            
            // Crear panel para contener el progreso
            var panelListaProgreso = new Panel
            {
                Location = new Point(10, 50),
                Size = new Size(340, 240),
                AutoScroll = true
            };
            panelProgresoReciente.Controls.Add(panelListaProgreso);
            
            // Mostrar progreso reciente
            int yPos = 5;
            foreach (var progreso in registrosProgreso.OrderByDescending(p => p.Fecha))
            {
                var panelProgreso = new Panel
                {
                    Location = new Point(5, yPos),
                    Size = new Size(310, 70),
                    BackColor = Color.FromArgb(240, 248, 255),
                    BorderStyle = BorderStyle.FixedSingle
                };
                
                var lblFecha = new Label
                {
                    Text = progreso.Fecha.ToShortDateString(),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(41, 128, 185),
                    Location = new Point(10, 5),
                    Size = new Size(290, 20)
                };
                panelProgreso.Controls.Add(lblFecha);
                
                var lblCalorias = new Label
                {
                    Text = $"Calorías: {progreso.CaloriasConsumidas} consumidas, {progreso.CaloriasQuemadas} quemadas",
                    Font = new Font("Segoe UI", 8),
                    ForeColor = Color.FromArgb(52, 73, 94),
                    Location = new Point(10, 25),
                    Size = new Size(290, 15)
                };
                panelProgreso.Controls.Add(lblCalorias);
                
                var lblActividad = new Label
                {
                    Text = $"Actividad: {progreso.MinutosActividad} min, {progreso.PasosRealizados} pasos",
                    Font = new Font("Segoe UI", 8),
                    ForeColor = Color.FromArgb(52, 73, 94),
                    Location = new Point(10, 40),
                    Size = new Size(200, 15)
                };
                panelProgreso.Controls.Add(lblActividad);
                
                var lblPuntos = new Label
                {
                    Text = $"+{progreso.PuntosGanados} pts",
                    Font = new Font("Segoe UI", 8, FontStyle.Bold),
                    ForeColor = Color.FromArgb(39, 174, 96),
                    Location = new Point(220, 40),
                    Size = new Size(80, 15),
                    TextAlign = ContentAlignment.MiddleRight
                };
                panelProgreso.Controls.Add(lblPuntos);
                
                panelListaProgreso.Controls.Add(panelProgreso);
                yPos += 75;
            }
        }

        private void ActualizarTarjetasResumen()
        {
            // Obtener el progreso del día actual
            var progresoHoy = _controladorProgreso.ObtenerProgresoPorFecha(DateTime.Now.Date);
            
            // Actualizar las tarjetas con los valores del progreso
            if (progresoHoy != null)
            {
                // Buscar las tarjetas por su posición y actualizar sus valores
                foreach (Control control in panelResumenDia.Controls)
                {
                    if (control is Panel tarjeta)
                    {
                        // Obtener la etiqueta de valor (segunda etiqueta en la tarjeta)
                        var lblValor = tarjeta.Controls.OfType<Label>().ElementAtOrDefault(1);
                        if (lblValor != null)
                        {
                            // Actualizar el valor según el título de la tarjeta
                            var lblTitulo = tarjeta.Controls.OfType<Label>().FirstOrDefault();
                            if (lblTitulo != null)
                            {
                                if (lblTitulo.Text.Contains("Calorías\nConsumidas"))
                                    lblValor.Text = progresoHoy.CaloriasConsumidas.ToString("0");
                                else if (lblTitulo.Text.Contains("Calorías\nQuemadas"))
                                    lblValor.Text = progresoHoy.CaloriasQuemadas.ToString("0");
                                else if (lblTitulo.Text.Contains("Minutos de\nActividad"))
                                    lblValor.Text = progresoHoy.MinutosActividad.ToString();
                                else if (lblTitulo.Text.Contains("Pasos\nRealizados"))
                                    lblValor.Text = progresoHoy.PasosRealizados.ToString();
                            }
                        }
                    }
                }
            }
            else
            {
                // Si no hay progreso hoy, mostrar valores en cero
                foreach (Control control in panelResumenDia.Controls)
                {
                    if (control is Panel tarjeta)
                    {
                        var lblValor = tarjeta.Controls.OfType<Label>().ElementAtOrDefault(1);
                        if (lblValor != null)
                        {
                            lblValor.Text = "0";
                        }
                    }
                }
            }
        }
    }
}