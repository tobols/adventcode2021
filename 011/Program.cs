using System;
using System.Linq;
using System.Collections.Generic;

namespace _011
{
    class Program
    {
        private static int[][] octopusies;


        static void Main(string[] args)
        {
            octopusies = ReadFile().ToArray();

            int sumFlashes = 0;

            for (int i = 0; i < 100; i++)
                sumFlashes += CycleTick();

            Console.WriteLine(sumFlashes);


            int sumOctopusies = octopusies.Length * octopusies[0].Length;
            int index = 100;
            for ( ; true; index++)
            {
                if (CycleTick() == sumOctopusies)
                    break;
            }

            Console.WriteLine(index + 1);
        }


        private static int CycleTick()
        {
            // Increase all energy levels
            for (var y = 0; y < octopusies.Length; y++)
                for (var x = 0; x < octopusies[y].Length; x++)
                    octopusies[y][x]++;

            int totalFlashes = 0;
            int flashes;
            do
            {
                flashes = 0;

                for (var y = 0; y < octopusies.Length; y++)
                    for (var x = 0; x < octopusies[y].Length; x++)
                    {
                        // Flash
                        if (octopusies[y][x] > 9)
                        {
                            for (var i = -1; i <= 1; i++)
                                for (var j = -1; j <= 1; j++)
                                {
                                    if (y + i < 0 || y + i >= octopusies.Length || x + j < 0 || x + j >= octopusies[y].Length || (i == 0 && j == 0))
                                        continue;

                                    if (octopusies[y + i][x + j] >= 0)
                                        octopusies[y + i][x + j]++;
                                }

                            octopusies[y][x] = -1;
                            flashes++;
                        }
                    }

                totalFlashes += flashes;
            } while (flashes > 0);

            // Set energy level to 0 for flashed octopusies
            for (var y = 0; y < octopusies.Length; y++)
                for (var x = 0; x < octopusies[y].Length; x++)
                    if (octopusies[x][y] < 0)
                        octopusies[x][y] = 0;

            return totalFlashes;
        }


        private static IEnumerable<int[]> ReadFile()
        {
            var file = new System.IO.StreamReader("input.txt");
            string line;
            while ((line = file.ReadLine()) != null)
                yield return line.Select(c => (int)Char.GetNumericValue(c)).ToArray();
            file.Close();
        }
    }
}
