using CallService.Ticks;
using System;

namespace CallService.Calls
{
    public class Call
    {
        public string Name { get; private set; }
        public DateTime CallTime { get; private set; }
        public bool IsCalled { get; private set; }

        public Call(DateTime callTime, string name)
        {
            this.Name = name;
            this.CallTime = callTime;

            IsCalled = CheckIsAlreadyCalled();

            if (IsCalled == false)
            {
                TicksController.TickEvery1Sec += CheckIsCall;
            }
        }

        private bool CheckIsAlreadyCalled()
        {
            return CallTime < DateTime.Now;
        }

        private void CheckIsCall(object sender, EventArgs e)
        {
            if (DateTime.Now <= CallTime) return;

            CallsManager.CallEvent.Invoke(this, new EventArgs());
            IsCalled = true;

            TicksController.TickEvery1Sec -= CheckIsCall;
        }
    }
}
