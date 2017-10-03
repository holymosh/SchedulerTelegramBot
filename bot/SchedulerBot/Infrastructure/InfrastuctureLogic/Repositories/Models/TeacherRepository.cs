using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Infrastructure.InfrastuctureLogic.Repositories.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.InfrastuctureLogic.Repositories.Models
{
    public class TeacherRepository:ITeacherRepository
    {
        private DbSet<Teacher> _teachers;
        private Func<int> _saveChanges;
        public ITeacherRepository UseContext(ScheduleContext context)
        {
            _teachers = context.Teachers;
            _saveChanges = context.SaveChanges;
            return this;
        }

        public IEnumerable<Teacher> GetCurrentTeacher(string day, WeekType invertedWeekType, string studentId)
        {
            return _teachers.Where(teacher => teacher.Courses.Any(
                course =>  course.Day.Name.Equals(day) && !course.WeekType.Equals(invertedWeekType) &&
                          course.Day.Schedule.Group.Students
                              .Any(student => student.Id.Equals(studentId)) && (course.EndHour > DateTime.Now.Hour + 3
                    || course.EndHour.Equals(DateTime.Now.Hour + 3)
                    && course.EndMinute > DateTime.Now.Minute)));
        }

    }
}
