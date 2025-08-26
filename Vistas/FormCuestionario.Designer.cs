using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Habitus.Vistas
{
    partial class FormCuestionario
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
            this.Text = "Cuestionario Inicial - Habitus";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(240, 248, 255);

            // Declaración de controles
            this.lblPregunta = new Label();
            this.radioButtons = new RadioButton[4];
            this.btnSiguiente = new Button();
            this.btnAnterior = new Button();
            this.progressBar = new ProgressBar();
            this.lblProgreso = new Label();

            // Título
            var lblTitulo = new Label
            {
                Text = "Cuestionario de Evaluación Inicial",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                Location = new Point(50, 20),
                Size = new Size(500, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblTitulo);

            // Barra de progreso
            this.progressBar.Location = new Point(50, 60);
            this.progressBar.Size = new Size(500, 20);
            this.progressBar.Style = ProgressBarStyle.Continuous;
            this.Controls.Add(this.progressBar);

            // Label de progreso
            this.lblProgreso.Location = new Point(50, 85);
            this.lblProgreso.Size = new Size(500, 20);
            this.lblProgreso.TextAlign = ContentAlignment.MiddleCenter;
            this.lblProgreso.Font = new Font("Segoe UI", 9);
            this.lblProgreso.ForeColor = Color.Gray;
            this.Controls.Add(this.lblProgreso);

            // Pregunta
            this.lblPregunta.Location = new Point(50, 120);
            this.lblPregunta.Size = new Size(500, 60);
            this.lblPregunta.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.lblPregunta.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblPregunta.TextAlign = ContentAlignment.MiddleLeft;
            this.Controls.Add(this.lblPregunta);

            // RadioButtons para opciones
            for (int i = 0; i < 4; i++)
            {
                this.radioButtons[i] = new RadioButton
                {
                    Location = new Point(70, 200 + (i * 40)),
                    Size = new Size(450, 30),
                    Font = new Font("Segoe UI", 10),
                    ForeColor = Color.FromArgb(52, 73, 94),
                    UseVisualStyleBackColor = true
                };
                this.Controls.Add(this.radioButtons[i]);
            }

            // Botón Anterior
            this.btnAnterior.Text = "Anterior";
            this.btnAnterior.Location = new Point(200, 400);
            this.btnAnterior.Size = new Size(100, 35);
            this.btnAnterior.Font = new Font("Segoe UI", 10);
            this.btnAnterior.BackColor = Color.FromArgb(149, 165, 166);
            this.btnAnterior.ForeColor = Color.White;
            this.btnAnterior.FlatStyle = FlatStyle.Flat;
            this.btnAnterior.Enabled = false;
            this.btnAnterior.FlatAppearance.BorderSize = 0;
            this.btnAnterior.Click += BtnAnterior_Click;
            this.Controls.Add(this.btnAnterior);

            // Botón Siguiente
            this.btnSiguiente.Text = "Siguiente";
            this.btnSiguiente.Location = new Point(320, 400);
            this.btnSiguiente.Size = new Size(100, 35);
            this.btnSiguiente.Font = new Font("Segoe UI", 10);
            this.btnSiguiente.BackColor = Color.FromArgb(41, 128, 185);
            this.btnSiguiente.ForeColor = Color.White;
            this.btnSiguiente.FlatStyle = FlatStyle.Flat;
            this.btnSiguiente.FlatAppearance.BorderSize = 0;
            this.btnSiguiente.Click += BtnSiguiente_Click;
            this.Controls.Add(this.btnSiguiente);
        }

        // Declaración de controles
        private Label lblPregunta;
        private RadioButton[] radioButtons;
        private Button btnSiguiente;
        private Button btnAnterior;
        private ProgressBar progressBar;
        private Label lblProgreso;

        #endregion
    }
}