using System.Collections.Generic;

namespace Infrastructure.Models
{
    public class Day
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ISet<Course> Lessons { get; set; }
    }
}