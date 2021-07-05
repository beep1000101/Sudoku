using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku {
    class SudokuApp {

        [STAThread]
        static void Main() {
           Application.EnableVisualStyles();
           Application.SetCompatibleTextRenderingDefault(false);
           Application.Run(new Form1());
        }
    }
}
