using System;
using System.Linq;
using System.Collections.Generic;

namespace _004
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = ReadNumbers();
            var boards = ReadFile();


            var score = GetBoardScore(numbers, boards);
            Console.WriteLine(score);


            score = GetBoardScore(numbers, boards, true);
            Console.WriteLine(score);
        }



        private static int GetBoardScore(int[] numbers, int[][] boards, bool loosingBoard = false)
        {
            List<int> winningBoards = new List<int>();

            var marks = new bool[boards.Length][];
            for (var i = 0; i < marks.Length; i++)
                marks[i] = new bool[25];


            for (var i = 0; i < numbers.Length; i++)
            {
                for (var j = 0; j < boards.Length; j++)
                {
                    if (winningBoards.Contains(j))
                        continue;

                    var index = Array.IndexOf(boards[j], numbers[i]);
                    if (index >= 0)
                    {
                        marks[j][index] = true;

                        if (TestBingo(j, index, marks))
                        {
                            if (loosingBoard)
                            {
                                winningBoards.Add(j);
                                if (winningBoards.Count == boards.Length)
                                    return (GetScore(j, boards, marks, numbers[i]));
                            }
                            else
                                return(GetScore(j, boards, marks, numbers[i]));
                        }
                    }
                }
            }

            throw new Exception("This should never happen!!");
        }


        private static bool TestBingo(int boardIndex, int index, bool[][] marks)
        {
            var multi = index / 5;
            var rest = index % 5;
            bool horizontal = marks[boardIndex][(multi * 5)..(multi * 5 + 5)].Aggregate((p, c) => p && c);
            bool vertical = marks[boardIndex].Where((value, ix) => ix % 5 == rest).Aggregate((p, c) => p && c);

            return horizontal || vertical;
        }


        private static int GetScore(int boardIndex, int[][] boards, bool[][] marks, int lastNumber)
        {
            var sum = 0;
            for (var k = 0; k < marks[boardIndex].Length; k++)
                if (!marks[boardIndex][k])
                    sum += boards[boardIndex][k];

            return (sum * lastNumber);
        }


        private static int[][] ReadFile()
        {
            var file = new System.IO.StreamReader("input.txt");
            var boards = new List<int[]>();
            string line;
            int[] board = new int[25];
            var index = 0;
            while ((line = file.ReadLine()) != null)
            {
                var numbers = line.Split(' ').Where(c => !string.IsNullOrWhiteSpace(c)).ToArray();

                if (numbers.Length == 0)
                    continue;

                for (var i = 0; i < numbers.Length; i++)
                    board[index * 5 + i] = int.Parse(numbers[i]);

                if (index >= 4)
                {
                    index = 0;
                    boards.Add(board);
                    board = new int[25];
                }
                else
                    index++;
            }

            file.Close();
            return boards.ToArray();
        }


        private static int[] ReadNumbers()
        {
            var file = new System.IO.StreamReader("bingoNumbers.txt");
            string line = file.ReadLine();
            return line.Split(',').Where(c => !string.IsNullOrWhiteSpace(c)).Select(c => int.Parse(c)).ToArray();
        }
    }
}
