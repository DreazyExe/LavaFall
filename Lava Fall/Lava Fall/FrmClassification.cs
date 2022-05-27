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
    public partial class FrmClassification : Form
    {
        #region Global costants
        const string CLASSIFICATION_FILE_POSITION = @"..\classification.txt";
        #endregion

        #region Global variables
        // Array of matches
        Program.sMatch[] _matches = new Program.sMatch[0];
        #endregion

        public FrmClassification()
        {
            InitializeComponent();
        }

        #region Functions and procedures
        /// <summary>
        /// Loads the classification from the file
        /// </summary>
        private void loadClassification()
        {
            // Verify if the classification exists
            if (File.Exists(CLASSIFICATION_FILE_POSITION))
            {
                // Open the classification
                StreamReader _classification = new StreamReader(CLASSIFICATION_FILE_POSITION);
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
                    // Add this structure to the array of matches
                    Array.Resize(ref _matches, _matches.Length + 1);
                    _matches[_matches.Length - 1] = _newMatch;
                    // Read a new line
                    _line = _classification.ReadLine();
                }
                // Close the file
                _classification.Close();
            }
        }

        /// <summary>
        /// Inserts the actual match informations on the classification
        /// </summary>
        private void insertActualMatchInformations()
        {
            // Variable declaration
            int i;
            // Add one position to the array of matches
            Array.Resize(ref _matches, _matches.Length + 1);
            // Set the indicator variable
            i = _matches.Length - 2;
            // Bring forward all the matches with more points
            while (i >= 0 && _matches[i].points < Program._actualMatch.points)
            {
                _matches[i + 1] = _matches[i];
                i = i - 1; // Change the indicator position
            }
            // Insert in the correct position the new match
            _matches[i + 1] = Program._actualMatch; // alla posizione i + 1 del vettore assegno il valore di carattere
        }

        /// <summary>
        /// Saves the classification in the file
        /// </summary>
        private void saveClassification()
        {
            // Open the file in write mode
            StreamWriter _classification = new StreamWriter(CLASSIFICATION_FILE_POSITION);
            // Write all the classification in the file
            foreach (Program.sMatch _matchToSave in _matches)
                _classification.WriteLine(_matchToSave.nickname + "|" + _matchToSave.nMatch + "|" + _matchToSave.date + "|" + _matchToSave.points);
            // Close the classification
            _classification.Close();
        }

        /// <summary>
        /// Shows the updated classification on ListView
        /// </summary>
        private void refreshListView()
        {
            // Remove all the ListView items
            lvClassification.Items.Clear();
            // Insert all the elements on it
            foreach(Program.sMatch _matchToLoad in _matches)
            {
                // Create a new ListView line and load the informations of the match
                ListViewItem _match = new ListViewItem(_matchToLoad.nickname);
                _match.SubItems.Add(_matchToLoad.nMatch.ToString());
                _match.SubItems.Add(_matchToLoad.date);
                _match.SubItems.Add(_matchToLoad.points.ToString());
                // Add the line to the ListView
                lvClassification.Items.Add(_match);
            }
        }
        #endregion

        #region Events
        private void FrmClassifica_Load(object sender, EventArgs e)
        {
            // Load the classification from file
            loadClassification();
            // Insert the numer of match in the actual match information
            Program._actualMatch.nMatch = _matches.Count() + 1;
            // Add actual match informations in the correct position on the matches list
            insertActualMatchInformations();
            // Save them to the classification file
            saveClassification();
            // Refresh ListView
            refreshListView();
        }
        private void btPlayAgain_Click(object sender, EventArgs e)
        {
            // Reset game points
            Program._points = 0;
            // Open the homepage of the game
            FrmHomePage frmHomepage = new FrmHomePage();
            frmHomepage.Show();
            // Close this form
            this.Close();
        }
        #endregion
    }
}
