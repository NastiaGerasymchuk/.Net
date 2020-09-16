using System;
using System.Collections.Generic;
using System.Text;

namespace lab1Lect
{
    class PlaceWork
    {
        public int Id { get; set; }
        public String PlaceName { get; set; }
        public override string ToString()
        {
            return $"Place name {PlaceName}";
        }
        public PlaceWork(int id, String placeName)
        {
            Id = id;
            PlaceName = placeName;
        }
        public PlaceWork() { }
    }
}
