using ScheduleController.Ticks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ScheduleController.Threads
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
            _tickThread.Start();
        }
    }
}
