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
        public int DaysCarRentedFor { get; set; }
        public int DayOfCarRent { get; set; }
        public bool CarAvailability { get; set; }
        public int DaysUntilCarAvailable { get; set; }

        public Car(string carMake, int rentPerDay, int odometerReading)
        {
            CarMake = carMake;
            RentPerDay = rentPerDay;
            OdometerReading = odometerReading;
            CarRevenue = 0;
            CarAvailability = true;
            DaysUntilCarAvailable = 0;
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
                availabilityString = $"Unavailable\nAvailable in: {DaysUntilCarAvailable} days";
            }

            string stringToReturn = $"Car make: {CarMake}\nRent: SEK {RentPerDay} per dag\nMileage: {OdometerReading}\nAvailability: {availabilityString}";
            return stringToReturn;
        }

        public void Rent(int dayOfCarRent, int daysRented, int kmDriven)
        {
            DayOfCarRent = dayOfCarRent;
            DaysCarRentedFor = daysRented;
            DaysUntilCarAvailable = daysRented;
            CarRevenue = daysRented * this.RentPerDay;
            OdometerReading += kmDriven;
            CarAvailability = false;
        }

        public void ReduceDaysUntilCarAvailable()
        {
            if (DaysUntilCarAvailable == 1)
            {
                CarAvailability = true;
                DaysCarRentedFor = 0;
            }
            DaysUntilCarAvailable--;
            Console.WriteLine(DaysUntilCarAvailable);
        }
    }
}