using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Biluthyrning.Classes
{
    internal class UI
    {
        private int[] AllUiOptions = { 0, 1, 2, 3 };

        private int[] AllEmployeeActions = { 0, 1, 2, 3 };

        public LiveTicker ALiveTicker { get; set; }
        public bool StopUI { get; set; }
        public bool RefreshLiveTicker { get; set; }
        public int ChosenOffice { get; set; }
        public int ChosenUI { get; set; }
        public int CarChosen { get; set; }
        public int CurrentDay { get; set; }
        public int ChosenAction { get; set; }
        public List<CarRentalOffice> CarRentalOffices { get; set; }

        public UI(List<CarRentalOffice> carRentalOffices)
        {
            CarRentalOffices = carRentalOffices;
            StopUI = false;
            RefreshLiveTicker = false;
        }

        //Common UI

        public void StartUI()
        {
            while (!StopUI)
            {
                ALiveTicker = new LiveTicker(CarRentalOffices[ChosenOffice]);
                WelcomeUI();
                SelectUI();
                switch (ChosenUI)
                {
                    case 1:
                        StartEmployeeUI();
                        break;

                    case 2:
                        StartCustomerUI();
                        break;

                    case 3:
                        RefreshLiveTicker = true;
                        while (ALiveTicker.StringToReturn != "N" || ALiveTicker.StringToReturn != "R")
                        {
                            switch (ALiveTicker.StringToReturn)
                            {
                                case "R":
                                    RefreshLiveTicker = false;
                                    RestartUI();
                                    break;

                                case "N":
                                    StopUI = true;
                                    RefreshLiveTicker = false;

                                    break;
                            }
                        }
                        break;
                }
            }
        }

        public void WelcomeUI()
        {
            int chosenOffice;
            Console.Clear();
            Console.WriteLine("Welcome to CSharp Car rental!\n");
            for (int i = 0; i < CarRentalOffices.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {CarRentalOffices[i].OfficeName}");
            }
            Console.WriteLine("\nInput the choice number");

            while (!(int.TryParse(Console.ReadLine(), out chosenOffice)
                && chosenOffice <= CarRentalOffices.Count
                && chosenOffice > 0))
            {
                Console.WriteLine("Invalid value. Choose the office again!");
            }
            ChosenOffice = chosenOffice - 1;
        }

        public void SelectUI()
        {
            int chosenUI;
            Console.Clear();
            Console.WriteLine($"Welcome to {CarRentalOffices[ChosenOffice].OfficeName}!");
            Console.WriteLine("\nLogin as:");
            Console.WriteLine("1: Employee");
            Console.WriteLine("2: Customer");
            Console.WriteLine("3: Show Live Ticker");
            Console.WriteLine("\nInput the choice number");
            Console.WriteLine("Input '0' to go to start");

            while (!(int.TryParse(Console.ReadLine(), out chosenUI)
                && (AllUiOptions.Contains(chosenUI))))
            {
                Console.WriteLine("Invalid input. Please choose again!");
            }
            if (chosenUI == 0)
            {
                RestartUI();
            }
            ChosenUI = chosenUI;
        }

        public void RestartUI()
        {
            ChosenOffice = 0;
            CarChosen = 0;
            ChosenUI = 0;
            StopUI = false;
            RefreshLiveTicker = false;
            StartUI();
        }

        //Customer UI

        public void StartCustomerUI()
        {
            OfficeUI();
            CarRentUI();
            ThankYouCustomerUI();
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

        public void ThankYouCustomerUI()
        {
            Console.Clear();
            Console.WriteLine("Thank you for choosing CSharp Car rental!");
            Console.WriteLine("\nUpdated car details after renting:\n");
            Console.WriteLine(CarRentalOffices[ChosenOffice].Cars[CarChosen]);
            Console.WriteLine("Day of car rent: " + CarRentalOffices[ChosenOffice].Cars[CarChosen].DayOfCarRent);
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
                        StartCustomerUI();
                        break;

                    case "R":
                        RestartUI();
                        break;

                    case "N":
                        flag = true;
                        StopUI = true;
                        ALiveTicker.StopUI = true;

                        break;
                }
            }
        }

        //Employee UI

        public void StartEmployeeUI()
        {
            Console.Clear();
            ChooseActionUI();
            switch (ChosenAction)
            {
                case 0:
                    RestartUI();
                    break;

                case 1:
                    Console.Clear();
                    Console.WriteLine(CarRentalOffices[ChosenOffice].OfficeName + " Revenue: SEK " + CarRentalOffices[ChosenOffice].OfficeRevenue);

                    break;

                case 2:
                    AddOfficeUI();
                    break;

                case 3:
                    AddCarUI();
                    break;
            }
            ThankYouEmployeeUI();
        }

        public void ChooseActionUI()
        {
            int chosenAction;
            Console.Clear();
            Console.WriteLine($"Welcome to {CarRentalOffices[ChosenOffice].OfficeName}!");
            Console.WriteLine("\nWhat will you do? ");
            Console.WriteLine("1: See the revenue");
            Console.WriteLine("2: Add an office");
            Console.WriteLine("3: Add a car");
            Console.WriteLine("Input '0' to go to start");
            while (!(int.TryParse(Console.ReadLine(), out chosenAction)
                && AllEmployeeActions.Contains(chosenAction)))
            {
                Console.WriteLine("Invalid input. Please choose again!");
                Console.WriteLine("Input '0' to go to start");
            }

            ChosenAction = chosenAction;
        }

        public void AddOfficeUI()
        {
            string officeName;
            string carMake;
            int carRentPerDay;
            int carOdometerReading;
            Console.Clear();
            Console.WriteLine($"Welcome to {CarRentalOffices[ChosenOffice].OfficeName}!");
            Console.WriteLine("\nAdd an office\n");
            Console.WriteLine("What is the office name?");
            officeName = Console.ReadLine();
            Console.WriteLine($"\nAdd {officeName}'s first car\n");
            Console.WriteLine("What is the car brand name?");
            carMake = Console.ReadLine();
            Console.WriteLine("\nWhat is the car rent per day?");
            while (!(int.TryParse(Console.ReadLine(), out carRentPerDay)
                && (carRentPerDay >= 0 || carRentPerDay <= 10000)))
            {
                Console.WriteLine("Invalid input. Please choose a value between 1 and 10000!");
                Console.WriteLine("Input '0' to go to start");
            }
            Console.WriteLine("\nWhat is the car's odometer reading?");
            while (!(int.TryParse(Console.ReadLine(), out carOdometerReading)
                && (carOdometerReading >= 0 || carOdometerReading <= 100000)))
            {
                Console.WriteLine("Invalid input. Please choose a value between 1 and 100000!");
                Console.WriteLine("Input '0' to go to start");
            }
            AddOffice(new CarRentalOffice(officeName, new Car(carMake, carRentPerDay, carOdometerReading)));
            Console.WriteLine($"\nThe new {officeName} has been created and it's first car was added successfully.");
            Thread.Sleep(1000);
        }

        public void AddCarUI()
        {
            string carMake;
            int carRentPerDay;
            int carOdometerReading;
            Console.Clear();
            Console.WriteLine($"Welcome to {CarRentalOffices[ChosenOffice].OfficeName}!");
            Console.WriteLine("\nAdd a car\n");
            Console.WriteLine("What is the car brand name?");
            carMake = Console.ReadLine();
            Console.WriteLine("\nWhat is the car rent per day?");
            while (!(int.TryParse(Console.ReadLine(), out carRentPerDay)
                && (carRentPerDay >= 0 || carRentPerDay <= 10000)))
            {
                Console.WriteLine("Invalid input. Please choose a value between 1 and 10000!");
                Console.WriteLine("Input '0' to go to start");
            }
            Console.WriteLine("\nWhat is the car's odometer reading?");
            while (!(int.TryParse(Console.ReadLine(), out carOdometerReading)
                && (carOdometerReading >= 0 || carOdometerReading <= 100000)))
            {
                Console.WriteLine("Invalid input. Please choose a value between 1 and 100000!");
                Console.WriteLine("Input '0' to go to start");
            }
            CarRentalOffices[ChosenOffice].AddCar(new Car(carMake, carRentPerDay, carOdometerReading));
            Console.WriteLine("\nThe new car was added successfully.");
            Thread.Sleep(1000);
        }

        public void ThankYouEmployeeUI()
        {
            bool flag = false;
            while (!flag)
            {
                Console.WriteLine("\nInput 'Y' to choose another action");
                Console.WriteLine("Input 'R' to go to start");
                Console.WriteLine("Input 'N' to close the program");

                string resetOrEnd = Console.ReadLine().ToUpper();
                switch (resetOrEnd)
                {
                    case "Y":
                        flag = true;
                        StartEmployeeUI();
                        break;

                    case "R":
                        flag = true;
                        RestartUI();
                        break;

                    case "N":
                        flag = true;
                        StopUI = true;
                        break;
                }
            }
        }

        //Operations

        public void AddOffice(CarRentalOffice anOffice)
        {
            CarRentalOffices.Add(anOffice);
        }

        public void UpdateCurrentDay(int currentDayFromCalender)
        {
            CurrentDay = currentDayFromCalender;
        }

        public void UpdateCarDaysToAvailability()
        {
            Parallel.ForEach(CarRentalOffices, office =>
            {
                Parallel.ForEach(office.Cars, car =>
                    {
                        if (!car.CarAvailability)
                        {
                            car.ReduceDaysUntilCarAvailable();
                        }
                    });
            });
        }
    }
}