using System;
using System.Collections.Generic;
using System.Linq;

namespace _010
{
    class Program
    {
        static char[] openers = new char[] { '(', '[', '{', '<' };
        static char[] closers = new char[] { ')', ']', '}', '>' };

        static void Main(string[] args)
        {
            var chunks = ReadFile().ToList();

            var errorScore = 0;
            var incompleteScores = new List<long>();

            foreach (var chunk in chunks)
            {
                try
                {
                    var missing = TestChunk(chunk);
                    incompleteScores.Add(CalculateScore(missing));
                }
                catch (ChunkException ex)
                {
                    switch (ex.IllegalCharacter)
                    {
                        case ')': errorScore += 3; break;
                        case ']': errorScore += 57; break;
                        case '}': errorScore += 1197; break;
                        case '>': errorScore += 25137; break;
                    }
                }
            }

            incompleteScores.Sort();
            var winningScore = incompleteScores[incompleteScores.Count() / 2];

            Console.WriteLine(errorScore);
            Console.WriteLine(winningScore);
        }


        private static long CalculateScore(string str)
        {
            return str.Aggregate(0L, (p, c) =>
            {
                int val = 0;
                switch(c)
                {
                    case ')': val = 1; break;
                    case ']': val = 2; break;
                    case '}': val = 3; break;
                    case '>': val = 4; break;
                }

                return p * 5 + val;
            });
        }


        private static string TestChunk(string chunk)
        {
            var space = 0;
            var missingStr = "";

            while (space < chunk.Length)
            {
                var (spc, str) = TestChunkRecursive(chunk[space..]);
                space += spc;
                missingStr = str;
            }

            return missingStr;
        }


        private static (int, string) TestChunkRecursive(string chunk)
        {
            var c = chunk[0];
            var space = 1;
            var missingStr = "";

            if (Array.IndexOf(closers, c) != -1)
                return (1, "");

            while (space + 1 <= chunk.Length)
            {
                var x = chunk[space];

                if (Array.IndexOf(closers, x) != -1)
                {
                    if (Array.IndexOf(openers, c) != Array.IndexOf(closers, x))
                    {
                        throw new ChunkException($"Expected {closers[Array.IndexOf(openers, c)]}, but found {x} instead.")
                        {
                            IllegalCharacter = x
                        };
                    }

                    return (space + 1, "");
                }

                var (spc, str) = TestChunkRecursive(chunk[(space)..]);
                missingStr = str;
                space += spc;
            }

            return (space + 1, missingStr + closers[Array.IndexOf(openers, c)]);
        }


        private static IEnumerable<string> ReadFile()
        {
            var file = new System.IO.StreamReader("input.txt");
            string line;
            while ((line = file.ReadLine()) != null)
                yield return line;
            file.Close();
        }


        public class ChunkException : Exception
        {
            public ChunkException(string msg)
                : base(msg)
            { }

            public Char IllegalCharacter { get; set; }
        }
    }
}
