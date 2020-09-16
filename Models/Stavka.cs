using System;
using System.Collections.Generic;
using System.Text;

namespace lab1Lect
{
    class Stavka
    {
        public int Id { get; set; }
        public double Count { get; set; }
        public override string ToString()
        {
            return $"Count: {Count}";
        }
        public Stavka(int id, double count)
        {
            Id = id;
            Count = count;
        }
        public Stavka() { }
    }
}
