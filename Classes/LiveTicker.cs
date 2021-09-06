using System;

namespace Biluthyrning.Classes
{
    internal class LiveTicker
    {
        public bool StopUI { get; set; }
        public string StringToReturn { get; set; }
        public CarRentalOffice Office { get; set; }

        public LiveTicker(CarRentalOffice office)
        {
            Office = office;
            StopUI = false;
        }

        public void StartUI(Calendar calendar, UI ui)
        {
            string stringToReturn = "";

            while (!StopUI)
            {
                Console.Clear();
                Console.WriteLine($"Welcome to {Office.OfficeName}!\n");
                Console.WriteLine(Office);
                Console.WriteLine("\nInput 'R' to go to start");
                Console.WriteLine("Input 'N' to close the program");
                if (Reader.TryReadLine(out stringToReturn, 2000))
                {
                    StringToReturn = stringToReturn.ToUpper();
                    switch (StringToReturn)
                    {
                        case "R":
                            StopUI = true;
                            break;

                        case "N":
                            StopUI = true;
                            break;
                    }
                }
                calendar.IncreaseCurrentDay();
                ui.UpdateCurrentDay(calendar.CurrentDay);
                ui.UpdateCarDaysToAvailability();
            }
        }
    }
}