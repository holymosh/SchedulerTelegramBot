using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    [Table("Teacher")]
    public class Teacher
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50),Required]
        public string Name { get; set; }

        [MaxLength(50),Required]
        public string Surname { get; set; }

        [MaxLength(50),Required]
        public string FatherName { get; set; }
        
        public ISet<Course> Courses { get; set; }
    }
}