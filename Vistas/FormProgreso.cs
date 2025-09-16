using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Habitus.Controladores;
using Habitus.Modelos;

namespace Habitus.Vistas
{
    public partial class FormProgreso : Form
    {
        private ControladorProgreso _controladorProgreso;
        private ControladorPerfilUsuario _controladorUsuario;

        public FormProgreso()
        {
            InitializeComponent();
            _controladorProgreso = new ControladorProgreso();
            _controladorUsuario = new ControladorPerfilUsuario();
            
            CrearPanelEstadisticas();
            CrearPanelGraficos();
            CrearPanelRegistro(this.Controls.Find("panelRegistro", true).FirstOrDefault() as Panel);
            CargarDatos();
        }

        private void CrearPanelEstadisticas()
        {
            var lblTituloStats = new Label
            {
                Text = "Resumen del Período",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(15, 10),
                Size = new Size(200, 25)
            };
            panelEstadisticas.Controls.Add(lblTituloStats);

            lblResumenPeriodo = new Label
            {
                Text = "Selecciona un período para ver las estadísticas",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(127, 140, 141),
                Location = new Point(15, 40),
                Size = new Size(800, 70)
            };
            panelEstadisticas.Controls.Add(lblResumenPeriodo);
        }

        private void CrearPanelGraficos()
        {
            var lblTituloGraficos = new Label
            {
                Text = "Tendencias",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(15, 10),
                Size = new Size(200, 25)
            };
            panelGraficos.Controls.Add(lblTituloGraficos);

            // Simulación de gráfico simple con barras
            CrearGraficoSimple();
        }

        private void CrearGraficoSimple()
        {
            var panelGrafico = new Panel
            {
                Location = new Point(15, 40),
                Size = new Size(800, 200),
                BackColor = Color.FromArgb(250, 250, 250),
                BorderStyle = BorderStyle.FixedSingle
            };
            panelGraficos.Controls.Add(panelGrafico);

            var lblGrafico = new Label
            {
                Text = "Gráfico de Progreso\n\n" +
                       "📊 Calorías Consumidas vs Quemadas\n" +
                       "📈 Tendencia de Peso\n" +
                       "⏱️ Minutos de Actividad\n" +
                       "👟 Pasos Diarios\n\n" +
                       "Actualiza el período para ver los datos",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(127, 140, 141),
                Location = new Point(20, 20),
                Size = new Size(760, 160),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelGrafico.Controls.Add(lblGrafico);
        }

        private void CrearPanelRegistro(Panel panelPadre)
        {
            if(panelPadre == null)
            {
                panelPadre = new();
            }
            var lblTituloRegistro = new Label
            {
                Text = "Registro Diario",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(15, 10),
                Size = new Size(150, 25)
            };
            panelPadre.Controls.Add(lblTituloRegistro);

            // Estado de ánimo
            var lblEstadoAnimo = new Label
            {
                Text = "Estado de Ánimo:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(20, 45),
                Size = new Size(120, 20)
            };
            panelPadre.Controls.Add(lblEstadoAnimo);

            cmbEstadoAnimo = new ComboBox
            {
                Location = new Point(150, 43),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 9),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbEstadoAnimo.Items.AddRange(new[] { "😊 Excelente", "😃 Muy Bien", "🙂 Bien", "😐 Regular", "😞 Mal" });
            cmbEstadoAnimo.SelectedIndex = 2;
            panelPadre.Controls.Add(cmbEstadoAnimo);

            // Notas
            var lblNotas = new Label
            {
                Text = "Notas del día:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(300, 45),
                Size = new Size(100, 20)
            };
            panelPadre.Controls.Add(lblNotas);

            txtNotas = new TextBox
            {
                Location = new Point(410, 43),
                Size = new Size(250, 25),
                Font = new Font("Segoe UI", 9),
                PlaceholderText = "Escribe tus notas del día..."
            };
            panelPadre.Controls.Add(txtNotas);

            btnRegistrarProgreso = new Button
            {
                Text = "Registrar",
                Location = new Point(680, 43),
                Size = new Size(80, 25),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRegistrarProgreso.FlatAppearance.BorderSize = 0;
            btnRegistrarProgreso.Click += BtnRegistrarProgreso_Click;
            panelPadre.Controls.Add(btnRegistrarProgreso);
        }

        private void CargarDatos()
        {
            ActualizarEstadisticas();
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarEstadisticas();
        }

        private void ActualizarEstadisticas()
        {
            var fechaInicio = dtpFechaInicio.Value.Date;
            var fechaFin = dtpFechaFin.Value.Date;

            if (fechaInicio > fechaFin)
            {
                MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha de fin.", 
                               "Fechas inválidas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var progresoPeriodo = _controladorProgreso.ObtenerProgresoPorPeriodo(fechaInicio, fechaFin);
            var resumen = _controladorProgreso.GenerarResumen(fechaInicio, fechaFin);

            if (progresoPeriodo.Any())
            {
                var totalDias = progresoPeriodo.Count;
                var promedioCaloriasConsumidas = progresoPeriodo.Average(p => p.CaloriasConsumidas);
                var promedioCaloriasQuemadas = progresoPeriodo.Average(p => p.CaloriasQuemadas);
                var totalMinutosActividad = progresoPeriodo.Sum(p => p.MinutosActividad);
                var totalPasos = progresoPeriodo.Sum(p => p.PasosRealizados);
                var totalPuntos = progresoPeriodo.Sum(p => p.PuntosGanados);

                var pesoInicial = progresoPeriodo.First().Peso;
                var pesoFinal = progresoPeriodo.Last().Peso;
                var cambioPeso = pesoFinal - pesoInicial;

                lblResumenPeriodo.Text = $"📅 Período: {fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy} ({totalDias} días)\n" +
                                       $"🍽️ Promedio Calorías Consumidas: {promedioCaloriasConsumidas:F0} kcal/día\n" +
                                       $"🔥 Promedio Calorías Quemadas: {promedioCaloriasQuemadas:F0} kcal/día\n" +
                                       $"⏱️ Total Minutos de Actividad: {totalMinutosActividad} min\n" +
                                       $"👟 Total Pasos: {totalPasos:N0}\n" +
                                       $"⚖️ Cambio de Peso: {cambioPeso:+0.0;-0.0;0.0} kg\n" +
                                       $"🏆 Total Puntos Ganados: {totalPuntos}";
            }
            else
            {
                lblResumenPeriodo.Text = "No hay datos registrados para el período seleccionado.\n\n" +
                                       "💡 Consejo: Comienza registrando tus actividades y comidas para ver tu progreso.";
            }
        }

        private void BtnRegistrarProgreso_Click(object sender, EventArgs e)
        {
            var usuario = _controladorUsuario.ObtenerUsuario();
            if (usuario == null)
            {
                MessageBox.Show("No se pudo obtener la información del usuario.", "Error", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Obtener estado de ánimo seleccionado
            var estadoAnimo = cmbEstadoAnimo.SelectedIndex + 1; // 1-5
            var notas = txtNotas.Text.Trim();

            // Verificar si ya existe un registro para hoy
            var progresoHoy = _controladorProgreso.ObtenerProgresoPorFecha(DateTime.Today);
            if (progresoHoy != null)
            {
                var result = MessageBox.Show("Ya existe un registro para hoy. ¿Deseas actualizarlo?", 
                                            "Registro Existente", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;
            }

            try
            {
               // _controladorProgreso.RegistrarProgreso(estadoAnimo.ToString(), notas);
                
                MessageBox.Show("Progreso registrado exitosamente.", "Registro Completado", 
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Limpiar campos
                txtNotas.Clear();
                cmbEstadoAnimo.SelectedIndex = 2;
                
                // Actualizar estadísticas
                ActualizarEstadisticas();
                
                // Establecer el resultado como OK para que el formulario principal se actualice
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar el progreso: {ex.Message}", "Error", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}