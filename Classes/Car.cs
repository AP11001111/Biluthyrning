using System;
using System.Collections.Generic;
using System.Text;

namespace Biluthyrning.Classes
{
    internal class Car
    {
        public string CarMake { get; set; }
        public int RentPerDay { get; set; }
        public int OdometerReading { get; set; }
        public long CarRevenue { get; set; }
        public bool CarAvailability { get; set; }

        public Car(string carMake, int rentPerDay, int odometerReading)
        {
            CarMake = carMake;
            RentPerDay = rentPerDay;
            OdometerReading = odometerReading;
            CarRevenue = 0;
            CarAvailability = true;
        }

        public override string ToString()
        {
            string availabilityString;
            if (CarAvailability)
            {
                availabilityString = "Available";
            }
            else
            {
                availabilityString = "Unavailable";
            }

            string stringToReturn = $"Car make: {CarMake}\nRent: SEK {RentPerDay} per dag\nMileage: {OdometerReading}\nAvailability: {availabilityString}";
            return stringToReturn;
        }

        public void Rent(int daysRented, int kmDriven)
        {
            CarRevenue = daysRented * this.RentPerDay;
            OdometerReading += kmDriven;
            CarAvailability = false;
        }
    }
}