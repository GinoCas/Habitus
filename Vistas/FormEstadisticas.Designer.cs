using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Habitus.Vistas
{
    partial class FormEstadisticas
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
            
            // Configuración básica del formulario
            this.Text = "Estadísticas y Análisis - Habitus";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 248, 255);

            // Declaración de controles
            this.cmbPeriodo = new ComboBox();
            this.dtpFechaInicio = new DateTimePicker();
            this.dtpFechaFin = new DateTimePicker();
            this.panelGraficos = new Panel();
            this.panelResumen = new Panel();
            this.panelTendencias = new Panel();

            // Título
            var lblTitulo = new Label
            {
                Text = "📊 Estadísticas y Análisis de Progreso",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                Location = new Point(50, 20),
                Size = new Size(900, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblTitulo);

            // Panel de filtros
            var panelFiltros = new Panel
            {
                Location = new Point(20, 60),
                Size = new Size(940, 60),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(panelFiltros);

            // Panel de resumen general
            this.panelResumen.Location = new Point(20, 130);
            this.panelResumen.Size = new Size(940, 120);
            this.panelResumen.BackColor = Color.White;
            this.panelResumen.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(this.panelResumen);

            // Panel de gráficos
            this.panelGraficos.Location = new Point(20, 260);
            this.panelGraficos.Size = new Size(600, 380);
            this.panelGraficos.BackColor = Color.White;
            this.panelGraficos.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(this.panelGraficos);

            // Panel de tendencias
            this.panelTendencias = new Panel
            {
                Location = new Point(630, 260),
                Size = new Size(330, 380),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(this.panelTendencias);

            // Configuración de filtros en el panel
            var lblPeriodo = new Label
            {
                Text = "Periodo:",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(10, 20),
                Size = new Size(70, 20)
            };
            panelFiltros.Controls.Add(lblPeriodo);

            this.cmbPeriodo = new ComboBox
            {
                Location = new Point(80, 18),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            this.cmbPeriodo.Items.AddRange(new object[] { "Última semana", "Último mes", "Últimos 3 meses" });
            this.cmbPeriodo.SelectedIndex = 0;
            this.cmbPeriodo.SelectedIndexChanged += CmbPeriodo_SelectedIndexChanged;
            panelFiltros.Controls.Add(this.cmbPeriodo);
            
            var btnAplicar = new Button
            {
                Text = "Aplicar",
                Location = new Point(700, 15),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnAplicar.FlatAppearance.BorderSize = 0;
            btnAplicar.Click += (sender, e) => CargarEstadisticas();
            panelFiltros.Controls.Add(btnAplicar);

            var btnVolver = new Button
            {
                Text = "Volver",
                Location = new Point(820, 15),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.Click += (sender, e) => this.Close();
            panelFiltros.Controls.Add(btnVolver);
        }

        // Declaración de controles
        private ComboBox cmbPeriodo;
        private DateTimePicker dtpFechaInicio;
        private DateTimePicker dtpFechaFin;
        private Panel panelGraficos;
        private Panel panelResumen;
        private Panel panelTendencias;

        #endregion
    }
}