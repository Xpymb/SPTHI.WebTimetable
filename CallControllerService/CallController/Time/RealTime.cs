using System;
using CallController;
using CallControllerService.CallController.Ticks;

namespace CallControllerService.CallController.Time
{
    public class RealTime
    {
        //Define Tick Controller instance
        private TicksController _ticksController;

        //Define public fields
        public DateTime CurrentDateTime { get; private set; }

        //Define and create Update Time Event
        public class RealTimeEventArgs
        {
            public DateTime Value { get; private set; }

            public RealTimeEventArgs(DateTime value)
            {
                this.Value = value;
            }

            public string GetDateTime()
            {
                return Value.ToString();
            }

            public string GetLongTime()
            {
                return Value.ToLongTimeString();
            }

            public string GetShortTime()
            {
                return Value.ToShortTimeString();
            }

            public string GetLongDate()
            {
                return Value.ToLongDateString();
            }

            public string GetShortDate()
            {
                return Value.ToShortDateString();
            }
        }
        public EventHandler<RealTimeEventArgs> TimeUpdated;

        public RealTime()
        {
            //Get and subscribe on Tick Event
            _ticksController = InstancesContainer._TicksController;
            _ticksController.TickEvery1Sec += UpdateTime;
            
            //Create Current DateTime instance
            CurrentDateTime = new DateTime();
        }

        private void UpdateTime(object sender, TicksController.DefaultEventArgs e)
        {
            CurrentDateTime = DateTime.Now;

            TimeUpdated?.Invoke(this, new RealTimeEventArgs(CurrentDateTime));
        }
    }
}
