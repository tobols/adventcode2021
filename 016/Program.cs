using System;
using System.Collections.Generic;
using System.Linq;

namespace _016
{
    class Program
    {
        static void Main(string[] args)
        {
            var hex = ReadFile();
            var binary = String.Join(String.Empty, hex.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

            var (typeSum, _, sum) = Process(binary);
            Console.WriteLine(typeSum);
            Console.WriteLine(sum);
        }


        private static (int, int, long) Process(string binary)
        {
            var version = binary[..3];
            var type = binary[3..6];
            var sum = Convert.ToInt32(version, 2);
            var index = 6;
            List<long> values = new List<long>();

            if (type == "100") // number
            {
                var num = "";
                string numPiece;

                do
                {
                    numPiece = binary[index..(index + 5)];
                    num += numPiece[1..];
                    index += 5;
                } while (numPiece.StartsWith('1'));


                var number = Convert.ToInt64(num, 2);
                values.Add(number);
            }
            else // operator
            {
                if (binary[6] == '0') // 15 bit subpacket length
                {
                    var subpacketLength = Convert.ToInt32(binary[7..22], 2);
                    index = 22;

                    while (index - 22 < subpacketLength)
                    {
                        var (s, i, n) = Process(binary[index..]);
                        sum += s;
                        index += i;
                        values.Add(n);
                    }
                }
                else // 11 bit subpacket count
                {
                    var subpacketCount = Convert.ToInt32(binary[7..18], 2);
                    index = 18;

                    for (int c = 0; c < subpacketCount; c++)
                    {
                        var (s, i, n) = Process(binary[index..]);
                        sum += s;
                        index += i;
                        values.Add(n);
                    }
                }
            }


            return (sum, index, Calculate(type, values));
        }


        private static long Calculate(string type, List<long> values) =>
            type switch
            {
                "000" => values.Sum(),
                "001" => values.Aggregate((p, c) => p * c),
                "010" => values.Min(),
                "011" => values.Max(),
                "100" => values.FirstOrDefault(),
                "101" => values[0] > values[1] ? 1 : 0,
                "110" => values[0] < values[1] ? 1 : 0,
                "111" => values[0] == values[1] ? 1: 0,
                _ => throw new Exception("Incorrect operator type!")
            };


        private static string ReadFile()
        {
            var file = new System.IO.StreamReader("input.txt");
            string line = file.ReadLine();
            file.Close();

            return line;
        }
    }
}
