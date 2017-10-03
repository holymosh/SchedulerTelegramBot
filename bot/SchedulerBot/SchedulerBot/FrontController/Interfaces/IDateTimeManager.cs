using Infrastructure.Models;

namespace SchedulerBot.FrontController.Interfaces
{
    public interface IDateTimeManager
    {
        WeekType GetInvertedWeekType();
        string GetNextDayName();
        string GetCurrentDayName();
    }
}
