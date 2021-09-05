using System;
using System.Collections.Generic;
using System.Threading;

namespace Biluthyrning.Classes
{
    internal class CustomerUI : UI
    {
        private readonly object CurrentDayLock = new object();

        public CustomerUI(List<CarRentalOffice> carRentalOffices) : base(carRentalOffices)
        {
            CarRentalOffices = carRentalOffices;
        }

        public void StartUI()
        {
            OfficeUI();
            CarRentUI();
            ThankYouUI();
        }

        public void OfficeUI()
        {
            int carChosen;

            Console.Clear();
            Console.WriteLine($"Welcome to {CarRentalOffices[ChosenOffice].OfficeName}!\n");
            Console.WriteLine(CarRentalOffices[ChosenOffice]);
            Console.WriteLine("\nInput car number to rent it");
            Console.WriteLine("Input '0' to go to start");
            while (!(int.TryParse(Console.ReadLine(), out carChosen)
                && (carChosen == 0 ?
                true :
                carChosen <= CarRentalOffices[ChosenOffice].Cars.Count
                && carChosen >= 0
                && CarRentalOffices[ChosenOffice].Cars[carChosen - 1].CarAvailability)))
            {
                Console.WriteLine("Invalid value. Choose the car again!");
                Console.WriteLine("Input '0' to go to start");
            }
            if (carChosen == 0)
            {
                RestartUI();
            }
            CarChosen = carChosen - 1;
        }

        public void CarRentUI()
        {
            int carDaysRented;
            int carKmDriven;
            Console.Clear();
            Console.WriteLine(CarRentalOffices[ChosenOffice].Cars[CarChosen].ToString());
            Console.WriteLine("\nHow many days do you want to rent the car for?");
            Console.WriteLine("Input '0' to go to start");
            while (!(int.TryParse(Console.ReadLine(), out carDaysRented)
                && carDaysRented <= 30
                && carDaysRented >= 0))
            {
                Console.WriteLine("Invalid input. Choose between 1 and 30 days!");
                Console.WriteLine("Input '0' to go to start");
            }
            if (carDaysRented == 0)
            {
                RestartUI();
            }
            Console.WriteLine("\nHur många km vill du köra?");
            Console.WriteLine("Input '0' to go to start");
            while (!(int.TryParse(Console.ReadLine(), out carKmDriven)
                && carKmDriven <= 3000
                && carKmDriven >= 0))
            {
                Console.WriteLine("Invalid input. Choose between 1 and 3000 Kms!");
                Console.WriteLine("Input '0' to go to start");
            }
            if (carKmDriven == 0)
            {
                RestartUI();
            }
            //lock (CurrentDayLock)
            //{
            CarRentalOffices[ChosenOffice].Cars[CarChosen].Rent(CurrentDay, carDaysRented, carKmDriven);
            //}
        }

        public void ThankYouUI()
        {
            Console.Clear();
            Console.WriteLine("Thank you for choosing CSharp Car rental!");
            Console.WriteLine("\nUpdated car details after renting:\n");
            Console.WriteLine(CarRentalOffices[ChosenOffice].Cars[CarChosen]);
            Console.WriteLine("Day of car rent: " + CarRentalOffices[ChosenOffice].Cars[CarChosen].DayOfCarRent);
            Console.WriteLine("Car rented for: " + CarRentalOffices[ChosenOffice].Cars[CarChosen].DaysCarRentedFor + " days");
            bool flag = false;
            while (!flag)
            {
                Console.WriteLine("\nInput 'Y' to rent another car");
                Console.WriteLine("Input 'R' to go to start");
                Console.WriteLine("Input 'N' to close the program");

                string resetOrEnd = Console.ReadLine().ToUpper();
                switch (resetOrEnd)
                {
                    case "Y":
                        StartUI();
                        break;

                    case "R":
                        RestartUI();
                        break;

                    case "N":
                        flag = true;
                        StopUI = true;
                        break;
                }
            }
        }
    }
}