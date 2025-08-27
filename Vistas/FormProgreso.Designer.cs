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
            MinimizeBox = false;
            MaximizeBox = false;

            dtpFechaInicio = new DateTimePicker();
            dtpFechaFin = new DateTimePicker();
            btnActualizar = new Button();
            panelGraficos = new Panel();
            panelEstadisticas = new Panel();
            lblResumenPeriodo = new Label();
            txtNotas = new TextBox();
            btnGuardarNotas = new Button();
            cmbEstadoAnimo = new ComboBox();
            btnRegistrarProgreso = new Button();
            lblTitulo = new Label();
            panelFiltros = new Panel();
            lblFechaInicio = new Label();
            lblFechaFin = new Label();
            panelRegistro = new Panel();
            panelFiltros.SuspendLayout();
            SuspendLayout();
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Font = new Font("Segoe UI", 9F);
            dtpFechaInicio.Format = DateTimePickerFormat.Short;
            dtpFechaInicio.Location = new Point(80, 18);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(120, 27);
            dtpFechaInicio.TabIndex = 1;
            dtpFechaInicio.Value = new DateTime(2025, 7, 27, 0, 0, 0, 0);
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Font = new Font("Segoe UI", 9F);
            dtpFechaFin.Format = DateTimePickerFormat.Short;
            dtpFechaFin.Location = new Point(280, 18);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(120, 27);
            dtpFechaFin.TabIndex = 3;
            dtpFechaFin.Value = new DateTime(2025, 8, 26, 0, 0, 0, 0);
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.FromArgb(52, 152, 219);
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.Font = new Font("Segoe UI", 9F);
            btnActualizar.ForeColor = Color.White;
            btnActualizar.Location = new Point(420, 18);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(80, 25);
            btnActualizar.TabIndex = 4;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += BtnActualizar_Click;
            // 
            // panelGraficos
            // 
            panelGraficos.BackColor = Color.White;
            panelGraficos.BorderStyle = BorderStyle.FixedSingle;
            panelGraficos.Location = new Point(20, 280);
            panelGraficos.Name = "panelGraficos";
            panelGraficos.Size = new Size(840, 250);
            panelGraficos.TabIndex = 3;
            // 
            // panelEstadisticas
            // 
            panelEstadisticas.BackColor = Color.White;
            panelEstadisticas.BorderStyle = BorderStyle.FixedSingle;
            panelEstadisticas.Location = new Point(20, 140);
            panelEstadisticas.Name = "panelEstadisticas";
            panelEstadisticas.Size = new Size(840, 120);
            panelEstadisticas.TabIndex = 2;
            // 
            // lblResumenPeriodo
            // 
            lblResumenPeriodo.Location = new Point(0, 0);
            lblResumenPeriodo.Name = "lblResumenPeriodo";
            lblResumenPeriodo.Size = new Size(100, 23);
            lblResumenPeriodo.TabIndex = 0;
            // 
            // txtNotas
            // 
            txtNotas.Location = new Point(0, 0);
            txtNotas.Name = "txtNotas";
            txtNotas.Size = new Size(100, 27);
            txtNotas.TabIndex = 0;
            // 
            // btnGuardarNotas
            // 
            btnGuardarNotas.Location = new Point(0, 0);
            btnGuardarNotas.Name = "btnGuardarNotas";
            btnGuardarNotas.Size = new Size(75, 23);
            btnGuardarNotas.TabIndex = 0;
            // 
            // cmbEstadoAnimo
            // 
            cmbEstadoAnimo.Location = new Point(0, 0);
            cmbEstadoAnimo.Name = "cmbEstadoAnimo";
            cmbEstadoAnimo.Size = new Size(121, 28);
            cmbEstadoAnimo.TabIndex = 0;
            // 
            // btnRegistrarProgreso
            // 
            btnRegistrarProgreso.Location = new Point(0, 0);
            btnRegistrarProgreso.Name = "btnRegistrarProgreso";
            btnRegistrarProgreso.Size = new Size(75, 23);
            btnRegistrarProgreso.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(100, 23);
            lblTitulo.TabIndex = 0;
            // 
            // panelFiltros
            // 
            panelFiltros.Controls.Add(lblFechaInicio);
            panelFiltros.Controls.Add(dtpFechaInicio);
            panelFiltros.Controls.Add(lblFechaFin);
            panelFiltros.Controls.Add(dtpFechaFin);
            panelFiltros.Controls.Add(btnActualizar);
            panelFiltros.Location = new Point(0, 0);
            panelFiltros.Name = "panelFiltros";
            panelFiltros.Size = new Size(200, 100);
            panelFiltros.TabIndex = 1;
            // 
            // lblFechaInicio
            // 
            lblFechaInicio.Location = new Point(0, 0);
            lblFechaInicio.Name = "lblFechaInicio";
            lblFechaInicio.Size = new Size(100, 23);
            lblFechaInicio.TabIndex = 0;
            // 
            // lblFechaFin
            // 
            lblFechaFin.Location = new Point(0, 0);
            lblFechaFin.Name = "lblFechaFin";
            lblFechaFin.Size = new Size(100, 23);
            lblFechaFin.TabIndex = 2;
            // 
            // panelRegistro
            // 
            panelRegistro.Location = new Point(0, 0);
            panelRegistro.Name = "panelRegistro";
            panelRegistro.Size = new Size(200, 100);
            panelRegistro.TabIndex = 4;
            // 
            // FormProgreso
            // 
            BackColor = Color.FromArgb(240, 248, 255);
            ClientSize = new Size(882, 653);
            Controls.Add(lblTitulo);
            Controls.Add(panelFiltros);
            Controls.Add(panelEstadisticas);
            Controls.Add(panelGraficos);
            Controls.Add(panelRegistro);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "FormProgreso";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Progreso - Habitus";
            panelFiltros.ResumeLayout(false);
            ResumeLayout(false);
        }
        private Label lblTitulo;
        private Panel panelFiltros;
        private Label lblFechaInicio;
        private Label lblFechaFin;
        private Panel panelRegistro;
    }
}