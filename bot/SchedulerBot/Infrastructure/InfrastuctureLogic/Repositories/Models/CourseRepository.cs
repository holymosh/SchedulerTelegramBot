using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.InfrastuctureLogic.Repositories.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.InfrastuctureLogic.Repositories.Models
{
    public class CourseRepository:ICourseRepository
    {
        private DbSet<Course> _courses;
        private Func<int> _saveChanges;

        public ICourseRepository UseContext(ScheduleContext context)
        {
            _courses = context.Courses;
            _saveChanges = context.SaveChanges;
            return this;
        }

        public IEnumerable<Course> GetNextDayLessons(string studentId, string nextDay, WeekType type)
        {
            return _courses.Where(course => !course.WeekType.Equals(type) && course.Day.Name.Equals(nextDay) &&
                                            course.Day.Schedule.Group.Students.Any(
                                                student => student.Id.Equals(studentId)));
        }
    }
}
