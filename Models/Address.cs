using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace LabLastGer8
{
    public partial class Address
    {
        public Address()  
        {
            People = new HashSet<Person>();
        }

        [HiddenInput(DisplayValue=false)]
        public int Id { get; set; } 
        
       
        [Display(Name="Country")]
        
        [StringLength(50, MinimumLength = 3, ErrorMessage = "string length neds to be more or equal 3 and less than or equal 50 ")]
        public string Country { get; set; }
        
        [Display(Name = "City")]
        [Required(ErrorMessage = "this filed needs to be installed ")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "string length neds to be more or equal 3 and less than or equal 50 ")]
        public string City { get; set; }
        [Display(Name = "Street")]       
        [StringLength(50, MinimumLength = 3, ErrorMessage = "string length neds to be more or equal 3 and less than or equal 50 ")]       
        public string Street { get; set; }

        [Display(Name = "House Number")]      
        public string HouseNumber { get; set; }

        public virtual ICollection<Person> People { get; set; }
        //public override string ToString()
        //{
        //    return $"Country: {Country}, city: {City}, street:{Street}, house number: {HouseNumber}";
        //}
    }
}
