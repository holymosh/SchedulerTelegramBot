using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        [Range(8,19)]
        public int BeginHour { get; set; }

        [Range(9,20)]
        public int EndHour { get; set; }

        [Range(1,59)]
        public int BeginMinute { get; set; }

        [Range(1,59)]
        public int EndMinute { get; set; }

        public LessonType LessonType { get; set; }

        public Teacher Teacher { get; set; }
    }
}