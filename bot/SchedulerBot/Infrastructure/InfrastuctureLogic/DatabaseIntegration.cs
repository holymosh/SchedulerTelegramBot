using Domain;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.InfrastuctureLogic
{
    public class DatabaseIntegration
    {
        public DatabaseIntegration(IServiceCollection collection)
        {
            collection.AddDbContext<ScheduleContext>(builder => builder.UseSqlServer(BotConfig.ConnectionString,optionsBuilder => optionsBuilder.EnableRetryOnFailure()));
        }
    }
}
