using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace LabLastGer8
{
    public partial class Lecturer
    {
        public Lecturer()
        {
            SubLecturers = new HashSet<SubLecturer>();
        }
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Person")]
        public int PersonId { get; set; }
        [Required]
        [Display(Name = "Place name")]
        public int PlaceWorkId { get; set; }
        [Required]
        [Display(Name = "Count")]
        public int StavkaId { get; set; }
        [Required]
        [Display(Name = "Post name")]
        public int PostId { get; set; }
        [Required]
        public string Characteristic { get; set; }
        [Required]
        public virtual Person Person { get; set; }
        [Required]
        public virtual PlacesWork PlaceWork { get; set; }
        [Required]
        public virtual Post Post { get; set; }
        [Required]
        public virtual Stavka Stavka { get; set; }

        public virtual ICollection<SubLecturer> SubLecturers { get; set; }
       
    }
}
