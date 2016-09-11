using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sudoku_Solver
{
    public class SudokuBoard : Panel
    {
        private sealed class SudokuBoardCell : TextBox
        {
            public SudokuBoardCell()
            {
                MaxLength = 1;
                AutoSize = false;
                Width = (int) CellWidth;
                Height = (int) CellHeight;
                TextAlign = HorizontalAlignment.Center;
            }

            protected override void OnKeyPress(KeyPressEventArgs e)
            {
                base.OnKeyPress(e);

                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    e.Handled = true;
            }

            public uint Value
            {
                get { return Text == string.Empty ? 0 : uint.Parse(Text); }
                set { Text = Convert.ToString(value); }
            }
        }

        private sealed class SudokuBoardRow : Panel
        {
            private readonly SudokuBoardCell[] _cells;

            public SudokuBoardCell this[int x] => _cells[x];

            public SudokuBoardRow()
            {
                _cells = new SudokuBoardCell[CellCount];

                SetupRow();
            }

            private void SetupRow()
            {
                AutoSize = false;
                Height = (int) CellHeight;
                Width = (int) (CellCount*CellWidth + (CellCount + 1)*CellGap);

                int tmpStartLocation = 0;
                for (int i = 0; i < CellCount; i++)
                {
                    _cells[i] = new SudokuBoardCell()
                    {
                        Location = new Point(tmpStartLocation, 0)
                    };

                    Controls.Add(_cells[i]);
                    tmpStartLocation += (int) (CellWidth + CellGap);

                    if ((i + 1)%3 == 0)
                    {
                        tmpStartLocation += (int) CellGap;
                    }
                }
            }
        }

        #region Fields

        private static readonly uint RowGap = 3;
        private static readonly uint CellGap = 3;
        private static readonly uint RowCount = 9;
        private static readonly uint CellCount = 9;
        private static readonly uint CellWidth = 20;
        private static readonly uint CellHeight = 20;
        private readonly SudokuBoardRow[] _boardRows;

        #endregion

        public uint this[int x, int y]
        {
            get { return _boardRows[x][y].Value; }
            set { _boardRows[x][y].Value = value; }
        }

        public SudokuBoard()
        {
            _boardRows = new SudokuBoardRow[RowCount];

            SetupBoard();
        }

        private void SetupBoard()
        {
            AutoSize = true;

            int tmpStartLocation = 0;
            for (int i = 0; i < RowCount; i++)
            {
                _boardRows[i] = new SudokuBoardRow
                {
                    Location = new Point(0, tmpStartLocation)
                };

                Controls.Add(_boardRows[i]);
                tmpStartLocation += (int) (_boardRows[i].Height + RowGap);

                if ((i + 1)%3 == 0)
                {
                    tmpStartLocation += (int) RowGap;
                }
            }
        }

        public uint[,] GetValues()
        {
            var matrix = new uint[RowCount, CellCount];
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < CellCount; j++)
                {
                    matrix[i, j] = _boardRows[i][j].Value;
                }
            }

            return matrix;
        }

        public void SetValues(ref uint[,] matrix)
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < CellCount; j++)
                {
                    _boardRows[i][j].Value = matrix[i, j];
                }
            }
        }

        public void ClearValues()
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < CellCount; j++)
                {
                    _boardRows[i][j].Clear();
                }
            }
        }
    }
}
