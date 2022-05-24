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
            Application.Run(new frmHomePage());
        }

        #region Public structs
        // Game match informations struct
        public struct sMatch
        {
            string nickname;
            int nMatch;
            string date;
            int points;
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
        #endregion
    }
}
