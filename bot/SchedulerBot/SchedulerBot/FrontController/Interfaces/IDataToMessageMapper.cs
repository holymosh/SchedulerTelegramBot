using System.Collections.Generic;
using Infrastructure.Models;

namespace SchedulerBot.FrontController.Interfaces
{
    public interface IDataToMessageMapper
    {
        string CreateMessageWithSchedule(IEnumerable<Course> courses);
        string CreateMessageFromTeacherData(Teacher teacher);
    }
}
