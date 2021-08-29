namespace Sudoku
{
    /// <summary>
    /// Class containing methods for solving a soduku
    /// </summary>
    public interface ISudokuSolver
    {
        /// <summary>
        /// Returns bool indication if specified board is solvable
        /// </summary>
        bool IsSolvable(int[,] boardData);

        /// <summary>
        /// Returns true if specified sudoku is solved. Solution is returned as out
        /// </summary>
        bool TrySolve(int[,] boardData, out int[,] solution);
    }
}