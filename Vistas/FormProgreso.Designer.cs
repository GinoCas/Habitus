using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Habitus.Vistas
{
    partial class FormProgreso
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        // Controles del formulario
        private DateTimePicker dtpFechaInicio;
        private DateTimePicker dtpFechaFin;
        private Button btnActualizar;
        private Panel panelGraficos;
        private Panel panelEstadisticas;
        private Label lblResumenPeriodo;
        private TextBox txtNotas;
        private Button btnGuardarNotas;
        private ComboBox cmbEstadoAnimo;
        private Button btnRegistrarProgreso;

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
            
            // Configuración básica del formulario
            this.Text = "Progreso - Habitus";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 248, 255);
            
            // Inicialización de controles
            this.dtpFechaInicio = new DateTimePicker();
            this.dtpFechaFin = new DateTimePicker();
            this.btnActualizar = new Button();
            this.panelGraficos = new Panel();
            this.panelEstadisticas = new Panel();
            this.lblResumenPeriodo = new Label();
            this.txtNotas = new TextBox();
            this.btnGuardarNotas = new Button();
            this.cmbEstadoAnimo = new ComboBox();
            this.btnRegistrarProgreso = new Button();
            
            // Título
            var lblTitulo = new Label
            {
                Text = "Seguimiento de Progreso",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                Location = new Point(50, 20),
                Size = new Size(800, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblTitulo);

            // Panel de filtros
            var panelFiltros = new Panel
            {
                Location = new Point(20, 60),
                Size = new Size(840, 60),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(panelFiltros);

            // Filtros de fecha
            var lblFechaInicio = new Label
            {
                Text = "Desde:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(50, 20)
            };
            panelFiltros.Controls.Add(lblFechaInicio);

            this.dtpFechaInicio.Location = new Point(80, 18);
            this.dtpFechaInicio.Size = new Size(120, 25);
            this.dtpFechaInicio.Font = new Font("Segoe UI", 9);
            this.dtpFechaInicio.Format = DateTimePickerFormat.Short;
            this.dtpFechaInicio.Value = DateTime.Today.AddDays(-30);
            panelFiltros.Controls.Add(this.dtpFechaInicio);

            var lblFechaFin = new Label
            {
                Text = "Hasta:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(220, 20),
                Size = new Size(50, 20)
            };
            panelFiltros.Controls.Add(lblFechaFin);

            this.dtpFechaFin.Location = new Point(280, 18);
            this.dtpFechaFin.Size = new Size(120, 25);
            this.dtpFechaFin.Font = new Font("Segoe UI", 9);
            this.dtpFechaFin.Format = DateTimePickerFormat.Short;
            this.dtpFechaFin.Value = DateTime.Today;
            panelFiltros.Controls.Add(this.dtpFechaFin);

            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.Location = new Point(420, 18);
            this.btnActualizar.Size = new Size(80, 25);
            this.btnActualizar.Font = new Font("Segoe UI", 9);
            this.btnActualizar.BackColor = Color.FromArgb(52, 152, 219);
            this.btnActualizar.ForeColor = Color.White;
            this.btnActualizar.FlatStyle = FlatStyle.Flat;
            this.btnActualizar.FlatAppearance.BorderSize = 0;
            this.btnActualizar.Click += BtnActualizar_Click;
            panelFiltros.Controls.Add(this.btnActualizar);

            // Panel de estadísticas
            this.panelEstadisticas.Location = new Point(20, 140);
            this.panelEstadisticas.Size = new Size(840, 120);
            this.panelEstadisticas.BackColor = Color.White;
            this.panelEstadisticas.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(this.panelEstadisticas);

            // Panel de gráficos
            this.panelGraficos.Location = new Point(20, 280);
            this.panelGraficos.Size = new Size(840, 250);
            this.panelGraficos.BackColor = Color.White;
            this.panelGraficos.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(this.panelGraficos);

            // Panel de registro diario
            var panelRegistro = new Panel
            {
                Location = new Point(20, 550),
                Size = new Size(840, 100),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(panelRegistro);
        }
    }
}