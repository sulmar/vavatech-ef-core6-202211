﻿namespace OwnedEntityTypes.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Address InvoiceAddress { get; set; }
        public Address ShipAddress { get; set; }

        public Coordinate Location { get; set; }


        public override string ToString() => $"{FirstName} {LastName} invoice: {InvoiceAddress} ship: {ShipAddress}";
    }

    public class Coordinate
    {
        public Coordinate(double latitude, double longitude, double altitude = 0)
        {
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public override string ToString() => $"{ZipCode} {Street} {City} {Country}";
    }
}
