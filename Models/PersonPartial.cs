using System;
using System.Collections.Generic;

#nullable disable

namespace LabLastGer8
{
    public partial class Person
    {
        public override string ToString()
        {
            return $"First name:{FirstName}, Last name: {LastName}, Middle name: {MiddleName}, Phone number: {PhoneNumber}, Address: {Address}";
        }
    }
}
