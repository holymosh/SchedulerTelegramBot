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

        public IEnumerable<Course> GetLessonsAtDay(string studentId, string day, WeekType invertedWeekType)
        {
            return _courses.Where(course => !course.WeekType.Equals(invertedWeekType) && course.Day.Name.Equals(day) &&
                                            course.Day.Schedule.Group.Students.Any(
                                                student => student.Id.Equals(studentId)));
        }

        public IEnumerable<Course> GetNextLessons(string studentId, string day, WeekType invertedWeekType)
        {
            return _courses.Where(course => !course.WeekType.Equals(invertedWeekType) && 
                                              course.Day.Name.Equals(day) && 
                                    (course.EndHour > DateTime.Now.Hour+3 || 
                               course.EndHour.Equals(DateTime.Now.Hour+3) && course.EndMinute > DateTime.Now.Minute));
        }
    }
}
