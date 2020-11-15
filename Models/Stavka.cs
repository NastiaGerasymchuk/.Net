using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace LabLastGer8
{
    public partial class Stavka
    {
        public Stavka()
        {
            Lecturers = new HashSet<Lecturer>();
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Count")]
        [Required(ErrorMessage = "this filed needs to be installed ")]
       
        public float? Count { get; set; }

        public virtual ICollection<Lecturer> Lecturers { get; set; }
        
    }
}
