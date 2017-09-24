using Infrastructure.Models;

namespace Infrastructure.InfrastuctureLogic.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        IStudentRepository UseContext(ScheduleContext context);
        bool IsRegistered(string id);
        void JoinStudent(Student student);
        void RemoveStudent(string id);
    }
}
