using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace Infrastructure.Models
{
    [Table("Student")]
    public class Student
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [MaxLength(50),Required]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }

        [DefaultValue(false)]
        public bool IsAdmin { get; set; }
        public int GroupId { get; set; }

        public Student(string id, string firstName, string lastName, bool isAdmin, int groupId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            IsAdmin = isAdmin;
            GroupId = groupId;
        }

        public Student()
        {
            
        }

        public Student(string id, string firstName, string lastName, bool isAdmin)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            IsAdmin = isAdmin;
        }
    }
}