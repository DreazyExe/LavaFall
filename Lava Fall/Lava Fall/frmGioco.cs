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
            //ANIMAZIONE LAVA
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
            
        }

        private void punteggio_Tick(object sender, EventArgs e)
        {
            //aumento il punteggio di uno ogni secondo
            _punteggio++;
            lbPunteggio.Text = Convert.ToString(_punteggio);


        }

        private void spostamento_basi_Tick(object sender, EventArgs e)
        {
            char ultimaBase = 'B';

            //Random r = new Random();
            //int rInt = r.Next(0, 100); //for ints
            //int range = 100;
            //double rDouble = r.NextDouble() * range; //for doubles

            //Lo schermo è compreso tra 0 e 600

            //I tre range della generazione del numero sono

            //1) Da 0 alla prima x più bassa diminuita di 60
            //2) Dalla prima x più bassa aumentata di 60 alla prossima x diminuita di 60
            //3) Dalla seconda x aumentata di 60 a 540 (fine schermo diminuito di 60)

            //Spostamento prima base
            if (pbBase1.Location.Y >= 710)
            {
                Random rnd = new Random();
                switch (ultimaBase)
                {
                    //Istruzioni se la base precedente era la seconda
                    case 'B':
                        pbBase1.Location = new Point(rnd.Next(rnd.Next(0, , 600), 0 - pbBase1.Size.Height);
                        break;
                }
                
                pbBase1.Location = new Point(rnd.Next(rnd.Next(0, , 600), 0 - pbBase1.Size.Height);
                ultimaBase = '1';
            }
            else
            {
                pbBase1.Location = new Point(pbBase1.Location.X, pbBase1.Location.Y + 10);
            }

            //Spostamento seconda base
            if (pbBase2.Location.Y >= 710)
            {
                Random rnd = new Random();
                pbBase2.Location = new Point(rnd.Next(0, 600), 0 - pbBase2.Size.Height);
                ultimaBase = '2';
            }
            else
            {
                pbBase2.Location = new Point(pbBase2.Location.X, pbBase2.Location.Y + 10);
            }

            //Spostamento terzo base
            if (pbBase3.Location.Y >= 710)
            {
                Random rnd = new Random();
                pbBase3.Location = new Point(rnd.Next(0, 600), 0 - pbBase3.Size.Height);
                ultimaBase = '3';
            }
            else
            {
                pbBase3.Location = new Point(pbBase3.Location.X, pbBase3.Location.Y + 10);
            }


        }
    }
}
