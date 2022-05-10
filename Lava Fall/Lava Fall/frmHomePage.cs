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
    public partial class frmHomePage : Form
    {
        public frmHomePage()
        {
            InitializeComponent();
        }

        #region Eventi
        // Pulsante "Play" - Status: OK
        private void pbPlay_Click(object sender, EventArgs e)
        {
            //Quando premi il pulsante play, apri frmGioco e nascondi la homepage.
            FormGioco frmGioco = new FormGioco();
            frmGioco.Show();
            this.Hide();
        }
        // Pulsante "Help" - Status: OK
        private void pbHelp_Click(object sender, EventArgs e)
        {
            //Quando premi il pulsante play, apri frmHelp.
            frmHelp frmHelp = new frmHelp();
            frmHelp.ShowDialog();
        }
        #endregion
    }
}
