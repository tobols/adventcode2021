using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace _009
{
    class Program
    {
        static void Main(string[] args)
        {
            var map = ReadFile().ToArray();

            var lowPoints = new List<Point>();
            var sum = 0;
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    var n = i - 1 < 0 || map[i - 1][j] > map[i][j];
                    var e = j + 1 >= map[i].Length || map[i][j + 1] > map[i][j];
                    var s = i + 1 >= map.Length || map[i + 1][j] > map[i][j];
                    var w = j - 1 < 0 || map[i][j - 1] > map[i][j];

                    if (n && e && s && w)
                    {
                        lowPoints.Add(new Point { X = j, Y = i });
                        sum += map[i][j] + 1;
                    }
                }
            }

            Console.WriteLine(sum);


            var basins = new List<List<Point>>();
            foreach (var lowPoint in lowPoints)
            {
                var basin = new List<Point>();
                basin.Concat(MapBasin(lowPoint.X, lowPoint.Y, map, basin, -1)).ToList();
                basins.Add(basin);
            }

            var basinProduct = basins.Select(b => b.Count()).OrderByDescending(n => n).Take(3).Aggregate((p, c) => p * c);
            Console.WriteLine(basinProduct);
        }


        public static List<Point> MapBasin(int x, int y, int[][] map, List<Point> foundPoints, int lastPoint)
        {
            var p = new Point(x, y);
            if (foundPoints.Any(fp => fp.X == p.X && fp.Y == p.Y) || x < 0 || y < 0 || y >= map.Length || x >= map[y].Length || map[y][x] == 9 || map[y][x] <= lastPoint)
                return new List<Point>();

            foundPoints.Add(p);
            foundPoints.Concat(MapBasin(x - 1, y, map, foundPoints, map[y][x])).ToList();
            foundPoints.Concat(MapBasin(x, y - 1, map, foundPoints, map[y][x])).ToList();
            foundPoints.Concat(MapBasin(x + 1, y, map, foundPoints, map[y][x])).ToList();
            foundPoints.Concat(MapBasin(x, y + 1, map, foundPoints, map[y][x])).ToList();

            return foundPoints;
        }


        private static IEnumerable<int[]> ReadFile()
        {
            var file = new System.IO.StreamReader("input.txt");
            string line;
            while ((line = file.ReadLine()) != null)
                yield return line.Select(c=> (int)Char.GetNumericValue(c)).ToArray();
            file.Close();
        }
    }
}
