using System.Collections.Generic;
using Infrastructure.Models;

namespace SchedulerBot.FrontController.Interfaces
{
    public interface IScheduleMapper
    {
        string DisplayScheduleForNextDay(IEnumerable<Course> courses);
    }
}
