﻿using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.InfrastuctureLogic.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        IStudentRepository UseContext(ScheduleContext context);
        bool IsRegistered(string id);
        void JoinStudent(Student student);
        void RemoveStudent(string id);
        void Register(Student student);
        IEnumerable<Student> GetGroupmates(string studentId);
    }
}
