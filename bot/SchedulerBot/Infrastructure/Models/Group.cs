using System.Collections.Generic;

namespace Infrastructure.Models
{
    public class Group
    {
        public int Id { get; set; }
        public ISet<Student> Students { get; set; }
        public Schedule Schedule { get; set; }
    }
}
