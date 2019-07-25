using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceX
{
    class CpuEmulator
    {
        public static string Emulate(string[] subroutine)
        {
            long[] registers = new long[43];
            long output;
            string[] parameters = { null, null };
            string[] firstSplit;
            string[] secondSplit;
            string action;
            string binary;
            string bin32;
            string invertedBits;
            string command;
            int regValue;

            for (int i = 0, j; i < subroutine.Length; i++)
            {
                command = subroutine[i];
                parameters[0] = null;
                parameters[1] = null;

                firstSplit = command.Split(' ');
                action = firstSplit[0];

                if (firstSplit.Length > 1)
                {
                    secondSplit = firstSplit[1].Split(',');
                    parameters[0] = secondSplit[0];

                    if (secondSplit.Length > 1)
                    {
                        parameters[1] = secondSplit[1];
                    }
                }

                switch (action)
                {
                    case "MOV":
                        if (!parameters[0][0].Equals('R'))
                        {
                            regValue = int.Parse(parameters[1].Substring(1));

                            try
                            {
                                registers[regValue] = long.Parse(parameters[0]);
                            }
                            catch (Exception) { }
                        }
                        else
                        {
                            regValue = int.Parse(parameters[1].Substring(1));
                            registers[regValue] = registers[int.Parse(parameters[0].Substring(1))];
                        }

                        break;

                    case "ADD":
                        regValue = int.Parse(parameters[0].Substring(1));
                        registers[regValue] += registers[long.Parse(parameters[1].Substring(1))];
                        registers[regValue] %= (long)Math.Pow(2, 32);
                        break;

                    case "DEC":
                        regValue = int.Parse(parameters[0].Substring(1));

                        if (registers[regValue] == 0)
                        {
                            registers[regValue] = (long)Math.Pow(2, 32) - 1;
                        }
                        else
                        {
                            registers[regValue]--;
                        }

                        break;

                    case "INC":
                        regValue = int.Parse(parameters[0].Substring(1));

                        if (registers[regValue] == Math.Pow(2, 32) - 1)
                        {
                            registers[regValue] = 0;
                        }
                        else
                        {
                            registers[regValue]++;
                        }

                        break;

                    case "INV":
                        regValue = int.Parse(parameters[0].Substring(1));
                        binary = Convert.ToString(registers[regValue], 2);
                        bin32 = "";
                        invertedBits = "";

                        for (j = 0; j < 32 - binary.Length; j++)
                        {
                            bin32 += "0";
                        }

                        bin32 += binary;

                        for (j = 0; j < 32; j++)
                        {
                            if (bin32[j].Equals('1'))
                            {
                                invertedBits += 0;
                            }
                            else
                            {
                                invertedBits += 1;
                            }
                        }

                        output = Convert.ToInt64(invertedBits, 2);
                        registers[regValue] = output;
                        break;

                    case "JMP":
                        i = int.Parse(parameters[0]) - 2;
                        break;

                    case "JZ":
                        if (registers[0] == 0)
                        {
                            i = int.Parse(parameters[0]) - 2;
                        }

                        break;

                    default:
                        break;
                }

            }

            Console.WriteLine("Result in R42: " + registers[42]);
            return registers[42].ToString();
        }
    }
}
