using System;
using System.Collections.Generic;
using System.Linq;
using CallController;
using CallController.Google;
using CallControllerService.CallController.Tools;
using Google.Apis.Calendar.v3.Data;

namespace CallControllerService.CallController.Calls
{
    public class CallsManager
    {
        //Define and create Update List of Calls Event
        public class UpdateCallsListEventArgs
        {
            public List<Call> ListCalls { get; }

            public UpdateCallsListEventArgs(List<Call> listCalls)
            {
                this.ListCalls = listCalls;
            }
        }
        public EventHandler<UpdateCallsListEventArgs> ListCallsUpdated;

        //Define and create Call event
        public class CallEventArgs
        {
            public Call Call { get; }

            public CallEventArgs(Call call)
            {
                this.Call = call;
            }
        }
        public EventHandler<CallEventArgs> CallEvent;

        //Define Google Calendar instance
        private readonly GoogleCalendar _googleCalendar;

        //Define private fields
        private Events _calendarEvents;

        //Define public fields
        public List<Call> ListCalls { get; private set; }
        public List<Call> ListNextCalls { get; private set; }
        public Call NextCall { get; private set; }

        public CallsManager()
        {
            //Get instance of google calendar
            _googleCalendar = InstancesContainer._GoogleCalendar;

            //Set default values for additional variables
            _calendarEvents = null;
            ListCalls = new List<Call>();
            ListNextCalls = new List<Call>();

            //Subscribe to Events Update
            _googleCalendar.EventsUpdated += OnUpdateListCalls;

            //Subscribe to Call Event
            CallEvent += OnCallEvent;
        }

        private void OnUpdateListCalls(object sender, GoogleCalendar.CalendarEventArgs e)
        {
            if (_calendarEvents != e.Events)
            {
                _calendarEvents = e.Events;
                ListCalls.Clear();

                if (_calendarEvents.Items != null && _calendarEvents.Items.Count > 0)
                {
                    foreach (var calendarEvent in _calendarEvents.Items)
                    {
                        if (calendarEvent.Start.DateTime == null || calendarEvent.End.DateTime == null) continue;
                        
                        var callStart = new Call((DateTime)calendarEvent.Start.DateTime, $"{calendarEvent.Summary} начало");
                        var callEnd = new Call((DateTime)calendarEvent.End.DateTime, $"{calendarEvent.Summary} конец");
                        
                        ListCalls.Add(callStart);
                        ListCalls.Add(callEnd);
                    }
                }

                ListCalls = ArrayTools.BubbleSort(ListCalls);
            }

            UpdateNextCall();
            UpdateListNextCalls();

            ListCallsUpdated?.Invoke(this, new UpdateCallsListEventArgs(ListCalls));
        }

        private void UpdateListNextCalls()
        {
            ListNextCalls.Clear();

            foreach (var call in ListCalls.Where(call => call.IsCalled == false))
            {
                ListNextCalls.Add(call);
            }
        }
        
        private void UpdateNextCall()
        { 
            foreach (var call in ListCalls.Where(call => call.IsCalled == false))
            {
                NextCall = call;
                return;
            }

            NextCall = null;
        }

        private void OnCallEvent(object sender, CallEventArgs e)
        {
            UpdateNextCall();
            UpdateListNextCalls();
        }
    }
}
