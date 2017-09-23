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

        public bool IsRegistered(string id)
        {
            return _students.Any(entity => entity.Id.Equals(id));
        }

        public void JoinStudent(Student student)
        {
            _students.Add(student);
            SaveChanges();
        }
    }
}
