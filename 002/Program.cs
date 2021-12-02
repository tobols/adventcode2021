using System;
using System.Collections.Generic;
using System.Linq;

namespace _002
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = ReadFile();

            int fwd, dwn, up;
            fwd = data.Where(d => d.Item1 == "forward") .Sum(d => d.Item2);
            dwn = data.Where(d => d.Item1 == "down").Sum(d => d.Item2);
            up = data.Where(d => d.Item1 == "up").Sum(d => d.Item2);

            var depth = dwn - up;

            Console.WriteLine(fwd * depth);


            var aim = 0L;
            var aimDepth = 0L;
            foreach (var d in data)
            {
                if (d.Item1 == "down")
                    aim += d.Item2;
                else if (d.Item1 == "up")
                    aim -= d.Item2;
                else
                    aimDepth += d.Item2 * aim;
            }

            Console.WriteLine(aimDepth * fwd);
        }


        private static List<ValueTuple<string, int>> ReadFile()
        {
            var file = new System.IO.StreamReader(@"input.txt");
            var input = new List<ValueTuple<string, int>>();

            string line;
            while ((line = file.ReadLine()) != null)
            {
                var values = line.Split(' ');
                input.Add(new(values[0], int.Parse(values[1])));
            }

            file.Close();
            return input;
        }
    }
}
