using System;
using System.Linq;
using System.Collections.Generic;

namespace _003
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = ReadFile().ToArray();
            var count = new int[data[0].Length];

            for (var i = 0; i < data.Length; i++)
                for (var j = data[i].Length - 1; j >= 0; j--)
                    count[j] += data[i][j] & 1;

            var gamma = count.Select(c => c > data.Length / 2 ? 1 : 0)
                             .Select((c, i) => (int)(c * Math.Pow(2, data[0].Length - 1 - i)))
                             .Sum();
            var epsilon = ~gamma & ((1 << 12) - 1);

            Console.WriteLine(gamma * epsilon);


            var oxygens = new int[data.Length][];
            var scrubbers = new int[data.Length][];
            Array.Copy(data, oxygens, data.Length);
            Array.Copy(data, scrubbers, data.Length);

            var index = 0;
            while(oxygens.Length > 1)
            {
                var t = oxygens.Count(o => o[index] == 1) >= (double)oxygens.Length / 2 ? 1 : 0;
                oxygens = oxygens.Where(arr => arr[index] == t ).ToArray();
                index++;
            }

            index = 0;
            while (scrubbers.Length > 1)
            {
                var t = scrubbers.Count(o => o[index] == 1) < (double)scrubbers.Length / 2 ? 1 : 0;
                scrubbers = scrubbers.Where(arr => arr[index] == t).ToArray();
                index++;
            }

            var oxygen = oxygens[0].Select((c, i) => (int)(c * Math.Pow(2, data[0].Length - 1 - i))).Sum();
            var scrubber = scrubbers[0].Select((c, i) => (int)(c * Math.Pow(2, data[0].Length - 1 - i))).Sum();

            Console.WriteLine(oxygen * scrubber);
        }


        private static IEnumerable<int[]> ReadFile()
        {
            var file = new System.IO.StreamReader(@"input.txt");
            string line;
            while ((line = file.ReadLine()) != null)
                yield return line.Select(c => int.Parse(c.ToString())).ToArray();
            file.Close();
        }
    }
}
