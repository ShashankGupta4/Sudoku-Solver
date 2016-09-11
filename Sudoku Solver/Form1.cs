using System;
using System.Windows.Forms;
using SudokuSolverApp.Sudoku;

namespace SudokuSolverApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            uint[,] matrix = sudokuBoard1.GetValues();

            SudokuSolver.Solve(ref matrix);

            sudokuBoard1.SetValues(ref matrix);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sudokuBoard1.ClearValues();
        }
    }
}
