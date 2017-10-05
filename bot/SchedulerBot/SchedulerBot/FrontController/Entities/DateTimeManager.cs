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
        private readonly IDictionary<string, string> _commandToDays;

        public DateTimeManager()
        {
            _days = new Dictionary<DayOfWeek, string>
            {
                {DayOfWeek.Sunday, "Воскресенье"},
                {DayOfWeek.Monday, "Понедельник"},
                {DayOfWeek.Tuesday, "Вторник"},
                {DayOfWeek.Wednesday, "Среда"},
                {DayOfWeek.Thursday, "Четверг" },
                {DayOfWeek.Friday, "Пятница"},
                {DayOfWeek.Saturday, "Cуббота"}
            };
            _commandToDays = new Dictionary<string, string>()
            {
                {"/monday", "Понедельник"},
                {"/tuesday","Вторник"},
                {"/wednesday", "Среда"},
                {"/thursday", "Четверг" },
                {"/friday", "Пятница"},
                {"/saturday", "Cуббота"}
            };
        }

        public WeekType GetInvertedWeekType() => 
            (DateTime.Today.DayOfYear / 7 % 2).Equals(0) ? WeekType.Even :WeekType.Uneven;

        public string GetNextDayName() => 
            _days.SingleOrDefault(pair => pair.Key.Equals(DateTime.Today.DayOfWeek +1 )).Value;

        public string GetCurrentDayName() =>
            _days.SingleOrDefault(pair => pair.Key.Equals(DateTime.Today.DayOfWeek)).Value;

        public string GetDayNameByCommand(string command)
        {
            return _commandToDays.SingleOrDefault(pair => pair.Key.Equals(command)).Value;
        }
    }
}
