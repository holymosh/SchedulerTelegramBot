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
            var student = new Student()
            {
                Id = "136329961",
                FirstName = "Dmitry",
                LastName = "Holymosh"
            };
            var result = studentRepository.UseContext(context).IsRegistered(student);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetUserFromDatabaseWhenUserDoesntExists()
        {
            IStudentRepository studentRepository = new StudentRepository();
            ScheduleContextTest context = new ScheduleContextTest(new DbContextOptions<ScheduleContext>());
            var student = new Student()
            {
                Id = "48923741",
                FirstName = "no",
                LastName = "user"
            };
            var result = studentRepository.UseContext(context).IsRegistered(student);
            Assert.IsFalse(result);
        }
    }
}
