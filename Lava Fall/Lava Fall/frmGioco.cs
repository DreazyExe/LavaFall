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
        #region Global costants
        // game state
        enum eGameState
        {
            atstake,
            suspended,
        }
        // Movement costants
        const int MAXRIGHTFORMPOSITION = 650;
        const int MAXLEFTFORMPOSITION = 0;
        const int CHARACTERWIDTH = 145;
        #endregion

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
        bool jump;                // Indicates if the character is jumping
        int initialForce = 75;    // Initial force of the jump
        int force = 0;            // ?

        // VETTORE DI PROVA PER IL MIO ALGORITMO
        //(CONTIENE I PUNTI X DOVE PUO SPOWNARE LA BASE)
        //LUNGHEZZA FORM ORIZZONTALE 791
        //791 / 4 = 197
        //ipotizzo una piattaforma lunga circa 200

        // Array of possible position of objects (position x)
        int[] defaultXPositions = { 10 , 220 ,430, 650  };

        // stores the position of the previous platform
        int _lastRandomNumber;

        // indix of state game
        eGameState _stateGame;

        #endregion

        // FORM BUILDER
        public FormGioco()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            // when the program start the game is suspended
            _stateGame = eGameState.suspended;
        }

        // STARTING COUNTDOWN - STATUS: RESOLVE BUGS
        // Problems:
        // 1 - Go isn't in the middle of the screen
        // 2 (not important) - The background of the label isn't trasparent
        private void countDown_Tick(object sender, EventArgs e)
        {
            // Subtract 1 second to the counter 
            _counter--;

            // Check if the countdown finishes
            // If the countdown finished
            if (_counter <= 0)
            {
                _stateGame = eGameState.atstake;

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

        // INCREASE POINTS - STATUS: OK
        /// <summary>
        /// Increases score of a set quantity
        /// </summary>
        /// <param name="quantityToAdd"> Quantity to add to the points.</param>
        private void increasePoints(byte quantityToAdd)
        {
            // Add the quantity to the total points
            _points += quantityToAdd;

            // Refresh of the points label
            lblPunteggio.Text = _points.ToString();
        }
        
        // MOVEMENT OF THE LAVA - STATUS: OK
        private void timerLava_Tick(object sender, EventArgs e)
        {
            if(_stateGame == eGameState.atstake)
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
            
        }

        // MOVE OF THE BASIS - STATUS: OK
        private void spostamento_basi_Tick(object sender, EventArgs e)
        {
            // For every tick move all the bases 10 pixels down
            pbBase1.Top += 10;
            pbBase2.Top += 10;
            pbBase3.Top += 10;
            pbBase4.Top += 10;

            // If one of the basis is in the bottom of form respawn it in the top
            RespawnBasesCheck();

            // Add 1 point to the points    
            increasePoints(1);
        }

        /// <summary>
        /// Controls if one of the bases reached lava and, if true, brings it to the top of the form
        /// </summary>
        private void RespawnBasesCheck()
        {
            // If the first base reached lava respawn it
            if (pbBase1.Top > 657)
                respawnObject(pbBase1);
            // If the second base reached lava respawn it
            else if (pbBase2.Top > 657)
                respawnObject(pbBase2);
            // If the third base reached lava respawn it
            else if (pbBase3.Top > 657)
                respawnObject(pbBase3);
            // If the fourth base reached lava respawn it
            else if (pbBase4.Top > 657)
                respawnObject(pbBase4);
            // If no bases reached lava exit
            else
                return;
        }

        /// <summary>
        /// Respawns an object in the bottom of the form
        /// </summary>
        /// <param name="objectToMove"> Name of the PictureBox to move</param>
        private void respawnObject(PictureBox objectToMove)
        {
            // Declaration of variables
            int _randomNumber;
            Random random = new Random();
            // Generate a random number to choose the X position from the array of default X positions
            _randomNumber = random.Next(0, defaultXPositions.Length - 1);
            // this condition prevents the platform from spawning in the same location as the previous one
            if (_randomNumber != _lastRandomNumber && _randomNumber != _randomNumber+1 && _randomNumber != _randomNumber - 1)
            {
                int X = defaultXPositions[_randomNumber];
                // Calculate the Y position (take the Y position of the image and substract 668 to go to the top of the form
                int Y = objectToMove.Location.Y - 668;
                // Move the object in the top of the form with the calculated coordinates
                objectToMove.Location = new Point(X, Y);
            }
            _lastRandomNumber = _randomNumber;
        }

        // MOVE OF THE CHARACTER (EVENT IF A KEY IS PRESSED AND LEFT-RIGHT MOVEMENT) - STATUS: OK
        /// <summary>
        /// Event if the user presses a key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="key"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs key)
        {
            // if the game is not at stake you can't move
            if(_stateGame == eGameState.atstake)
            {
                // Perform an action based on the pressed key
                switch (key.KeyCode)
                {
                    // If the key is "left arrow" go back 20 px only if the character doesn't go outside the form
                    case Keys.Left:
                        if (pbPersonaggio.Left - 20 >= MAXLEFTFORMPOSITION)
                            pbPersonaggio.Left -= 20;
                        break;
                    // If the key is "right arrow" move forward 20 px only if the character doesn't go outside the form
                    case Keys.Right:
                        if (pbPersonaggio.Left + 20 <= MAXRIGHTFORMPOSITION)
                            pbPersonaggio.Left += 20;
                        break;
                    // Istructions if the key is "space"
                    case Keys.Space:
                        // Jump only if the character is not jumping
                        if (!jump)
                        {
                            jump = true;
                            force = initialForce;
                        }
                        break;
                }
            }
            
        }

        // MOVE OF THE CHARACTER (JUMP) - STATUS: RESOLVE BUGS
        // Problems:
        // 1: Collision is not ok: The character automatically jumps
        // 2: If the character is jumping on a base it won't jump with the space key

        /// <summary>
        /// Makes the character jump
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
    }
}