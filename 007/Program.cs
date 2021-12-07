using System;
using System.Linq;

namespace _007
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = ReadFile();
            Array.Sort(numbers);
            var median = numbers[numbers.Length / 2];

            var sum = 0;
            for (int i = 0; i < numbers.Length; i++)
                sum += Math.Abs(numbers[i] - median);

            Console.WriteLine(sum);


            var mean = (decimal)numbers.Sum() / numbers.Length;
            var meanLow = (int)Math.Floor(mean);
            var meanHigh = (int)Math.Ceiling(mean);
            var sumLow = 0;
            var sumHigh = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                var diff = Math.Abs(numbers[i] - meanLow);
                sumLow += ((int)Math.Pow(diff, 2)+diff) / 2;

                diff = Math.Abs(numbers[i] - meanHigh);
                sumHigh += ((int)Math.Pow(diff, 2) + diff) / 2;
            }

            Console.WriteLine(sumLow > sumHigh ? sumHigh : sumLow);
        }


        private static int[] ReadFile()
        {
            var file = new System.IO.StreamReader("input.txt");
            string line = file.ReadLine();
            return line.Split(',').Where(c => !string.IsNullOrWhiteSpace(c)).Select(c => int.Parse(c)).ToArray();
        }
    }
}
