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
        int _points = 0; // Point variable
        
        // Movement variables
        bool right;     // Direction of the move (right)
        bool left;      // Direction of the move (left)
        bool jump;      // Direction of the move (up)
        int initialForce = 100;    // Initial force of the jump
        int force = 0;  // ?
        #endregion

        public FormGioco()
        {
            InitializeComponent();
        }

        #region Starting countdown
        private void countDown_Tick(object sender, EventArgs e)
        {
            // Subtract 1 second to the counter 
            _counter--;

            // Check if the countdown finishes
            // If the countdown finished
            if (_counter <= 0)
            {
                // Write "Go!" on the label of the countdown to indicate the beginning of the game
                lblCountDown.Text = "Go!";

                // Enable movement timer
                spostamento_basi.Enabled = true;

                // Disable countdown timer
                this.Enabled = false;

                // Hide counter label
                lblCountDown.Visible = false;
            }
            // If the countdown hasn't finished yet
            else
                // Refresh counter label
                lblCountDown.Text = _counter.ToString();
        }
        #endregion

        #region Increase points
        /// <summary>
        /// Increases score of a set quantity
        /// </summary>
        /// <param name="quantityToAdd"> Quantity to add to the points</param>
        private void increasePoints(byte quantityToAdd)
        {
            // Add the quantity to the total points
            _points += quantityToAdd;

            // Refresh of the points label
            lblPunteggio.Text = _points.ToString();
        }
        #endregion

        #region Lava movement
        private void timerLava_Tick(object sender, EventArgs e)
        {
            // Set the new frame of the lava
            pbLava.Image = arrayLava[i];

            // Increase the indicator of frame
            if (i < 11)
                i++;

            // If frames are finished restart the animation
            else
                i = 0;
        }
        #endregion

        // TODO Sistemare spostamento basi ottimizzando il codice che rallenta il programma
        #region Spostamento basi
        private void spostamento_basi_Tick(object sender, EventArgs e)
        {
            // TODO Spostare una base alla volta e non effettuare il controllo di tutti i controlli nella form
            // TODO Progettare un algoritmo per riportare le basi in alto cambiando la posizione ed evitando che siano appiccicate

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

            // Add 1 point to the points    
            increasePoints(1);
        }
        #endregion

        // Ok
        #region Events if the user presses a key
        private void Form1_KeyDown(object sender, KeyEventArgs key)
        {
            // Perform an action based on the pressed key
            switch (key.KeyCode)
            {
                // If the key is "left arrow" go back 20 px
                case Keys.Left:
                    pbPersonaggio.Left -= 20;
                    break;
                // If the key is "right arrow" move forward 20 px
                case Keys.Right:
                    pbPersonaggio.Left += 20;
                    break;
                // Istructions if the key is "space"
                case Keys.Space:
                    // Jump only if the character is not jumping
                    if (jump == false)
                    {
                        jump = true;
                        force = initialForce;
                    }
                    break;
            }
        }
        #endregion

        #region Character jump
        private void characterJump(object sender, EventArgs e)
        {
            // Verifies if there's the necessity to jump
            if (jump)
            {
                // Decrease the distance between the top of the window and the character
                pbPersonaggio.Top -= force;
                // Decrease the force of the jump (pixels movement)
                force -= 20;
                // If the character touches a base start the jump again
                if (pbPersonaggio.Bounds.IntersectsWith(pbBase1.Bounds) || pbPersonaggio.Bounds.IntersectsWith(pbBase2.Bounds) || pbPersonaggio.Bounds.IntersectsWith(pbBase3.Bounds) || pbPersonaggio.Bounds.IntersectsWith(pbBase4.Bounds))
                {
                    // Enable jump
                    jump = true;
                    // Reset the initial force
                    force = initialForce;
                }
            }
            // Verify if the character has passed the top of the form
            if (pbPersonaggio.Top + pbPersonaggio.Height >= 810)
            {
                // Move down the character
                pbPersonaggio.Top = 810 - pbPersonaggio.Height;
                // Disable the jump
                jump = false;
            }
        }
        #endregion
    }
}