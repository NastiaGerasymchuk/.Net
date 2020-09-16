using System;
using System.Collections.Generic;
using System.Text;

namespace lab1Lect
{
    class Subject
    {
        public int Id { get; set; }
        public String SubjName { get; set; }
        public int Hours { get; set; }
        public override string ToString()
        {
            return $"Subject name {SubjName}, Hours {Hours}";
        }
        public Subject (int id,String subjName,int hours=0)
        {
            Id = id;
            SubjName = subjName;
            Hours = hours;
        }
        public Subject() { }
    }
}
