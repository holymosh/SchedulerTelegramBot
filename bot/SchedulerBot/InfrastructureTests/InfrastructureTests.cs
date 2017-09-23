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
    }
}
