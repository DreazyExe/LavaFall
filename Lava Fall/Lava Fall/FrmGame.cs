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
        #region Global costants
        // Movement costants
        const int MAXRIGHTFORMPOSITION = 650;
        const int MAXLEFTFORMPOSITION = 0;
        const int CHARACTERWIDTH = 145;

        // Jump costants
        const int STARTINGFORCE = 60;
        const int MOVEMENT = 10;

        // Change background
        const int BACKGROUNDMOVEMENT = 10;
        const int TOPSCREEN = -750;
        const int MIDDLESCREEN = -550;
        const int BOTTOMSCREEN = 657;
        #endregion

        #region Global variables
        // Starting count variable
        int _counter = 3;

        // Jump variables
        int _actualForce;
        bool _jump = false;
        bool _descent = false;

        // Bases movement variables
        int[] defaultXPositions = { 10, 220, 430, 650 };    // Array of possible position of objects (position x)
        int _lastRandomNumber;                              // Positions of the last respawned platform

        // Lava movement variables
        int _lavaIndicator = 0;  //Counter of the image
        System.Drawing.Image[] arrayLava = 
        {
            Properties.Resources.B, Properties.Resources.C, Properties.Resources.D,
            Properties.Resources.E, Properties.Resources.F, Properties.Resources.G,
            Properties.Resources.H, Properties.Resources.I, Properties.Resources.J,
            Properties.Resources.K, Properties.Resources.L, Properties.Resources.A
        };  //Array of lava images

        // Background change variables
        bool _ChangePlatForm1 = false;      // Was platform 1 changed?
        bool _ChangePlatForm2 = false;      // Was platform 2 changed?
        bool _ChangePlatForm3 = false;      // Was platform 3 changed?
        bool _ChangePlatForm4 = false;      // Was platform 4 changed?
        // Background to set
        Program.eBackgrounds _newBackgroundToSet;
        // PictureBox of gradual background changing
        PictureBox pbBackgroundChanger = new PictureBox
        {
            Name = "pbBackgroundChanger",
            Size = new Size(810, 750),
            Location = new Point(0, TOPSCREEN),
        };

        // Character death variables
        int _deathIndicator = 0;
        System.Drawing.Image[] arrayDeath =
        {
            Properties.Resources.death1, Properties.Resources.death2, Properties.Resources.death3,
            Properties.Resources.death4, Properties.Resources.death5, Properties.Resources.death6,
            Properties.Resources.death7, Properties.Resources.death8, Properties.Resources.death9,
            Properties.Resources.death10, Properties.Resources.death11, Properties.Resources.death12
        };  //Array of character death image

        // Actaul background variable
        Program.eBackgrounds _actualBackground = Program.eBackgrounds.lava;
        #endregion

        #region Form builder
        public FormGioco()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            // when the program start the game is suspended
            Program._stateGame = Program.eGameState.suspended;
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
                Program._stateGame = Program.eGameState.atstake;

                // Write "Go!" on the label of the countdown to indicate the beginning of the game
                lblCountDown.Text = "Go!";

                // Enable movement timer
                gameClock.Enabled = true;

                // Disable countdown timer
                countDown.Enabled = false;

                // Hide counter label
                lblCountDown.Visible = false;
            }
            // If the countdown hasn't finished yet
            else
                // Refresh counter label
                lblCountDown.Text = _counter.ToString();
        }

        // MOVEMENT OF THE LAVA
        private void timerLava_Tick(object sender, EventArgs e)
        {
            // Verify if the game is at strake
            if (Program._stateGame == Program.eGameState.atstake)
            {
                // Set the new frame of the lava
                pbLava.Image = arrayLava[_lavaIndicator];

                // Increase the indicator of frame
                if (_lavaIndicator < arrayLava.Length - 1)
                    _lavaIndicator++;
                // If frames are finished restart the animation
                else
                    _lavaIndicator = 0;
            }
        }

        // MOVE OF THE BASIS
        private void gameClock_Tick(object sender, EventArgs e)
        {
            // Verify if the game is at strake
            if(Program._stateGame == Program.eGameState.atstake)
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

                // CHANGE OF THE BACKGROUND
                // If the points are between 10000 and 30000 set clouds background (if not set before)
                if (Program._points >= 10000 && Program._points < 35000 && _actualBackground != Program.eBackgrounds.clouds)
                {
                    // Add the temporary background changer to the controls, make it visible and set its image
                    this.Controls.Add(pbBackgroundChanger);
                    pbBackgroundChanger.Image = Properties.Resources.cloudBackground;
                    pbBackgroundChanger.Visible = true;
                    // Indicate the background to set
                    _newBackgroundToSet = Program.eBackgrounds.clouds;
                    // Enable the change background timer
                    timerChangeBackground.Enabled = true;
                    // Modify game speed
                    gameClock.Interval = 200;
                }
                // Else if the points are more that 35000 set space background (if not set before)
                else if (Program._points >= 35000 && _actualBackground != Program.eBackgrounds.space)
                {
                    // Make the background changer visible and set its image
                    pbBackgroundChanger.Image = Properties.Resources.spaceBackground;
                    pbBackgroundChanger.Visible = true;
                    // Indicate the background to set
                    _newBackgroundToSet = Program.eBackgrounds.space;
                    // Enable the change background timer
                    timerChangeBackground.Enabled = true;
                    // Modify game speed
                    gameClock.Interval = 100;
                }

                // VERIFY IF THE PLAYER HAS LOST
                if (pbPersonaggio.Bottom > BOTTOMSCREEN)
                {
                    // Set the state of the game to lost
                    Program._stateGame = Program.eGameState.lost;
                    // Start the death of the character
                    timercharacterDeath.Enabled = true;
                }

                // POINTS
                // Add 1 point to the points    
                increasePoints(50);
            }
        }

        // JUMP OF THE CHARACTER
        private void characterJump_Tick(object sender, EventArgs e)
        {
            // Verify if the game is at strake
            if(Program._stateGame == Program.eGameState.atstake)
            {
                // Verify if the character has to jump or descent
                if (_actualForce > 0 && _jump)
                    // If he has to jump start the jump
                    jump();
                else if (_descent)
                    // If he has to descent start the descent
                    startCharacterDescent();
            }
        }

        // GRAVITY OF THE CHARACTER
        private void gravity_Tick(object sender, EventArgs e)
        {
            // Verify if the game is at strake
            if(Program._stateGame == Program.eGameState.atstake)
            {
                // If the character is not on a platform start his descent (for the gravity)
                if (!pbPersonaggio.Bounds.IntersectsWith(pbBase1.Bounds) && !pbPersonaggio.Bounds.IntersectsWith(pbBase2.Bounds) && !pbPersonaggio.Bounds.IntersectsWith(pbBase3.Bounds) && !pbPersonaggio.Bounds.IntersectsWith(pbBase4.Bounds) && !pbPersonaggio.Bounds.IntersectsWith(pbBasePrincipale.Bounds) && !_jump && !_descent)
                {
                    // Set the actual force of the character
                    _actualForce = 10;
                    // Start his descent
                    startCharacterDescent();
                }
            }
        }
        
        // MOVE OF THE CHARACTER (EVENT IF A KEY IS PRESSED AND LEFT-RIGHT MOVEMENT)
        private void frmGioco_KeyDown(object sender, KeyEventArgs key)
        {
            // Disable Windows error sound when pressing a key
            key.SuppressKeyPress = true;
            // Verify if the game is at strake
            if (Program._stateGame == Program.eGameState.atstake)
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
                        if (!_jump && !_descent)
                        {
                            // Set the starting force of the jump
                            _actualForce = STARTINGFORCE;
                            // Indicate that the character is jumping
                            _jump = true;
                            // Enable the jump
                            characterJump.Enabled = true;
                        }   
                        break;
                    // Istructions if the key is "P"
                    case Keys.P:
                        // Set the game state to suspended
                        Program._stateGame = Program.eGameState.suspended;
                        // Open the pause menu
                        FrmPause frmPausa = new FrmPause();
                        DialogResult dr = frmPausa.ShowDialog();
                        if(dr == DialogResult.OK)
                        {
                            // Enable the timer to continue the game
                            _counter = 3;
                            lblCountDown.Text = "3";
                            lblCountDown.Visible = true;
                            countDown.Enabled = true;
                        }
                        else
                        {
                            // Save the points and the date in the file
                            Program._actualMatch.points = Program._points;
                            Program._actualMatch.date = DateTime.Now.ToString();
                            // Open the classification
                            FrmClassification frmclassifica = new FrmClassification();
                            frmclassifica.Show();
                            // Close this form
                            this.Close();
                        }
                        break;
                }
            }
        }

        // CHANGE OF BASES
        private void timerChangeBackground_Tick(object sender, EventArgs e)
        {
            // Verify if the game is at strake
            if(Program._stateGame == Program.eGameState.atstake)
            {
                // Verify it the background to set is "clouds"
                if (_newBackgroundToSet == Program.eBackgrounds.clouds)
                {
                    // Move down the background while it doesn't cover the form
                    if (pbBackgroundChanger.Bottom < this.Height + (this.ClientRectangle.Height - this.Height))
                    {
                        pbLava.Top += 10;
                        pbBackgroundChanger.Top += 10;
                    }
                    // When the new background covers the form
                    else
                    {
                        // Change form's background image
                        this.BackgroundImage = Properties.Resources.cloudBackground;
                        // Indicate that the actual background is "clouds"
                        _actualBackground = Program.eBackgrounds.clouds;
                        // Hide background changer PictureBox and bring it to the top of the form
                        pbBackgroundChanger.Visible = false;
                        pbBackgroundChanger.Location = new Point(0, TOPSCREEN);
                        // Disable lava movement timer
                        timerLava.Enabled = false;
                        // Enable the base background change timer
                        timerChangeBaseBackgrounds.Enabled = true;
                        // Disable background change timer
                        timerChangeBackground.Enabled = false;
                    }
                }
                // Else if the background to set is "space"
                else
                {
                    // Move down the background while it doesn't cover the form
                    if (pbBackgroundChanger.Bottom < this.Height + (this.ClientRectangle.Height - this.Height))
                        pbBackgroundChanger.Top += 10;
                    // When the new background covers the form
                    else
                    {
                        // Change form's background image
                        this.BackgroundImage = Properties.Resources.spaceBackground;
                        // Indicate that the actual background is "space"
                        _actualBackground = Program.eBackgrounds.space;
                        // Hide background changer PictureBox and bring it to the top of the form
                        pbBackgroundChanger.Visible = false;
                        pbBackgroundChanger.Location = new Point(0, TOPSCREEN);
                        // Enable the base background change timer
                        timerChangeBaseBackgrounds.Enabled = true;
                        // Disable background change timer
                        timerChangeBackground.Enabled = false;
                    }
                }
            }
        }
        private void timerChangeBaseBackgrounds_Tick(object sender, EventArgs e)
        {
            // Verify if the game is at strake
            if(Program._stateGame == Program.eGameState.atstake)
            {
                // Verify if the actual background is "clouds"
                if (_actualBackground == Program.eBackgrounds.clouds)
                {
                    // Change the background of a platform only if it hasn't changed before, if the new background position has passed the center of the screen and if the base has passed the bottom of the form
                    // Check for first platform
                    if (!_ChangePlatForm1 && pbBase1.Location.Y > BOTTOMSCREEN - 10)
                    {
                        // Change base1 dimensions and image and confirm the change
                        changeBaseBackground(pbBase1, Properties.Resources.elicottero1);
                        _ChangePlatForm1 = true;
                    }
                    // Check for second platform
                    if (!_ChangePlatForm2 && pbBase2.Location.Y > BOTTOMSCREEN - 10)
                    {
                        // Change base2 dimensions and image and confirm the change
                        changeBaseBackground(pbBase2, Properties.Resources.elicottero2);
                        _ChangePlatForm2 = true;
                    }
                    // Check for third platform
                    if (!_ChangePlatForm3 && pbBase3.Location.Y > BOTTOMSCREEN - 10)
                    {
                        // Change base3 dimensions and image and confirm the change
                        changeBaseBackground(pbBase3, Properties.Resources.elicottero3);
                        _ChangePlatForm3 = true;
                    }
                    // Check for fourth platform
                    if (!_ChangePlatForm4 && pbBase4.Location.Y > BOTTOMSCREEN - 10)
                    {
                        // Change base4 dimensions and image and confirm the change
                        changeBaseBackground(pbBase4, Properties.Resources.elicottero1);
                        _ChangePlatForm4 = true;
                    }
                    // If the change of platforms background has finished stop this timer and reset variables
                    if (_ChangePlatForm1 && _ChangePlatForm2 && _ChangePlatForm3 && _ChangePlatForm4)
                    {
                        timerChangeBaseBackgrounds.Enabled = false;
                        _ChangePlatForm1 = false;
                        _ChangePlatForm2 = false;
                        _ChangePlatForm3 = false;
                        _ChangePlatForm4 = false;
                    }

                }
                // Else if the actual background is "space"
                else
                {
                    // Change the background of a platform only if it hasn't changed before, if the new background position has passed the center of the screen and if the base has passed the bottom of the form
                    // Check for first platform
                    if (!_ChangePlatForm1 && pbBase1.Location.Y > BOTTOMSCREEN - 10)
                    {
                        // Change base1 dimensions and image and confirm the change
                        changeBaseBackground(pbBase1, Properties.Resources.spaceship1);
                        _ChangePlatForm1 = true;
                    }
                    // Check for second platform
                    if (!_ChangePlatForm2 && pbBase2.Location.Y > BOTTOMSCREEN - 10)
                    {
                        // Change base2 dimensions and image and confirm the change
                        changeBaseBackground(pbBase2, Properties.Resources.spaceship2);
                        _ChangePlatForm2 = true;
                    }
                    // Check for third platform
                    if (!_ChangePlatForm3 && pbBase3.Location.Y > BOTTOMSCREEN - 10)
                    {
                        // Change base3 dimensions and image and confirm the change
                        changeBaseBackground(pbBase3, Properties.Resources.spaceship3);
                        _ChangePlatForm3 = true;
                    }
                    // Check for fourth platform
                    if (!_ChangePlatForm4 && pbBase4.Location.Y > BOTTOMSCREEN - 10)
                    {
                        // Change base4 dimensions and image and confirm the change
                        changeBaseBackground(pbBase4, Properties.Resources.spaceship1);
                        _ChangePlatForm4 = true;
                    }
                    // If the change of platforms background has finished stop this timer and reset variables
                    if (_ChangePlatForm1 && _ChangePlatForm2 && _ChangePlatForm3 && _ChangePlatForm4)
                    {
                        timerChangeBaseBackgrounds.Enabled = false;
                        _ChangePlatForm1 = false;
                        _ChangePlatForm2 = false;
                        _ChangePlatForm3 = false;
                        _ChangePlatForm4 = false;
                    }
                }
            }
        }

        // DEATH OF THE CHARACTER
        private void timercharacterDeath_Tick(object sender, EventArgs e)
        {
            // Verify if the game is lost
            if (Program._stateGame == Program.eGameState.lost)
            {
                // Set the new frame of the lava
                pbPersonaggio.Image = arrayDeath[_deathIndicator];

                // Increase the indicator of frame
                if (_deathIndicator < arrayDeath.Length - 1)
                    _deathIndicator++;
                // If frames are finished do the other lost actions
                else
                {
                    // Save the points and the date in the file
                    Program._actualMatch.points = Program._points;
                    Program._actualMatch.date = DateTime.Now.ToString();
                    // Open the classification
                    FrmClassification frmclassifica = new FrmClassification();
                    frmclassifica.Show();
                    // Close this form
                    this.Close();
                }
            }
        }
        #endregion

        #region Functions and procedures
        // INCREASE POINTS
        /// <summary>
        /// Increases score of a set quantity
        /// </summary>
        /// <param name="quantityToAdd"> Quantity to add to the points.</param>
        private void increasePoints(byte quantityToAdd)
        {
            // Add the quantity to the total points
            Program._points += quantityToAdd;

            // Refresh of the points label
            lblPunteggio.Text = Program._points.ToString();
        }

        // RESPAWN BASES
        /// <summary>
        /// Controls if one of the bases reached lava and, if true, brings it to the top of the form
        /// </summary>
        private void RespawnBasesCheck()
        {
            // If the first base reached lava respawn it
            if (pbBase1.Top > BOTTOMSCREEN)
                respawnObject(pbBase1);
            // If the second base reached lava respawn it
            else if (pbBase2.Top > BOTTOMSCREEN)
                respawnObject(pbBase2);
            // If the third base reached lava respawn it
            else if (pbBase3.Top > BOTTOMSCREEN)
                respawnObject(pbBase3);
            // If the fourth base reached lava respawn it
            else if (pbBase4.Top > BOTTOMSCREEN)
                respawnObject(pbBase4);
            // If no bases reached lava exit
            else
                return;
        }

        // RESPAWN OBJECTS
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

        /// <summary>
        /// Changes the background of a base
        /// </summary>
        /// <param name="newBackgroundImage">New background of the base</param>
        private void changeBaseBackground(PictureBox pbDaModificare, Image newBackgroundImage)
        {
            pbDaModificare.Width = newBackgroundImage.Width;
            pbDaModificare.Height = newBackgroundImage.Height;
            pbDaModificare.Image = newBackgroundImage;
        }

        // MOVE OF THE CHARACTER (JUMP AND DESCENT)
        /// <summary>
        /// Makes the character jump
        /// </summary>
        private void jump()
        {
            // Move up by as many pixels as the actaul force is
            pbPersonaggio.Top -= _actualForce;
            // Decrease the actual force of the jump
            _actualForce -= MOVEMENT;
            // Disable jump and enable descent if it finished (se la sua forza vale 0)
            if (_actualForce <= 0)
            {
                _jump = false;
                _descent = true;
            }
        }

        /// <summary>
        /// Starts the descent of the character
        /// </summary>
        private void startCharacterDescent()
        {
            // If the character touches a base stop his descent
            if (pbPersonaggio.Bounds.IntersectsWith(pbBase1.Bounds))
                stopCharacterDescent(pbBase1);
            else if (pbPersonaggio.Bounds.IntersectsWith(pbBase2.Bounds))
                stopCharacterDescent(pbBase2);
            else if (pbPersonaggio.Bounds.IntersectsWith(pbBase3.Bounds))
                stopCharacterDescent(pbBase3);
            else if (pbPersonaggio.Bounds.IntersectsWith(pbBase4.Bounds))
                stopCharacterDescent(pbBase4);
            else if (pbPersonaggio.Bounds.IntersectsWith(pbBasePrincipale.Bounds))
                stopCharacterDescent(pbBasePrincipale);
            else
            {
                // Increase the actual force of the jump
                _actualForce += MOVEMENT;
                // Move down by as many pixels as the actual force
                pbPersonaggio.Top += _actualForce;
            }
        }

        /// <summary>
        /// Stops the descent of a PictureBox
        /// </summary>
        /// <param name="pbObject"> PictureBox to stop</param>
        private void stopCharacterDescent(PictureBox pbObject)
        {
            // Porta il personaggio esattamente sopra la base
            pbPersonaggio.Location = new Point(pbPersonaggio.Location.X, pbObject.Location.Y - pbPersonaggio.Height + 1);
            // Indica che il personaggio non sta più scendendo
            _descent = false;
            // Termina il salto
            characterJump.Enabled = false;
        }
        #endregion
    }
}