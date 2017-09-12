﻿using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Models
{
    public class ScheduleContext: DbContext
    {
        public ScheduleContext(DbContextOptions<ScheduleContext> contextOptions) : base(contextOptions)
        {
            
        }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Week> Weeks { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }



    }
}