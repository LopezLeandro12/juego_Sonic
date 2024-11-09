using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace juego_Sonic
{
    public partial class Frm_menu : Form
    {
        private Label lblMejorPuntuacion;
        private Button btnIniciar;
        private Button btnSalir;

        public Frm_menu()
        {
            InitializeComponent();
            InicializarMenu();
        }

        private void InicializarMenu()
        {
            
            this.Text = "Menú Principal";
            this.Size = new Size(1260, 616);
            this.StartPosition = FormStartPosition.CenterScreen;

            
            lblMejorPuntuacion = new Label
            {
                
                Location = new Point(120, 50),
                Font = new Font("Arial", 16),
                ForeColor = Color.Black,
                AutoSize = true
            };
            Controls.Add(lblMejorPuntuacion);

            
            btnIniciar = new Button
            {
                Text = "Iniciar Juego",
                Location = new Point(520, 470),
                Size = new Size(160, 40),
                Font = new Font("Arial", 14)
            };
            btnIniciar.Click += BtnIniciar_Click;
            Controls.Add(btnIniciar);

          
            btnSalir = new Button
            {
                Text = "Salir",
                Location = new Point(520, 520),
                Size = new Size(160, 40),
                Font = new Font("Arial", 14)
            };
            btnSalir.Click += BtnSalir_Click;
            Controls.Add(btnSalir);
        }

       
        private void BtnIniciar_Click(object sender, EventArgs e)
        {
            Juego juegoForm = new Juego();  
            this.Hide();  
            juegoForm.ShowDialog();   
            this.Show();  
        }

       
        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

}
