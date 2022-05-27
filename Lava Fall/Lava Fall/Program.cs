using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lava_Fall
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmHomePage());
        }

        #region Public structs
        // Game match informations struct
        public struct sMatch
        {
            public string nickname;
            public int nMatch;
            public string date;
            public int points;
        }
        #endregion

        #region Public enumerators
        // Game state
        public enum eGameState
        {
            atstake,
            suspended,
            lost,
        }

        // Game background names
        public enum eBackgrounds
        {
            lava,
            clouds,
            space,
        }
        #endregion

        #region Public variables
        // Points
        public static int _points = 0;
        // Actual match informations
        public static sMatch _actualMatch;
        // State of the game
        public static Program.eGameState _stateGame;
        #endregion
    }
}
