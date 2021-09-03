using System;
using System.Collections.Generic;

namespace Biluthyrning.Classes
{
    internal class UI
    {
        public bool StopUI { get; set; }
        private int ChosenOffice { get; set; }
        private int CarChosen { get; set; }
        public List<CarRentalOffice> CarRentalOffices { get; set; }

        public UI()
        {
            CarRentalOffices = new List<CarRentalOffice>();
            StopUI = false;
        }

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
            while (!StopUI)
            {
                WelcomeUI();
                OfficeUI();
                CarRentUI();
                ThankYouUI();
            }
        }

        private void ThankYouUI()
        {
            Console.Clear();
            Console.WriteLine("Tack för att du valde CSharp biluthyrning!");
            Console.WriteLine("\nUppdaterade uppgifter för bilen efter inlämning:\n");
            Console.WriteLine(CarRentalOffices[ChosenOffice].Cars[CarChosen]);
            bool flag = false;
            while (!flag)
            {
                Console.WriteLine("\nSkriv 'Y' för att hyra en till bil");
                Console.WriteLine("Skriv 'R' för att starta om");
                Console.WriteLine("Skriv 'N' för att stänga programmet");

                string resetOrEnd = Console.ReadLine().ToUpper();
                switch (resetOrEnd)
                {
                    case "Y":
                        flag = true;
                        break;

                    case "R":
                        ResetUI();
                        break;

                    case "N":
                        flag = true;
                        StopUI = true;
                        break;
                }
            }
        }

        public void WelcomeUI()
        {
            int chosenOffice;
            Console.Clear();
            Console.WriteLine("Välkommen till CSharp Biluthyrning\n");
            for (int i = 0; i < CarRentalOffices.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {CarRentalOffices[i].OfficeName}");
            }
            Console.WriteLine("\nSkriv nummret brevid kontoret för att välja den");
            Console.WriteLine("Skriv '0' för att börja om");

            while (!(int.TryParse(Console.ReadLine(), out chosenOffice)
                && chosenOffice <= CarRentalOffices.Count
                && chosenOffice >= 0))
            {
                Console.WriteLine("Ogiltigt värde. Vänligen välj kontoret igen!");
                Console.WriteLine("Skriv '0' för att börja om");
            }
            if (chosenOffice == 0)
            {
                ResetUI();
            }
            ChosenOffice = chosenOffice - 1;
        }

        private void OfficeUI()
        {
            int carChosen;

            Console.Clear();
            Console.WriteLine($"Välkommen till {CarRentalOffices[ChosenOffice].OfficeName}!\n");
            Console.WriteLine(CarRentalOffices[ChosenOffice]);
            Console.WriteLine("\nSkriv bilnumret för att hyra den");
            Console.WriteLine("Skriv '0' för att börja om");
            while (!(int.TryParse(Console.ReadLine(), out carChosen)
                && carChosen <= CarRentalOffices[ChosenOffice].Cars.Count
                && carChosen >= 0
                && CarRentalOffices[ChosenOffice].Cars[carChosen - 1].CarAvailability))
            {
                Console.WriteLine("Ogiltigt värde. Vänligen välj bilen igen!");
                Console.WriteLine("Skriv '0' för att börja om");
            }
            if (carChosen == 0)
            {
                ResetUI();
            }
            CarChosen = carChosen - 1;
        }

        private void CarRentUI()
        {
            int carDaysRented;
            int carKmDriven;
            Console.Clear();
            Console.WriteLine(CarRentalOffices[ChosenOffice].Cars[CarChosen].ToString());
            Console.WriteLine("\nHur många dagar vill du hyra bilen för?");
            Console.WriteLine("Skriv '0' för att börja om");
            while (!(int.TryParse(Console.ReadLine(), out carDaysRented)
                && carDaysRented <= 30
                && carDaysRented >= 0))
            {
                Console.WriteLine("Ogiltigt värde. Vänligen välj mellan 1 och 30 dagar!");
                Console.WriteLine("Skriv '0' för att börja om");
            }
            if (carDaysRented == 0)
            {
                ResetUI();
            }
            Console.WriteLine("\nHur många km vill du köra?");
            Console.WriteLine("Skriv '0' för att börja om");
            while (!(int.TryParse(Console.ReadLine(), out carKmDriven)
                && carKmDriven <= 3000
                && carKmDriven >= 0))
            {
                Console.WriteLine("Ogiltigt värde. Vänligen välj mellan 1 och 3000!");
                Console.WriteLine("Skriv '0' för att börja om");
            }
            if (carKmDriven == 0)
            {
                ResetUI();
            }
            CarRentalOffices[ChosenOffice].Cars[CarChosen].Rent(carDaysRented, carKmDriven);
        }

        public void ResetUI()
        {
            foreach (CarRentalOffice office in CarRentalOffices)
            {
                for (int i = 0; i < office.Cars.Count; i++)
                {
                    office.Cars[i].CarAvailability = true;
                }
            }
            ChosenOffice = 0;
            CarChosen = 0;
            StartUI();
        }
    }
}