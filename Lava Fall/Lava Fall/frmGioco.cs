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
        int i = 0;  //Counter of the image
        System.Drawing.Image[] arrayLava = 
        {
            Properties.Resources.B, Properties.Resources.C, Properties.Resources.D,
            Properties.Resources.E, Properties.Resources.F, Properties.Resources.G,
            Properties.Resources.H, Properties.Resources.I, Properties.Resources.J,
            Properties.Resources.K, Properties.Resources.L, Properties.Resources.A
        };  //Array of lava images

        // State of the game variable
        Program.eGameState _stateGame;

        // Actual background variable
        Program.eBackgrounds _actualBackground = Program.eBackgrounds.lava;

        // Background change variables
        bool _ChangeSuccesful = false;      // State of the change
        bool _ChangePlatForm1 = false;      // Was platform 1 changed?
        bool _ChangePlatForm2 = false;      // Was platform 2 changed?
        bool _ChangePlatForm3 = false;      // Was platform 3 changed?
        bool _ChangePlatForm4 = false;      // Was platform 4 changed?
        // Background to set
        Image _newBackgroundToSet;
        // PictureBox of gradual background changing
        PictureBox pbBackgroundChanger = new PictureBox
        {
            Name = "pbBackgroundChanger",
            Size = new Size(810, 750),
            Location = new Point(0, TOPSCREEN),
        };
        #endregion

        #region Form builder
        public FormGioco()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            // when the program start the game is suspended
            _stateGame = Program.eGameState.suspended;
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
                _stateGame = Program.eGameState.atstake;

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
            if (_stateGame == Program.eGameState.atstake)
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

            // CHANGE OF THE BACKGROUND
            // If the points are between 10000 and 30000 set clouds background (if not set before)
            if (Program._points >= 10000 && Program._points < 35000)
            {
                // Start the background change
                timerChangeBackground.Enabled = true;
                // Indicate the background to set
                _newBackgroundToSet = Properties.Resources.cloudBackground;
                pbBackgroundChanger.Image = Properties.Resources.cloudBackground;
                // Make the game faster
                gameClock.Interval = 45;
            }                
            // Else if the points are more that 35000 set space background (if not set before)
            else if (Program._points >= 35000)
            {
                setSpaceBackground();
                gameClock.Interval = 35;
            }
                
            // VERIFY IF THE PLAYER HAS LOST
            if (pbPersonaggio.Bottom > BOTTOMSCREEN)
            {
                // Save the points in the file
                savePointToClassification();
                // Open the classification
                FrmClassifica frmclassifica = new FrmClassifica();
                frmclassifica.Show();
                // Close this form
                this.Close();
            }
            
            // POINTS
            // Add 1 point to the points    
            increasePoints(50);
        }

        // JUMP OF THE CHARACTER - STATUS: OK
        private void characterJump_Tick(object sender, EventArgs e)
        {
            // Verify if the character has to jump or descent
            if (_actualForce > 0 && _jump)
                // If he has to jump start the jump
                jump();
            else if (_descent)
                // If he has to descent start the descent
                startCharacterDescent();
        }

        // GRAVITY OF THE CHARACTER - STATUS: OK
        private void gravity_Tick(object sender, EventArgs e)
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

        // ADD TEMPORARY PICTUREBOXES TO THE CONTROLS - STATUS: OK
        private void StartBackground()
        {
            // Add the temporary PictureBoxes to the controls
            this.Controls.Add(pbBackgroundChanger);
        }

        // MOVE OF THE CHARACTER (EVENT IF A KEY IS PRESSED AND LEFT-RIGHT MOVEMENT) - STATUS: OK
        /// <summary>
        /// Event if the user presses a key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="key"></param>
        private void frmGioco_KeyDown(object sender, KeyEventArgs key)
        {
            // if the game is not at stake you can't move
            if (_stateGame == Program.eGameState.atstake)
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
            if (Program._points > 10000 && Program._points < 22000)
            {
                StartBackground();
            }

            // Add the quantity to the total points
            Program._points += quantityToAdd;

            // Refresh of the points label
            lblPunteggio.Text = Program._points.ToString();
        }

        // SAVE POINTS
        private void savePointToClassification()
        {
            // Save the points to the file
            using (StreamWriter sw = File.AppendText(@"..\classification.txt"))
                sw.Write("|" + Program._points + "\r\n");
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

        // SWITCH BACKGROUND - STATUS CREARE PROCEDURE
        /// <summary>
        /// Set clouds background
        /// </summary>
        private void changeTheme(Image bgBase1, Image bgBase2, Image bgBase3, Image bgToSet)
        {
            // When the change of the platforms backgrounds hasn't finished yet
            if (!_ChangeSuccesful)
            {
                pbBackgroundChanger.Top += 10;
                // Save the y of the background
                int backgroundLocationY = pbBackgroundChanger.Location.Y;
                // Change the background of a platform only if it hasn't changed before, if the new background position has passed the center of the screen and if the base has passed the bottom of the form
                // Check for first platform
                if (!_ChangePlatForm1 && pbBase1.Location.Y > BOTTOMSCREEN && backgroundLocationY > MIDDLESCREEN)
                {
                    // Change base1 dimensions and image
                    changeBaseBackground(pbBase1, bgBase1, bgToSet);
                    // Confirm the change
                    _ChangePlatForm1 = true;
                }
                // Check for second platform
                if (!_ChangePlatForm2 && pbBase2.Location.Y > BOTTOMSCREEN && backgroundLocationY > MIDDLESCREEN)
                {
                    // Change base2 dimensions and image
                    changeBaseBackground(pbBase2, bgBase2, bgToSet);
                    // Confirm the change
                    _ChangePlatForm2 = true;
                }
                // Check for third platform
                if (!_ChangePlatForm3 && pbBase3.Location.Y > BOTTOMSCREEN && backgroundLocationY > MIDDLESCREEN)
                {
                    // Change base3 dimensions and image
                    changeBaseBackground(pbBase3, bgBase3, bgToSet);
                    // Confirm the change
                    _ChangePlatForm3 = true;
                }
                // Check for fourth platform
                if (pbBase4.Location.Y > BOTTOMSCREEN && _ChangePlatForm4 == false && backgroundLocationY > MIDDLESCREEN)
                {
                    // Change base4 dimensions and image
                    changeBaseBackground(pbBase4, bgBase3, bgToSet);
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
                pbBackgroundChanger.Visible = false;

                // Remove the temporary bases background
                pbBase1.BackgroundImage = null;
                pbBase2.BackgroundImage = null;
                pbBase3.BackgroundImage = null;
                pbBase4.BackgroundImage = null;

                _actualBackground = Program.eBackgrounds.clouds;
            }

        }

        /// <summary>
        /// Set space background
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
                int backgroundLocationY = pbBackgroundChanger.Location.Y;

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
                pbBackgroundChanger.Visible = false;

                // Remove the temporary bases background
                pbBase1.BackgroundImage = null;
                pbBase2.BackgroundImage = null;
                pbBase3.BackgroundImage = null;
                pbBase4.BackgroundImage = null;
            }
            // Move down space PictureBox temporary background
            pbBackgroundChanger.Top += 10;
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

        // MOVE OF THE CHARACTER (JUMP AND DESCENT) - STATUS: OK
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

        private void timerChangeBackground_Tick(object sender, EventArgs e)
        {
            // Verify if the background to set is clouds
            if(_newBackgroundToSet == Properties.Resources.cloudBackground)
            {
                // Verify if the background hasn't finished to change
                if (pbBackgroundChanger.Bottom < this.Height + (this.ClientRectangle.Height - this.Height))
                {
                    // Move down lava
                    if (pbLava.Location.Y < 750)
                        pbLava.Top += 10;
                    // Move the temporary background changer PictureBox and change platforms
                    changeTheme(Properties.Resources.elicottero1, Properties.Resources.elicottero2, Properties.Resources.elicottero3, Properties.Resources.cloudBackground);
                }
                    
                // Else if the change finished
                else
                {
                    this.BackgroundImage = Properties.Resources.cloudBackground;
                    pbBackgroundChanger.Visible = false;
                    timerChangeBackground.Enabled = false;
                    timerLava.Enabled = false;
                }
            }
            
        }
    }
}