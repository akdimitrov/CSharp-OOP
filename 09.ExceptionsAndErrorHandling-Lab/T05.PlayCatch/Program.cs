using System;
using System.Linq;

namespace T05.PlayCatch
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int caughtExceptions = 0;
            do
            {
                try
                {
                    string[] cmdArgs = Console.ReadLine().Split();
                    string command = cmdArgs[0];
                    int index = int.Parse(cmdArgs[1]);

                    if (command == "Replace")
                    {
                        int element = int.Parse(cmdArgs[2]);
                        nums[index] = element;
                    }
                    else if (command == "Print")
                    {
                        int endIndex = int.Parse(cmdArgs[2]);
                        Console.WriteLine(string.Join(", ", nums.ToList().GetRange(index, endIndex)));
                    }
                    else if (command == "Show")
                    {
                        Console.WriteLine(nums[index]);
                    }
                }
                catch (FormatException)
                {
                    caughtExceptions++;
                    Console.WriteLine("The variable is not in the correct format!");
                }
                catch (SystemException)
                {
                    caughtExceptions++;
                    Console.WriteLine("The index does not exist!");
                }

            } while (caughtExceptions < 3);

            Console.WriteLine(string.Join(", ", nums));
        }
    }
}
