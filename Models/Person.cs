
using System;
using System.Collections.Generic;
using System.Text;

namespace lab1Lect
{
    class Person
    {
       
        public int Id{ get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String MiddleName { get; set; }
        public String PhoneNumber { get; set; }
        public int AddressId { get; set; }
        public override string ToString()
        {
            //return $"First name: {FirstName}, LastName {LastName}, Middle Name {MiddleName}, Phone Number {PhoneNumber}, Address {Connections.getData.getAddressById(AddressId, Connections.dataBase.GetSqlConnection())}";
            return $"First name: {FirstName}, LastName {LastName}, Middle Name {MiddleName}, Phone Number {PhoneNumber}";
        }

        public Person(int id,String firstName,String lastName,String middleName,String phoneNumber,int addressId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            PhoneNumber = phoneNumber;
            AddressId = addressId; 
        }
        public Person() { }
    }
}
