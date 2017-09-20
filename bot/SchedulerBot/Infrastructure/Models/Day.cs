using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Day
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(15),Required]
        public Schedule Schedule { get; set; }
        public string Name { get; set; }
        public ISet<Course> Lessons { get; set; }
    }
}