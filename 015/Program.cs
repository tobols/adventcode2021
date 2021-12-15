using System;
using System.Collections.Generic;
using System.Linq;

namespace _015
{
    class Program
    {
        static void Main(string[] args)
        {
            var riskMap = ReadFile().ToArray();
            var riskPaths = Dijkstra(riskMap);
            Console.WriteLine(riskPaths[(riskMap.Length - 1) * 1000 + riskMap.Length - 1].risk);


            var expanded = ExpandMap(riskMap);
            riskPaths = Dijkstra(expanded);
            Console.WriteLine(riskPaths[(expanded.Length - 1) * 1000 + expanded.Length - 1].risk);
        }


        private static Dictionary<int, RiskNode> Dijkstra(int[][] riskMap)
        {
            var nodes = new List<RiskNode>();
            var nodeIndex = new Dictionary<int, RiskNode>();
            nodes.Add(new RiskNode { x = 0, y = 0, risk = 0, visited = false });
            nodeIndex.Add(0, nodes.First());

            for (int i = 0; i < riskMap.Length; i++)
            {
                for (int j = 0; j < riskMap[i].Length; j++)
                {
                    if (i == 0 && j == 0) j++;
                    var node = new RiskNode { x = j, y = i, risk = 100000, visited = false };
                    //nodes.Add(node);
                    nodeIndex.Add(i * 1000 + j, node);
                }
            }
            
            while (nodes.Any())
            {
                var node = nodes.First();
                nodes.Remove(node);

                for (int y = node.y - 1; y <= node.y + 1; y++)
                    for (int x = node.x - 1; x <= node.x + 1; x++)
                    {
                        if (y < 0 || y >= riskMap.Length || x < 0 || x >= riskMap[y].Length || (x == node.x && y == node.y) || (x != node.x && y != node.y))
                            continue;

                        var neighbourNode = nodeIndex[y * 1000 + x];
                        if (neighbourNode.visited)
                            continue;

                        if (neighbourNode.risk > node.risk + riskMap[neighbourNode.y][neighbourNode.x])
                        {
                            neighbourNode.risk = node.risk + riskMap[neighbourNode.y][neighbourNode.x];
                            neighbourNode.prevX = node.x;
                            neighbourNode.prevY = node.y;

                            var index = nodes.FindIndex(n => n.risk > neighbourNode.risk);
                            if (index >= 0)
                            {
                                nodes.Remove(neighbourNode);
                                nodes.Insert(index, neighbourNode);
                            }
                            else
                                nodes.Add(neighbourNode);
                        }

                        node.visited = true;
                    }
            }

            return nodeIndex;
        }


        private static int[][] ExpandMap(int[][] wrappedMap)
        {
            var expandedMap = new int[wrappedMap.Length * 5][];

            for (int i = 0; i < 5; i++)
            {
                for (int y = 0; y < wrappedMap.Length; y++)
                {
                    expandedMap[i * wrappedMap.Length + y] = new int[wrappedMap[y].Length * 5];

                    for (int j = 0; j < 5; j++)
                        for (int x = 0; x < wrappedMap[y].Length; x++)
                            expandedMap[i * wrappedMap.Length + y][j * wrappedMap[y].Length + x] = (wrappedMap[y][x] - 1 + j + i) % 9 + 1;
                }
            }

            return expandedMap;
        }


        private static IEnumerable<int[]> ReadFile()
        {
            var file = new System.IO.StreamReader("input.txt");
            string line;
            while ((line = file.ReadLine()) != null)
                yield return line.Select(c => (int)Char.GetNumericValue(c)).ToArray();
            file.Close();
        }


        private class RiskNode
        {
            public int x;
            public int y;
            public int risk;
            public int prevX;
            public int prevY;
            public bool visited;
        }
    }
}
