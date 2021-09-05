using System;
using System.Collections.Generic;
using System.Text;

namespace Biluthyrning.Classes
{
    internal class LiveTicker
    {
        public string StringToReturn { get; set; }
        public CarRentalOffice Office { get; set; }

        public LiveTicker(CarRentalOffice office)
        {
            Office = office;
        }

        public string StartUI()
        {
            //bool reduceDays = true;

            string stringToReturn = "";
            bool flag = false;

            while (!flag)
            {
                //Console.Clear();
                Console.WriteLine($"Welcome to {Office.OfficeName}!\n");
                Console.WriteLine(Office);
                Console.WriteLine("\nInput 'R' to go to start");
                Console.WriteLine("Input 'N' to close the program");
                if (Reader.TryReadLine(out stringToReturn, 1500))
                {
                    StringToReturn = stringToReturn.ToUpper();
                }
                //stringToReturn = Console.ReadLine().ToUpper();
                switch (StringToReturn)
                {
                    case "R":
                        flag = true;

                        break;

                    case "N":
                        flag = true;
                        break;
                }
            }
            return StringToReturn;
        }
    }
}