using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Habitus.Vistas
{
    partial class FormDatosPersonales
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
            this.Text = "Datos Personales - Habitus";
            this.Size = new Size(500, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(240, 248, 255);

            // Declaración de controles
            this.txtNombre = new TextBox();
            this.numEdad = new NumericUpDown();
            this.cmbGenero = new ComboBox();
            this.numPeso = new NumericUpDown();
            this.numAltura = new NumericUpDown();
            this.cmbNivelActividad = new ComboBox();
            this.btnGuardar = new Button();
            this.btnCancelar = new Button();

            // Configuración de controles
            ((System.ComponentModel.ISupportInitialize)(this.numEdad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPeso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAltura)).BeginInit();

            // Título
            var lblTitulo = new Label
            {
                Text = "Completa tu Perfil",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                Location = new Point(50, 20),
                Size = new Size(400, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblTitulo);

            // Subtítulo - Se configurará en el constructor con los puntos iniciales
            var lblSubtitulo = new Label
            {
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(39, 174, 96),
                Location = new Point(50, 55),
                Size = new Size(400, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblSubtitulo);

            int yPos = 100;
            int spacing = 60;

            // Nombre
            var lblNombre = new Label
            {
                Text = "Nombre:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(50, yPos),
                Size = new Size(100, 20)
            };
            this.Controls.Add(lblNombre);

            this.txtNombre.Location = new Point(50, yPos + 25);
            this.txtNombre.Size = new Size(300, 25);
            this.txtNombre.Font = new Font("Segoe UI", 10);
            this.Controls.Add(this.txtNombre);

            yPos += spacing;

            // Edad
            var lblEdad = new Label
            {
                Text = "Edad:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(50, yPos),
                Size = new Size(100, 20)
            };
            this.Controls.Add(lblEdad);

            this.numEdad.Location = new Point(50, yPos + 25);
            this.numEdad.Size = new Size(100, 25);
            this.numEdad.Font = new Font("Segoe UI", 10);
            this.numEdad.Minimum = 10;
            this.numEdad.Maximum = 120;
            this.numEdad.Value = 25;
            this.Controls.Add(this.numEdad);

            yPos += spacing;

            // Género
            var lblGenero = new Label
            {
                Text = "Género:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(50, yPos),
                Size = new Size(100, 20)
            };
            this.Controls.Add(lblGenero);

            this.cmbGenero.Location = new Point(50, yPos + 25);
            this.cmbGenero.Size = new Size(150, 25);
            this.cmbGenero.Font = new Font("Segoe UI", 10);
            this.cmbGenero.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbGenero.Items.AddRange(new[] { "Masculino", "Femenino", "Otro" });
            this.cmbGenero.SelectedIndex = 0;
            this.Controls.Add(this.cmbGenero);

            yPos += spacing;

            // Peso
            var lblPeso = new Label
            {
                Text = "Peso (kg):",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(50, yPos),
                Size = new Size(100, 20)
            };
            this.Controls.Add(lblPeso);

            this.numPeso.Location = new Point(50, yPos + 25);
            this.numPeso.Size = new Size(100, 25);
            this.numPeso.Font = new Font("Segoe UI", 10);
            this.numPeso.Minimum = 30;
            this.numPeso.Maximum = 300;
            this.numPeso.Value = 70;
            this.numPeso.DecimalPlaces = 1;
            this.Controls.Add(this.numPeso);

            yPos += spacing;

            // Altura
            var lblAltura = new Label
            {
                Text = "Altura (cm):",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(50, yPos),
                Size = new Size(100, 20)
            };
            this.Controls.Add(lblAltura);

            this.numAltura.Location = new Point(50, yPos + 25);
            this.numAltura.Size = new Size(100, 25);
            this.numAltura.Font = new Font("Segoe UI", 10);
            this.numAltura.Minimum = 100;
            this.numAltura.Maximum = 250;
            this.numAltura.Value = 170;
            this.Controls.Add(this.numAltura);

            yPos += spacing;

            // Nivel de Actividad
            var lblNivelActividad = new Label
            {
                Text = "Nivel de Actividad:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(50, yPos),
                Size = new Size(150, 20)
            };
            this.Controls.Add(lblNivelActividad);

            this.cmbNivelActividad.Location = new Point(50, yPos + 25);
            this.cmbNivelActividad.Size = new Size(200, 25);
            this.cmbNivelActividad.Font = new Font("Segoe UI", 10);
            this.cmbNivelActividad.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbNivelActividad.Items.AddRange(new[] { 
                "Sedentario", 
                "Ligeramente activo", 
                "Moderadamente activo", 
                "Muy activo", 
                "Extremadamente activo" 
            });
            this.cmbNivelActividad.SelectedIndex = 0;
            this.Controls.Add(this.cmbNivelActividad);

            yPos += spacing;

            // Botones
            this.btnGuardar = new Button
            {
                Text = "Guardar",
                Location = new Point(150, yPos + 25),
                Size = new Size(100, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.Click += BtnGuardar_Click;
            this.Controls.Add(this.btnGuardar);

            this.btnCancelar = new Button
            {
                Text = "Cancelar",
                Location = new Point(260, yPos + 25),
                Size = new Size(100, 35),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.Click += BtnCancelar_Click;
            this.Controls.Add(this.btnCancelar);

            ((System.ComponentModel.ISupportInitialize)(this.numEdad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPeso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAltura)).EndInit();
        }

        // Declaración de controles
        private TextBox txtNombre;
        private NumericUpDown numEdad;
        private ComboBox cmbGenero;
        private NumericUpDown numPeso;
        private NumericUpDown numAltura;
        private ComboBox cmbNivelActividad;
        private Button btnGuardar;
        private Button btnCancelar;

        #endregion
    }
}