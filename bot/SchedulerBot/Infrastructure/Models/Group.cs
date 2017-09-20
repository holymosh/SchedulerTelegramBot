using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(10),Required]
        public string Name { get; set; }

        public ISet<Student> Students { get; set; }

        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
    }
}
