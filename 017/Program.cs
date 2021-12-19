using System;
using System.Linq;
using System.Collections.Generic;

namespace _017
{
    class Program
    {
        static void Main(string[] args)
        {
            var xMin = 287;
            var xMax = 309;
            var yMin = -76;
            var yMax = -48;


            var xHits = new List<int[]>();

            for (int i = 0; i <= xMax; i++)
            {
                var x = 0;
                var xVel = i;
                var steps = 0;

                do
                {
                    x += xVel;
                    xVel--;
                    steps++;

                    if (x >= xMin && x <= xMax)
                        xHits.Add(new int[] { x, i, steps });

                } while (x <= xMax && xVel > 0);
            }
            var xVels = xHits.Select(h => h[1]).Distinct().ToList();


            var totalMax = 0;
            var xyHits = new List<int[]>();

            foreach (var xvel in xVels)
            {
                var returnVel = 0;
                var startVelY = yMin - 1;

                do
                {
                    var probe = new Probe { xVel = xvel, yVel = startVelY };
                    var steps = 0;
                    var max = 0;

                    while (probe.yPos >= yMin)
                    {
                        if (probe.yPos == 0 && probe.yVel != startVelY)
                            returnVel = probe.yVel;

                        if (probe.yPos > max)
                            max = probe.yPos;

                        if (probe.yPos <= yMax && probe.xPos >= xMin && probe.xPos <= xMax)
                        {
                            // hit
                            xyHits.Add(new int[] { xvel, startVelY });

                            if (max > totalMax)
                                totalMax = max;

                            break;
                        }

                        probe.Tick();
                        steps++;
                    }

                    startVelY++;

                } while (returnVel >= yMin);
            }

            Console.WriteLine(totalMax);
            Console.WriteLine(xyHits.Count());
        }


        private class Probe
        {
            public int xPos = 0;
            public int yPos = 0;
            public int xVel;
            public int yVel;

            public void Tick()
            {
                xPos += xVel;
                yPos += yVel;

                xVel = xVel > 0 ? xVel - 1 : xVel < 0 ? xVel + 1 : 0;
                yVel--;
            }
        }
    }
}
