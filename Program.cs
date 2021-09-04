using Biluthyrning.Classes;
using System;
using System.Collections.Generic;

namespace Biluthyrning
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<CarRentalOffice> aListOfOffices = new List<CarRentalOffice>();
            List<Car> aListOfCars = new List<Car>
            {
                new Car("Volvo1",500,50000),
                new Car("Volvo2",700,10000),
                new Car("Volvo3",300,100000),
                new Car("Volvo4",550,40000),
                new Car("Volvo5",400,70000),
            };

            List<Car> anotherListOfCars = new List<Car>
            {
                new Car("Volvo1",500,50000),
                new Car("Volvo2",700,10000),
                new Car("Volvo3",300,100000),
                new Car("Volvo4",550,40000),
                new Car("Volvo5",400,70000),
            };

            CarRentalOffice StockholmOffice = new CarRentalOffice("Stockholm office", aListOfCars);
            CarRentalOffice GothenbergOffice = new CarRentalOffice("Göteborg office", anotherListOfCars);

            aListOfOffices.Add(StockholmOffice);
            aListOfOffices.Add(GothenbergOffice);

            UI uiForOffices = new UI(aListOfOffices);
            uiForOffices.AddOffice(new CarRentalOffice("Uppsala office", new Car("Volvo1", 1000, 1310)));

            StockholmOffice.AddCar(new Car("Volvo6", 900, 231));

            uiForOffices.StartUI();
            //Console.WriteLine(StockholmOffice);

            StockholmOffice.Cars[2].Rent(10, 1121);
            StockholmOffice.Cars[4].Rent(14, 2733);
            StockholmOffice.Cars[1].Rent(1, 632);
            StockholmOffice.Cars[2].Rent(3, 1321);

            //Console.WriteLine(StockholmOffice);
        }
    }
}