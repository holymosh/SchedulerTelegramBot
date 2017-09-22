using System;
using System.Linq;
using Infrastructure.InfrastuctureLogic.Repositories.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.InfrastuctureLogic.Repositories.Models
{
    public class StudentRepository : IStudentRepository
    {
        private Func<int> SaveChanges;
        private DbSet<Student> _students; 

        public IStudentRepository UseContext(ScheduleContext context)
        {
            _students = context.Students;
            SaveChanges = context.SaveChanges;
            return this;
        }

        public bool IsRegistered(Student student)
        {
            return _students.Contains(student);
        }

        public void JoinStudent(Student student)
        {
            _students.Add(student);
            SaveChanges();
        }
    }
}
