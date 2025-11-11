using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Habitus.Controladores;
using Habitus.Modelos;

namespace Habitus.Vistas
{
    public partial class FormEstadisticas : Form
    {
        private ControladorActividad _controladorActividad;
        private ControladorComida _controladorComida;
        private ControladorRetos _controladorRetos;

        public FormEstadisticas()
        {
            InitializeComponent();
            _controladorActividad = new ControladorActividad();
            _controladorComida = new ControladorComida();
            _controladorRetos = new ControladorRetos();

            ConfigurarControlesAdicionales();
            CrearPanelResumen();
            CrearPanelGraficos();
            CargarEstadisticas();
            CrearPanelTendencias();
        }

        private void ConfigurarControlesAdicionales()
        {
            // Título
            var lblTitulo = new Label
            {
                Text = "📊 Estadísticas y Análisis de Progreso",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                Location = new Point(50, 20),
                Size = new Size(900, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblTitulo);

            // Panel de filtros
            var panelFiltros = new Panel
            {
                Location = new Point(20, 60),
                Size = new Size(940, 60),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(panelFiltros);

            // Configuración de filtros en el panel
            var lblPeriodo = new Label
            {
                Text = "Periodo:",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(10, 20),
                Size = new Size(70, 20)
            };
            panelFiltros.Controls.Add(lblPeriodo);

            this.cmbPeriodo = new ComboBox
            {
                Location = new Point(80, 18),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            this.cmbPeriodo.Items.AddRange(new object[] { "Última semana", "Último mes", "Últimos 3 meses" });
            this.cmbPeriodo.SelectedIndex = 0;
            this.cmbPeriodo.SelectedIndexChanged += CmbPeriodo_SelectedIndexChanged;
            panelFiltros.Controls.Add(this.cmbPeriodo);

            // Fecha inicio
            var lblFechaInicio = new Label
            {
                Text = "Desde:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(250, 20),
                Size = new Size(50, 20)
            };
            panelFiltros.Controls.Add(lblFechaInicio);

            this.dtpFechaInicio = new DateTimePicker
            {
                Location = new Point(300, 18),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 9),
                Value = DateTime.Today.AddMonths(-1),
                Enabled = false
            };
            this.dtpFechaInicio.ValueChanged += DtpFecha_ValueChanged;
            panelFiltros.Controls.Add(this.dtpFechaInicio);

            // Fecha fin
            var lblFechaFin = new Label
            {
                Text = "Hasta:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(440, 20),
                Size = new Size(50, 20)
            };
            panelFiltros.Controls.Add(lblFechaFin);

            this.dtpFechaFin = new DateTimePicker
            {
                Location = new Point(495, 18),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 9),
                Value = DateTime.Today,
                Enabled = false
            };
            this.dtpFechaFin.ValueChanged += DtpFecha_ValueChanged;
            panelFiltros.Controls.Add(this.dtpFechaFin);

            // Botones

            var btnVolver = new Button
            {
                Text = "Volver",
                Location = new Point(820, 15),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.Click += (sender, e) => this.Close();
            panelFiltros.Controls.Add(btnVolver);
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
            //CrearTarjetaResumen("🎯 Retos", "0", "Completados", new Point(740, 40), Color.FromArgb(155, 89, 182));
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

            // Panel para gráfico de actividades
            var panelGraficoActividades = new Panel
            {
                Location = new Point(20, 40),
                Size = new Size(560, 325),
                BackColor = Color.FromArgb(248, 249, 250),
                BorderStyle = BorderStyle.FixedSingle,
                Name = "panelGraficoActividades"
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

        private void DibujarGraficoActividades(Panel panel, Point ubicacion, Size tamaño, DateTime fechaInicio, DateTime fechaFin)
        {
            var panelGrafico = new Panel
            {
                Location = ubicacion,
                Size = tamaño,
                BackColor = Color.White,
            };
            panel.Controls.Add(panelGrafico);

            // Obtener datos reales de actividades
            var fechas = new List<DateTime>();
            var actividadesPorDia = new Dictionary<DateTime, int>();

            // Determinar el número de días en el período
            int diasEnPeriodo = (int)(fechaFin - fechaInicio).TotalDays + 1;
            int numPuntos = Math.Min(7, diasEnPeriodo); // Mostrar máximo 7 puntos

            // Calcular el intervalo entre puntos
            int intervalo = Math.Max(1, diasEnPeriodo / numPuntos);

            // Generar las fechas a mostrar
            for (int j = 1; j < numPuntos+1; j++)
            {
                var fecha = fechaInicio.AddDays(j * intervalo);
                if (fecha <= fechaFin)
                {
                    fechas.Add(fecha);
                    actividadesPorDia[fecha] = 0;
                }
            }

            // Contar actividades por día
            var actividades = _controladorActividad.ObtenerActividadesPorPeriodo(fechaInicio, fechaFin);
            foreach (var actividad in actividades)
            {
                var fechaActividad = actividad.Fecha.Date;
                // Encontrar la fecha más cercana en nuestro conjunto de fechas
                var fechaMasCercana = fechas.OrderBy(f => Math.Abs((f - fechaActividad).TotalDays)).FirstOrDefault();
                if (actividadesPorDia.ContainsKey(fechaMasCercana))
                {
                    actividadesPorDia[fechaMasCercana]++;
                }
            }

            // Encontrar el valor máximo para escalar el gráfico
            int maxActividades = actividadesPorDia.Values.Any() ? actividadesPorDia.Values.Max() : 1;
            maxActividades = Math.Max(1, maxActividades); // Evitar división por cero

            // Dibujar barras de gráfico
            int i = 0;
            foreach (var fecha in fechas)
            {
                int cantidadActividades = actividadesPorDia[fecha];
                int altura = (int)((cantidadActividades / (float)maxActividades) * (tamaño.Height - 40));
                altura = Math.Max(5, altura); // Altura mínima para visibilidad

                var barra = new Panel
                {
                    Location = new Point(50 + i * 70, tamaño.Height - altura - 20),
                    Size = new Size(40, altura),
                    BackColor = Color.FromArgb(52, 152, 219)
                };
                panelGrafico.Controls.Add(barra);

                // Tooltip con información
                var tooltip = new ToolTip();
                tooltip.SetToolTip(barra, $"{cantidadActividades} actividades");

                var lblDia = new Label
                {
                    Text = fecha.ToString("dd/MM"),
                    Font = new Font("Segoe UI", 8),
                    Location = new Point(40 + i * 70, tamaño.Height - 15),
                    Size = new Size(60, 15),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                panelGrafico.Controls.Add(lblDia);

                i++;
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
                //var retosCompletados = _controladorRetos.ObtenerRetosActivos().Count(r => r.Completado && r.FechaInicio >= fechaInicio && r.FechaInicio <= fechaFin);
                //MessageBox.Show("Completado:" + retosCompletados);
                //ActualizarTarjetaResumen("🎯 Retos", retosCompletados.ToString());

                // Actualizar gráficos con datos reales
                ActualizarGraficos(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar estadísticas: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarGraficos(DateTime fechaInicio, DateTime fechaFin)
        {
            // Limpiar gráficos anteriores
            var panelGraficoActividades = panelGraficos.Controls.Find("panelGraficoActividades", true).FirstOrDefault() as Panel;
            var panelGraficoCalorias = panelGraficos.Controls.Find("panelGraficoCalorias", true).FirstOrDefault() as Panel;

            if (panelGraficoActividades != null)
            {
                // Mantener solo la etiqueta del título
                var lblTitulo = panelGraficoActividades.Controls[0];
                panelGraficoActividades.Controls.Clear();
                panelGraficoActividades.Controls.Add(lblTitulo);

                // Dibujar gráfico de actividades con datos reales
                DibujarGraficoActividades(panelGraficoActividades, new Point(10, 35), new Size(540, 280), fechaInicio, fechaFin);
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
            switch (cmbPeriodo.SelectedIndex)
            {
                case 0: // "Última semana":
                    dtpFechaInicio.Value = DateTime.Today.AddDays(-7);
                    dtpFechaFin.Value = DateTime.Today;
                    break;
                case 1: // "Último mes":
                    dtpFechaInicio.Value = DateTime.Today.AddMonths(-1);
                    dtpFechaFin.Value = DateTime.Today;
                    break;
                case 2: // "Últimos 3 meses":
                    dtpFechaInicio.Value = DateTime.Today.AddMonths(-3);
                    dtpFechaFin.Value = DateTime.Today;
                    break;
            }
            CargarEstadisticas();
        }

        private void DtpFecha_ValueChanged(object sender, EventArgs e)
        {
            CargarEstadisticas();
        }

        private void FormEstadisticas_Load(object sender, EventArgs e)
        {
            CmbPeriodo_SelectedIndexChanged(this, EventArgs.Empty);
        }
    }
}