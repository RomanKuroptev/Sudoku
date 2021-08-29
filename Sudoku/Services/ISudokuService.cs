namespace WebApplication3.Services
{
    public interface ISudokuService
    {
        void ChangeDifficulty(int difficulty);
        int GetDifficulty();
        int[,] GetBoard();
        bool IsSolvabel();
        bool IsSolved();
        void SetResult(int[] singeDimensionalArray);
        void Solve();
    }
}