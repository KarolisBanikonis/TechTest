using System;
using System.Collections.Generic;

namespace LaneSelection
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                //Read input
                var destinationsInput = ReadInput("Set up the number of available destinations (0-n):");
                var hasParsed = Int32.TryParse(destinationsInput, out int destinations);
                if (!hasParsed)
                {
                    Console.WriteLine("You must write integer number!");
                }
                var strategy = ReadInput("Choose the destination selection strategy: 1 - Round robin, 2 - Random.");
                if(strategy != "1" && strategy != "2")
                {
                    Console.WriteLine("You must write strategy number(1 or 2).");
                    continue;
                }
                var consecutiveCountInput = ReadInput("Choose consecutive load number:");
                hasParsed = Int32.TryParse(consecutiveCountInput, out int consecutiveCount);
                if (!hasParsed)
                {
                    Console.WriteLine("You must write integer number!");
                }
                var failurePercentageInput = ReadInput("Choose percentage failure (0-100)%:");
                hasParsed = Int32.TryParse(failurePercentageInput, out int failurePercentage);
                if (!hasParsed)
                {
                    Console.WriteLine("You must write integer number!");
                    continue;
                }
                else
                {
                    if(failurePercentage < 0 || failurePercentage > 100)
                    {
                        Console.WriteLine("Percentages value must be between 0-100.");
                        continue;
                    }
                }
                var autoLoadCountInput = ReadInput("Choose the number of loads:");
                hasParsed = Int32.TryParse(autoLoadCountInput, out int autoLoadCount);
                if (!hasParsed)
                {
                    Console.WriteLine("You must write integer number!");
                    continue;
                }
                //Execution
                int allLoad = autoLoadCount + consecutiveCount;
                int failureCount = Convert.ToInt32(failurePercentage / 100.0 * allLoad);
                Dictionary<int, Destination> dictionary = new Dictionary<int, Destination>();
                dictionary.TryAdd(0, new Destination(0)); //0 Lane always exist
                Random random = new Random();
                int index = 1;
                int randIndex = random.Next(1, destinations + 1);
                for (int i = 0; i < allLoad; i++)
                {
                    if(strategy == "1")//Round robin
                    {
                        if(index == destinations + 1 && consecutiveCount <= 0)
                        {
                            index = 1;
                        }
                        dictionary.TryAdd(index, new Destination(index));
                        if (consecutiveCount > 0)
                        {
                            consecutiveCount--;
                        }
                        if(failureCount > 0)
                        {
                            dictionary[0].IncreaseReachedLoadCount();
                            dictionary[index].IncreaseTotalLoadCount();
                            failureCount--;
                        }
                        else
                        {
                            dictionary[index].IncreaseReachedLoadCount();
                        }
                        if(consecutiveCount <= 0)
                        {
                            index++;
                        }
                    }
                    else//Random
                    {
                        if (consecutiveCount <= 0)
                        {
                            randIndex = random.Next(1, destinations + 1);
                        }
                        dictionary.TryAdd(randIndex, new Destination(randIndex));
                        if (consecutiveCount > 0)
                        {
                            consecutiveCount--;
                        }
                        if (failureCount > 0)
                        {
                            dictionary[0].IncreaseReachedLoadCount();
                            dictionary[randIndex].IncreaseTotalLoadCount();
                            failureCount--;
                        }
                        else
                        {
                            dictionary[randIndex].IncreaseReachedLoadCount();
                        }
                    }
                }
                foreach (var item in dictionary.Values)
                {
                    Console.WriteLine("Destination Nr. " + item.GetNumber() + " reached " + item.CalculateReachedPercentage() + "% of loads. Total: " + item.GetTotalLoadCount() + " Reached: " + item.GetReachedLoadCount());
                }
            }
        }

        static string ReadInput(string text)
        {
            Console.WriteLine(text);
            string input = Console.ReadLine();
            return input;
        }
    }
}
