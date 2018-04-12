using System;
using System.Collections.Generic;
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

        public Teacher GetCurrentTeacher(string day, WeekType invertedWeekType, string studentId ,Func<string,string,WeekType,IEnumerable<Course>> GetNextLessons)
        {
            return _teachers.SingleOrDefault(
                teacher => teacher.Id.Equals(GetNextLessons(studentId, day, invertedWeekType)
                    .OrderBy(course => course.StartHour).First().Teacher.Id));
        }

    }
}
