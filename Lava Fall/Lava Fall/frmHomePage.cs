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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            //Quando premi il pulsante play, apri frmGioco.
            FormGioco frmGioco = new FormGioco();
            frmGioco.Show();

            this.Hide(); //nascondo la homepage

        }
    }
}
