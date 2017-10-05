using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.InfrastuctureLogic.Repositories.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.InfrastuctureLogic.Repositories.Models
{
    public class StudentRepository : IStudentRepository
    {
        private Func<int> _saveChanges;
        private DbSet<Student> _students; 

        public IStudentRepository UseContext(ScheduleContext context)
        {
            _students = context.Students;
            _saveChanges = context.SaveChanges;
            return this;
        }

        public bool IsRegistered(string id)
        {
            return _students.Any(entity => entity.Id.Equals(id));
        }

        public void JoinStudent(Student student)
        {
            _students.Add(student);
            _saveChanges();
        }

        public void RemoveStudent(string id)
        {
            _students.Remove(new Student{Id = id});
            _saveChanges();
        }

        public void Register(Student student)
        {
            _students.Add(student);
            try
            {
                _students.Add(student);
                _saveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
