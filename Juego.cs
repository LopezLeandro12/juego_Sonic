using juego_SonicBack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;



namespace juego_Sonic
{

    public partial class Juego : Form
    {
        private Cl_personaje jugador;
        private List<Cl_anillo> anillos;
        private Esmeralda esmeralda;
        private Random random;
        private Label lblPuntuacion;
        private Label lblMejorPuntuacion;
        private Timer timer;
        private int mejorPuntuacion;
        private int maxAnillos = 5;
        private bool esmeraldaGenerada = false;

        public Juego()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            jugador = new Cl_personaje("Jugador 1");
            anillos = new List<Cl_anillo>();
            random = new Random();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();

            lblPuntuacion = new Label
            {
                Text = "Score: 0",
                Location = new Point(10, 10),
                Font = new Font("Arial", 14),
                ForeColor = Color.Black
            };
            Controls.Add(lblPuntuacion);

            lblMejorPuntuacion = new Label
            {
                Text = "H.score: " + LeerMejorPuntuacion(),
                Location = new Point(10, 40),
                Font = new Font("Arial", 14),
                ForeColor = Color.Black
            };
            Controls.Add(lblMejorPuntuacion);

            Controls.Add(jugador.ImagenJugador);

            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
        }

        private int LeerMejorPuntuacion()
        {
            string archivo = "H.score.txt";
            if (File.Exists(archivo))
            {
                return int.Parse(File.ReadAllText(archivo));
            }
            return 0;
        }

        private void GuardarMejorPuntuacion()
        {
            string archivo = "H.score.txt";
            if (jugador.Puntuacion > mejorPuntuacion)
            {
                mejorPuntuacion = jugador.Puntuacion;
                File.WriteAllText(archivo, mejorPuntuacion.ToString());
                lblMejorPuntuacion.Text = "H.score: " + mejorPuntuacion;
            }
        }

        private void CrearAnillo()
        {
            if (anillos.Count < maxAnillos)
            {
                int x = random.Next(50, this.Width - 50);
                int y = random.Next(50, this.Height - 50);
                Cl_anillo anillo = new Cl_anillo(x, y);
                anillos.Add(anillo);
                Controls.Add(anillo.Item);
            }
        }

        private void CrearEsmeralda()
        {
            if (jugador.Puntuacion >= 50 && !esmeraldaGenerada)
            {
                esmeraldaGenerada = true;
                int x = random.Next(50, this.Width - 50);
                int y = random.Next(50, this.Height - 50);
                esmeralda = new Esmeralda(x, y);
                Controls.Add(esmeralda.Item);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CrearAnillo();
            CrearEsmeralda();
            ComprobarColisiones();
            lblPuntuacion.Text = "Score: " + jugador.Puntuacion;
        }

        private void ComprobarColisiones()
        {
            foreach (var anillo in anillos)
            {
                if (anillo.Item.Bounds.IntersectsWith(jugador.ImagenJugador.Bounds))
                {
                    jugador.ActualizarPuntuacion();
                    Controls.Remove(anillo.Item);
                    anillos.Remove(anillo);
                    break;
                }
            }

            if (esmeralda != null && esmeralda.Item.Bounds.IntersectsWith(jugador.ImagenJugador.Bounds))
            {
                FinDelJuego();
            }
        }

        private void FinDelJuego()
        {
            timer.Stop();
            MessageBox.Show("¡Sonic Got a Chaos Emerald! El juego ha terminado.", "Juego Terminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            GuardarMejorPuntuacion();
            Application.Exit();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                jugador.MoverArriba();
            }
            else if (e.KeyCode == Keys.Down)
            {
                jugador.MoverAbajo();  
            }
            else if (e.KeyCode == Keys.Left)
            {
                jugador.MoverIzquierda();
            }
            else if (e.KeyCode == Keys.Right)
            {
                jugador.MoverDerecha(); 
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            GuardarMejorPuntuacion();
        }
    }


}


