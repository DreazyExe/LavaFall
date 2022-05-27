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
    public partial class FrmHomePage : Form
    {
        public FrmHomePage()
        {
            InitializeComponent();
        }

        #region Events
        // Button "Play"
        private void pbPlay_Click(object sender, EventArgs e)
        {
            // Save the name of the player in actual match struct
            if (tbName.Text == string.Empty)
                Program._actualMatch.nickname = "Anonimo";
            else
                Program._actualMatch.nickname = tbName.Text;

            // Open the game form
            FormGioco frmGioco = new FormGioco();
            frmGioco.Show();
            // Hide this form
            this.Hide();
        }
        // Help button
        private void pbHelp_Click(object sender, EventArgs e)
        {
            //Quando premi il pulsante play, apri frmHelp.
            FrmHelp frmHelp = new FrmHelp();
            frmHelp.ShowDialog();
        }
        #endregion
    }
}
