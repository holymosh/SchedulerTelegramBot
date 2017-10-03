using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Models;
using SchedulerBot.FrontController.Interfaces;

namespace SchedulerBot.FrontController.Entities
{
    public class DataToMessageMapper:IDataToMessageMapper
    {
        private IDictionary<int, int> _pairNumberToHour;

        public string CreateMessageWithSchedule(IEnumerable<Course> courses)
        {
            var schedule = String.Empty;
            if (courses.Count().Equals(0))
            {
                return "Нет пар";
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

        public string CreateMessageFromTeacherData(Teacher teacher)
        {
            var result = "Рано спрашиваете";
            if (!teacher.Equals(null))
            {
                result = $"{teacher.Surname} {teacher.Name} {teacher.FatherName}";
                return result;
            }
            return result;

        }

        private string ToString(int argument)
        {
            var result = argument.ToString();
            return result.Length.Equals(2) ? result : "0" + result;
        }
    }
}
