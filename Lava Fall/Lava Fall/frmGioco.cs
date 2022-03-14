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
        //variabili globali
        int i = 1;
        int _punteggio = 0;
        int _counter = 3;

        //variabili movimento
        bool right;
        bool left;

        bool jump;
        int g = 100; //quanto salta in alto il personaggio
        int force = 0;

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

        
        private void timerLava_Tick(object sender, EventArgs e)
        {
            //ANIMAZIONE LAVA
            switch (i)
            {
                case 1:
                    pbLava.Image = Properties.Resources.B;
                    i++;
                    break;

                case 2:
                    pbLava.Image = Properties.Resources.C;
                    i++;
                    break;

                case 3:
                    pbLava.Image = Properties.Resources.D;
                    i++;
                    break;

                case 4:
                    pbLava.Image = Properties.Resources.E;
                    i++;
                    break;

                case 5:
                    pbLava.Image = Properties.Resources.F;
                    i++;
                    break;

                case 6:
                    pbLava.Image = Properties.Resources.G;
                    i++;
                    break;

                case 7:
                    pbLava.Image = Properties.Resources.H;
                    i++;
                    break;

                case 8:
                    pbLava.Image = Properties.Resources.I;
                    i++;
                    break;

                case 9:
                    pbLava.Image = Properties.Resources.J;
                    i++;
                    break;

                case 10:
                    pbLava.Image = Properties.Resources.K;
                    i++;
                    break;

                case 11:
                    pbLava.Image = Properties.Resources.L;
                    i++;
                    break;
                case 12:
                    pbLava.Image = Properties.Resources.A;
                    i = 1;
                    break;
            }

        }

        private void spostamento_basi_Tick(object sender, EventArgs e)
        {
            //BUG: LA SECONDA BASE NON RITORNA SOPRA
            //Dichiaro una variabile per l'ultima base che si è spostata. Inizialmente il suo valore è 2 perché non esiste una base precedente.
            char ultimaBase = '2';
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
                    if ((string)baseAttuale.Tag=="Base")
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

                    if((string)baseAttuale.Tag == "BasePrincipale")
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
