using System;
using System.Drawing;
using System.Windows.Forms;
using Habitus.Controladores;
using Habitus.Modelos;

namespace Habitus.Vistas
{
    public partial class FormDatosPersonales : Form
    {
        private ControladorUsuario _controladorUsuario;
        private int _puntosIniciales;

        public FormDatosPersonales(int puntosIniciales)
        {
            _puntosIniciales = puntosIniciales;
            _controladorUsuario = new ControladorUsuario();
            InitializeComponent();
            
            // Actualizar el subtítulo con los puntos iniciales
            var lblSubtitulo = this.Controls.Find("lblSubtitulo", true).FirstOrDefault() as Label;
            if (lblSubtitulo != null)
            {
                lblSubtitulo.Text = $"¡Felicidades! Has obtenido {_puntosIniciales} puntos iniciales.";
            }
            
            // Configurar el ComboBox de nivel de actividad
            var cmbNivelActividad = this.Controls.Find("cmbNivelActividad", true).FirstOrDefault() as ComboBox;
            if (cmbNivelActividad != null)
            {
                cmbNivelActividad.SelectedIndex = 0;
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                var cmbGenero = this.Controls.Find("cmbGenero", true).FirstOrDefault() as ComboBox;
                var cmbNivelActividad = this.Controls.Find("cmbNivelActividad", true).FirstOrDefault() as ComboBox;
                var txtNombre = this.Controls.Find("txtNombre", true).FirstOrDefault() as TextBox;
                var numEdad = this.Controls.Find("numEdad", true).FirstOrDefault() as NumericUpDown;
                var numPeso = this.Controls.Find("numPeso", true).FirstOrDefault() as NumericUpDown;
                var numAltura = this.Controls.Find("numAltura", true).FirstOrDefault() as NumericUpDown;
                
                var genero = (Genero)Enum.Parse(typeof(Genero), cmbGenero.SelectedItem.ToString());
                var nivelActividad = (NivelActividad)cmbNivelActividad.SelectedIndex;

                _controladorUsuario.CrearUsuario(
                    txtNombre.Text.Trim(),
                    (int)numEdad.Value,
                    (double)numPeso.Value,
                    (double)numAltura.Value,
                    genero,
                    nivelActividad,
                    _puntosIniciales
                );

                var usuario = _controladorUsuario.ObtenerUsuario();

                // Calcular IMC manualmente
                double alturaEnMetros = usuario.Altura / 100;
                double imc = usuario.Peso / (alturaEnMetros * alturaEnMetros);
                
                MessageBox.Show($"¡Bienvenido/a {usuario.Nombre}!\n\nTu perfil ha sido creado exitosamente.\nPuntos iniciales: {_puntosIniciales}\nIMC: {imc:F1}", 
                               "Perfil Creado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidarDatos()
        {
            var txtNombre = this.Controls.Find("txtNombre", true).FirstOrDefault() as TextBox;
            var numEdad = this.Controls.Find("numEdad", true).FirstOrDefault() as NumericUpDown;
            var numPeso = this.Controls.Find("numPeso", true).FirstOrDefault() as NumericUpDown;
            var numAltura = this.Controls.Find("numAltura", true).FirstOrDefault() as NumericUpDown;
            
            if (txtNombre == null || numEdad == null || numPeso == null || numAltura == null)
            {
                MessageBox.Show("Error al encontrar los controles del formulario.", "Error", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Por favor, ingresa tu nombre.", "Campo requerido", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (numEdad.Value < 10 || numEdad.Value > 120)
            {
                MessageBox.Show("Por favor, ingresa una edad válida (10-120 años).", "Edad inválida", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numEdad.Focus();
                return false;
            }

            if (numPeso.Value < 30 || numPeso.Value > 300)
            {
                MessageBox.Show("Por favor, ingresa un peso válido (30-300 kg).", "Peso inválido", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numPeso.Focus();
                return false;
            }

            if (numAltura.Value < 100 || numAltura.Value > 250)
            {
                MessageBox.Show("Por favor, ingresa una altura válida (100-250 cm).", "Altura inválida", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numAltura.Focus();
                return false;
            }

            return true;
        }
    }
}