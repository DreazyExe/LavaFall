using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lava_Fall
{
    public partial class FormGioco : Form
    {
        #region Global variables
        // Starting count
        int _counter = 3;

        // Lava movement
        int i = 0;  //Counter of the image
        System.Drawing.Image[] arrayLava = 
        {
            Properties.Resources.B, Properties.Resources.C, Properties.Resources.D,
            Properties.Resources.E, Properties.Resources.F, Properties.Resources.G,
            Properties.Resources.H, Properties.Resources.I, Properties.Resources.J,
            Properties.Resources.K, Properties.Resources.L, Properties.Resources.A
        };  //Array of lava images

        // Points
        int _punteggio = 0; // Point variable
        
        // Movement variables
        bool right;     // Direction of the move (right)
        bool left;      // Direction of the move (left)
        bool jump;      // Direction of the move (up)
        int g = 100;    // Width of the jump
        int force = 0;  // ?

        //Todo commenti

        #endregion

        public FormGioco()
        {
            InitializeComponent();
        }

        #region Aumento punteggio
        /// <summary>
        /// Aumenta il punteggio di una quantità stabilita.
        /// </summary>
        /// <param name="aumento">Quantità da aggiungere al punteggio</param>
        private void aumentoPunteggio(byte aumento)
        {
            //Aumento il punteggio di quanto stabilito
            _punteggio += aumento;

            //Aggiornare il punteggio sulla label apposita
            lblPunteggio.Text = _punteggio.ToString();
        }
        #endregion

        #region Lava movement
        private void timerLava_Tick(object sender, EventArgs e)
        {
            pbLava.Image = arrayLava[i];
            if (i < 11)
                i++;
            else
                i = 0;]
        }
        #endregion

        private void spostamento_basi_Tick(object sender, EventArgs e)
        {
            //BUG: LA SECONDA BASE NON RITORNA SOPRA
            //Dichiaro una variabile per l'ultima base che si è spostata. Inizialmente il suo valore è 2 perché non esiste una base precedente.
            //char ultimaBase = '2';
            //Dichiaro una variabile per generare numeri casuali
            Random rnd = new Random();

            //Lo schermo è compreso tra 0 e 600
            // Spostamento
            foreach(Control oggetto in this.Controls)
            {
                // Verifica se l'oggetto è una picturebox e il nome del tag è una stringa
                if (oggetto is PictureBox && oggetto.Tag is String)
                {
                    // Alla picturebox assegna l'oggetto preso
                    PictureBox baseAttuale = (PictureBox)oggetto;

                    //Entra se il tag dell'oggetto preso è base
                    if (baseAttuale.Tag=="Base")
                    {
                        // Facciamo spostare la base di 5 px in basso
                        // Controllo se ho raggiunto la lava
                        if (baseAttuale.Location.Y >= 786)
                            // In caso affermativo torna sopra
                            baseAttuale.Location = new Point(rnd.Next(rnd.Next(0, baseAttuale.Location.X - 20), rnd.Next(baseAttuale.Location.X + 80, 600)), 0 - baseAttuale.Size.Height);
                        else
                            // In caso negativo fai scendere di 1 pixel
                            baseAttuale.Location = new Point(baseAttuale.Location.X, baseAttuale.Location.Y + 5);
                    }

                    if(baseAttuale.Tag == "BasePrincipale")
                    {
                        if (baseAttuale.Location.Y <= 786)
                            // Facciamo spostare la base di 5 px in basso
                            baseAttuale.Location = new Point(baseAttuale.Location.X, baseAttuale.Location.Y + 5);
                    }
                }
            }
  
            //Aumento il punteggio di 1 ogni volta
            aumentoPunteggio(1);
        }

        #region Countdown iniziale
        private void countDown_Tick(object sender, EventArgs e)
        {
            //Se i secondi di attesa iniziale sono finiti
            if (_counter == 0)
            {
                //Attivo i timer di movimento
                spostamento_basi.Enabled = true;

                //Disattiva il timer
                this.Enabled = false;

                //Rimuovi il counter
                lblCountDown.Visible = false;
            }
            else
            {
                //Aggiorno il counter
                _counter--;

                if (_counter == 0)
                    //Aggiorno la label dei secondi rimasti
                    lblCountDown.Text = "Go!";
                else
                    lblCountDown.Text = _counter.ToString();
            }
        }
        #endregion

        #region Spostamento personaggio
        private void personaggio_pg(object sender, EventArgs e)
        {
            if (right)
            {
                pbPersonaggio.Left += 20;
            }
            if (left)
            {
                pbPersonaggio.Left -= 20;
            }
            if (jump)
            {
                pbPersonaggio.Top -= force;
                force -= 20;
                if (pbPersonaggio.Bounds.IntersectsWith(pbBase1.Bounds))
                {
                    jump = true;
                    force = g;
                }
            }
            if(pbPersonaggio.Top + pbPersonaggio.Height >= 810)
            {
                pbPersonaggio.Top = 810 - pbPersonaggio.Height;
                jump = false;
            }
        }
        #endregion

        #region Eventi quando un utente preme un carattere
        private void Form1_KeyDown(object sender,KeyEventArgs e)
        {
            //Eseguo un azione in base al tasto digitato
            switch (e.KeyCode)
            {
                //Istruzioni se il tasto è la freccia sinistra
                case Keys.Left:
                    left = true;
                    break;
                //Istruzioni se il tasto è la freccia destra
                case Keys.Right:
                    right = true;
                    break;
                //Istruzioni se il tasto è la barra spaziatrice
                case Keys.Space:
                    if (jump == false)
                    {
                        jump = true;
                        force = g;
                    }
                    break;


            }
        }
        #endregion

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            right = false;
            left = false;
        }
    }
}
