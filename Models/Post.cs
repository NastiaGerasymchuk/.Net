using System;
using System.Collections.Generic;
using System.Text;

namespace lab1Lect
{
    class Post
    {
        public int Id { get; set; }
        public String PostName { get; set; }
        public override string ToString()
        {
            return $"Post name: {PostName}";
        }
        public Post(int id,String postName)
        {
            Id = id;
            PostName = postName;
        }
        public Post() { }
    }
}
