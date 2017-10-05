using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.InfrastuctureLogic.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        ICourseRepository UseContext(ScheduleContext context);
        IEnumerable<Course> GetLessonsAtDay(string studentId,string day,WeekType invertedWeekType);
        IEnumerable<Course> GetNextLessons(string studentId, string day, WeekType invertedWeekType);
    }
}
