using System;
using System.Threading;

namespace ScheduleController.Ticks
{
    public static class TicksController
    {
        public static EventHandler<EventArgs> TickEvery1Sec;
        public static EventHandler<EventArgs> TickEvery5Min;
        public static EventHandler<EventArgs> TickEveryMidnight;

        public static void UpdateTicks()
        {
            DateTime currentTime = DateTime.Now;
            DateTime timeToTick1Sec = currentTime;
            DateTime timeToTick5Min = currentTime;
            DateTime timeToTickMidnight = currentTime;

            while (true)
            {
                currentTime = DateTime.Now;

                if (currentTime >= timeToTick1Sec)
                {
                    TickEvery1Sec?.Invoke(null, new EventArgs());
                    timeToTick1Sec = currentTime.AddSeconds(1);
                }

                if (currentTime >= timeToTick5Min)
                {
                    TickEvery5Min?.Invoke(null, new EventArgs());
                    timeToTick5Min = currentTime.AddMinutes(5);
                }

                if (currentTime >= timeToTickMidnight)
                {
                    TickEveryMidnight?.Invoke(null, new EventArgs());
                    timeToTickMidnight = currentTime.AddDays(1);
                }

                Thread.Sleep(1000);
            }
        }
    }
}
