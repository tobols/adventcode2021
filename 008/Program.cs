using System;
using System.Collections.Generic;
using System.Linq;

namespace _008
{
    class Program
    {
        static void Main(string[] args)
        {
            var outputs = ReadFile().ToList();

            var lengths = new int[] { 2, 3, 4, 7 };
            var sumUniques = outputs.SelectMany(o => o[1]).Count(s => lengths.Contains(s.Length));

            Console.WriteLine(sumUniques);


            var totSum = 0;
            foreach (var signals in outputs)
            {
                var n1 = signals[0].First(s => s.Length == 2);
                var n4 = signals[0].First(s => s.Length == 4);
                var n7 = signals[0].First(s => s.Length == 3);
                var n8 = signals[0].First(s => s.Length == 7);

                var fives = signals[0].Where(s => s.Length == 5).ToArray();
                var sixes = signals[0].Where(s => s.Length == 6).ToArray();

                var a = n7.Except(n1).FirstOrDefault();
                var eg = n8.Except(n4).Where(c => c != a).ToArray();

                var n2 = fives.First(f => f.Contains(eg[0]) && f.Contains(eg[1]));
                var n3 = fives.First(f => f != n2 && f.Contains(n1[0]) && f.Contains(n1[1]));
                var n5 = fives.First(f => f != n2 && f != n3);

                var n9 = sixes.Where(s => n4.All(n => s.Contains(n))).First();
                var n0 = sixes.Where(s => s != n9 && n1.All(n => s.Contains(n))).First();
                var n6 = sixes.Where(s => s != n9 && s != n0).First();

                var nums = new string[] { n0, n1, n2, n3, n4, n5, n6, n7, n8, n9 };
                var num = 0;
                for (int i = 3; i >= 0; i--)
                {
                    var sig = nums.FirstOrDefault(x => x.Length == signals[1][i].Length && x.All(s => signals[1][i].Contains(s)));
                    var n = Array.IndexOf(nums, sig);
                    num += n * (int)Math.Pow(10, 3 - i);
                }

                totSum += num;
            }

            Console.WriteLine(totSum);
        }


        private static IEnumerable<string[][]> ReadFile()
        {
            var file = new System.IO.StreamReader("input.txt");
            string line;
            while ((line = file.ReadLine()) != null)
                yield return line.Split('|').Select(x => x.Trim().Split(' ')).ToArray();
            file.Close();
        }
    }
}
