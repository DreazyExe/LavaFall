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
        // TODO:
        // -Risolvere i bug indicati nelle apposite sezioni;
        // -Creare un logo (quadrato) per l'applicazione (sarà visibile nella barra delle applicazioni, nel collegamento del gioco e nel titolo della finestra di Windows);
        // -Migliorare le collisioni;
        // -Scrivere la guida di Lava Fall;
        // -Creare un pulsante pausa nella form del gioco per interrompere temporaneamente il tutto;
        // -Salvare i vari punteggi in un file e mostrarli all'utente all'avvio del software (appena ci verranno spiegati i file).
        //
        // Stiamo facendo un ottimo lavoro Picci ;)

        #region Global costants
        // Game state
        enum eGameState
        {
            atstake,
            suspended,
            lost,
        }

        // Game background names
        enum eBackgroundNames
        {
            lava,
            clouds,
            space,
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
        bool _jump;                // Indicates if the character is jumping
        int _initialForce = 75;    // Initial force of the jump
        int _force = 0;            // Force of the jump

        // Background state variable
        int _background = 0;

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

        // to check if he has jumped at least once
        bool jump = false;

        // for the change of the platforms
        bool _ChangeSuccesful = false;
        bool _ChangePlatForm1 = false;
        bool _ChangePlatForm2 = false;
        bool _ChangePlatForm3 = false;
        bool _ChangePlatForm4 = false;
        //clouds background

        PictureBox _bgClouds = new PictureBox
        {
            Name = "prova",
            Size = new Size(810, 750),
            Location = new Point(0, -750),
            Image = Lava_Fall.Properties.Resources.cloudBackground,
        };
        #endregion

        // FORM BUILDER
        public FormGioco()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            // when the program start the game is suspended
            _stateGame = eGameState.suspended;
        }

        #region Events
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

                // Enable the change of background
                backgroundChange.Enabled = true;

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

        // MOVEMENT OF THE LAVA - STATUS: OK
        private void timerLava_Tick(object sender, EventArgs e)
        {
            if (_stateGame == eGameState.atstake)
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
            pbBasePrincipale.Top += 10;

            // at the beginning if he has never jumped the player goes down with the base
            if (jump == false)
                pbPersonaggio.Top += 10;

            // Change of the background
            if (_points >= 10000 && _points < 30000)
            {
                if(pbLava.Location.Y < 750)
                    pbLava.Top += 10;

                if (_ChangeSuccesful == false)
                {
                    setCloudMode();
                }
                else
                {
                    this.BackgroundImage = Properties.Resources.cloudBackground;
                    pbBase1.BackgroundImage = null;
                    pbBase2.BackgroundImage = null;
                    pbBase3.BackgroundImage = null;
                    pbBase4.BackgroundImage = null;
                }
                _bgClouds.Top += 10;
            }
            else if (_points > 30000)
                _bgClouds.Visible = false;

            // Verify if the player has lost
            if (_points < 10000 && pbPersonaggio.Location.Y > 730 || _points > 10000 && pbPersonaggio.Location.Y > 750)
            {
                _stateGame = eGameState.lost;
                FrmClassifica frmclassifica = new FrmClassifica();
                this.Close();
            }
                

            // If one of the basis is in the bottom of form respawn it in the top
            RespawnBasesCheck();

            // Add 1 point to the points    
            increasePoints(255);
        }
         
        private void StartBackground()
        {
            this.Controls.Add(_bgClouds);
        }
        // CHANGE OF THE BACKGROND - STATUS OK
        private void backgroundChange_Tick(object sender, EventArgs e)
        {
            //idea di tomas
            //// If the point are equal or more than 10000 and the cloud background is disabled set it
            //if (_points >= 10000 && _points < 35000 && _background != (int)eBackgroundNames.clouds)
            //    setCloudMode();
            //// Else if the point are equal or more than 35000 and the space background is disabled set it
            //else if (_points >= 35000 && _background != (int)eBackgroundNames.space)
            //    setSpaceMode();
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
            if (_stateGame == eGameState.atstake)
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
                        if (!_jump)
                        {
                            jump = true;
                            _jump = true;
                            _force = _initialForce;
                        }
                        break;
                }
            }

        }
        #endregion

        #region Functions and procedures
        // INCREASE POINTS - STATUS: OK
        /// <summary>
        /// Increases score of a set quantity
        /// </summary>
        /// <param name="quantityToAdd"> Quantity to add to the points.</param>
        private void increasePoints(byte quantityToAdd)
        {
            if (_points > 10000 && _points < 22000)
            {
                StartBackground();
            }

            // Add the quantity to the total points
            _points += quantityToAdd;

            // Refresh of the points label
            lblPunteggio.Text = _points.ToString();
        }

        // RESPAWN BASES - STATUS: OK
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

        // RESPAWN OBJECTS - STATUS: OK
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
            if (_randomNumber != _lastRandomNumber && _randomNumber != _randomNumber + 1 && _randomNumber != _randomNumber - 1)
            {
                int X = defaultXPositions[_randomNumber];
                // Calculate the Y position (take the Y position of the image and substract 668 to go to the top of the form
                int Y = objectToMove.Location.Y - 668;
                // Move the object in the top of the form with the calculated coordinates
                objectToMove.Location = new Point(X, Y);
            }
            _lastRandomNumber = _randomNumber;
        }

        // CHANGE OF THE BACKGROUND - STATUS: OK
        /// <summary>
        /// Sets the graphic of the game to the cloud mode
        /// </summary>
        private void setCloudMode()
        {

            int backgroundLocationY = _bgClouds.Location.Y;

            if(pbBase1.Location.Y > 657 && _ChangePlatForm1 == false && backgroundLocationY > -550)
            {
                // Change base1 dimensions and image
                pbBase1.Width = Properties.Resources.elicottero3.Width;
                pbBase1.Height = Properties.Resources.elicottero3.Height;
                pbBase1.Image = Properties.Resources.elicottero3;
                pbBase1.BackgroundImage = Lava_Fall.Properties.Resources.cloudBackground;

                _ChangePlatForm1 = true;
            }
            
            if(pbBase2.Location.Y > 657 && _ChangePlatForm2 == false && backgroundLocationY > -550)
            {
                // Change base2 dimensions and image
                pbBase2.Width = Properties.Resources.elicottero2.Width;
                pbBase2.Height = Properties.Resources.elicottero2.Height;
                pbBase2.Image = Properties.Resources.elicottero2;
                pbBase2.BackgroundImage = Lava_Fall.Properties.Resources.cloudBackground;

                _ChangePlatForm2 = true;
            }

            if(pbBase3.Location.Y > 657 && _ChangePlatForm3 == false && backgroundLocationY > -550)
            {
                // Change base3 dimensions and image
                pbBase3.Width = Properties.Resources.elicottero1.Width;
                pbBase3.Height = Properties.Resources.elicottero1.Height;
                pbBase3.Image = Properties.Resources.elicottero1;
                pbBase3.BackgroundImage = Lava_Fall.Properties.Resources.cloudBackground;

                _ChangePlatForm3 = true;
            }

            if (pbBase4.Location.Y > 657 && _ChangePlatForm4 == false && backgroundLocationY > -550)
            {
                // Change base4 dimensions and image
                pbBase4.Width = Properties.Resources.elicottero3.Width;
                pbBase4.Height = Properties.Resources.elicottero3.Height;
                pbBase4.Image = Properties.Resources.elicottero3;
                pbBase4.BackgroundImage = Lava_Fall.Properties.Resources.cloudBackground;
                _ChangePlatForm4 = true;
            }


            if (_ChangePlatForm1 && _ChangePlatForm2 && _ChangePlatForm3 && _ChangePlatForm4)
            {
                _ChangeSuccesful = true;

            }






            //// Remove the principal base
            //pbBasePrincipale.Visible = false;

            //// Change the background type
            //_background = (int)eBackgroundNames.clouds;
        }

        /// <summary>
        /// Sets the graphic of the game to the space mode
        /// </summary>
        private void setSpaceMode()
        {
            // Change background
            this.BackgroundImage = Properties.Resources.spaceBackground;

            // Change base1 dimensions and image
            pbBase1.Width = Properties.Resources.spaceship1.Width;
            pbBase1.Height = Properties.Resources.spaceship1.Height;
            pbBase1.Image = Properties.Resources.spaceship1;

            // Change base2 dimensions and image
            pbBase2.Width = Properties.Resources.spaceship2.Width;
            pbBase2.Height = Properties.Resources.spaceship2.Height;
            pbBase2.Image = Properties.Resources.spaceship2;

            // Change base3 dimensions and image
            pbBase3.Width = Properties.Resources.spaceship3.Width;
            pbBase3.Height = Properties.Resources.spaceship3.Height;
            pbBase3.Image = Properties.Resources.spaceship3;

            // Change base4 dimensions and image
            pbBase4.Width = Properties.Resources.spaceship1.Width;
            pbBase4.Height = Properties.Resources.spaceship1.Height;
            pbBase4.Image = Properties.Resources.spaceship1;

            // Change the background type
            _background = (int)eBackgroundNames.space;
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
            if (_jump)
            {
                // Decrease the distance between the top of the window and the character
                pbPersonaggio.Top -= _force;
                // Decrease the force of the jump (pixels movement)
                _force -= 20;
                // If the character touches a base start the jump again
                if (pbPersonaggio.Bounds.IntersectsWith(pbBase1.Bounds) || pbPersonaggio.Bounds.IntersectsWith(pbBase2.Bounds) || pbPersonaggio.Bounds.IntersectsWith(pbBase3.Bounds) || pbPersonaggio.Bounds.IntersectsWith(pbBase4.Bounds))
                {
                    // Enable jump
                    _jump = true;
                    // Reset the initial force
                    _force = _initialForce;
                }
            }
            // Verify if the character has passed the top of the form
            if (pbPersonaggio.Top + pbPersonaggio.Height >= 810)
            {
                // Move down the character
                pbPersonaggio.Top = 810 - pbPersonaggio.Height;
                // Disable the jump
                _jump = false;
            }
        }
        #endregion
    }
}