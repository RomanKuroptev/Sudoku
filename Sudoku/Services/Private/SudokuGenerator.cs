using System;
using System.Collections.Generic;

namespace WebApplication3.Services
{
    /// <summary>
    /// Generates a random sudoku with specified difficutly
    /// </summary>
    internal class SudokuGenerator : ISudokuGenerator
    {
        private const int BoardSize = 9;
        private int cells;
        private Random rnd;

        public SudokuGenerator()
        {
            this.rnd = new Random();
            cells = (int)Math.Sqrt(BoardSize);
        }

        /// <summary>
        /// Generates a random sudoku with specified difficutly
        /// </summary>
        public int[,] GetBoardWithDifficutly(int difficulty)
        {
            int[,] board = GetRandomBoard();

            List<int> randomList = new List<int>();

            while (randomList.Count < difficulty)
            {
                int candidate = rnd.Next(0, 81);
                if (!randomList.Contains(candidate))
                {
                    randomList.Add(candidate);
                }
            }
            foreach (int value in randomList)
            {
                var x = value / 9;
                var y = value % 9;
                board[x, y] = 0;
            }
            return board;
        }

        private int[,] GetRandomBoard()
        {
            int[,] board = new int[9, 9];
            rnd = new Random();
            board = FillDiagonal(board);
            FillRemaining(ref board, 0, 3);
            return board;
        }

        private bool FillRemaining(ref int[,] board, int x, int y)
        {
            if (y >= BoardSize && x < BoardSize - 1)
            {
                x = x + 1;
                y = 0;
            }
            if (x >= BoardSize && y >= BoardSize)
                return true;

            if (x < cells)
            {
                if (y < cells)
                    y = cells;
            }
            else if (x < BoardSize - cells)
            {
                if (y == x / cells * cells)
                    y = y + cells;
            }
            else
            {
                if (y == BoardSize - cells)
                {
                    x = x + 1;
                    y = 0;
                    if (x >= BoardSize)
                        return true;
                }
            }

            for (int num = 1; num <= BoardSize; num++)
            {
                if (UniqueInAllWays(board, num, x, y))
                {
                    board[x, y] = num;
                    if (FillRemaining(ref board, x, y + 1))
                        return true;

                    board[x, y] = 0;
                }
            }
            return false;
        }

        private int[,] FillDiagonal(int[,] board)
        {

            for (int i = 0; i < 9; i += 3)
            {
                board = FillCell(board, i, i);
            }

            return board;
        }

        private int[,] FillCell(int[,] board, int x, int y)
        {
            int num;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    do
                    {
                        num = rnd.Next(1, 10);
                    }
                    while (!UniqueInAllWays(board, num, x, y));

                    board[x + i, y + j] = num;
                }
            }
            return board;
        }

        private static bool UniqueInCol(int[,] boardData, int val, int y)
        {
            bool result = true;
            for (int i = 0; i < 9; i++)
            {
                if (boardData[i, y] == val)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private static bool UniqueInRow(int[,] boardData, int val, int x)
        {
            bool result = true;
            for (int i = 0; i < 9; i++)
            {
                if (boardData[x, i] == val)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private static bool UniqueInCell(int[,] boardData, int val, int x, int y)
        {
            bool result = true;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (boardData[x + i, y + j] == val)
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        private static bool UniqueInAllWays(int[,] boardData, int val, int x, int y)
        {

            return 
                UniqueInCol(boardData, val, y) &&
                UniqueInRow(boardData, val, x) &&
                UniqueInCell(boardData, val, x - x % 3, y - y % 3);
            
        }
    }
}
