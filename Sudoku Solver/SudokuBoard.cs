using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Sudoku_Solver
{
    internal class SudokuBoardCell : TextBox
    {
        public SudokuBoardCell(uint length)
        {
            Size = new Size((int) length, (int) length);

            SetupCell();
        }

        private void SetupCell()
        {
            AutoSize = false;
            TextAlign = HorizontalAlignment.Center;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }

    public class SudokuBoard
    {

    }
}
