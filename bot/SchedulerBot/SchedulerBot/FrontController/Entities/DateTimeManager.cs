using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Models;
using SchedulerBot.FrontController.Interfaces;

namespace SchedulerBot.FrontController.Entities
{
    public class DateTimeManager:IDateTimeManager
    {
        private readonly IDictionary<DayOfWeek, string> _days;

        public DateTimeManager()
        {
            _days = new Dictionary<DayOfWeek, string>()
            {
                {DayOfWeek.Sunday, "Воскресенье"},
                {DayOfWeek.Monday, "Понедельник"},
                {DayOfWeek.Tuesday, "Вторник"},
                {DayOfWeek.Wednesday, "Среда"},
                {DayOfWeek.Thursday, "Четверг" },
                {DayOfWeek.Friday, "Пятница"},
                {DayOfWeek.Saturday, "Cуббота"}
            };
        }

        public WeekType GetCurrentWeekType() => 
            (DateTime.Today.DayOfYear / 7 % 2).Equals(0) ? WeekType.Uneven :WeekType.Even;

        public string GetNextDayName() => 
            _days.SingleOrDefault(pair => pair.Key.Equals(DateTime.Today.DayOfWeek +1 )).Value;
    }
}
