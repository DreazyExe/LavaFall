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
        #region Global variables
        // Array of matches
        List<Program.sMatch> _matches;
        #endregion

        public FrmClassifica()
        {
            InitializeComponent();
        }

        #region Functions and procedures
        private void sortClassification(int left, int right)
        {
            // Apply the quicksort algorithm to sort the classification
            int i = left;
            int j = right;
            Program.sMatch tmp;

            Program.sMatch pivot = _matches[(left + right) / 2];
            do
            {
                while (_matches[i].points < pivot.points)
                    i += 1;
                while (_matches[j].points > pivot.points)
                    j -= 1;

                if (i <= j)
                {
                    tmp = _matches[i];
                    _matches[i] = _matches[j];
                    _matches[j] = tmp;
                    i += 1;
                    j -= 1;
                }
            } while (i < j);

            if (j > left)
                sortClassification(left, j);
            if (i < right)
                sortClassification(i, right);
        }
        #endregion

        #region Events
        private void FrmClassifica_Load(object sender, EventArgs e)
        {
            #region Load the classification from file
            // Open the classification
            StreamReader _classification = new StreamReader(@"..\classification.txt");
            // Read one line of the file at time
            string _line = _classification.ReadLine();
            while (_line != null)
            {
                // Declaration of a temporary array
                List<string> _matchInformations = new List<string>(0);
                // Save all the informations of the match on this array
                foreach (string information in _line.Split('|'))
                    _matchInformations.Add(information);
                // Save all this informations on a structure
                Program.sMatch _newMatch;
                _newMatch.nickname = _matchInformations[0];
                _newMatch.nMatch = Convert.ToInt32(_matchInformations[1]);
                _newMatch.date = _matchInformations[2];
                _newMatch.points = Convert.ToInt32(_matchInformations[3]);
                // Add this structure to the list of matches
                _matches.Add(_newMatch);
                // Read a new line
                _line = _classification.ReadLine();
            }
            // Close the file
            _classification.Close();
            #endregion

            #region Sort the classification

            #endregion
        }
        #endregion
    }
}
