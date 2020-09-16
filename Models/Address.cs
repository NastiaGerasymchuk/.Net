using System;
using System.Collections.Generic;
using System.Text;

namespace lab1Lect
{
    class Address
    {
        public int Id { get; set; }
        public String Country { get; set; }
        public String City { get; set; }
        public String Street { get; set; }
        public String HouseNumber { get; set; }
        public override string ToString()
        {
            return $"Address: Country {Country},City: {City},Street: {Street},House number: {HouseNumber}.";
            
        }
        public Address(String country,String city,String street,String houseNumber,int id)
        {
            Country = country;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            Id = id;
        }
        public Address() : this("country", "city", "street", "houseNumber", 0) { }
    }
}
