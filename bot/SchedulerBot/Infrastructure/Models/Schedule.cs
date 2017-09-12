using System.Collections.Generic;

namespace Infrastructure.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public ISet<Week> Weeks { get; set; }
    }
}