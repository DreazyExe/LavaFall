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
        //variabili globali
        int i = 1;
        int _punteggio = 0;
        public FormGioco()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void timerLava_Tick(object sender, EventArgs e)
        {
            switch (i)
            {
                case 1:
                    pbLava.Image = Properties.Resources.B;
                    i++;
                    break;

                case 2:
                    pbLava.Image = Properties.Resources.C;
                    i++;
                    break;

                case 3:
                    pbLava.Image = Properties.Resources.D;
                    i++;
                    break;

                case 4:
                    pbLava.Image = Properties.Resources.E;
                    i++;
                    break;

                case 5:
                    pbLava.Image = Properties.Resources.F;
                    i++;
                    break;

                case 6:
                    pbLava.Image = Properties.Resources.G;
                    i++;
                    break;

                case 7:
                    pbLava.Image = Properties.Resources.H;
                    i++;
                    break;

                case 8:
                    pbLava.Image = Properties.Resources.I;
                    i++;
                    break;

                case 9:
                    pbLava.Image = Properties.Resources.J;
                    i++;
                    break;

                case 10:
                    pbLava.Image = Properties.Resources.K;
                    i++;
                    break;

                case 11:
                    pbLava.Image = Properties.Resources.L;
                    i++;
                    break;
                case 12:
                    pbLava.Image = Properties.Resources.A;
                    i = 1;
                    break;
            }
            //string _nomeImmagineLava = Convert.ToString(pbLava.Image);
            //MessageBox.Show(Convert.ToString(_nomeImmagineLava));
            //timerLava.Enabled = false;
        }

        private void punteggio_Tick(object sender, EventArgs e)
        {
            //aumento il punteggio di uno ogni secondo
            _punteggio++;
            lbPunteggio.Text = Convert.ToString(_punteggio);


        }
    }
}
