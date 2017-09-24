using Infrastructure.InfrastuctureLogic;
using Infrastructure.InfrastuctureLogic.Repositories.Interfaces;
using Infrastructure.InfrastuctureLogic.Repositories.Models;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfrastructureTests
{
    [TestClass]
    public class InfrastructureTests
    {
        [TestMethod]
        public void GetUserFromDatabaseWhenUserExists()
        {
            IStudentRepository studentRepository = new StudentRepository();
            ScheduleContextTest context = new ScheduleContextTest(new DbContextOptions<ScheduleContext>());
            var result = studentRepository.UseContext(context).IsRegistered("136329961");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetUserFromDatabaseWhenUserDoesntExists()
        {
            IStudentRepository studentRepository = new StudentRepository();
            ScheduleContextTest context = new ScheduleContextTest(new DbContextOptions<ScheduleContext>());
            var result = studentRepository.UseContext(context).IsRegistered("fail");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RemoveStudent()
        {
            IStudentRepository studentRepository = new StudentRepository();
            ScheduleContextTest saveContext = new ScheduleContextTest(new DbContextOptions<ScheduleContext>());
            saveContext.Students.Add(new Student()
            {
                Id = "1337",
                FirstName = "first",
                LastName = "last",
                IsAdmin = false,
                GroupId = 3
            });
            saveContext.SaveChanges();
            saveContext.Dispose();
            ScheduleContextTest deleteContext = new ScheduleContextTest(new DbContextOptions<ScheduleContext>());
            studentRepository.UseContext(deleteContext).RemoveStudent("1337");
            var exists = studentRepository.IsRegistered("1337");
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void GetGroupByStudent()
        {
            IGroupRepository repository = new GroupRepository();
            ScheduleContextTest contextTest = new ScheduleContextTest(new DbContextOptions<ScheduleContext>());
            var student = new Student();
            student.Id = "136329961";
            var group = repository.UseContext(contextTest).GetGroupByStudent(student);
            Assert.AreEqual(group.Name , "MM-15-2");
        }
    }
}
