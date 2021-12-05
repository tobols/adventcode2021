using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;

namespace _005
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = ReadFile().ToList();
            Console.WriteLine(GetIntersections(lines.Where(line => line.Item1.X == line.Item2.X || line.Item1.Y == line.Item2.Y)));
            Console.WriteLine(GetIntersections(lines));
        }

        private static int GetIntersections(IEnumerable<ValueTuple<Point, Point>> lines)
        {
            var locations = new Dictionary<Point, int>();

            foreach (var line in lines)
            {
                var x = line.Item1.X;
                var y = line.Item1.Y;
                var stepX = x < line.Item2.X ? 1 : x == line.Item2.X ? 0 : -1;
                var stepY = y < line.Item2.Y ? 1 : y == line.Item2.Y ? 0 : -1;

                AddLocation(locations, x, y);
                do
                {
                    x += stepX;
                    y += stepY;
                    AddLocation(locations, x, y);
                } while (x != line.Item2.X || y != line.Item2.Y);
            }

            return locations.Count(l => l.Value > 1);
        }

        private static void AddLocation(Dictionary<Point, int> locations, int x, int y)
        {
            var p = new Point { X = x, Y = y };
            if (locations.ContainsKey(p))
                locations[p]++;
            else
                locations[p] = 1;
        }

        private static IEnumerable<ValueTuple<Point, Point>> ReadFile()
        {
            var file = new System.IO.StreamReader("input.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var coords =  line.Split(" -> ").SelectMany(l => l.Split(',')).Select(c => int.Parse(c)).ToArray();
                yield return new(new Point { X = coords[0], Y = coords[1] }, new Point { X = coords[2], Y = coords[3] });
            }

            file.Close();
        }
    }
}
