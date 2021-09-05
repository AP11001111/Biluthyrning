using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Biluthyrning.Classes
{
    internal class EmployeeUI : UI
    {
        private List<int> allActions;
        private int ChosenAction { get; set; }

        public EmployeeUI(List<CarRentalOffice> carRentalOffices) : base(carRentalOffices)
        {
            CarRentalOffices = carRentalOffices;
            allActions = new List<int> { 0, 1, 2, 3 };
        }

        public void StartUI()
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
            ThankYouUI();
        }

        private void ThankYouUI()
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
                        StartUI();
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

        private void ChooseActionUI()
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
                && allActions.Contains(chosenAction)))
            {
                Console.WriteLine("Invalid input. Please choose again!");
                Console.WriteLine("Input '0' to go to start");
            }

            ChosenAction = chosenAction;
        }

        private void AddCarUI()
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

        private void AddOfficeUI()
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
    }
}