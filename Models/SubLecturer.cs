using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace LabLastGer8
{
    public partial class SubLecturer
    {
        public int Id { get; set; }
        [Display(Name = "Lecturer")]
        public int LecturerId { get; set; }
        [Display(Name = "Subject")]
        public int SubjectId { get; set; }
        [Display(Name = "Count Hours")]
        public int Hours { get; set; }

        public virtual Lecturer Lecturer { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
