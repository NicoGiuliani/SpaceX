using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceX
{
    class LaunchSequenceChecker
    {
        public static bool CheckSequence(string[] systemNames, int[] stepNumbers)
        {
            Dictionary<string, int> map = new Dictionary<string, int> { };
            bool result;

            for (int i = 0; i < systemNames.Length; i++)
            {
                if (!map.ContainsKey(systemNames[i]))
                {
                    map.Add(systemNames[i], stepNumbers[i]);
                }
                else
                {
                    if (stepNumbers[i] <= map[systemNames[i]])
                    {
                        result = false;
                        break;
                    }

                    map[systemNames[i]] = stepNumbers[i];
                }
            }

            result = true;
            string message = (result == true) ? "Valid launch sequence verified." : "Invalid launch sequence detected.";
            Console.WriteLine(message);
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey(true);
            return result;
        }
    }
}
