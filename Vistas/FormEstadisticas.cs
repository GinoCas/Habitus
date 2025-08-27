using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Habitus.Controladores;
using Habitus.Modelos;

namespace Habitus.Vistas
{
    public partial class FormEstadisticas : Form
    {
        private ControladorProgreso _controladorProgreso;
        private ControladorUsuario _controladorUsuario;
        private ControladorActividad _controladorActividad;
        private ControladorComida _controladorComida;
        private ControladorRetos _controladorRetos;
        private ControladorNiveles _controladorNiveles;
        private Usuario _usuario;

        public FormEstadisticas()
        {
            InitializeComponent();
            _controladorProgreso = new ControladorProgreso();
            _controladorUsuario = new ControladorUsuario();
            _controladorActividad = new ControladorActividad();
            _controladorComida = new ControladorComida();
            _controladorRetos = new ControladorRetos();
            _controladorNiveles = new ControladorNiveles();
            _usuario = _controladorUsuario.ObtenerUsuario();
            
            CrearFiltros(this.Controls.Find("panelFiltros", true).FirstOrDefault() as Panel);
            CrearPanelResumen();
            CrearPanelGraficos();
            CargarEstadisticas();
            CrearPanelTendencias();
        }

        private void CrearFiltros(Panel panel)
        {
            if (panel == null)
            {
                panel = new();
            }
            var lblPeriodo = new Label
            {
                Text = "Período:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(60, 20)
            };
            panel.Controls.Add(lblPeriodo);

            cmbPeriodo = new ComboBox
            {
                Location = new Point(90, 18),
                Size = new Size(120, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9)
            };
            cmbPeriodo.Items.AddRange(new[] { "Última semana", "Último mes", "Últimos 3 meses", "Personalizado" });
            cmbPeriodo.SelectedIndex = 1;
            cmbPeriodo.SelectedIndexChanged += CmbPeriodo_SelectedIndexChanged;
            panel.Controls.Add(cmbPeriodo);

            var lblDesde = new Label
            {
                Text = "Desde:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(240, 20),
                Size = new Size(50, 20)
            };
            panel.Controls.Add(lblDesde);

            dtpFechaInicio = new DateTimePicker
            {
                Location = new Point(295, 18),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 9),
                Value = DateTime.Today.AddMonths(-1),
                Enabled = false
            };
            dtpFechaInicio.ValueChanged += DtpFecha_ValueChanged;
            panel.Controls.Add(dtpFechaInicio);

            var lblHasta = new Label
            {
                Text = "Hasta:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(440, 20),
                Size = new Size(50, 20)
            };
            panel.Controls.Add(lblHasta);

            dtpFechaFin = new DateTimePicker
            {
                Location = new Point(495, 18),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 9),
                Value = DateTime.Today,
                Enabled = false
            };
            dtpFechaFin.ValueChanged += DtpFecha_ValueChanged;
            panel.Controls.Add(dtpFechaFin);

            var btnActualizar = new Button
            {
                Text = "🔄 Actualizar",
                Location = new Point(640, 15),
                Size = new Size(100, 30),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.Click += BtnActualizar_Click;
            panel.Controls.Add(btnActualizar);

            var btnExportar = new Button
            {
                Text = "📄 Exportar",
                Location = new Point(750, 15),
                Size = new Size(100, 30),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnExportar.FlatAppearance.BorderSize = 0;
            btnExportar.Click += BtnExportar_Click;
            panel.Controls.Add(btnExportar);
        }

        private void CrearPanelResumen()
        {
            var lblTitulo = new Label
            {
                Text = "📈 Resumen del Período",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(20, 10),
                Size = new Size(200, 25)
            };
            panelResumen.Controls.Add(lblTitulo);

            // Crear tarjetas de resumen
            CrearTarjetaResumen("🏃 Actividades", "0", "Total registradas", new Point(20, 40), Color.FromArgb(52, 152, 219));
            CrearTarjetaResumen("🔥 Calorías", "0", "Quemadas", new Point(200, 40), Color.FromArgb(231, 76, 60));
            CrearTarjetaResumen("🍎 Comidas", "0", "Registradas", new Point(380, 40), Color.FromArgb(39, 174, 96));
            CrearTarjetaResumen("🏆 Puntos", "0", "Ganados", new Point(560, 40), Color.FromArgb(230, 126, 34));
            CrearTarjetaResumen("🎯 Retos", "0", "Completados", new Point(740, 40), Color.FromArgb(155, 89, 182));
        }

        private void CrearTarjetaResumen(string titulo, string valor, string descripcion, Point ubicacion, Color color)
        {
            var panel = new Panel
            {
                Location = ubicacion,
                Size = new Size(160, 70),
                BackColor = color,
                BorderStyle = BorderStyle.None
            };
            panelResumen.Controls.Add(panel);

            var lblTitulo = new Label
            {
                Text = titulo,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, 5),
                Size = new Size(140, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel.Controls.Add(lblTitulo);

            var lblValor = new Label
            {
                Text = valor,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, 20),
                Size = new Size(140, 25),
                TextAlign = ContentAlignment.MiddleLeft,
                Name = $"valor_{titulo}"
            };
            panel.Controls.Add(lblValor);

            var lblDesc = new Label
            {
                Text = descripcion,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.White,
                Location = new Point(10, 45),
                Size = new Size(140, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel.Controls.Add(lblDesc);
        }

        private void CrearPanelGraficos()
        {
            var lblTitulo = new Label
            {
                Text = "📊 Gráficos de Progreso",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(20, 10),
                Size = new Size(200, 25)
            };
            panelGraficos.Controls.Add(lblTitulo);

            // Simulación de gráfico de actividades
            var panelGraficoActividades = new Panel
            {
                Location = new Point(20, 40),
                Size = new Size(560, 150),
                BackColor = Color.FromArgb(248, 249, 250),
                BorderStyle = BorderStyle.FixedSingle
            };
            panelGraficos.Controls.Add(panelGraficoActividades);

            var lblGraficoActividades = new Label
            {
                Text = "📈 Actividades por Día",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(540, 20)
            };
            panelGraficoActividades.Controls.Add(lblGraficoActividades);

            // Simulación de barras de gráfico
            DibujarGraficoSimulado(panelGraficoActividades, new Point(10, 35), new Size(540, 100));

            // Simulación de gráfico de calorías
            var panelGraficoCalorias = new Panel
            {
                Location = new Point(20, 200),
                Size = new Size(560, 150),
                BackColor = Color.FromArgb(248, 249, 250),
                BorderStyle = BorderStyle.FixedSingle
            };
            panelGraficos.Controls.Add(panelGraficoCalorias);

            var lblGraficoCalorias = new Label
            {
                Text = "🔥 Calorías Quemadas vs Consumidas",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(540, 20)
            };
            panelGraficoCalorias.Controls.Add(lblGraficoCalorias);

            DibujarGraficoCaloriasSimulado(panelGraficoCalorias, new Point(10, 35), new Size(540, 100));
        }

        private void CrearPanelTendencias()
        {
            var lblTitulo = new Label
            {
                Text = "📈 Tendencias y Análisis",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(20, 10),
                Size = new Size(280, 25)
            };
            panelTendencias.Controls.Add(lblTitulo);

            // Lista de tendencias
            var lstTendencias = new ListView
            {
                Location = new Point(20, 40),
                Size = new Size(290, 200),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Font = new Font("Segoe UI", 9),
                HeaderStyle = ColumnHeaderStyle.Nonclickable
            };
            
            lstTendencias.Columns.Add("Métrica", 120);
            lstTendencias.Columns.Add("Tendencia", 80);
            lstTendencias.Columns.Add("Cambio", 70);
            
            panelTendencias.Controls.Add(lstTendencias);

            // Panel de recomendaciones
            var lblRecomendaciones = new Label
            {
                Text = "💡 Recomendaciones",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(20, 250),
                Size = new Size(280, 25)
            };
            panelTendencias.Controls.Add(lblRecomendaciones);

            var txtRecomendaciones = new TextBox
            {
                Location = new Point(20, 280),
                Size = new Size(290, 80),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(248, 249, 250),
                Text = "Cargando recomendaciones...",
                Name = "txtRecomendaciones"
            };
            panelTendencias.Controls.Add(txtRecomendaciones);

            // Cargar datos iniciales
            CargarTendencias(lstTendencias);
            CargarRecomendaciones(txtRecomendaciones);
        }

        private void DibujarGraficoSimulado(Panel panel, Point ubicacion, Size tamaño)
        {
            var panelGrafico = new Panel
            {
                Location = ubicacion,
                Size = tamaño,
                BackColor = Color.White
            };
            panel.Controls.Add(panelGrafico);

            // Simular barras de gráfico
            var random = new Random();
            for (int i = 0; i < 7; i++)
            {
                var altura = random.Next(20, 80);
                var barra = new Panel
                {
                    Location = new Point(50 + i * 70, tamaño.Height - altura - 20),
                    Size = new Size(40, altura),
                    BackColor = Color.FromArgb(52, 152, 219)
                };
                panelGrafico.Controls.Add(barra);

                var lblDia = new Label
                {
                    Text = DateTime.Today.AddDays(-6 + i).ToString("dd/MM"),
                    Font = new Font("Segoe UI", 8),
                    Location = new Point(40 + i * 70, tamaño.Height - 15),
                    Size = new Size(60, 15),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                panelGrafico.Controls.Add(lblDia);
            }
        }

        private void DibujarGraficoCaloriasSimulado(Panel panel, Point ubicacion, Size tamaño)
        {
            var panelGrafico = new Panel
            {
                Location = ubicacion,
                Size = tamaño,
                BackColor = Color.White
            };
            panel.Controls.Add(panelGrafico);

            // Simular líneas de gráfico
            var lblLeyenda = new Label
            {
                Text = "🔥 Quemadas    🍎 Consumidas",
                Font = new Font("Segoe UI", 9),
                Location = new Point(10, 10),
                Size = new Size(200, 20),
                ForeColor = Color.FromArgb(127, 140, 141)
            };
            panelGrafico.Controls.Add(lblLeyenda);

            // Simular puntos de datos
            var random = new Random();
            for (int i = 0; i < 7; i++)
            {
                var x = 50 + i * 70;
                var yQuemadas = 30 + random.Next(0, 40);
                var yConsumidas = 30 + random.Next(0, 40);

                // Punto quemadas
                var puntoQuemadas = new Panel
                {
                    Location = new Point(x - 3, yQuemadas - 3),
                    Size = new Size(6, 6),
                    BackColor = Color.FromArgb(231, 76, 60)
                };
                panelGrafico.Controls.Add(puntoQuemadas);

                // Punto consumidas
                var puntoConsumidas = new Panel
                {
                    Location = new Point(x - 3, yConsumidas - 3),
                    Size = new Size(6, 6),
                    BackColor = Color.FromArgb(39, 174, 96)
                };
                panelGrafico.Controls.Add(puntoConsumidas);
            }
        }

        private void CargarEstadisticas()
        {
            try
            {
                var fechaInicio = dtpFechaInicio.Value.Date;
                var fechaFin = dtpFechaFin.Value.Date;

                // Cargar actividades
                var actividades = _controladorActividad.ObtenerActividadesPorPeriodo(fechaInicio, fechaFin);
                ActualizarTarjetaResumen("🏃 Actividades", actividades.Count.ToString());

                // Cargar calorías quemadas
                var caloriasQuemadas = actividades.Sum(a => a.CaloriasQuemadas);
                ActualizarTarjetaResumen("🔥 Calorías", caloriasQuemadas.ToString("N0"));

                // Cargar comidas
                var comidas = _controladorComida.ObtenerComidasPorPeriodo(fechaInicio, fechaFin);
                ActualizarTarjetaResumen("🍎 Comidas", comidas.Count.ToString());

                // Cargar puntos (simulado)
                var puntosGanados = actividades.Count * 10 + comidas.Count * 5;
                ActualizarTarjetaResumen("🏆 Puntos", puntosGanados.ToString());

                // Cargar retos completados
                var retosCompletados = _controladorRetos.ObtenerRetosActivos().Count(r => r.Completado && r.FechaInicio >= fechaInicio && r.FechaInicio <= fechaFin);
                ActualizarTarjetaResumen("🎯 Retos", retosCompletados.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar estadísticas: {ex.Message}", "Error", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarTarjetaResumen(string titulo, string valor)
        {
            foreach (Control control in panelResumen.Controls)
            {
                if (control is Panel panel)
                {
                    foreach (Control subControl in panel.Controls)
                    {
                        if (subControl is Label lbl && lbl.Name == $"valor_{titulo}")
                        {
                            lbl.Text = valor;
                            break;
                        }
                    }
                }
            }
        }

        private void CargarTendencias(ListView lst)
        {
            lst.Items.Clear();
            
            // Simular tendencias
            var tendencias = new[]
            {
                new { Metrica = "Actividad Física", Tendencia = "↗️ Subiendo", Cambio = "+15%" },
                new { Metrica = "Peso Corporal", Tendencia = "↘️ Bajando", Cambio = "-2%" },
                new { Metrica = "Calorías Quemadas", Tendencia = "↗️ Subiendo", Cambio = "+8%" },
                new { Metrica = "Consistencia", Tendencia = "➡️ Estable", Cambio = "0%" },
                new { Metrica = "Puntos Ganados", Tendencia = "↗️ Subiendo", Cambio = "+12%" }
            };

            foreach (var tendencia in tendencias)
            {
                var item = new ListViewItem(tendencia.Metrica);
                item.SubItems.Add(tendencia.Tendencia);
                item.SubItems.Add(tendencia.Cambio);
                
                if (tendencia.Tendencia.Contains("↗️"))
                    item.BackColor = Color.FromArgb(230, 255, 230);
                else if (tendencia.Tendencia.Contains("↘️"))
                    item.BackColor = Color.FromArgb(255, 240, 240);
                
                lst.Items.Add(item);
            }
        }

        private void CargarRecomendaciones(TextBox txt)
        {
            var recomendaciones = new[]
            {
                "• Mantén la consistencia en tus actividades diarias",
                "• Considera aumentar la intensidad de tus entrenamientos",
                "• Registra más comidas para un mejor seguimiento",
                "• Participa en más retos para ganar puntos adicionales",
                "• Establece metas semanales específicas"
            };

            txt.Text = string.Join("\r\n\r\n", recomendaciones);
        }

        private void CmbPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var personalizado = cmbPeriodo.SelectedItem.ToString() == "Personalizado";
            dtpFechaInicio.Enabled = personalizado;
            dtpFechaFin.Enabled = personalizado;

            if (!personalizado)
            {
                switch (cmbPeriodo.SelectedItem.ToString())
                {
                    case "Última semana":
                        dtpFechaInicio.Value = DateTime.Today.AddDays(-7);
                        dtpFechaFin.Value = DateTime.Today;
                        break;
                    case "Último mes":
                        dtpFechaInicio.Value = DateTime.Today.AddMonths(-1);
                        dtpFechaFin.Value = DateTime.Today;
                        break;
                    case "Últimos 3 meses":
                        dtpFechaInicio.Value = DateTime.Today.AddMonths(-3);
                        dtpFechaFin.Value = DateTime.Today;
                        break;
                }
                CargarEstadisticas();
            }
        }

        private void DtpFecha_ValueChanged(object sender, EventArgs e)
        {
            if (cmbPeriodo.SelectedItem.ToString() == "Personalizado")
            {
                CargarEstadisticas();
            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            CargarEstadisticas();
            MessageBox.Show("Estadísticas actualizadas correctamente.", "Actualización", 
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                var saveDialog = new SaveFileDialog
                {
                    Filter = "Archivo de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*",
                    Title = "Exportar Estadísticas",
                    FileName = $"Estadisticas_Habitus_{DateTime.Now:yyyyMMdd}.txt"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    var contenido = GenerarReporteTexto();
                    System.IO.File.WriteAllText(saveDialog.FileName, contenido);
                    MessageBox.Show($"Estadísticas exportadas exitosamente a:\n{saveDialog.FileName}", 
                                   "Exportación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar estadísticas: {ex.Message}", "Error", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerarReporteTexto()
        {
            var reporte = $"REPORTE DE ESTADÍSTICAS - HABITUS\n";
            reporte += $"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}\n";
            reporte += $"Período: {dtpFechaInicio.Value:dd/MM/yyyy} - {dtpFechaFin.Value:dd/MM/yyyy}\n";
            reporte += $"Usuario: {_usuario?.Nombre ?? "No especificado"}\n\n";
            
            reporte += "=== RESUMEN GENERAL ===\n";
            // Aquí se agregarían los datos reales del resumen
            reporte += "Actividades registradas: [Valor]\n";
            reporte += "Calorías quemadas: [Valor]\n";
            reporte += "Comidas registradas: [Valor]\n";
            reporte += "Puntos ganados: [Valor]\n";
            reporte += "Retos completados: [Valor]\n\n";
            
            reporte += "=== TENDENCIAS ===\n";
            reporte += "[Aquí irían las tendencias calculadas]\n\n";
            
            reporte += "=== RECOMENDACIONES ===\n";
            reporte += "[Aquí irían las recomendaciones personalizadas]\n";
            
            return reporte;
        }
    }
}