using System;
using CallController;
using CallControllerService.CallController.Time;

namespace CallControllerService.CallController.Calls
{
    public class Call
    {
        //Define fields
        public string Name { get; private set; }
        public DateTime CallTime { get; private set; }
        public bool IsCalled { get; private set; }

        //Define Calls Manager instance
        private readonly CallsManager _callsManager;

        public Call(DateTime callTime, string name)
        {
            //Set fields
            this.Name = name;
            this.CallTime = callTime;

            //Get Calls Manager instance
            _callsManager = InstancesContainer._CallsManager;

            //Check Call on Called
            IsCalled = CheckIsAlreadyCalled();

            //If isCalled = false, subscribe on Update Time Event
            if(IsCalled == false)
            {
                InstancesContainer._RealTime.TimeUpdated += CheckIsCall;
            }
        }

        private bool CheckIsAlreadyCalled()
        {
            return CallTime < DateTime.Now;
        }

        private void CheckIsCall(object sender, RealTime.RealTimeEventArgs e)
        {
            if (DateTime.Now <= CallTime) return;
            
            _callsManager.CallEvent.Invoke(this, new CallsManager.CallEventArgs(this));
            IsCalled = true;

            InstancesContainer._RealTime.TimeUpdated -= CheckIsCall;
        }

        public string GetShortTime()
        {
            return CallTime.ToShortTimeString();
        }

        public string GetShortDate()
        {
            return CallTime.ToShortDateString();
        }

        public string GetLongTime()
        {
            return CallTime.ToLongTimeString();
        }

        public string GetLongDate()
        {
            return CallTime.ToLongDateString();
        }
    }
}
