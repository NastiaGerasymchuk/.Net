using System;
using System.Collections.Generic;

#nullable disable

namespace LabLastGer8 { 
    public partial class SubLecturer
    {
        //public int Id { get; set; }
        //public int? LecturerId { get; set; }
        //public int? SubjectId { get; set; }
        //public int? Hours { get; set; }

        //public virtual Lecturer Lecturer { get; set; }
        //public virtual Subject Subject { get; set; }
        public override string ToString()
        {
            return $"{Subject}, Hours:{Hours}";
        }
    }
}
