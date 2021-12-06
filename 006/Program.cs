using System;
using System.Linq;

namespace _006
{
    class Program
    {
        static void Main(string[] args)
        {
            var startPopulation = ReadFile();

            Console.WriteLine(CalculatePopulation(startPopulation, 80));
            Console.WriteLine(CalculatePopulation(startPopulation, 256));
        }

        private static long CalculatePopulation(int[] startPop, int cycles)
        {
            var population = new long[9];
            foreach (var num in startPop)
                population[num]++;

            for (int c = 0; c < cycles; c++)
                population[(c + 7) % 9] += population[c % 9];

            return population.Sum();
        }

        private static int[] ReadFile()
        {
            var file = new System.IO.StreamReader("input.txt");
            string line = file.ReadLine();
            return line.Split(',').Where(c => !string.IsNullOrWhiteSpace(c)).Select(c => int.Parse(c)).ToArray();
        }
    }
}
