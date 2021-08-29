namespace WebApplication3.Services
{
    /// <summary>
    /// Generates a random sudoku with specified difficutly
    /// </summary>
    public interface ISudokuGenerator
    {
        /// <summary>
        /// Generates a random sudoku with specified difficutly
        /// </summary>
        int[,] GetBoardWithDifficutly(int difficulty);
    }
}