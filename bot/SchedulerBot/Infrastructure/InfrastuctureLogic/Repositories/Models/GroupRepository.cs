using System;
using System.Linq;
using Infrastructure.InfrastuctureLogic.Repositories.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.InfrastuctureLogic.Repositories.Models
{
    public class GroupRepository:IGroupRepository
    {
        private Func<int> SaveChanges;
        private DbSet<Group> _groups;

        public IGroupRepository UseContext(ScheduleContext context)
        {
            _groups = context.Groups;
            SaveChanges = context.SaveChanges;
            return this;
        }

        public Group GetGroupByStudent(string id)
        {
            return _groups.SingleOrDefault(group => 
            group.Students.SingleOrDefault(entity => 
            entity.Id.Equals(id)).GroupId.Equals(group.Id));
        }
    }
}
