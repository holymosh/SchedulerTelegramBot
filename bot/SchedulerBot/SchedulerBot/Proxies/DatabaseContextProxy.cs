using Infrastructure.InfrastuctureLogic;
using SchedulerBot.FrontController;
using SchedulerBot.FrontController.Interfaces;

namespace SchedulerBot.Proxies
{
    public class DatabaseContextProxy
    {
        private ScheduleContext _context;
        private IApiActionsFacade _actionsFacade;

        public void SetContext()
        {
            _actionsFacade.AddContext(_context);
        }

        public DatabaseContextProxy(ScheduleContext context, IApiActionsFacade actionsFacade)
        {
            _context = context;
            _actionsFacade = actionsFacade;

        }
    }
}
