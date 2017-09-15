using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [Range(8,19)]
        public int StartHour { get; set; }

        [Required]
        [Range(9,20)]
        public int EndHour { get; set; }

        [Required]
        [Range(1,59)]
        public int StartMinute { get; set; }

        [Required]
        [Range(1,59)]
        public int EndMinute { get; set; }

        [Required]
        public LessonType LessonType { get; set; }

        [Required]
        public Teacher Teacher { get; set; }
    }
}