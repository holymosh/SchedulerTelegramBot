using Infrastructure.InfrastuctureLogic;
using SchedulerBot.FrontController.Interfaces;

namespace SchedulerBot.Proxies
{
    public class DatabaseContextProxy
    {
        private ScheduleContext _context;
        private IActionsFacade _actionsFacade;

        public void SetContext()
        {
            _actionsFacade.AddContext(_context);
        }

        public DatabaseContextProxy(ScheduleContext context, IActionsFacade actionsFacade)
        {
            _context = context;
            _actionsFacade = actionsFacade;
        }
    }
}