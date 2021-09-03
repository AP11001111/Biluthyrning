using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biluthyrning.Classes
{
    internal class CarRentalOffice
    {
        public string OfficeName { get; set; }
        public List<Car> Cars { get; set; }

        public int CarsAvailable
        {
            get
            {
                int carsAvailable = 0;
                foreach (Car c in Cars)
                {
                    if (c.CarAvailability)
                    {
                        carsAvailable++;
                    }
                }
                return carsAvailable;
            }
        }

        public long OfficeRevenue
        {
            get
            {
                long tempOfficeRevenue = 0;
                foreach (Car c in Cars) { tempOfficeRevenue += c.CarRevenue; };
                return tempOfficeRevenue;
            }
            set {; }
        }

        public CarRentalOffice(string officeName)
        {
            OfficeName = officeName;
            Cars = new List<Car>();
            OfficeRevenue = 0;
        }

        public CarRentalOffice(string officeName, List<Car> listOfCars)
        {
            OfficeName = officeName;
            Cars = listOfCars;
            OfficeRevenue = 0;
        }

        public void AddCar(Car car)
        {
            if (!Cars.Contains(car))
            {
                Cars.Add(car);
            }
        }

        public override string ToString()
        {
            string stringToReturn = $"{OfficeName}s intäkter: SEK {OfficeRevenue}\nAntal tillgängliga bilar: {CarsAvailable}\n\nAlla bilar: " + CarInCarsToString();
            return stringToReturn;
        }

        private string CarInCarsToString()
        {
            string stringToReturn = "";
            for (int i = 0; i < Cars.Count; i++)
            {
                stringToReturn += "\n\n" + $"Bilnummer: {i + 1}\n" + Cars[i].ToString();
            }
            //foreach (Car c in Cars) { stringToReturn += "\n\n" + c.ToString(); };
            return stringToReturn;
        }
    }
}