using System.Collections.Generic;

namespace Infrastructure.Models
{
    public class Week
    {
        public int Id { get; set; }
        public ISet<Day> Lessons { get; set; }
        public WeekType WeekType { get; set; }
    }
}