using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Habitus.Controladores;
using Habitus.Modelos;

namespace Habitus.Vistas
{
    public partial class FormCuestionario : Form
    {
        private ControladorCuestionario _controladorCuestionario;
        private ControladorPerfilUsuario _controladorUsuario;
        private int _preguntaActual = 0;

        public FormCuestionario()
        {
            InitializeComponent();
            _controladorCuestionario = new ControladorCuestionario();
            _controladorUsuario = new ControladorPerfilUsuario();
            
            // Configurar la barra de progreso con el número de preguntas
            progressBar.Maximum = _controladorCuestionario.ObtenerCuestionario().Preguntas.Count;
            progressBar.Value = 1;
            
            MostrarPregunta();
        }

        private void MostrarPregunta()
        {
            var cuestionario = _controladorCuestionario.ObtenerCuestionario();
            var pregunta = cuestionario.Preguntas[_preguntaActual];

            lblPregunta.Text = pregunta.Texto;
            
            // Actualizar progreso
            progressBar.Value = _preguntaActual + 1;
            lblProgreso.Text = $"Pregunta {_preguntaActual + 1} de {cuestionario.Preguntas.Count}";

            // Configurar opciones
            for (int i = 0; i < radioButtons.Length; i++)
            {
                if (i < pregunta.Opciones.Count)
                {
                    radioButtons[i].Text = pregunta.Opciones[i];
                    radioButtons[i].Visible = true;
                    radioButtons[i].Checked = pregunta.RespuestaSeleccionada == pregunta.Opciones[i];
                }
                else
                {
                    radioButtons[i].Visible = false;
                }
            }

            // Configurar botones
            btnAnterior.Enabled = _preguntaActual > 0;
            btnSiguiente.Text = _preguntaActual == cuestionario.Preguntas.Count - 1 ? "Finalizar" : "Siguiente";
        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            GuardarRespuestaActual();
            _preguntaActual--;
            MostrarPregunta();
        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            if (!ValidarRespuesta())
            {
                MessageBox.Show("Por favor, selecciona una respuesta antes de continuar.", "Respuesta requerida", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GuardarRespuestaActual();

            var cuestionario = _controladorCuestionario.ObtenerCuestionario();
            if (_preguntaActual == cuestionario.Preguntas.Count - 1)
            {
                // Finalizar cuestionario
                FinalizarCuestionario();
            }
            else
            {
                _preguntaActual++;
                MostrarPregunta();
            }
        }

        private bool ValidarRespuesta()
        {
            return radioButtons.Any(rb => rb.Visible && rb.Checked);
        }

        private void GuardarRespuestaActual()
        {
            var respuestaSeleccionada = radioButtons.FirstOrDefault(rb => rb.Visible && rb.Checked)?.Text;
            if (!string.IsNullOrEmpty(respuestaSeleccionada))
            {
                _controladorCuestionario.ResponderPregunta(_preguntaActual, respuestaSeleccionada);
            }
        }

        private void FinalizarCuestionario()
        {
            _controladorCuestionario.CompletarCuestionario();
            var puntosObtenidos = _controladorCuestionario.CalcularPuntosTotal();
            _controladorUsuario.ActualizarPuntos(puntosObtenidos);

            // Mostrar formulario de datos personales
            var formDatosPersonales = new FormDatosPersonales(puntosObtenidos);
            formDatosPersonales.ShowDialog();

            // Cerrar este formulario y abrir el principal
            this.Hide();
            var formPrincipal = new FormPrincipal();
            formPrincipal.Show();
            this.Close();
        }
    }
}