using System;

namespace Sudoku_Solver
{
    public static class SudokuSolver
    {
        private static bool IsValidInRow(ref uint[,] matrix, uint x, uint y)
        {
            uint tmpValue = matrix[x, y];

            for (uint i = 0; i < 9; i++)
            {
                if (i == y)
                    continue;

                if (matrix[x, i] == tmpValue)
                    return false;
            }

            return true;
        }

        private static bool IsValidInColumn(ref uint[,] matrix, uint x, uint y)
        {
            uint tmpValue = matrix[x, y];

            for (uint i = 0; i < 9; i++)
            {
                if (i == x)
                    continue;

                if (matrix[i, y] == tmpValue)
                    return false;
            }

            return true;
        }

        private static bool IsValidInSquare(ref uint[,] matrix, uint x, uint y)
        {
            uint tmpValue = matrix[x, y];

            uint hNumber = x/3;
            uint vNumber = y/3;

            hNumber *= 3;
            vNumber *= 3;

            for (uint i = hNumber; i < hNumber + 3; i++)
            {
                for (uint j = vNumber; j < vNumber + 3; j++)
                {
                    if (i == x && y == j)
                        continue;

                    if (matrix[i, j] == tmpValue)
                        return false;
                }
            }

            return true;
        }

        private static bool IsValid(ref uint[,] matrix, uint x, uint y)
        {
            return IsValidInRow(ref matrix, x, y) && IsValidInColumn(ref matrix, x, y) && IsValidInSquare(ref matrix, x, y);
        }
    
        /// <summary>
        /// Solves a given Sudoku using a Backtracking algorithm; Time complexity = O(9^k), k = no. of blank cells to be filled.
        /// </summary>
        private static bool BacktrackSolve(ref uint[,] matrix, uint cellIndex)
        {
            uint x = cellIndex / 9; // Row
            uint y = cellIndex % 9; // Column

            if (x == 9 || y == 9)
                return true;

            if (matrix[x, y] > 9)
                throw new ArgumentOutOfRangeException(nameof(matrix));

            // Ignore pre-filled cell values.
            if (matrix[x, y] != 0)
                return BacktrackSolve(ref matrix, cellIndex + 1);

            uint currValue = 0;
            while (++currValue < 10)
            {
                matrix[x, y] = currValue;
                if (!IsValid(ref matrix, x, y))
                    continue;

                if (BacktrackSolve(ref matrix, cellIndex + 1))
                    return true;
            }

            matrix[x, y] = 0;
            return false;
        }

        public static void Solve(ref uint[,] matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            BacktrackSolve(ref matrix, 0);
        }
    }
}
