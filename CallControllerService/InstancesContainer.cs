using CallController.Google;
using CallControllerService.CallController.Calls;
using CallControllerService.CallController.Threads;
using CallControllerService.CallController.Ticks;
using CallControllerService.CallController.Time;

namespace CallController
{
    class InstancesContainer
    {
        public static ThreadsController _ThreadsController { get; private set; }
        public static TicksController _TicksController { get; private set; }
        public static RealTime _RealTime { get; private set; }
        public static GoogleCalendar _GoogleCalendar { get; private set; }
        public static CallsManager _CallsManager { get; private set; }

        public InstancesContainer()
        {
            _TicksController = new TicksController();
            _RealTime = new RealTime();
            _ThreadsController = new ThreadsController();
            _GoogleCalendar = new GoogleCalendar();
            _CallsManager = new CallsManager();

        }
    }
}
