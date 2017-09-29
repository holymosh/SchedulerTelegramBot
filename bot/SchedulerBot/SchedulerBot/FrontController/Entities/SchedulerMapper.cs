using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Models;
using SchedulerBot.FrontController.Interfaces;

namespace SchedulerBot.FrontController.Entities
{
    public class SchedulerMapper:IScheduleMapper
    {
        private IDictionary<int, int> _pairNumberToHour;

        public string DisplayScheduleForNextDay(IEnumerable<Course> courses)
        {
            var schedule = String.Empty;
            if (courses.Count().Equals(0))
            {
                return "Завтра нет пар";
            }
            var orderedCourses = courses.OrderBy(course => course.StartHour);
            foreach (var course in orderedCourses)
            {

                schedule += $"\t c {course.StartHour}:{ToString(course.StartMinute)} " +
                            $"до {course.EndHour}:{ToString(course.EndMinute)} {course.LessonType} \n";
                schedule += $"{course.Name} в {course.Location} \n \n";
            }
            schedule += "прогуливайте умеренно";
            return schedule;
        }

        private string ToString(int argument)
        {
            var result = argument.ToString();
            return result.Length.Equals(2) ? result : "0" + result;
        }
    }
}
