using WebApplication3.Services;

namespace Sudoku
{
    /// <summary>
    /// Class containing methods for solving a soduku
    /// </summary>
    internal class SudokuSolver : ISudokuSolver
    {

        public int BoardSize;

        public SudokuSolver()
        {
            BoardSize = Constants.BoardSize;
        }


        /// <summary>
        /// Returns bool indication if specified board is solvable
        /// </summary>
        public bool IsSolvable(int[,] boardData)
        {
            for (int ix = 0; ix < BoardSize; ix++)
            {
                for (int iy = 0; iy < BoardSize; iy++)
                {
                    if (boardData[ix, iy] > 0)
                    {
                        if (!UniqueInAllWays(boardData, boardData[ix, iy], ix, iy))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Returns true if specified sudoku is solved. Solution is returned as out
        /// </summary>
        public bool TrySolve(int[,] board, out int[,] solution)
        {
            if (IsSolvable(board))
            {
                return SolveInternal(board, out solution);
            }
            solution = null;
            return false;
        }

        private int[,] CopyBoard(int[,] boardData)
        {
            if (boardData == null)
            {
                return null;
            }

            int[,] result = new int[BoardSize, BoardSize];
            for (int ix = 0; ix < BoardSize; ix++)
            {
                for (int iy = 0; iy < BoardSize; iy++)
                {
                    result[ix, iy] = boardData[ix, iy];
                }
            }

            return result;
        }

        private bool UniqueInRow(int[,] boardData, int val, int x, int y)
        {
            bool result = true;
            for (int i = 0; i < BoardSize; i++)
            {
                if ((i != x) && (boardData[i, y] == val))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private bool UniqueInCol(int[,] boardData, int val, int x, int y)
        {
            bool result = true;
            for (int i = 0; i < BoardSize; i++)
            {
                if ((i != y) && (boardData[x, i] == val))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private bool UniqueInCell(int[,] boardData, int val, int x, int y)
        {
            bool result = true;

            int CellSize = BoardSize / 3;
            int startx = x / 3;
            startx = startx * 3;
            int starty = y / 3;
            starty = starty * 3;

            for (int ix = startx; ix < startx + CellSize; ix++)
            {
                for (int iy = starty; iy < starty + CellSize; iy++)
                {
                    if ((ix != x) && (iy != y) && (boardData[ix, iy] == val))
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        private bool UniqueInAllWays(int[,] boardData, int val, int x, int y)
        {
            return
              UniqueInRow(boardData, val, x, y) &&
              UniqueInCol(boardData, val, x, y) &&
              UniqueInCell(boardData, val, x, y);
        }

        private bool SolveInternal(int[,] boardData, out int[,] solution)
        {
            for (int ix = 0; ix < BoardSize; ix++)
            {
                for (int iy = 0; iy < BoardSize; iy++)
                {
                    if (boardData[ix, iy] == 0)
                    {
                        return SolveCell(boardData, ix, iy, out solution);
                    }
                }
            }
            solution = null;
            return true;
        }

        private bool SolveCell(int[,] boardData, int x, int y, out int[,] solution)
        {
            for (int testVal = 1; testVal <= 9; testVal++)
            {
                if (UniqueInAllWays(boardData, testVal, x, y))
                {
                    boardData[x, y] = testVal;
                    if (SolveInternal(boardData, out solution))
                    {
                        
                        
                        solution = CopyBoard(boardData);
                        
                        return true;
                    }
                }
            }
            solution = null;
            return false;
        }
    }
}
