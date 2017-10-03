using System;
using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.InfrastuctureLogic.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        ITeacherRepository UseContext(ScheduleContext context);
        Teacher GetCurrentTeacher(string day, WeekType invertedWeekType, string studentId, 
            Func<string, string, WeekType, IEnumerable<Course>> GetNextLessons);

    }
}
