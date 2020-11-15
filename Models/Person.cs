using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace LabLastGer8
{
    public partial class Person
    {
        public Person()
        {
            Lecturers = new HashSet<Lecturer>();
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "First name")]
        [Required(ErrorMessage = "this filed needs to be installed ")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "string length neds to be more or equal 3 and less than or equal 50 ")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field need to be installed")]
        [Display(Name = "Last name")]       
        [StringLength(50, MinimumLength = 3, ErrorMessage = "string length neds to be more or equal 3 and less than or equal 50 ")]
        public string LastName { get; set; }

        [Display(Name = "Middle name")]       
        [StringLength(50, MinimumLength = 3, ErrorMessage = "string length neds to be more or equal 3 and less than or equal 50 ")]
        public string MiddleName { get; set; }
        [Display(Name = "Phone number")]
        [RegularExpression(@"(^\+?3?8?(0\d{9})$)", ErrorMessage = "Incorrect phone number")]
        public string PhoneNumber { get; set; }
        public int AddressId { get; set; }

        [Display(Name = "Address")]
        public virtual Address Address { get; set; }
        public virtual ICollection<Lecturer> Lecturers { get; set; }
       
    }
}
