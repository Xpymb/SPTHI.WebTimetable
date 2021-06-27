using CallService.Ticks;
using System.Threading;

namespace CallService.Threads
{
    public static class ThreadsController
    {
        private static Thread _tickThread;

        public static void CreateThread()
        {
            _tickThread = new Thread(() => { TicksController.UpdateTicks(); });
        }

        public static void StartThreads()
        {
            CreateThread();
            _tickThread.Start();
        }
    }
}
