using System;

namespace Passenger.Core.Domain
{
    public class Vehicle
    {
        public string Brand { get; protected set; }
        public string Name { get; protected set; }
        public int Seats { get; protected set; }

        public Vehicle(string brand, string name, int seats)
        {
            SetBrand(brand);
            SetName(name);
            SetSeats(seats);
        }

        private void SetSeats(int seats)
        {
            if (seats < 1)
            {
                throw new Exception("You must have more extra seats in the car.");
            }

            if (seats > 4)
            {
                throw new Exception("You can only use a passenger car to transport passengers.");
            }

            Seats = seats;
        }

        private void SetName(string name)
        {
            if (name == null)
            {
                throw new Exception("You didn't enter a car name.");
            }
            
                Name = name;
        }

        private void SetBrand(string brand)
        {
            if (brand == null)
            {
                throw new Exception("You didn't specify the car brand.");
            }

                Brand = brand;
        }

        protected Vehicle()
        {
        }
    }
}