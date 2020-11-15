
using System;
using System.Collections.Generic;

#nullable disable

namespace LabLastGer8
{
        public partial class Address
        {
            public override string ToString()
            {
                return $"Country: {Country}, city: {City}, street:{Street}, house number: {HouseNumber}";
            }
        }
    }
