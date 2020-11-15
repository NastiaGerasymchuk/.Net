using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace LabLastGer8
{
    public partial class Subject
    {
        public Subject()
        {
            SubLecturers = new HashSet<SubLecturer>();
        }
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Subject name")]
        [Required(ErrorMessage = "this filed needs to be installed ")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "string length neds to be more or equal 3 and less than or equal 50 ")]
        public string SubjName { get; set; }

        public virtual ICollection<SubLecturer> SubLecturers { get; set; }
       
    }
}
