using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceX
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] subroutine1 =
            {
                "MOV 5,R00",
                 "MOV 10,R01",
                 "JZ 7",
                 "ADD R02,R01",
                 "DEC R00",
                 "JMP 3",
                 "MOV R02,R42"
            };

            CpuEmulator.Emulate(subroutine1);

            Console.WriteLine("----------------------------------------------------");

            string[] systemNames =
            {
                "stage_1",
                "stage_2",
                "dragon",
                "stage_1",
                "stage_2",
                "dragon"
            };

            int[] stepNumbers = { 1, 10, 11, 2, 12, 111 };

            LaunchSequenceChecker.CheckSequence(systemNames, stepNumbers);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey(true);
        }
    }
}
