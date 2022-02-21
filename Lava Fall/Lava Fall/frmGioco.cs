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
        int _counter = 3;
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
            //BUG: LA SECONDA BASE NON RITORNA SOPRA
            //Dichiaro una variabile per l'ultima base che si è spostata. Inizialmente il suo valore è 2 perché non esiste una base precedente.
            char ultimaBase = '2';
            //Dichiaro una variabile per generare numeri casuali
            Random rnd = new Random();

            //Lo schermo è compreso tra 0 e 600



            //spostamento





            ////Spostamento prima base
            //if (pbBase1.Location.Y >= 710)
            //{
            //    //Istruzioni se la base ha raggiunto la parte più bassa della form e deve essere spostata in alto.
            //    switch (ultimaBase)
            //    {
            //        //Istruzioni se la base precedente era la seconda
            //        case '2':
            //            //Porto la base ad un punto x distante almeno di 20 px rispetto alla base precedente (sia a destra che a sinistra) ed ad un punto y sopra la form.
            //            pbBase1.Location = new Point(rnd.Next(rnd.Next(0, pbBase2.Location.X - 20), rnd.Next(pbBase2.Location.X + 80, 600)), 0 - pbBase1.Size.Height);
            //            break;

            //        //Istruzioni se la base precedente era la terza
            //        case '3':
            //            //Porto la base ad un punto x distante almeno di 20 px rispetto alla base precedente (sia a destra che a sinistra) ed ad un punto y sopra la form.
            //            pbBase1.Location = new Point(rnd.Next(rnd.Next(0, pbBase3.Location.X - 20), rnd.Next(pbBase3.Location.X + 80, 600)), 0 - pbBase1.Size.Height);
            //            break;
            //    }
            //    //Memorizzo il fatto che l'ultima base modificata è stata la prima
            //    ultimaBase = '1';
            //}
            //else
            //{
            //    //Se la base non ha raggiunto la parte più bassa della form, spostarla verso il basso di 10 px.
            //    pbBase1.Location = new Point(pbBase1.Location.X, pbBase1.Location.Y + 10);
            //}

            ////Spostamento seconda base
            //if (pbBase2.Location.Y >= 710)
            //{
            //    //Istruzioni se la base ha raggiunto la parte più bassa della form e deve essere spostata in alto.
            //    switch (ultimaBase)
            //    {
            //        //Istruzioni se la base precedente era la seconda
            //        case '1':
            //            //Porto la base ad un punto x distante almeno di 20 px rispetto alla base precedente (sia a destra che a sinistra) ed ad un punto y sopra la form.
            //            pbBase2.Location = new Point(rnd.Next(rnd.Next(0, pbBase1.Location.X - 20), rnd.Next(pbBase1.Location.X + 80, 600)), 0 - pbBase2.Size.Height);
            //            break;

            //        //Istruzioni se la base precedente era la terza
            //        case '3':
            //            //Porto la base ad un punto x distante almeno di 20 px rispetto alla base precedente (sia a destra che a sinistra) ed ad un punto y sopra la form.
            //            pbBase2.Location = new Point(rnd.Next(rnd.Next(0, pbBase3.Location.X - 20), rnd.Next(pbBase3.Location.X + 80, 600)), 0 - pbBase2.Size.Height);
            //            break;
            //    }
            //    //Memorizzo il fatto che l'ultima base modificata è stata la prima
            //    ultimaBase = '2';
            //}
            //else
            //{
            //    //Se la base non ha raggiunto la parte più bassa della form, spostarla verso il basso di 10 px.
            //    pbBase2.Location = new Point(pbBase2.Location.X, pbBase2.Location.Y + 10);
            //}

            ////Spostamento terza base
            //if (pbBase3.Location.Y >= 710)
            //{
            //    //Istruzioni se la base ha raggiunto la parte più bassa della form e deve essere spostata in alto.
            //    switch (ultimaBase)
            //    {
            //        //Istruzioni se la base precedente era la seconda
            //        case '1':
            //            //Porto la base ad un punto x distante almeno di 20 px rispetto alla base precedente (sia a destra che a sinistra) ed ad un punto y sopra la form.
            //            pbBase3.Location = new Point(rnd.Next(rnd.Next(0, pbBase1.Location.X - 20), rnd.Next(pbBase1.Location.X + 80, 600)), 0 - pbBase3.Size.Height);
            //            break;

            //        //Istruzioni se la base precedente era la terza
            //        case '2':
            //            //Porto la base ad un punto x distante almeno di 20 px rispetto alla base precedente (sia a destra che a sinistra) ed ad un punto y sopra la form.
            //            pbBase3.Location = new Point(rnd.Next(rnd.Next(0, pbBase2.Location.X - 20), rnd.Next(pbBase2.Location.X + 80, 600)), 0 - pbBase3.Size.Height);
            //            break;
            //    }
            //    //Memorizzo il fatto che l'ultima base modificata è stata la prima
            //    ultimaBase = '3';
            //}
            //else
            //{
            //    //Se la base non ha raggiunto la parte più bassa della form, spostarla verso il basso di 10 px.
            //    pbBase3.Location = new Point(pbBase3.Location.X, pbBase3.Location.Y + 10);
            //}


        }

        private void pbBase1_Click(object sender, EventArgs e)
        {

        }

        private void FormGioco_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void countDown_Tick(object sender, EventArgs e)
        {
            //Se i secondi di attesa iniziale sono finiti
            if (_counter == 0)
            {
                //Attivo i timer di movimento
                punteggio.Enabled = true;
                spostamento_basi.Enabled = true;

                //Disattiva il timer
                this.Enabled = false;

                //Rimuovi il counter
                lblCountDown.Visible = false;
            }
            else
            {
                //Aggiorno il counter
                _counter--;

                if (_counter == 0)
                    //Aggiorno la label dei secondi rimasti
                    lblCountDown.Text = "Via!";
                else
                    lblCountDown.Text = _counter.ToString();
            }
        }
    }
}
