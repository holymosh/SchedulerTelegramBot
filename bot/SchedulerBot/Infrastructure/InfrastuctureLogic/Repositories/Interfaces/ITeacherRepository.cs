using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Models;

namespace Infrastructure.InfrastuctureLogic.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        ITeacherRepository UseContext(ScheduleContext context);
        IEnumerable<Teacher> GetCurrentTeacher(string day, WeekType invertedWeekType, string studentId);
    }
}
