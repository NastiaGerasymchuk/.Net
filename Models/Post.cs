using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace LabLastGer8
{
    public partial class Post
    {
        public Post()
        {
            Lecturers = new HashSet<Lecturer>();
        }
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Post name")]
        [Required(ErrorMessage = "this filed needs to be installed ")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "string length neds to be more or equal 3 and less than or equal 50 ")]
       // public string PlaceName { get; set; }
        public string PostName { get; set; }

        public virtual ICollection<Lecturer> Lecturers { get; set; }
        
    }
}
