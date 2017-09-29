using Infrastructure.Models;

namespace SchedulerBot.FrontController.Interfaces
{
    public interface IDateTimeManager
    {
        WeekType GetCurrentWeekType();
        string GetNextDayName();
    }
}
