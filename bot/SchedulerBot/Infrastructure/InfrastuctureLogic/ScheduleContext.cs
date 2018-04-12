using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.InfrastuctureLogic
{
    public class ScheduleContext : DbContext
    {
        public ScheduleContext(DbContextOptions<ScheduleContext> contextOptions) : base(contextOptions)
        {
            Groups = Set<Group>();
            Days = Set<Day>();
            Courses = Set<Course>();
            Schedules = Set<Schedule>();
            Students = Set<Student>();
            Teachers = Set<Teacher>();
            var creator = Database.GetService<IRelationalDatabaseCreator>();
            creator.EnsureCreated();
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var mapper = new ModelMapper();
            mapper.MapGroup(builder.Entity<Group>())
                .MapSchedule(builder.Entity<Schedule>())
                .MapStudent(builder.Entity<Student>())
                .MapDay(builder.Entity<Day>())
                .MapCourse(builder.Entity<Course>())
                .MapTeacher(builder.Entity<Teacher>())
                ;
            base.OnModelCreating(builder);
        }

    }
}