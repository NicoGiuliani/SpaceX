using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceX
{
    class PacketDescrambler
    {
        public static string Descramble(int[] seq, char[] fragmentData, int n)
        {
            // if the provided sequence is not sequential, return an empty string
            int[] q = seq.Distinct().ToArray();
            if (q.Max() - q.Min() + 1 != q.Length)
            {
                return "";
            }

            SortedDictionary<int, List<char>> map = new SortedDictionary<int, List<char>> { };
            string output = "";
            bool packetLost;

            // build a sorted dictionary of the sequence numbers and their corresponding data
            for (int i = 0; i < seq.Length; i++)
            {
                if (!map.ContainsKey(seq[i]))
                {
                    map.Add(seq[i], new List<char> { });
                }
                map[seq[i]].Add(fragmentData[i]);
            }


            Dictionary<char, int> occurrencePerChar;

            // build a dictionary of the data and the number of times each character occurs
            foreach (KeyValuePair<int, List<char>> entry in map)
            {
                occurrencePerChar = new Dictionary<char, int> { };
                packetLost = true;

                foreach (char x in entry.Value)
                {
                    if (!occurrencePerChar.ContainsKey(x))
                    {
                        occurrencePerChar.Add(x, 1);
                    }
                    else
                    {
                        occurrencePerChar[x]++;
                    }
                }

                // adds the char to the output string if it occurs more than 50% of the time for that sequence number
                foreach (KeyValuePair<char, int> y in occurrencePerChar)
                {
                    if (y.Value > (n / 2))
                    {
                        output += y.Key;
                        packetLost = false;
                    }
                }

                // if a packet has been lost, return an empty string
                if (packetLost)
                {
                    return "";
                }

            }

            // if a # occurs anywhere but the last character of output, return an empty string
            for (int i = 0; i < output.Length - 1; i++)
            {
                if (output[i].Equals('#'))
                {
                    return "";
                }
            }

            Console.WriteLine("Descrambled data: {0}\n", output);
            Console.Write("Press any key to continue. ");
            Console.ReadKey(true);
            return output;
        }

    }
}
