using Infrastructure.Models;

namespace Infrastructure.InfrastuctureLogic.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        IStudentRepository UseContext(ScheduleContext context);
        bool IsRegistered(Student student);
        void JoinStudent(Student student);
    }
}
