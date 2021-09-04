using System;
using System.Collections.Generic;
using System.Linq;

namespace Biluthyrning.Classes
{
    internal class UI
    {
        private int[] allUiOptions = { 0, 1, 2, 3 };
        public CustomerUI ACustomerUI { get; set; }

        public EmployeeUI AnEmployeeUI { get; set; }
        public bool StopUI { get; set; }
        public int ChosenOffice { get; set; }
        public int ChosenUI { get; set; }
        public int CarChosen { get; set; }
        public List<CarRentalOffice> CarRentalOffices { get; set; }

        public UI(List<CarRentalOffice> carRentalOffices)
        {
            CarRentalOffices = carRentalOffices;
            StopUI = false;
        }

        public void AddOffice(CarRentalOffice anOffice)
        {
            CarRentalOffices.Add(anOffice);
        }

        public void StartUI()
        {
            AnEmployeeUI = new EmployeeUI(CarRentalOffices);
            ACustomerUI = new CustomerUI(CarRentalOffices);
            while (!StopUI)
            {
                WelcomeUI();
                SelectUI();
                switch (ChosenUI)
                {
                    case 1:
                        AnEmployeeUI.StartUI();
                        break;

                    case 2:
                        ACustomerUI.StartUI();
                        break;

                    case 3:
                        //Start live ticker
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
            //Console.WriteLine("Skriv '0' för att börja om");

            while (!(int.TryParse(Console.ReadLine(), out chosenOffice)
                && chosenOffice <= CarRentalOffices.Count
                && chosenOffice > 0))
            {
                Console.WriteLine("Invalid value. Choose the office again!");
                //Console.WriteLine("Skriv '0' för att börja om");
            }
            //if (chosenOffice == 0)
            //{
            //    ResetUI();
            //}
            ChosenOffice = chosenOffice - 1;
            ACustomerUI.ChosenOffice = ChosenOffice;
            AnEmployeeUI.ChosenOffice = ChosenOffice;
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
                && (allUiOptions.Contains(chosenUI))))
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
            //foreach (CarRentalOffice office in CarRentalOffices)
            //{
            //    for (int i = 0; i < office.Cars.Count; i++)
            //    {
            //        office.Cars[i].CarAvailability = true;
            //    }
            //}
            ChosenOffice = 0;
            CarChosen = 0;
            ChosenUI = 0;
            StartUI();
        }
    }
}