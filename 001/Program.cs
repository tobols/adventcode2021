using System;
using System.Collections.Generic;
using System.Linq;

namespace _001
{
    class Program
    {
        static void Main(string[] args)
        {
            var values = ReadFile().ToArray();
            var sumDeeper = 0;

            for (int i = 0; i < values.Length - 1; i++)
            {
                if (values[i] < values[i + 1])
                    sumDeeper++;
            }

            Console.WriteLine($"Sum deeper measurements: {sumDeeper}");


            var sumDeeperWindow = 0;

            for (int i = 1; i < values.Length - 2; i++)
            {
                var windowSum = values[(i-1)..(i+2)].Sum();
                var nextWindow = values[(i)..(i+3)].Sum();
                if (nextWindow > windowSum)
                    sumDeeperWindow++;
            }

            Console.WriteLine($"Sum deeper window measurements: {sumDeeperWindow}");
        }


        private static IEnumerable<int> ReadFile()
        {
            var file = new System.IO.StreamReader(@"input.txt");
            string line;
            while ((line = file.ReadLine()) != null)
                yield return Convert.ToInt32(line);
            file.Close();
        }
    }
}
