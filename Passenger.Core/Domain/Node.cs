using System;
using System.Text.RegularExpressions;

namespace Passenger.Core.Domain
{
    public class Node
    {
        public string Adress { get; protected set; }
        public double Longitude { get; protected set; }
        public double Latitude { get; protected set; }

        public DateTime Updateat { get; protected set; }

        private Regex AdressRegex = new Regex(@"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$");

        protected Node()
        {

        }

        protected Node(string adress, double longitude, double latitude)
        {
            SetAdress(adress);
            SetLongitude(longitude);
            Setlatitude(latitude);
        }

        private void SetAdress(string adress)
        {
            if (string.IsNullOrWhiteSpace(adress))
            {
                throw new Exception("Address can not be empty");
            }

            if (!AdressRegex.IsMatch(adress))
            {
                throw new Exception("The format of the address is not valid.");
            }

            Adress = adress;
            Updateat = DateTime.UtcNow;
        }

        private void SetLongitude(double longitude)
        {
            if (longitude < 180 && longitude > 0)
            {
                throw new Exception("Enter values in the range 0-180.");
            }
            if (Longitude == longitude)
            {
                return;
            }

            Longitude = longitude;
            Updateat = DateTime.UtcNow;
        }

        private void Setlatitude(double latitude)
        {
            if (latitude < 90 && latitude > 0)
            {
                throw new Exception("Enter values in the range 0-90.");
            }
            if (Latitude == latitude)
            {
                return;
            }

            Latitude = latitude;
            Updateat = DateTime.UtcNow;
        }


    }
}