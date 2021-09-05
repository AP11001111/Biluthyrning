using Biluthyrning.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            Calendar calender = new Calendar();

            Parallel.Invoke(() =>
                            {
                                calender.StartCalender(uiForOffices);
                            },

                            () =>
                            {
                                uiForOffices.StartUI();
                            }
                        );
        }
    }
}