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
    public partial class FrmClassifica : Form
    {
        public FrmClassifica()
        {
            InitializeComponent();
        }
        #region Events
        private void FrmClassifica_Load(object sender, EventArgs e)
        {
            // Open the classification
            StreamReader _classification = new StreamReader(@"..\classification.txt");
            // Read all the lines and add their content to the table
            string _line = _classification.ReadLine();
            while (_line != null)
            {
                // Create a new ListView item
                ListViewItem _newMatchInformation = new ListViewItem();
                // Save all the informations of the match
                _newMatchInformation.SubItems.Clear();
                foreach (string information in _line.Split('|'))
                    _newMatchInformation.SubItems.Add(information);
                // Add this ListView item to the ListView
                lvClassifica.Items.Add(_newMatchInformation);
                // eseguo un nuova lettura
                _line = _classification.ReadLine();
            }
            // chisura del file
            _classification.Close();
        }
        #endregion
    }
}
