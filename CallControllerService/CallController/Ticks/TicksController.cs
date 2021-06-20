using System;
using System.Threading;

namespace CallControllerService.CallController.Ticks
{
    public class TicksController
    {
        //Define and create Tick Events
        public class DefaultEventArgs
        {
            public string EventName { get; private set; }

            public DefaultEventArgs(string eventName = "Default Tick")
            {
                this.EventName = eventName;
            }
        }
        public EventHandler<DefaultEventArgs> TickEvery1Sec;
        public EventHandler<DefaultEventArgs> TickEvery5Min;
        public EventHandler<DefaultEventArgs> TickEveryMidnight;

        public void UpdateTicks()
        {
            DateTime currentTime = DateTime.Now;
            DateTime timeToTick1Sec = currentTime.AddSeconds(1);
            DateTime timeToTick5Min = currentTime.AddMinutes(5);
            DateTime timeToTickMidnight = currentTime.AddDays(1);

            while (true)
            {
                currentTime = DateTime.Now;

                if (currentTime >= timeToTick1Sec)
                {
                    TickEvery1Sec?.Invoke(this, new DefaultEventArgs("Tick 1 sec"));
                    timeToTick1Sec = currentTime.AddSeconds(1);
                }

                if (currentTime >= timeToTick5Min)
                {
                    TickEvery5Min?.Invoke(this, new DefaultEventArgs("Tick 5 min"));
                    timeToTick5Min = currentTime.AddMinutes(5);
                }

                if (currentTime >= timeToTickMidnight)
                {
                    TickEveryMidnight?.Invoke(this, new DefaultEventArgs("Tick midnight"));
                    timeToTickMidnight = currentTime.AddDays(1);
                }

                Thread.Sleep(1000);
            }
        }
    }
}
