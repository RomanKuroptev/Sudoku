using Sudoku;
using System;

namespace WebApplication3.Services
{
    /// <summary>
    /// Service for generating and solving a sudoku
    /// </summary>
    internal class SudokuService : ISudokuService
    {
        private int[,] board;
        private int difficulty;
        private readonly ISudokuSolver sudokuSolver;
        private readonly ISudokuGenerator sudokuGenerator;

        public SudokuService(ISudokuSolver sudokuSolver, ISudokuGenerator sudokuGenerator)
        {
            difficulty = Constants.DefaultDifficulty;
            this.sudokuSolver = sudokuSolver;
            this.sudokuGenerator = sudokuGenerator;
            board = sudokuGenerator.GetBoardWithDifficutly(difficulty);
        }

        public int[,] GetBoard()
        {
            return board;
        }

        public void SetResult(int[] singeDimensionalArray)
        {
            board = ToRectangular(singeDimensionalArray, 9);
        }

        public bool IsSolvabel()
        {
            return sudokuSolver.IsSolvable(board);
        }

        public bool IsSolved()
        {
            return NumberUnset() == 0 && IsSolvabel();
        }

        public void Solve()
        {
            if (sudokuSolver.TrySolve(board, out int[,] solution))
            {
                board = solution;
            }
        }

        public void ChangeDifficulty(int difficulty)
        {
            this.difficulty = difficulty;
            board = sudokuGenerator.GetBoardWithDifficutly(difficulty);
        }

        public int GetDifficulty()
        {
            return difficulty;
        }

        private int NumberUnset()
        {
            int unsetValuesCount = 0;
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (board[x, y] == 0)
                    {
                        unsetValuesCount++;
                    }
                }
            }
            return unsetValuesCount;
        }

        private T[,] ToRectangular<T>(T[] flatArray, int width)
        {
            int height = (int)Math.Ceiling(flatArray.Length / (double)width);
            T[,] result = new T[height, width];
            int rowIndex, colIndex;

            for (int index = 0; index < flatArray.Length; index++)
            {
                rowIndex = index / width;
                colIndex = index % width;
                result[rowIndex, colIndex] = flatArray[index];
            }
            return result;
        }
    }

}
