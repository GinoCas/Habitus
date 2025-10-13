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
            cmbPeriodo = new ComboBox();
            dtpFechaInicio = new DateTimePicker();
            dtpFechaFin = new DateTimePicker();
            panelGraficos = new Panel();
            panelResumen = new Panel();
            panelTendencias = new Panel();
            SuspendLayout();
            // 
            // cmbPeriodo
            // 
            cmbPeriodo.Location = new Point(0, 0);
            cmbPeriodo.Name = "cmbPeriodo";
            cmbPeriodo.Size = new Size(121, 23);
            cmbPeriodo.TabIndex = 0;
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Location = new Point(0, 0);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(200, 23);
            dtpFechaInicio.TabIndex = 0;
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Location = new Point(0, 0);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(200, 23);
            dtpFechaFin.TabIndex = 0;
            // 
            // panelGraficos
            // 
            panelGraficos.BackColor = Color.White;
            panelGraficos.BorderStyle = BorderStyle.FixedSingle;
            panelGraficos.Location = new Point(20, 260);
            panelGraficos.Name = "panelGraficos";
            panelGraficos.Size = new Size(600, 380);
            panelGraficos.TabIndex = 1;
            // 
            // panelResumen
            // 
            panelResumen.BackColor = Color.White;
            panelResumen.BorderStyle = BorderStyle.FixedSingle;
            panelResumen.Location = new Point(20, 130);
            panelResumen.Name = "panelResumen";
            panelResumen.Size = new Size(940, 120);
            panelResumen.TabIndex = 0;
            // 
            // panelTendencias
            // 
            panelTendencias.BackColor = Color.White;
            panelTendencias.BorderStyle = BorderStyle.FixedSingle;
            panelTendencias.Location = new Point(630, 260);
            panelTendencias.Name = "panelTendencias";
            panelTendencias.Size = new Size(330, 380);
            panelTendencias.TabIndex = 2;
            // 
            // FormEstadisticas
            // 
            BackColor = Color.FromArgb(240, 248, 255);
            ClientSize = new Size(984, 661);
            Controls.Add(panelResumen);
            Controls.Add(panelGraficos);
            Controls.Add(panelTendencias);
            MaximizeBox = false;
            Name = "FormEstadisticas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Estadísticas y Análisis - Habitus";
            Load += FormEstadisticas_Load;
            ResumeLayout(false);
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