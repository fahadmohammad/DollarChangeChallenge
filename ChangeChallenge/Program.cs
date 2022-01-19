using System;
using System.Collections.Generic;
using System.Linq;

namespace ChangeChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var denomonations = new List<DenomonationInfo> {
                new DenomonationInfo
                {
                    Amount = 0.05M,
                    Name = "Nickle"
                }, 
                new DenomonationInfo
                {
                    Amount = 0.25M,
                    Name = "Quarter"
                },
                new DenomonationInfo
                {
                    Amount = 0.10M,
                    Name = "Dime"
                },
                new DenomonationInfo
                {
                    Amount = 0.01M,
                    Name = "Penny"
                },
                new DenomonationInfo
                {
                    Name = "One Dollar Bill", 
                    Amount = 1M
                },
                new DenomonationInfo
                {
                    Name = "Five Dollar Bill", 
                    Amount = 5M
                },
                new DenomonationInfo
                {
                    Name = "Ten Dollar Bill", 
                    Amount = 10M
                }

            };

            Console.WriteLine("Please enter the amount owed: ");
            decimal amountOwed = decimal.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            Console.WriteLine("Please enter the amount paid: ");
            decimal amountPaid = decimal.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            List<string> results = CalculateChange(amountOwed, amountPaid, denomonations);

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }

            Console.ReadLine();
        }

        private static List<string> CalculateChange(decimal amountOwed, decimal amountPaid, List<DenomonationInfo> denomonationInfos)
        {
            List<string> output = new List<string>();

            decimal change = amountPaid - amountOwed;

            if (change < 0)
            {
                output.Add("You need to pay more");
            }
            else if (change == 0)
            {
                output.Add("No change required");
            }
            else
            {
                int count = 0;
                foreach (var denomonationInfo in denomonationInfos.OrderByDescending(x => x.Amount))
                {
                    (change, count) = CalculateSpecificChange(change, denomonationInfo.Amount);

                    if (count > 0)
                    {
                        output.Add($"{count} {denomonationInfo.Name}");
                    }

                    if (change <= 0)
                        break;
                }
            }

            return output;
        }

        private static (decimal remainder, int noOfItems) CalculateSpecificChange(decimal amount, decimal denomination)
        {
            (decimal remainder, int noOfItems) output = (0, 0);

            output.noOfItems = (int)Math.Floor(amount / denomination);
            output.remainder = amount - (output.noOfItems * denomination);

            return output;
        }
    }
}
