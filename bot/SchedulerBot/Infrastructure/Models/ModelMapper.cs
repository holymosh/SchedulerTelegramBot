using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Models
{
    public class ModelMapper
    {
        public ModelMapper MapGroup(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Group");
            builder.HasKey(group => group.Id);
            builder.Property(group => group.Name);
            builder.HasMany(group => group.Students).WithOne().HasForeignKey(student => student.GroupId);
            builder.HasOne(group => group.Schedule).WithOne(schedule => schedule.Group);
            return this;
        }

        public ModelMapper MapStudent(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");
            builder.HasKey(student => student.Id);
            builder.Property(student => student.FirstName);
            builder.Property(student => student.LastName);
            builder.Property(student => student.IsAdmin);
            return this;
        }

        public ModelMapper MapSchedule(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("Schedule");
            builder.HasKey(schedule => schedule.Id);
            //builder.HasOne(schedule => schedule.Group).WithOne(group => group.Schedule);
            builder.HasMany(schedule => schedule.Days).WithOne(day => day.Schedule);
            return this;
        }


        public ModelMapper MapDay(EntityTypeBuilder<Day> builder)
        {
            builder.ToTable("Day");
            builder.HasKey(day => day.Id);
            builder.Property(day => day.Name);
            builder.HasMany(day=> day.Lessons).WithOne(course => course.Day);
            return this;
        }

        public ModelMapper MapCourse(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Course");
            builder.HasKey(course => course.Id);
            builder.Property(course => course.Name);
            builder.Property(course => course.StartHour);
            builder.Property(course => course.EndHour);
            builder.Property(course => course.StartMinute);
            builder.Property(course => course.EndMinute);
            builder.Property(course => course.LessonType);
            builder.Property(course => course.Location);
            //builder.HasOne(course => course.Day).WithMany(day => day.Lessons);
            builder.HasOne(course => course.Teacher).WithMany(teacher => teacher.Courses);
            return this;
        }

        public ModelMapper MapTeacher(EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable("Teacher");
            builder.HasKey(teacher => teacher.Id);
            builder.Property(teacher => teacher.Name);
            builder.Property(teacher => teacher.Surname);
            builder.Property(teacher => teacher.FatherName);
            //builder.HasMany(teacher => teacher.Courses).WithOne(course => course.Teacher);
            return this;
        }
    }
}
