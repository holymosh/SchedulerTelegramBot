using Infrastructure.InfrastuctureLogic;
using SchedulerBot.FrontController;

namespace SchedulerBot.Proxies
{
    public class DatabaseContextProxy
    {
        private ScheduleContext _context;
        private IApiActionsFacade _actionsFacade;

        public DatabaseContextProxy(ScheduleContext context, IApiActionsFacade actionsFacade)
        {
            _context = context;
            _actionsFacade = actionsFacade;

        }
    }
}
