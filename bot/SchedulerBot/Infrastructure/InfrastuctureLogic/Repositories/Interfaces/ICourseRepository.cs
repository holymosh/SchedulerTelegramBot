using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.InfrastuctureLogic.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        ICourseRepository UseContext(ScheduleContext context);
        IEnumerable<Course> GetNextDayLessons(string studentId,string nextDay,WeekType type);
    }
}
