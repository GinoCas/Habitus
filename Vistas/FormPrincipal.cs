using System;
using System.Drawing;
using System.Windows.Forms;
using Habitus.Controladores;
using Habitus.Modelos;

namespace Habitus.Vistas
{
    public partial class FormPrincipal : Form
    {
        private ControladorUsuario _controladorUsuario;
        private ControladorNiveles _controladorNiveles;
        private ControladorProgreso _controladorProgreso;
        private ControladorRetos _controladorRetos;
        private Usuario _usuario;

        public FormPrincipal()
        {
            InitializeComponent();
            InicializarControladores();
            CargarDatosUsuario();
        }

        private void InicializarControladores()
        {
            _controladorUsuario = new ControladorUsuario();
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

            // Puntos
            lblPuntos = new Label
            {
                Text = "Puntos: 0",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(241, 196, 15),
                Location = new Point(600, 15),
                Size = new Size(150, 25),
                TextAlign = ContentAlignment.MiddleRight
            };
            panelSuperior.Controls.Add(lblPuntos);

            // Nivel
            lblNivel = new Label
            {
                Text = "Nivel 1",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                Location = new Point(600, 45),
                Size = new Size(100, 20)
            };
            panelSuperior.Controls.Add(lblNivel);

            // Barra de progreso del nivel
            progressBarNivel = new ProgressBar
            {
                Location = new Point(710, 47),
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

            // Botón Ver Progreso
            btnVerProgreso = CrearBotonMenu("Ver Progreso", yPos);
            btnVerProgreso.Click += BtnVerProgreso_Click;
            panelLateral.Controls.Add(btnVerProgreso);
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

        private void CrearPanelContenido()
        {
            // Panel de resumen del día
            var panelResumen = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(740, 200),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            panelContenido.Controls.Add(panelResumen);

            // Título del resumen
            var lblTituloResumen = new Label
            {
                Text = "Resumen del Día",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(20, 15),
                Size = new Size(200, 25)
            };
            panelResumen.Controls.Add(lblTituloResumen);

            // Crear tarjetas de resumen
            CrearTarjetasResumen(panelResumen);

            // Panel de retos activos
            var panelRetosActivos = new Panel
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
            var panelProgresoReciente = new Panel
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
                new { Titulo = "Pasos\nRealizados", Valor = "0", Color = Color.FromArgb(155, 89, 182) }
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
                lblNivel.Text = $"Nivel {nivelActual.NumeroNivel}";
                
                var progresoNivel = _controladorNiveles.CalcularProgresoNivel(_usuario.Puntos);
                // El método CalcularProgresoNivel ya devuelve un valor multiplicado por 100
                // Aseguramos que el valor esté dentro del rango permitido (0-100)
                progressBarNivel.Value = Math.Min(100, (int)progresoNivel);
            }
        }

        private void BtnRegistrarActividad_Click(object sender, EventArgs e)
        {
            var formActividad = new FormRegistrarActividad();
            if (formActividad.ShowDialog() == DialogResult.OK)
            {
                CargarDatosUsuario(); // Actualizar datos después del registro
            }
        }

        private void BtnRegistrarComida_Click(object sender, EventArgs e)
        {
            var formComida = new FormRegistrarComida();
            if (formComida.ShowDialog() == DialogResult.OK)
            {
                CargarDatosUsuario(); // Actualizar datos después del registro
            }
        }

        private void BtnVerProgreso_Click(object sender, EventArgs e)
        {
            var formProgreso = new FormProgreso();
            formProgreso.ShowDialog();
        }

        private void BtnRetos_Click(object sender, EventArgs e)
        {
            var formRetos = new FormRetos();
            formRetos.ShowDialog();
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
    }
}