namespace Habitus.Vistas
{
    partial class FormPrincipal
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Text = "Habitus - Control de Salud";
            this.Size = new System.Drawing.Size(1000, 700);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;

            // Panel Superior
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Height = 80;
            this.panelSuperior.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);

            // Panel Lateral
            this.panelLateral = new System.Windows.Forms.Panel();
            this.panelLateral.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLateral.Width = 200;
            this.panelLateral.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);

            // Panel de Contenido
            this.panelContenido = new System.Windows.Forms.Panel();
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.panelContenido.Padding = new System.Windows.Forms.Padding(20);
            this.panelContenido.Resize += new System.EventHandler(this.panelContenido_Resize);

            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panelLateral);
            this.Controls.Add(this.panelSuperior);

            // Inicializar los componentes de los paneles
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.lblPuntos = new System.Windows.Forms.Label();
            this.lblNivel = new System.Windows.Forms.Label();
            this.progressBarNivel = new System.Windows.Forms.ProgressBar();
            this.btnRegistrarActividad = new System.Windows.Forms.Button();
            this.btnRegistrarComida = new System.Windows.Forms.Button();
            this.btnVerProgreso = new System.Windows.Forms.Button();
            this.btnRetos = new System.Windows.Forms.Button();
            this.btnEstadisticas = new System.Windows.Forms.Button();
            this.btnConfiguracion = new System.Windows.Forms.Button();

            CrearPanelSuperior();
            CrearPanelLateral();
            CrearPanelContenido();
        }

        #endregion

        // Controles de la interfaz
        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Panel panelLateral;
        private System.Windows.Forms.Panel panelContenido;
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Label lblPuntos;
        private System.Windows.Forms.Label lblNivel;
        private System.Windows.Forms.ProgressBar progressBarNivel;
        private System.Windows.Forms.Button btnRegistrarActividad;
        private System.Windows.Forms.Button btnRegistrarComida;
        private System.Windows.Forms.Button btnVerProgreso;
        private System.Windows.Forms.Button btnRetos;
        private System.Windows.Forms.Button btnEstadisticas;
        private System.Windows.Forms.Button btnConfiguracion;
    }
}