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
        enum eBackgrounds
        {
            lava,
            clouds,
            space,
        }

        enum eBackgroundToBeSet
        {
            cloudsBackground,
            spaceBackground,
        }
        // Movement costants
        const int MAXRIGHTFORMPOSITION = 650;
        const int MAXLEFTFORMPOSITION = 0;
        const int CHARACTERWIDTH = 145;

        // Change background
        const int TOPSCREEN = -750;
        const int MIDDLESCREEN = -550;
        const int BOTTOMSCREEN = 657;
        #endregion

        #region Global variables
        // Starting count
        int _counter = 3;

        // Lava movement
        int i = 0;  //Counter of the image
        System.Drawing.Image[] _arrayLava = 
        {
            Properties.Resources.B, Properties.Resources.C, Properties.Resources.D,
            Properties.Resources.E, Properties.Resources.F, Properties.Resources.G,
            Properties.Resources.H, Properties.Resources.I, Properties.Resources.J,
            Properties.Resources.K, Properties.Resources.L, Properties.Resources.A
        };  //Array of lava images

        // Points
        int _points = 0; // Point variable
        
        // Character movement variables
        bool _jump;                // Indicates if the character is jumping
        bool jump = false;
        int _initialForce = 75;    // Initial force of the jump
        int _force = 0;            // Force of the jump
        // Array of possible position of objects (position x)
        int[] defaultXPositions = { 10 , 220 , 430, 650  };
        // Positions of the last respawned platform
        int _lastRandomNumber;

        // Character death (array of character death images)
        System.Drawing.Image[] _arrayDeath =
        {
            Properties.Resources.death1, Properties.Resources.death2, Properties.Resources.death3,
            Properties.Resources.death4, Properties.Resources.death5, Properties.Resources.death6,
            Properties.Resources.death7, Properties.Resources.death8, Properties.Resources.death9,
            Properties.Resources.death10, Properties.Resources.death11, Properties.Resources.death12
        };

        // State of the game
        eGameState _stateGame;

        // Platforms movement
        // Status of the change
        bool _ChangeSuccesful = false;
        bool _ChangePlatForm1 = false;
        bool _ChangePlatForm2 = false;
        bool _ChangePlatForm3 = false;
        bool _ChangePlatForm4 = false;
        // Cloud temporary background
        PictureBox _bgClouds = new PictureBox
        {
            Name = "bgClouds",
            Size = new Size(810, 750),
            Location = new Point(0, TOPSCREEN),
            Image = Lava_Fall.Properties.Resources.cloudBackground,
        };
        // Space temporary background
        PictureBox _bgSpace = new PictureBox
        {
            Name = "bgSpace",
            Size = new Size(810, 750),
            Location = new Point(0, TOPSCREEN),
            Image = Lava_Fall.Properties.Resources.cloudBackground,
        };
        // Actual background
        eBackgrounds _actualBackground = eBackgrounds.lava;
        #endregion

        #region Form builder
        public FormGioco()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            // when the program start the game is suspended
            _stateGame = eGameState.suspended;
        }
        #endregion

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
                // Change the state of the game
                _stateGame = eGameState.atstake;

                // Write "Go!" on the label of the countdown to indicate the beginning of the game
                lblCountDown.Text = "Go!";

                // Enable movement timer
                gameClock.Enabled = true;

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
                pbLava.Image = _arrayLava[i];

                // Increase the indicator of frame
                if (i < _arrayLava.Length - 1)
                    i++;
                // If frames are finished restart the animation
                else
                    i = 0;
            }
        }

        // MOVE OF THE BASIS - STATUS: OK
        private void gameClock_Tick(object sender, EventArgs e)
        {
            // MOVEMENT OF BASES
            // For every tick move all the bases 10 pixels down
            pbBase1.Top += 10;
            pbBase2.Top += 10;
            pbBase3.Top += 10;
            pbBase4.Top += 10;
            pbBasePrincipale.Top += 10;
            // If one of the basis is in the bottom of form respawn it in the top
            RespawnBasesCheck();

            // Move down the character if he isn't jumping
            if (jump == false)
                pbPersonaggio.Top += 10;

            // CHANGE OF THE BACKGROUND
            // If the points are between 10000 and 30000 set clouds background (if not set before)
            if (_points >= 10000 && _points < 35000 && _actualBackground != eBackgrounds.clouds)
                setCloudsBackground();
            // Else if the points are more that 35000 set space background (if not set before)
            else if (_points >= 35000 && _actualBackground != eBackgrounds.space)
                setSpaceBackground();

            // VERIFY IF THE PLAYER HAS LOST
            if (pbPersonaggio.Bottom > BOTTOMSCREEN)
            {
                // Save the points in the file
                savePointToClassification();
                // Start the death animation
                for (int i = 0; i < _arrayDeath.Length - 1; i++)
                {
                    // Change the frame
                    pbPersonaggio.Image = _arrayDeath[i];
                    // TODO: Wait before changing frame
                    //System.Threading.Thread.Sleep(208);
                }

                // When it finishes do the other actions
                // Open the classification
                FrmClassifica frmclassifica = new FrmClassifica();
                frmclassifica.Show();
                // Close this form
                this.Close();
            }
            
            // POINTS
            // Add 1 point to the points    
            increasePoints(1);
        }
        
        // ADD TEMPORARY PICTUREBOXES TO THE CONTROLS - STATUS: OK
        private void StartBackground()
        {
            // Add the temporary PictureBoxes to the controls
            this.Controls.Add(_bgClouds);
            this.Controls.Add(_bgSpace);
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

        // SAVE POINTS
        private void savePointToClassification()
        {
            // Save the points to the file
            using (StreamWriter sw = File.AppendText(@"..\classification.txt"))
                sw.Write("|" + _points + "\r\n");
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

        // SWITCH BACKGROUND - STATUS OK
        /// <summary>
        /// Set clouds background
        /// </summary>
        private void setCloudsBackground()
        {
            // Move down lava
            if (pbLava.Location.Y < 750)
                pbLava.Top += 10;

            // When the change of the platforms backgrounds hasn't finished yet
            if (!_ChangeSuccesful)
            {
                // Cloud background images
                Image _backgroundClouds = Lava_Fall.Properties.Resources.cloudBackground;
                Image _helicopter3 = Properties.Resources.elicottero3;
                Image _helicopter2 = Properties.Resources.elicottero2;
                Image _helicopter1 = Properties.Resources.elicottero1;

                // Save the y of the background
                int backgroundLocationY = _bgClouds.Location.Y;
                // Change the background of a platform only if it hasn't changed before, if the new background position has passed the center of the screen and if the base has passed the bottom of the form
                // Check for first platform
                if (!_ChangePlatForm1 && pbBase1.Location.Y > BOTTOMSCREEN && backgroundLocationY > MIDDLESCREEN)
                {
                    // Change base1 dimensions and image
                    changeBaseBackground(pbBase1, _helicopter3, _backgroundClouds);
                    // Confirm the change
                    _ChangePlatForm1 = true;
                }
                // Check for second platform
                if (!_ChangePlatForm2 && pbBase2.Location.Y > BOTTOMSCREEN && backgroundLocationY > MIDDLESCREEN)
                {
                    // Change base2 dimensions and image
                    changeBaseBackground(pbBase2, _helicopter2, _backgroundClouds);
                    // Confirm the change
                    _ChangePlatForm2 = true;
                }
                // Check for third platform
                if (!_ChangePlatForm3 && pbBase3.Location.Y > BOTTOMSCREEN && backgroundLocationY > MIDDLESCREEN)
                {
                    // Change base3 dimensions and image
                    changeBaseBackground(pbBase3, _helicopter1, _backgroundClouds);
                    // Confirm the change
                    _ChangePlatForm3 = true;
                }
                // Check for fourth platform
                if (pbBase4.Location.Y > BOTTOMSCREEN && _ChangePlatForm4 == false && backgroundLocationY > MIDDLESCREEN)
                {
                    // Change base4 dimensions and image
                    changeBaseBackground(pbBase4, _helicopter3, _backgroundClouds);
                    _ChangePlatForm4 = true;
                }
                // Check if the change of platforms background has finished
                if (_ChangePlatForm1 && _ChangePlatForm2 && _ChangePlatForm3 && _ChangePlatForm4)
                    _ChangeSuccesful = true;
            }
            // When the change of the platforms backgrounds finishes
            else
            {
                // Reset all the background change variables
                _ChangePlatForm4 = false;
                _ChangePlatForm3 = false;
                _ChangePlatForm2 = false;
                _ChangePlatForm1 = false;
                _ChangeSuccesful = false;

                // Set the cloud background to the form
                this.BackgroundImage = Properties.Resources.cloudBackground;
                // Hide the clouds temporary PictureBox
                _bgClouds.Visible = false;

                // Remove the temporary bases background
                pbBase1.BackgroundImage = null;
                pbBase2.BackgroundImage = null;
                pbBase3.BackgroundImage = null;
                pbBase4.BackgroundImage = null;

                _actualBackground = eBackgrounds.clouds;
            }

            // Move down clouds PictureBox temporary background
            _bgClouds.Top += 10;
        }

        /// <summary>
        /// Sets space background
        /// </summary>
        private void setSpaceBackground()
        {
            // When the change of the platforms backgrounds hasn't finished yet
            if (!_ChangeSuccesful)
            {
                // Space background images
                Image _backgroundSpace = Lava_Fall.Properties.Resources.spaceBackground;
                Image spacecraft1 = Properties.Resources.spaceship1;
                Image spacecraft2 = Properties.Resources.spaceship2;
                Image spacecraft3 = Properties.Resources.spaceship3;

                // Save the y of the background
                int backgroundLocationY = _bgSpace.Location.Y;

                // Change the background of a platform only if it hasn't changed before, if the new background position has passed the center of the screen and if the base has passed the bottom of the form
                // Check for first platform
                if (!_ChangePlatForm1 && pbBase1.Location.Y > BOTTOMSCREEN && backgroundLocationY > MIDDLESCREEN)
                {
                    // Change base1 dimensions and image
                    changeBaseBackground(pbBase1, spacecraft3, _backgroundSpace);
                    // Confirm the change
                    _ChangePlatForm1 = true;
                }
                // Check for second platform
                if (!_ChangePlatForm2 && pbBase2.Location.Y > BOTTOMSCREEN && backgroundLocationY > MIDDLESCREEN)
                {
                    // Change base2 dimensions and image
                    changeBaseBackground(pbBase2, spacecraft2, _backgroundSpace);
                    // Confirm the change
                    _ChangePlatForm2 = true;
                }
                // Check for third platform
                if (!_ChangePlatForm3 && pbBase3.Location.Y > BOTTOMSCREEN && backgroundLocationY > MIDDLESCREEN)
                {
                    // Change base3 dimensions and image
                    changeBaseBackground(pbBase3, spacecraft1, _backgroundSpace);
                    // Confirm the change
                    _ChangePlatForm3 = true;
                }
                // Check for fourth platform
                if (!_ChangePlatForm4 && pbBase4.Location.Y > BOTTOMSCREEN && backgroundLocationY > MIDDLESCREEN)
                {
                    // Change base4 dimensions and image
                    changeBaseBackground(pbBase4, spacecraft3, _backgroundSpace);
                    // Confirm the change
                    _ChangePlatForm4 = true;
                }
                // Check if the change of platforms background has finished
                if (_ChangePlatForm1 && _ChangePlatForm2 && _ChangePlatForm3 && _ChangePlatForm4)
                    _ChangeSuccesful = true;
            }
            else
            {
                // Reset all the background change variables
                _ChangePlatForm4 = false;
                _ChangePlatForm3 = false;
                _ChangePlatForm2 = false;
                _ChangePlatForm1 = false;
                _ChangeSuccesful = false;

                // Set the space background to the form
                this.BackgroundImage = Properties.Resources.spaceBackground;
                // Hide the space temporary PictureBox
                _bgClouds.Visible = false;

                // Remove the temporary bases background
                pbBase1.BackgroundImage = null;
                pbBase2.BackgroundImage = null;
                pbBase3.BackgroundImage = null;
                pbBase4.BackgroundImage = null;
            }
            // Move down space PictureBox temporary background
            _bgSpace.Top += 10;
        }

        /// <summary>
        /// Changes the background of a base
        /// </summary>
        /// <param name="newBackgroundImage">New background of the base</param>
        private void changeBaseBackground(PictureBox pbDaModificare, Image newBackgroundImage, Image formBackground)
        {
            pbDaModificare.Width = newBackgroundImage.Width;
            pbDaModificare.Height = newBackgroundImage.Height;
            pbDaModificare.Image = newBackgroundImage;
            pbDaModificare.BackgroundImage = formBackground;
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