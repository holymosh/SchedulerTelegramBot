using Domain;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.InfrastuctureLogic;
using Infrastructure.InfrastuctureLogic.Repositories.Interfaces;
using Infrastructure.InfrastuctureLogic.Repositories.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SchedulerBot.FrontController.Entities;
using SchedulerBot.FrontController.Interfaces;
using SchedulerBot.Proxies;

namespace SchedulerBot
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddSingleton(new RequestLogger());
            services.AddSingleton(new BotConfig());
            services.AddSingleton<IButtonFactoryMethod,ButtonFactoryMethod>();
            services.AddSingleton<ITelegramApiProxy,TelegramApiProxy>();
            services.AddSingleton<IActionsFacade,ActionsFacade>();
            services.AddSingleton<ITelegramFrontController,TelegramFrontController>();
            services.AddSingleton(new DatabaseIntegration(services));
            services.AddScoped<DatabaseContextProxy>();
            services.AddSingleton<IStudentRepository, StudentRepository>();
            services.AddSingleton<IUpdateReader, UpdateReader>();
            services.AddSingleton<IGroupRepository, GroupRepository>();
            services.AddSingleton<ICourseRepository,CourseRepository>();
            services.AddSingleton<IDateTimeManager, DateTimeManager>();
            services.AddSingleton<IDataToMessageMapper,DataToMessageMapper>();
            services.AddSingleton<ITeacherRepository, TeacherRepository>();
        }   

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseMvc();
        }

    }
}