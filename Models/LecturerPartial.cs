
using System;
using System.Collections.Generic;

#nullable disable

namespace LabLastGer8
{
    public partial class Lecturer
    {
        public override string ToString()
        {
           
           string res= $"{Person}, Place work: {PlaceWork}, Stavka: {Stavka}, Post: {Post} teach such subjects as:\n";
            foreach(SubLecturer subLecturer in SubLecturers)
            {
                res += $" {subLecturer}\n";
            }
            return res;
        }
    }
}
