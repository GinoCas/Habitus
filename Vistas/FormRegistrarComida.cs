using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Habitus.Controladores;
using Habitus.Modelos;

namespace Habitus.Vistas
{
    public partial class FormRegistrarComida : Form
    {
        private ControladorComida _controladorComida;
        private ControladorUsuario _controladorUsuario;
        private DateTimePicker dtpFecha;
        private ComboBox cmbTipoComida;
        private TextBox txtBuscarAlimento;
        private ListBox lstAlimentos;
        private NumericUpDown numCantidad;
        private Label lblCaloriasSeleccionadas;
        private ListBox lstAlimentosSeleccionados;
        private Button btnAgregarAlimento;
        private Button btnRemoverAlimento;
        private Button btnGuardar;
        private Button btnCancelar;
        private Button btnBuscar;
        private Alimento _alimentoSeleccionado;
        private BindingSource _alimentosSeleccionados;

        public FormRegistrarComida()
        {
            InitializeComponent();
            _controladorComida = new ControladorComida();
            _controladorUsuario = new ControladorUsuario();
            _alimentosSeleccionados = new BindingSource();
            InicializarComponentes();
            CargarAlimentos();
        }

        private void InitializeComponent()
        {
            this.Text = "Registrar Comida - Habitus";
            this.Size = new Size(700, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(240, 248, 255);
        }

        private void InicializarComponentes()
        {
            // Título
            var lblTitulo = new Label
            {
                Text = "Registrar Comida",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                Location = new Point(50, 20),
                Size = new Size(600, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblTitulo);

            int yPos = 70;

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
                Location = new Point(150, yPos),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today
            };
            this.Controls.Add(dtpFecha);

            // Tipo de comida
            var lblTipoComida = new Label
            {
                Text = "Tipo de Comida:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(350, yPos),
                Size = new Size(120, 20)
            };
            this.Controls.Add(lblTipoComida);

            cmbTipoComida = new ComboBox
            {
                Location = new Point(480, yPos),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            
            var tiposComida = Enum.GetValues(typeof(TipoComida))
                .Cast<TipoComida>()
                .Select(t => new { Value = t, Text = ObtenerTextoTipoComida(t) })
                .ToArray();
            
            cmbTipoComida.DataSource = tiposComida;
            cmbTipoComida.DisplayMember = "Text";
            cmbTipoComida.ValueMember = "Value";
            this.Controls.Add(cmbTipoComida);

            yPos += 50;

            // Buscar alimentos
            var lblBuscar = new Label
            {
                Text = "Buscar Alimento:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(50, yPos),
                Size = new Size(120, 20)
            };
            this.Controls.Add(lblBuscar);

            txtBuscarAlimento = new TextBox
            {
                Location = new Point(50, yPos + 25),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10)
            };
            txtBuscarAlimento.TextChanged += TxtBuscarAlimento_TextChanged;
            this.Controls.Add(txtBuscarAlimento);

            btnBuscar = new Button
            {
                Text = "Buscar",
                Location = new Point(260, yPos + 25),
                Size = new Size(70, 25),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.Click += BtnBuscar_Click;
            this.Controls.Add(btnBuscar);

            yPos += 60;

            // Lista de alimentos disponibles
            var lblAlimentos = new Label
            {
                Text = "Alimentos Disponibles:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(50, yPos),
                Size = new Size(200, 20)
            };
            this.Controls.Add(lblAlimentos);

            lstAlimentos = new ListBox
            {
                Location = new Point(50, yPos + 25),
                Size = new Size(280, 150),
                Font = new Font("Segoe UI", 9)
            };
            lstAlimentos.SelectedIndexChanged += LstAlimentos_SelectedIndexChanged;
            this.Controls.Add(lstAlimentos);

            // Cantidad y agregar
            var lblCantidad = new Label
            {
                Text = "Cantidad (g):",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(350, yPos + 25),
                Size = new Size(100, 20)
            };
            this.Controls.Add(lblCantidad);

            numCantidad = new NumericUpDown
            {
                Location = new Point(350, yPos + 50),
                Size = new Size(80, 25),
                Font = new Font("Segoe UI", 10),
                Minimum = 1,
                Maximum = 2000,
                Value = 100
            };
            this.Controls.Add(numCantidad);

            btnAgregarAlimento = new Button
            {
                Text = "Agregar",
                Location = new Point(450, yPos + 50),
                Size = new Size(80, 25),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };
            btnAgregarAlimento.FlatAppearance.BorderSize = 0;
            btnAgregarAlimento.Click += BtnAgregarAlimento_Click;
            this.Controls.Add(btnAgregarAlimento);

            // Información nutricional del alimento seleccionado
            var panelInfo = new Panel
            {
                Location = new Point(350, yPos + 85),
                Size = new Size(280, 90),
                BackColor = Color.FromArgb(236, 240, 241),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(panelInfo);

            var lblInfoNutricional = new Label
            {
                Text = "Información Nutricional (por 100g):",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(5, 5),
                Size = new Size(270, 15)
            };
            panelInfo.Controls.Add(lblInfoNutricional);

            lblCaloriasSeleccionadas = new Label
            {
                Text = "Selecciona un alimento",
                Font = new Font("Segoe UI", 8),
                Location = new Point(5, 25),
                Size = new Size(270, 60),
                ForeColor = Color.Gray
            };
            panelInfo.Controls.Add(lblCaloriasSeleccionadas);

            yPos += 200;

            // Alimentos seleccionados
            var lblSeleccionados = new Label
            {
                Text = "Alimentos Seleccionados:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(50, yPos),
                Size = new Size(200, 20)
            };
            this.Controls.Add(lblSeleccionados);

            lstAlimentosSeleccionados = new ListBox
            {
                Location = new Point(50, yPos + 25),
                Size = new Size(480, 100),
                Font = new Font("Segoe UI", 9),
                DataSource = _alimentosSeleccionados
            };
            this.Controls.Add(lstAlimentosSeleccionados);

            btnRemoverAlimento = new Button
            {
                Text = "Remover",
                Location = new Point(550, yPos + 25),
                Size = new Size(80, 30),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRemoverAlimento.FlatAppearance.BorderSize = 0;
            btnRemoverAlimento.Click += BtnRemoverAlimento_Click;
            this.Controls.Add(btnRemoverAlimento);

            yPos += 140;

            // Total de calorías
            var lblTotalCalorias = new Label
            {
                Text = "Total de Calorías: 0 kcal",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(39, 174, 96),
                Location = new Point(50, yPos),
                Size = new Size(300, 25)
            };
            this.Controls.Add(lblTotalCalorias);
            _alimentosSeleccionados.ListChanged += (s, e) => ActualizarTotalCalorias(lblTotalCalorias);

            // Botones
            btnCancelar = new Button
            {
                Text = "Cancelar",
                Location = new Point(430, yPos + 40),
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
                Location = new Point(550, yPos + 40),
                Size = new Size(100, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Click += BtnGuardar_Click;
            this.Controls.Add(btnGuardar);
        }

        private string ObtenerTextoTipoComida(TipoComida tipo)
        {
            return tipo switch
            {
                TipoComida.Desayuno => "Desayuno",
                TipoComida.Almuerzo => "Almuerzo",
                TipoComida.Cena => "Cena",
                TipoComida.Merienda => "Merienda",
                TipoComida.Snack => "Snack",
                _ => tipo.ToString()
            };
        }

        private void CargarAlimentos()
        {
            var alimentos = _controladorComida.BuscarAlimentos("");
            lstAlimentos.DataSource = alimentos;
            lstAlimentos.DisplayMember = "Nombre";
        }

        private void TxtBuscarAlimento_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscarAlimento.Text.Length >= 2)
            {
                BuscarAlimentos();
            }
            else if (string.IsNullOrEmpty(txtBuscarAlimento.Text))
            {
                CargarAlimentos();
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            BuscarAlimentos();
        }

        private void BuscarAlimentos()
        {
            var termino = txtBuscarAlimento.Text.Trim();
            var alimentos = _controladorComida.BuscarAlimentos(termino);
            lstAlimentos.DataSource = alimentos;
            lstAlimentos.DisplayMember = "Nombre";
            lstAlimentos.ValueMember = "Id";
        }

        private void LstAlimentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAlimentos.SelectedItem is Alimento alimento)
            {
                _alimentoSeleccionado = alimento;
                btnAgregarAlimento.Enabled = true;
                
                lblCaloriasSeleccionadas.Text = $"Calorías: {alimento.CaloriasPor100g} kcal\n" +
                                              $"Proteínas: {alimento.Proteinas}g\n" +
                                              $"Carbohidratos: {alimento.Carbohidratos}g\n" +
                                              $"Grasas: {alimento.Grasas}g";
            }
            else
            {
                _alimentoSeleccionado = null;
                btnAgregarAlimento.Enabled = false;
                lblCaloriasSeleccionadas.Text = "Selecciona un alimento";
            }
        }

        private void BtnAgregarAlimento_Click(object sender, EventArgs e)
        {
            if (_alimentoSeleccionado != null)
            {
                var cantidad = (double)numCantidad.Value;
                var factor = cantidad / 100.0;
                
                var alimentoComida = new
                {
                    Alimento = _alimentoSeleccionado,
                    Cantidad = cantidad,
                    CaloriasTotal = _alimentoSeleccionado.CaloriasPor100g * factor,
                    Descripcion = $"{_alimentoSeleccionado.Nombre} - {cantidad}g ({_alimentoSeleccionado.CaloriasPor100g * factor:F0} kcal)"
                };
                
                _alimentosSeleccionados.Add(alimentoComida);
            }
        }

        private void BtnRemoverAlimento_Click(object sender, EventArgs e)
        {
            if (lstAlimentosSeleccionados.SelectedItem != null)
            {
                _alimentosSeleccionados.Remove(lstAlimentosSeleccionados.SelectedItem);
            }
        }

        private void ActualizarTotalCalorias(Label lblTotal)
        {
            double totalCalorias = 0;
            foreach (var item in _alimentosSeleccionados)
            {
                var alimentoComida = item as dynamic;
                if (alimentoComida != null)
                {
                    totalCalorias += alimentoComida.CaloriasTotal;
                }
            }
            lblTotal.Text = $"Total de Calorías: {totalCalorias:F0} kcal";
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                var fecha = dtpFecha.Value.Date;
                var tipoComida = (TipoComida)cmbTipoComida.SelectedValue;
                
                foreach (var item in _alimentosSeleccionados)
                {
                    var alimentoComida = item as dynamic;
                    if (alimentoComida != null)
                    {
                        var comida = new Comida
                        {
                            Id = Guid.NewGuid().ToString(),
                            Fecha = fecha,
                            TipoComida = tipoComida,
                            AlimentoId = alimentoComida.Alimento.Id,
                            NombreAlimento = alimentoComida.Alimento.Nombre,
                            Cantidad = alimentoComida.Cantidad,
                            CaloriasConsumidas = alimentoComida.CaloriasTotal
                        };
                        
                        _controladorComida.RegistrarComida(comida.NombreAlimento, comida.Cantidad, comida.TipoComida.ToString());
                    }
                }

                // Calcular puntos ganados
                var totalCalorias = _alimentosSeleccionados.Cast<dynamic>()
                    .Sum(item => (double)item.CaloriasTotal);
                var puntosGanados = CalcularPuntosComida(totalCalorias);
                _controladorUsuario.ActualizarPuntos(puntosGanados);

                MessageBox.Show($"Comida registrada exitosamente.\n\nTotal de calorías: {totalCalorias:F0} kcal\nPuntos ganados: {puntosGanados}", 
                               "Comida Registrada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private int CalcularPuntosComida(double totalCalorias)
        {
            // Puntos basados en el registro de comida (no en la cantidad de calorías)
            return Math.Max(1, (int)(totalCalorias / 100)); // 1 punto por cada 100 calorías registradas
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidarDatos()
        {
            if (cmbTipoComida.SelectedValue == null)
            {
                MessageBox.Show("Por favor, selecciona un tipo de comida.", "Campo requerido", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTipoComida.Focus();
                return false;
            }

            if (_alimentosSeleccionados.Count == 0)
            {
                MessageBox.Show("Por favor, agrega al menos un alimento.", "Sin alimentos", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dtpFecha.Value.Date > DateTime.Today)
            {
                MessageBox.Show("No puedes registrar comidas en fechas futuras.", "Fecha inválida", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFecha.Focus();
                return false;
            }

            return true;
        }
    }
}