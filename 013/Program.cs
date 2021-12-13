using System;
using System.Linq;
using System.Collections.Generic;

namespace _013
{
    class Program
    {
        static void Main(string[] args)
        {
            var (points, folds) = ReadFile();

            foreach (var fold in folds)
            {
                Fold(points, fold);
                Console.WriteLine(points.GroupBy(p => p.X * 1000 + p.Y).Count());
            }

            Print(points);
        }


        private static List<Point> Fold(List<Point> points, string instruction)
        {
            var instr = instruction.Split('=');
            var num = int.Parse(instr[1]);

            foreach (var point in points)
            {
                if (instr[0] == "x")
                    point.X = num - Math.Abs(num - point.X);
                else
                    point.Y = num - Math.Abs(num - point.Y);
            }

            return points;
        }


        private static void Print(List<Point> points)
        {
            var groupedPoints = points.GroupBy(p => p.X * 1000 + p.Y).Select(p => p.FirstOrDefault()).ToList();
            var maxX = groupedPoints.Max(p => p.X);
            var maxY = groupedPoints.Max(p => p.Y);


            for (var y = 0; y <= maxY; y++)
            {
                for (var x = 0; x <= maxX; x++)
                {
                    if (groupedPoints.Any(p => p.X == x && p.Y == y))
                        Console.Write('#');
                    else
                        Console.Write(' ');
                }
                Console.WriteLine();
            }
        }


        private static (List<Point>, List<string>) ReadFile()
        {
            var file = new System.IO.StreamReader("input.txt");

            var points = new List<Point>();
            var folds = new List<string>();

            string line;
            while ((line = file.ReadLine()) != "")
            {
                var xy = line.Split(',').Select(int.Parse).ToArray();
                points.Add(new Point { X = xy[0], Y = xy[1] });
            }

            while ((line = file.ReadLine()) != null)
                folds.Add(line[11..]);

            file.Close();

            return (points, folds);
        }


        private class Point
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
