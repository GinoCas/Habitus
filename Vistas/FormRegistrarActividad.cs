using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Habitus.Controladores;
using Habitus.Modelos;

namespace Habitus.Vistas
{
    public partial class FormRegistrarActividad : Form
    {
        private ControladorActividad _controladorActividad;
        private ControladorUsuario _controladorUsuario;
        private ComboBox cmbTipoActividad;
        private ComboBox cmbIntensidad;
        private NumericUpDown numDuracion;
        private DateTimePicker dtpFecha;
        private TextBox txtDescripcion;
        private Label lblCaloriasEstimadas;
        private Button btnGuardar;
        private Button btnCancelar;
        private Button btnCalcularCalorias;

        private ControladorProgreso _controladorProgreso;

        public FormRegistrarActividad(ControladorProgreso controladorProgreso = null)
        {
            InitializeComponent();
            _controladorActividad = new ControladorActividad();
            _controladorUsuario = new ControladorUsuario();
            _controladorProgreso = controladorProgreso ?? new ControladorProgreso();
            InicializarComponentes();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // FormRegistrarActividad
            // 
            BackColor = Color.FromArgb(240, 248, 255);
            ClientSize = new Size(482, 553);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "FormRegistrarActividad";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Registrar Actividad - Habitus";
            ResumeLayout(false);
        }

        private void InicializarComponentes()
        {
            // Título
            var lblTitulo = new Label
            {
                Text = "Registrar Nueva Actividad",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                Location = new Point(50, 20),
                Size = new Size(400, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblTitulo);

            int yPos = 80;
            int spacing = 70;

            // Fecha
            var lblFecha = new Label
            {
                Text = "Fecha:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(50, yPos),
                Size = new Size(100, 20)
            };
            this.Controls.Add(lblFecha);

            dtpFecha = new DateTimePicker
            {
                Location = new Point(50, yPos + 25),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today
            };
            this.Controls.Add(dtpFecha);

            yPos += spacing;

            // Tipo de Actividad
            var lblTipoActividad = new Label
            {
                Text = "Tipo de Actividad:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(50, yPos),
                Size = new Size(150, 20)
            };
            this.Controls.Add(lblTipoActividad);

            cmbTipoActividad = new ComboBox
            {
                Location = new Point(50, yPos + 25),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            
            // Cargar tipos de actividad desde el enum
            var tiposActividad = Enum.GetValues(typeof(TipoActividad))
                .Cast<TipoActividad>()
                .Select(t => new { Value = t, Text = ObtenerTextoTipoActividad(t) })
                .ToArray();
            
            cmbTipoActividad.DataSource = tiposActividad;
            cmbTipoActividad.DisplayMember = "Text";
            cmbTipoActividad.ValueMember = "Value";
            cmbTipoActividad.SelectedIndexChanged += CmbTipoActividad_SelectedIndexChanged;
            this.Controls.Add(cmbTipoActividad);

            yPos += spacing;

            // Intensidad
            var lblIntensidad = new Label
            {
                Text = "Intensidad:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(50, yPos),
                Size = new Size(100, 20)
            };
            this.Controls.Add(lblIntensidad);

            cmbIntensidad = new ComboBox
            {
                Location = new Point(50, yPos + 25),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            
            var intensidades = Enum.GetValues(typeof(Intensidad))
                .Cast<Intensidad>()
                .Select(i => new { Value = i, Text = ObtenerTextoIntensidad(i) })
                .ToArray();
            
            cmbIntensidad.DataSource = intensidades;
            cmbIntensidad.DisplayMember = "Text";
            cmbIntensidad.ValueMember = "Value";
            cmbIntensidad.SelectedIndexChanged += CmbIntensidad_SelectedIndexChanged;
            this.Controls.Add(cmbIntensidad);

            yPos += spacing;

            // Duración
            var lblDuracion = new Label
            {
                Text = "Duración (minutos):",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(50, yPos),
                Size = new Size(150, 20)
            };
            this.Controls.Add(lblDuracion);

            numDuracion = new NumericUpDown
            {
                Location = new Point(50, yPos + 25),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10),
                Minimum = 1,
                Maximum = 480, // 8 horas máximo
                Value = 30
            };
            numDuracion.ValueChanged += NumDuracion_ValueChanged;
            this.Controls.Add(numDuracion);

            // Botón calcular calorías
            btnCalcularCalorias = new Button
            {
                Text = "Calcular Calorías",
                Location = new Point(170, yPos + 25),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCalcularCalorias.FlatAppearance.BorderSize = 0;
            btnCalcularCalorias.Click += BtnCalcularCalorias_Click;
            this.Controls.Add(btnCalcularCalorias);

            yPos += spacing;

            // Calorías estimadas
            var lblCaloriasTexto = new Label
            {
                Text = "Calorías Estimadas:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(50, yPos),
                Size = new Size(150, 20)
            };
            this.Controls.Add(lblCaloriasTexto);

            lblCaloriasEstimadas = new Label
            {
                Text = "0 kcal",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(39, 174, 96),
                Location = new Point(210, yPos),
                Size = new Size(100, 20)
            };
            this.Controls.Add(lblCaloriasEstimadas);

            yPos += spacing / 2;

            // Descripción
            var lblDescripcion = new Label
            {
                Text = "Descripción (opcional):",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(50, yPos),
                Size = new Size(250, 20)
            };
            this.Controls.Add(lblDescripcion);

            txtDescripcion = new TextBox
            {
                Location = new Point(50, yPos + 25),
                Size = new Size(370, 60),
                Font = new Font("Segoe UI", 10),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            this.Controls.Add(txtDescripcion);

            // Botones
            yPos += spacing;
            btnCancelar = new Button
            {
                Text = "Cancelar",
                Location = new Point(200, yPos + 25),
                Size = new Size(100, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += BtnCancelar_Click;
            this.Controls.Add(btnCancelar);

            btnGuardar = new Button
            {
                Text = "Guardar",
                Location = new Point(320, yPos + 25),
                Size = new Size(100, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Click += BtnGuardar_Click;
            this.Controls.Add(btnGuardar);

            // Calcular calorías iniciales
            CalcularCalorias();
        }

        private string ObtenerTextoTipoActividad(TipoActividad tipo)
        {
            return tipo switch
            {
                TipoActividad.Cardio => "Cardio",
                TipoActividad.Fuerza => "Entrenamiento de Fuerza",
                TipoActividad.Flexibilidad => "Flexibilidad",
                TipoActividad.Deportes => "Deportes",
                TipoActividad.Caminar => "Caminar",
                TipoActividad.Correr => "Correr",
                TipoActividad.Ciclismo => "Ciclismo",
                TipoActividad.Natacion => "Natación",
                TipoActividad.Yoga => "Yoga",
                TipoActividad.Pilates => "Pilates",
                TipoActividad.Baile => "Baile",
                TipoActividad.Otro => "Otro",
                _ => tipo.ToString()
            };
        }

        private string ObtenerTextoIntensidad(Intensidad intensidad)
        {
            return intensidad switch
            {
                Intensidad.Baja => "Baja",
                Intensidad.Moderada => "Moderada",
                Intensidad.Alta => "Alta",
                _ => intensidad.ToString()
            };
        }

        private void CmbTipoActividad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcularCalorias();
        }

        private void CmbIntensidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcularCalorias();
        }

        private void NumDuracion_ValueChanged(object sender, EventArgs e)
        {
            CalcularCalorias();
        }

        private void BtnCalcularCalorias_Click(object sender, EventArgs e)
        {
            CalcularCalorias();
        }

        private void CalcularCalorias()
        {
            if (cmbTipoActividad.SelectedValue != null && cmbIntensidad.SelectedValue != null)
            {
                var tipoActividad = (TipoActividad)cmbTipoActividad.SelectedValue;
                var intensidad = (Intensidad)cmbIntensidad.SelectedValue;
                var duracion = (int)numDuracion.Value;

                var usuario = _controladorUsuario.ObtenerUsuario();
                if (usuario != null)
                {
                    var calorias = _controladorActividad.CalcularCaloriasQuemadas(
                        tipoActividad, intensidad, duracion, usuario.Peso);
                    lblCaloriasEstimadas.Text = $"{calorias:F0} kcal";
                }
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                var tipoActividad = (TipoActividad)cmbTipoActividad.SelectedValue;
                var intensidad = (Intensidad)cmbIntensidad.SelectedValue;
                var duracion = (int)numDuracion.Value;
                var fecha = dtpFecha.Value.Date;
                var descripcion = txtDescripcion.Text.Trim();

                var usuario = _controladorUsuario.ObtenerUsuario();
                var calorias = _controladorActividad.CalcularCaloriasQuemadas(
                    tipoActividad, intensidad, duracion, usuario.Peso);

                _controladorActividad.RegistrarActividad(tipoActividad.ToString(), duracion, intensidad.ToString());

                // Actualizar el progreso con las calorías quemadas y minutos de actividad
                _controladorProgreso.RegistrarCaloriasQuemadas(fecha, calorias);
                _controladorProgreso.RegistrarMinutosActividad(fecha, duracion);

                // Actualizar puntos del usuario
                var puntosGanados = CalcularPuntosActividad(duracion, intensidad);
                _controladorUsuario.ActualizarPuntos(puntosGanados);

                MessageBox.Show($"Actividad registrada exitosamente.\n\nCalorías quemadas: {calorias:F0} kcal\nPuntos ganados: {puntosGanados}", 
                               "Actividad Registrada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private int CalcularPuntosActividad(int duracion, Intensidad intensidad)
        {
            int puntosPorMinuto = intensidad switch
            {
                Intensidad.Baja => 1,
                Intensidad.Moderada => 2,
                Intensidad.Alta => 3,
                _ => 1
            };

            return duracion * puntosPorMinuto;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidarDatos()
        {
            if (cmbTipoActividad.SelectedValue == null)
            {
                MessageBox.Show("Por favor, selecciona un tipo de actividad.", "Campo requerido", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTipoActividad.Focus();
                return false;
            }

            if (cmbIntensidad.SelectedValue == null)
            {
                MessageBox.Show("Por favor, selecciona una intensidad.", "Campo requerido", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbIntensidad.Focus();
                return false;
            }

            if (numDuracion.Value <= 0)
            {
                MessageBox.Show("La duración debe ser mayor a 0 minutos.", "Duración inválida", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numDuracion.Focus();
                return false;
            }

            if (dtpFecha.Value.Date > DateTime.Today)
            {
                MessageBox.Show("No puedes registrar actividades en fechas futuras.", "Fecha inválida", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFecha.Focus();
                return false;
            }

            return true;
        }
    }
}