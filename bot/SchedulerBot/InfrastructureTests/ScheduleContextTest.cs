using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Infrastructure.InfrastuctureLogic;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureTests
{
    class ScheduleContextTest:ScheduleContext
    {
        public ScheduleContextTest(DbContextOptions<ScheduleContext> contextOptions) : base(contextOptions)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            BotConfig config = new BotConfig();
            optionsBuilder.UseSqlServer(BotConfig.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
