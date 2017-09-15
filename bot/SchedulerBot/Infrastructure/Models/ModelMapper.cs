using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Models
{
    public class ModelMapper
    {
        public ModelMapper MapGroup(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(group => group.Id);
            builder.Property(group => group.Name).HasField("Group");
            builder.HasMany(typeof(Student), "Student").WithOne();
            builder.HasOne(typeof(Schedule), "Scheduler").WithOne();
            return this;
        }

        public ModelMapper MapStudent(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(student => student.Id);
            builder.Property(student => student.FirstName).HasField("FirstName");
            builder.Property(student => student.LastName).HasField("LastName");
            builder.Property(student => student.IsAdmin).HasField("IsAdmin");
            return this;
        }

        public ModelMapper MapSchedule(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasKey(schedule => schedule.Id);
            builder.HasMany(typeof(Week), "Week").WithOne();
            return this;
        }

        public ModelMapper MapWeek(EntityTypeBuilder<Week> builder)
        {
            builder.HasKey(week => week.Id);
            builder.HasMany(typeof(Day), "Day").WithOne();
            builder.Property(week => week.WeekType).HasField("WeekType");
            return this;
        }

        public ModelMapper MapDay(EntityTypeBuilder<Day> builder)
        {
            builder.HasKey(day => day.Id);
            builder.Property(day => day.Name).HasField("Day");
            builder.HasMany(typeof(Course), "Course").WithOne();
            return this;
        }

        public ModelMapper MapCourse(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(course => course.Id);
            builder.Property(course => course.Name).HasField("Name");
            builder.Property(course => course.StartHour).HasField("StartHour");
            builder.Property(course => course.EndHour).HasField("EndHour");
            builder.Property(course => course.StartMinute).HasField("StartMinute");
            builder.Property(course => course.EndMinute).HasField("EndMinute");
            builder.Property(course => course.LessonType).HasField("LessonType");
            builder.Property(course => course.Location).HasField("Location");
            builder.HasOne(typeof(Teacher), "Teacher").WithOne();
            return this;
        }

        public ModelMapper MapTeacher(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(teacher => teacher.Id);
            builder.Property(teacher => teacher.Name).HasField("Name");
            builder.Property(teacher => teacher.Surname).HasField("Surname");
            builder.Property(teacher => teacher.FatherName).HasField("FatherName");
            return this;
        }
    }
}
