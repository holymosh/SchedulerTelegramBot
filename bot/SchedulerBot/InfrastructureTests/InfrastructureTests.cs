using System.Linq;
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
        public void GetGroupByStudent()
        {
            IGroupRepository repository = new GroupRepository();
            var contextTest = new ScheduleContextTest(new DbContextOptions<ScheduleContext>());
            var student = new Student();
            student.Id = "136329961";
            var group = repository.UseContext(contextTest).GetGroupByStudent(student.Id);
            Assert.AreEqual(group.Name , "MM-15-2");
        }

    }
}
