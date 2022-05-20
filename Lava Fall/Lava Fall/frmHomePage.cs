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
    public partial class frmHomePage : Form
    {
        #region Procedures
        /// <summary>
        /// Writes something to the classification
        /// </summary>
        /// <param name="name"></param>
        private void writeNameToClassification(string name)
        {
            // Determine the match number
            int matchNumber;
            if (File.Exists(@"..\classification.txt"))
                matchNumber = File.ReadLines(@"..\classification.txt").Count() + 1;
            else
                matchNumber = 1;
            // Add to the classification the name of the player, the number of match and the current date
            using (StreamWriter sw = File.AppendText(@"..\classification.txt"))
                sw.Write(name + "|" + matchNumber + "|" + DateTime.Now);
        }
        #endregion

        public frmHomePage()
        {
            InitializeComponent();
        }

        #region Events
        // Button "Play"
        private void pbPlay_Click(object sender, EventArgs e)
        {
            // Save the name of the player in the file
            if (tbName.Text == null)
                writeNameToClassification("Anonimo");
            else
                writeNameToClassification(tbName.Text);
            
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
            frmHelp frmHelp = new frmHelp();
            frmHelp.ShowDialog();
        }
        #endregion
    }
}
