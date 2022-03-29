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
        int initialForce = 50;    // Initial force of the jump
        int force = 0;  // ?

        // VETTORE DI PROVA PER IL MIO ALGORITMO
        //(CONTIENE I PUNTI X DOVE PUO SPOWNARE LA BASE)
        //LUNGHEZZA FORM ORIZZONTALE 791
        //791 / 4 = 197
        //ipotizzo una piattaforma lunga circa 200


        int[] possiblePositions = { 10 , 220 ,430, 650  };

        Point coordinates;
        int _newY = 657;

        #endregion

        public FormGioco()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
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
            // each tick moves the base 10 px down
            pbBase1.Top += 10;
            pbBase2.Top += 10;
            pbBase3.Top += 10;
            pbBase4.Top += 10;

            // procedure to restore the bases
            RespownBases();

            // Add 1 point to the points    
            increasePoints(1);
        }

        private void RespownBases()
        {
            if (pbBase1.Top > 657)
            {
                var PositionPb1 = pbBase1.PointToScreen(new Point(10, 10));
            }
            else if (pbBase4.Location.Y > _newY)
            {

                //PRENDO LA POSIZIONE DELLA BASE ATTUALMENTE (LA POSIZIONE DI QUANDO GIA DOVREBBE TORNARE SU LA BASE)
                coordinates = pbBase4.Location;
                

                

                pbBase4.Location = new Point(possiblePositions[2], coordinates.Y - _newY);

                _newY += 650;
            }
            else
                return;

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