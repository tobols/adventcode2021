using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _012
{
    class Program
    {
        private static Regex upper = new(@"^[A-Z]+$");
        private static Dictionary<string, List<string>> caves;


        static void Main(string[] args)
        {
            var connections = ReadFile().ToList();
            caves = new Dictionary<string, List<string>>();

            foreach (var connection in connections)
            {
                var cvs = connection.Split('-');

                if (!caves.ContainsKey(cvs[0]))
                    caves.Add(cvs[0], new List<string>());
                if (!caves.ContainsKey(cvs[1]))
                    caves.Add(cvs[1], new List<string>());

                caves[cvs[0]].Add(cvs[1]);
                caves[cvs[1]].Add(cvs[0]);
            }


            var paths = SearchCave("start", "");
            Console.WriteLine(paths.Count());


            paths = SearchCave("start", "", true);
            Console.WriteLine(paths.Count());
        }


        private static List<string> SearchCave(string cave, string path, bool AllowOneDoubleDip = false)
        {
            if (cave == "end")
                return new List<string> { $"{path},{cave}" };

            if (!upper.IsMatch(cave))
            {
                var segments = path.Split(',');
                if (segments.Any(s => s == cave))
                {
                    if (!AllowOneDoubleDip || cave == "start" || HasDoubleDip(path))
                        return new List<string>();
                }
            }

            List<string> foundPaths = new List<string>();

            foreach (var connection in caves[cave])
            {
                var paths = SearchCave(connection, $"{path},{cave}", AllowOneDoubleDip);
                foundPaths = foundPaths.Concat(paths).ToList();
            }

            return foundPaths;
        }


        private static bool HasDoubleDip(string path)
        {
            var cvs = path.Split(',').Where(s => !string.IsNullOrWhiteSpace(s) && !upper.IsMatch(s)).GroupBy(s => s);
            return cvs.Any(c => c.Count() > 1);
        }


        private static IEnumerable<string> ReadFile()
        {
            var file = new System.IO.StreamReader("input.txt");
            string line;
            while ((line = file.ReadLine()) != null)
                yield return line;
            file.Close();
        }
    }
}
