using Domain;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.InfrastuctureLogic
{
    public class DatabaseIntegration
    {
        public DatabaseIntegration(IServiceCollection collection)
        {
            collection.AddEntityFrameworkSqlServer()
                .AddDbContext<ScheduleContext>(
                    (provider, builder) =>
                    {
                        builder.UseSqlServer(BotConfig.ConnectionString);
                    });
        }
    }
}
