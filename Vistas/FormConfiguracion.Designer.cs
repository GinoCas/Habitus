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