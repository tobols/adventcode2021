using System;
using System.Collections.Generic;
using System.Linq;

namespace _014
{
    class Program
    {
        static void Main(string[] args)
        {
            var polymer = "FPNFCVSNNFSFHHOCNBOB";
            var rules = ReadFile().ToList();

            for (var j = 0; j < 10; j++)
            {
                for (var i = 0; i < polymer.Length - 1; i++)
                {
                    var section = polymer[i..(i + 2)];
                    var rule = rules.FirstOrDefault(r => r[0] == section);

                    if (rule != null)
                    {
                        polymer = polymer.Insert(i + 1, rule[1]);
                        i++;
                    }
                }
            }

            var elems = polymer.GroupBy(c => c);
            long maxElement = elems.Max(e => e.Count());
            long minElement = elems.Min(e => e.Count());

            Console.WriteLine(maxElement - minElement);


            polymer = "FPNFCVSNNFSFHHOCNBOB";
            var pairs = new Dictionary<string, long>();

            for (int i = 0; i < polymer.Length - 1; i++)
            {
                if (pairs.ContainsKey(polymer[i..(i + 2)]))
                    pairs[polymer[i..(i + 2)]]++;
                else
                    pairs[polymer[i..(i + 2)]] = 1;
            }


            for (var i = 0; i < 40; i++)
            {
                var keys = pairs.Keys.ToList();
                var nextPairs = new Dictionary<string, long>();
                foreach (var key in keys)
                {
                    var pair = pairs[key];
                    var rule = rules.FirstOrDefault(r => r[0] == key);
                    if (rule == null)
                    {
                        if (nextPairs.ContainsKey(key))
                            nextPairs[key] += pair;
                        else
                            nextPairs[key] = pair;

                        continue;
                    }

                    var newPairs = new string[] { key[0] + rule[1], rule[1] + key[1] };

                    foreach (var newPair in newPairs)
                    {
                        if (nextPairs.ContainsKey(newPair))
                            nextPairs[newPair] += pair;
                        else
                            nextPairs[newPair] = pair;
                    }
                }

                pairs = nextPairs;
            }


            var elements = new Dictionary<char, long>();
            foreach (var pair in pairs.Where(p => p.Value > 0))
            {
                var c = pair.Key[1];

                if (elements.ContainsKey(c))
                    elements[c] += pair.Value;
                else
                    elements[c] = pair.Value;
            }

            var firstPair = pairs.FirstOrDefault();
            if (elements.ContainsKey(firstPair.Key[0]))
                elements[firstPair.Key[0]] += firstPair.Value;
            else
                elements[firstPair.Key[0]] = firstPair.Value;


            maxElement = elements.Max(e => e.Value);
            minElement = elements.Min(e => e.Value);

            Console.WriteLine(maxElement - minElement);
        }


        private static IEnumerable<string[]> ReadFile()
        {
            var file = new System.IO.StreamReader("input.txt");

            string line;
            while ((line = file.ReadLine()) != null)
                yield return line.Split(" -> ").ToArray();

            file.Close();
        }
    }
}
