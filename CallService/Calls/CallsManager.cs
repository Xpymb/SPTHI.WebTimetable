using CallService.GoogleCalendar;
using CallService.Ticks;
using CallService.Tools;
using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallService.Calls
{
    public static class CallsManager
    {
        private static List<Call> _listCalls = new();
        private static List<Call> _listNextCalls = new();

        public static Call NextCall { get; private set; }
        public static IReadOnlyList<Call> ListCalls { get => _listCalls.AsReadOnly(); }
        public static IReadOnlyList<Call> ListNextCalls { get => _listNextCalls.AsReadOnly(); }

        public static EventHandler<EventArgs> CallEvent;

        public static void SubscribeToEvents()
        {
            CallEvent += OnCall;
            TicksController.TickEvery5Min += OnTick;
        }

        private static void OnTick(object sender, EventArgs e)
        {
            UpdateCalls();
        }

        private static void OnCall(object sender, EventArgs e)
        {
            UpdateCalls();
        }

        private static void UpdateCalls()
        {
            
            _listCalls = UpdateListCalls();
            NextCall = UpdateNextCall();
            _listNextCalls = UpdateListNextCalls();

            foreach (var call in ListCalls)
            {
                Console.WriteLine(call.CallTime);
            }
        }

        private static Call UpdateNextCall()
        {
            if (_listCalls == null) return null;

            foreach (var call in _listCalls.Where(call => call.IsCalled == false))
            {
                return call;
            }

            return null;
        }

        private static List<Call> UpdateListCalls()
        {
            var events = CalendarAPI.GetEventsToday();

            if (events == null) return _listCalls;

            var listCalls = new List<Call>();

            foreach (var calendarEvent in events)
            {
                if (calendarEvent.Start.DateTime == null || calendarEvent.End.DateTime == null) continue;

                var callStart = new Call(calendarEvent.Start.DateTime.Value, $"{calendarEvent.Summary} начало");
                var callEnd = new Call(calendarEvent.End.DateTime.Value, $"{calendarEvent.Summary} конец");

                listCalls.Add(callStart);
                listCalls.Add(callEnd);
            }

            listCalls = ArrayTools.BubbleSort(listCalls);

            return listCalls;
        }

        private static List<Call> UpdateListNextCalls()
        {
            if (_listCalls == null) return null;

            var listNextCalls = new List<Call>();

            foreach (var call in _listCalls.Where(call => call.IsCalled == false))
            {
                listNextCalls.Add(call);
            }

            return listNextCalls;
        }
    }
}
