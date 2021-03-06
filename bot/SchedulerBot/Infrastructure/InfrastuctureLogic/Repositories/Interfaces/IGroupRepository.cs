﻿using Infrastructure.Models;

namespace Infrastructure.InfrastuctureLogic.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        IGroupRepository UseContext(ScheduleContext context);
        Group GetGroupByStudent(string id);
        string GetGroupNameById(int id);
        void SaveGroup(Group group);
    }
}
