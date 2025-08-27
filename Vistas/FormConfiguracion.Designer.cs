namespace Habitus.Vistas
{
    partial class FormConfiguracion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
                this.components = new System.ComponentModel.Container();
                this.Text = "Configuración - Habitus";
                this.Size = new System.Drawing.Size(700, 600);
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                this.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);

                // Inicializar los componentes
                this.tabControl = new System.Windows.Forms.TabControl();
                this.txtNombre = new System.Windows.Forms.TextBox();
                this.nudEdad = new System.Windows.Forms.NumericUpDown();
                this.cmbGenero = new System.Windows.Forms.ComboBox();
                this.nudPeso = new System.Windows.Forms.NumericUpDown();
                this.nudAltura = new System.Windows.Forms.NumericUpDown();
                this.cmbNivelActividad = new System.Windows.Forms.ComboBox();
                this.chkNotificaciones = new System.Windows.Forms.CheckBox();
                this.chkSonidos = new System.Windows.Forms.CheckBox();
                this.cmbTema = new System.Windows.Forms.ComboBox();
                this.nudObjetivoCalorias = new System.Windows.Forms.NumericUpDown();
                this.nudObjetivoActividad = new System.Windows.Forms.NumericUpDown();
                this.btnGuardar = new System.Windows.Forms.Button();
                this.btnRestaurar = new System.Windows.Forms.Button();
                this.btnExportarDatos = new System.Windows.Forms.Button();
                this.btnImportarDatos = new System.Windows.Forms.Button();
                this.btnReiniciarApp = new System.Windows.Forms.Button();

                // Título
                var lblTitulo = new System.Windows.Forms.Label
                {
                    Text = "⚙️ Configuración",
                    Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold),
                    ForeColor = System.Drawing.Color.FromArgb(41, 128, 185),
                    Location = new System.Drawing.Point(50, 20),
                    Size = new System.Drawing.Size(600, 30),
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                };
                this.Controls.Add(lblTitulo);

                // TabControl
                this.tabControl.Location = new System.Drawing.Point(20, 60);
                this.tabControl.Size = new System.Drawing.Size(640, 450);
                this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10);
                this.Controls.Add(this.tabControl);

                // Crear pestañas
                CrearTabPerfil();
                CrearTabPreferencias();
                CrearTabObjetivos();
                CrearTabDatos();

                // Botones principales
                this.btnGuardar.Text = "💾 Guardar Cambios";
                this.btnGuardar.Location = new System.Drawing.Point(400, 520);
                this.btnGuardar.Size = new System.Drawing.Size(130, 35);
                this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10);
                this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
                this.btnGuardar.ForeColor = System.Drawing.Color.White;
                this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.btnGuardar.FlatAppearance.BorderSize = 0;
                this.btnGuardar.Click += BtnGuardar_Click;
                this.Controls.Add(this.btnGuardar);

                this.btnRestaurar.Text = "🔄 Restaurar";
                this.btnRestaurar.Location = new System.Drawing.Point(540, 520);
                this.btnRestaurar.Size = new System.Drawing.Size(100, 35);
                this.btnRestaurar.Font = new System.Drawing.Font("Segoe UI", 10);
                this.btnRestaurar.BackColor = System.Drawing.Color.FromArgb(127, 140, 141);
                this.btnRestaurar.ForeColor = System.Drawing.Color.White;
                this.btnRestaurar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.btnRestaurar.FlatAppearance.BorderSize = 0;
                this.btnRestaurar.Click += BtnRestaurar_Click;
                this.Controls.Add(this.btnRestaurar);

                InitializeTabPerfil();
                InitializeTabPreferencias();
                InitializeTabObjetivos();
                InitializeTabDatos();
            }

            private void InitializeTabPerfil()
            {
                var tabPerfil = new System.Windows.Forms.TabPage("👤 Perfil");
                tabPerfil.BackColor = System.Drawing.Color.White;
                this.tabControl.TabPages.Add(tabPerfil);

                // Información del nivel actual
                var panelNivel = new System.Windows.Forms.Panel
                {
                    Location = new System.Drawing.Point(20, 20),
                    Size = new System.Drawing.Size(580, 80),
                    BackColor = System.Drawing.Color.FromArgb(52, 152, 219),
                    BorderStyle = System.Windows.Forms.BorderStyle.None
                };
                tabPerfil.Controls.Add(panelNivel);

                var lblNivelTitulo = new System.Windows.Forms.Label
                {
                    Text = "🏆 Información del Nivel",
                    Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold),
                    ForeColor = System.Drawing.Color.White,
                    Location = new System.Drawing.Point(20, 10),
                    Size = new System.Drawing.Size(200, 25)
                };
                panelNivel.Controls.Add(lblNivelTitulo);

                var lblNivelInfo = new System.Windows.Forms.Label
                {
                    Text = "Cargando información del nivel...",
                    Font = new System.Drawing.Font("Segoe UI", 10),
                    ForeColor = System.Drawing.Color.White,
                    Location = new System.Drawing.Point(20, 35),
                    Size = new System.Drawing.Size(540, 40),
                    Name = "lblNivelInfo"
                };
                panelNivel.Controls.Add(lblNivelInfo);

                // Datos personales
                var lblDatosPersonales = new System.Windows.Forms.Label
                {
                    Text = "📝 Datos Personales",
                    Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold),
                    ForeColor = System.Drawing.Color.FromArgb(52, 73, 94),
                    Location = new System.Drawing.Point(20, 120),
                    Size = new System.Drawing.Size(200, 25)
                };
                tabPerfil.Controls.Add(lblDatosPersonales);

                // Nombre
                var lblNombre = new System.Windows.Forms.Label
                {
                    Text = "Nombre:",
                    Font = new System.Drawing.Font("Segoe UI", 10),
                    Location = new System.Drawing.Point(40, 160),
                    Size = new System.Drawing.Size(80, 20)
                };
                tabPerfil.Controls.Add(lblNombre);

                this.txtNombre.Location = new System.Drawing.Point(130, 158);
                this.txtNombre.Size = new System.Drawing.Size(200, 25);
                this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10);
                tabPerfil.Controls.Add(this.txtNombre);

                // Edad
                var lblEdad = new System.Windows.Forms.Label
                {
                    Text = "Edad:",
                    Font = new System.Drawing.Font("Segoe UI", 10),
                    Location = new System.Drawing.Point(350, 160),
                    Size = new System.Drawing.Size(50, 20)
                };
                tabPerfil.Controls.Add(lblEdad);

                this.nudEdad.Location = new System.Drawing.Point(410, 158);
                this.nudEdad.Size = new System.Drawing.Size(80, 25);
                this.nudEdad.Font = new System.Drawing.Font("Segoe UI", 10);
                this.nudEdad.Minimum = 10;
                this.nudEdad.Maximum = 120;
                this.nudEdad.Value = 25;
                tabPerfil.Controls.Add(this.nudEdad);

                // Género
                var lblGenero = new System.Windows.Forms.Label
                {
                    Text = "Género:",
                    Font = new System.Drawing.Font("Segoe UI", 10),
                    Location = new System.Drawing.Point(40, 200),
                    Size = new System.Drawing.Size(80, 20)
                };
                tabPerfil.Controls.Add(lblGenero);

                this.cmbGenero.Location = new System.Drawing.Point(130, 198);
                this.cmbGenero.Size = new System.Drawing.Size(120, 25);
                this.cmbGenero.Font = new System.Drawing.Font("Segoe UI", 10);
                this.cmbGenero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.cmbGenero.Items.AddRange(new[] { "Masculino", "Femenino", "Otro" });
                tabPerfil.Controls.Add(this.cmbGenero);

                // Peso
                var lblPeso = new System.Windows.Forms.Label
                {
                    Text = "Peso (kg):",
                    Font = new System.Drawing.Font("Segoe UI", 10),
                    Location = new System.Drawing.Point(40, 240),
                    Size = new System.Drawing.Size(80, 20)
                };
                tabPerfil.Controls.Add(lblPeso);

                this.nudPeso.Location = new System.Drawing.Point(130, 238);
                this.nudPeso.Size = new System.Drawing.Size(80, 25);
                this.nudPeso.Font = new System.Drawing.Font("Segoe UI", 10);
                this.nudPeso.Minimum = 30;
                this.nudPeso.Maximum = 300;
                this.nudPeso.DecimalPlaces = 1;
                this.nudPeso.Value = 70;
                tabPerfil.Controls.Add(this.nudPeso);

                // Altura
                var lblAltura = new System.Windows.Forms.Label
                {
                    Text = "Altura (cm):",
                    Font = new System.Drawing.Font("Segoe UI", 10),
                    Location = new System.Drawing.Point(250, 240),
                    Size = new System.Drawing.Size(80, 20)
                };
                tabPerfil.Controls.Add(lblAltura);

                this.nudAltura.Location = new System.Drawing.Point(340, 238);
                this.nudAltura.Size = new System.Drawing.Size(80, 25);
                this.nudAltura.Font = new System.Drawing.Font("Segoe UI", 10);
                this.nudAltura.Minimum = 100;
                this.nudAltura.Maximum = 250;
                this.nudAltura.Value = 170;
                tabPerfil.Controls.Add(this.nudAltura);

                // Nivel de actividad
                var lblNivelActividad = new System.Windows.Forms.Label
                {
                    Text = "Nivel de Actividad:",
                    Font = new System.Drawing.Font("Segoe UI", 10),
                    Location = new System.Drawing.Point(40, 280),
                    Size = new System.Drawing.Size(120, 20)
                };
                tabPerfil.Controls.Add(lblNivelActividad);

                this.cmbNivelActividad.Location = new System.Drawing.Point(170, 278);
                this.cmbNivelActividad.Size = new System.Drawing.Size(150, 25);
                this.cmbNivelActividad.Font = new System.Drawing.Font("Segoe UI", 10);
                this.cmbNivelActividad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.cmbNivelActividad.Items.AddRange(new[] { "Sedentario", "Ligero", "Moderado", "Activo", "Muy Activo" });
                tabPerfil.Controls.Add(this.cmbNivelActividad);

                // Información adicional
                var lblIMC = new System.Windows.Forms.Label
                {
                    Text = "IMC: Calculando...",
                    Font = new System.Drawing.Font("Segoe UI", 10),
                    ForeColor = System.Drawing.Color.FromArgb(127, 140, 141),
                    Location = new System.Drawing.Point(40, 320),
                    Size = new System.Drawing.Size(200, 20),
                    Name = "lblIMC"
                };
                tabPerfil.Controls.Add(lblIMC);

                // Eventos para calcular IMC automáticamente
                this.nudPeso.ValueChanged += (sender, e) => CalcularIMC(sender, e);
                this.nudAltura.ValueChanged += (sender, e) => CalcularIMC(sender, e);
            }

            private void InitializeTabPreferencias()
            {
                var tabPreferencias = new System.Windows.Forms.TabPage("⚙️ Preferencias");
                tabPreferencias.BackColor = System.Drawing.Color.White;
                this.tabControl.TabPages.Add(tabPreferencias);

                // Preferencias de la aplicación
                var lblPreferencias = new System.Windows.Forms.Label
                {
                    Text = "🔧 Preferencias de la Aplicación",
                    Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold),
                    ForeColor = System.Drawing.Color.FromArgb(52, 73, 94),
                    Location = new System.Drawing.Point(20, 20),
                    Size = new System.Drawing.Size(300, 25)
                };
                tabPreferencias.Controls.Add(lblPreferencias);

                // Notificaciones
                this.chkNotificaciones.Text = "Habilitar notificaciones";
                this.chkNotificaciones.Font = new System.Drawing.Font("Segoe UI", 10);
                this.chkNotificaciones.Location = new System.Drawing.Point(40, 60);
                this.chkNotificaciones.Size = new System.Drawing.Size(200, 25);
                tabPreferencias.Controls.Add(this.chkNotificaciones);

                // Sonidos
                this.chkSonidos.Text = "Habilitar sonidos";
                this.chkSonidos.Font = new System.Drawing.Font("Segoe UI", 10);
                this.chkSonidos.Location = new System.Drawing.Point(40, 100);
                this.chkSonidos.Size = new System.Drawing.Size(200, 25);
                tabPreferencias.Controls.Add(this.chkSonidos);

                // Tema de la aplicación
                var lblTema = new System.Windows.Forms.Label
                {
                    Text = "Tema de la aplicación:",
                    Font = new System.Drawing.Font("Segoe UI", 10),
                    Location = new System.Drawing.Point(40, 140),
                    Size = new System.Drawing.Size(150, 25)
                };
                tabPreferencias.Controls.Add(lblTema);

                this.cmbTema.Location = new System.Drawing.Point(200, 138);
                this.cmbTema.Size = new System.Drawing.Size(150, 25);
                this.cmbTema.Font = new System.Drawing.Font("Segoe UI", 10);
                this.cmbTema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.cmbTema.Items.AddRange(new[] { "Claro", "Oscuro" });
                tabPreferencias.Controls.Add(this.cmbTema);
            }

            private void InitializeTabObjetivos()
            {
                var tabObjetivos = new System.Windows.Forms.TabPage("🎯 Objetivos");
                tabObjetivos.BackColor = System.Drawing.Color.White;
                this.tabControl.TabPages.Add(tabObjetivos);

                // Objetivos de salud y bienestar
                var lblObjetivos = new System.Windows.Forms.Label
                {
                    Text = "🎯 Objetivos de Salud",
                    Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold),
                    ForeColor = System.Drawing.Color.FromArgb(52, 73, 94),
                    Location = new System.Drawing.Point(20, 20),
                    Size = new System.Drawing.Size(300, 25)
                };
                tabObjetivos.Controls.Add(lblObjetivos);

                // Objetivo de calorías diarias
                var lblObjetivoCalorias = new System.Windows.Forms.Label
                {
                    Text = "Calorías diarias (kcal):",
                    Font = new System.Drawing.Font("Segoe UI", 10),
                    Location = new System.Drawing.Point(40, 60),
                    Size = new System.Drawing.Size(160, 25)
                };
                tabObjetivos.Controls.Add(lblObjetivoCalorias);

                this.nudObjetivoCalorias.Location = new System.Drawing.Point(210, 58);
                this.nudObjetivoCalorias.Size = new System.Drawing.Size(100, 25);
                this.nudObjetivoCalorias.Font = new System.Drawing.Font("Segoe UI", 10);
                this.nudObjetivoCalorias.Minimum = 1000;
                this.nudObjetivoCalorias.Maximum = 5000;
                this.nudObjetivoCalorias.Value = 2000;
                this.nudObjetivoCalorias.Increment = 50;
                tabObjetivos.Controls.Add(this.nudObjetivoCalorias);

                // Objetivo de minutos de actividad
                var lblObjetivoActividad = new System.Windows.Forms.Label
                {
                    Text = "Minutos de actividad diarios:",
                    Font = new System.Drawing.Font("Segoe UI", 10),
                    Location = new System.Drawing.Point(40, 100),
                    Size = new System.Drawing.Size(180, 25)
                };
                tabObjetivos.Controls.Add(lblObjetivoActividad);

                this.nudObjetivoActividad.Location = new System.Drawing.Point(230, 98);
                this.nudObjetivoActividad.Size = new System.Drawing.Size(100, 25);
                this.nudObjetivoActividad.Font = new System.Drawing.Font("Segoe UI", 10);
                this.nudObjetivoActividad.Minimum = 10;
                this.nudObjetivoActividad.Maximum = 300;
                this.nudObjetivoActividad.Value = 30;
                this.nudObjetivoActividad.Increment = 10;
                tabObjetivos.Controls.Add(this.nudObjetivoActividad);
            }

            private void InitializeTabDatos()
            {
                var tabDatos = new System.Windows.Forms.TabPage("💾 Datos");
                tabDatos.BackColor = System.Drawing.Color.White;
                this.tabControl.TabPages.Add(tabDatos);

                // Gestión de datos
                var lblGestionDatos = new System.Windows.Forms.Label
                {
                    Text = "💾 Gestión de Datos",
                    Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold),
                    ForeColor = System.Drawing.Color.FromArgb(52, 73, 94),
                    Location = new System.Drawing.Point(20, 20),
                    Size = new System.Drawing.Size(300, 25)
                };
                tabDatos.Controls.Add(lblGestionDatos);

                // Botones de exportar e importar
                this.btnExportarDatos.Text = "Exportar Datos";
                this.btnExportarDatos.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
                this.btnExportarDatos.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
                this.btnExportarDatos.ForeColor = System.Drawing.Color.White;
                this.btnExportarDatos.Location = new System.Drawing.Point(40, 60);
                this.btnExportarDatos.Size = new System.Drawing.Size(150, 40);
                this.btnExportarDatos.Click += BtnExportarDatos_Click;
                tabDatos.Controls.Add(this.btnExportarDatos);

                this.btnImportarDatos.Text = "Importar Datos";
                this.btnImportarDatos.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
                this.btnImportarDatos.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
                this.btnImportarDatos.ForeColor = System.Drawing.Color.White;
                this.btnImportarDatos.Location = new System.Drawing.Point(210, 60);
                this.btnImportarDatos.Size = new System.Drawing.Size(150, 40);
                this.btnImportarDatos.Click += BtnImportarDatos_Click;
                tabDatos.Controls.Add(this.btnImportarDatos);

                // Información sobre la gestión de datos
                var lblInfoDatos = new System.Windows.Forms.Label
                {
                    Text = "Exporta tus datos para tener una copia de seguridad o importarlos en otro dispositivo.",
                    Font = new System.Drawing.Font("Segoe UI", 9),
                    ForeColor = System.Drawing.Color.FromArgb(127, 140, 141),
                    Location = new System.Drawing.Point(40, 110),
                    Size = new System.Drawing.Size(540, 40)
                };
                tabDatos.Controls.Add(lblInfoDatos);

                // Opciones avanzadas
                var lblOpcionesAvanzadas = new System.Windows.Forms.Label
                {
                    Text = "⚙️ Opciones Avanzadas",
                    Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold),
                    ForeColor = System.Drawing.Color.FromArgb(52, 73, 94),
                    Location = new System.Drawing.Point(20, 170),
                    Size = new System.Drawing.Size(300, 25)
                };
                tabDatos.Controls.Add(lblOpcionesAvanzadas);

                // Botón de reiniciar la aplicación
                this.btnReiniciarApp.Text = "Reiniciar Aplicación";
                this.btnReiniciarApp.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
                this.btnReiniciarApp.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
                this.btnReiniciarApp.ForeColor = System.Drawing.Color.White;
                this.btnReiniciarApp.Location = new System.Drawing.Point(40, 210);
                this.btnReiniciarApp.Size = new System.Drawing.Size(180, 40);
                this.btnReiniciarApp.Click += BtnReiniciarApp_Click;
                tabDatos.Controls.Add(this.btnReiniciarApp);

                // Advertencia sobre el reinicio
                var lblAdvertencia = new System.Windows.Forms.Label
                {
                    Text = "Advertencia: Esta acción eliminará todos tus datos y configuraciones. Úsala con precaución.",
                    Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Italic),
                    ForeColor = System.Drawing.Color.FromArgb(192, 57, 43),
                    Location = new System.Drawing.Point(40, 260),
                    Size = new System.Drawing.Size(540, 40)
                };
                tabDatos.Controls.Add(lblAdvertencia);
        }

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.NumericUpDown nudEdad;
        private System.Windows.Forms.ComboBox cmbGenero;
        private System.Windows.Forms.NumericUpDown nudPeso;
        private System.Windows.Forms.NumericUpDown nudAltura;
        private System.Windows.Forms.ComboBox cmbNivelActividad;
        private System.Windows.Forms.CheckBox chkNotificaciones;
        private System.Windows.Forms.CheckBox chkSonidos;
        private System.Windows.Forms.ComboBox cmbTema;
        private System.Windows.Forms.NumericUpDown nudObjetivoCalorias;
        private System.Windows.Forms.NumericUpDown nudObjetivoActividad;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnRestaurar;
        private System.Windows.Forms.Button btnExportarDatos;
        private System.Windows.Forms.Button btnImportarDatos;
        private System.Windows.Forms.Button btnReiniciarApp;
    }
}