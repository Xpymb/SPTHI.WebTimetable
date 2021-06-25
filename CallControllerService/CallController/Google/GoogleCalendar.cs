using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using CallControllerService.CallController.Ticks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace CallController.Google
{
    public class GoogleCalendar
    {
        //Define Google API required variables
        private readonly string[] _scopes = { CalendarService.Scope.CalendarReadonly };
        private const string ApplicationName = "CallController SPHTI";
        private const string CalendarId = "pit5gsmeoig0m329opvjh8f5o4@group.calendar.google.com";
        private const string _credentialPath = "Credentials/token/credentials.p12";

        //Define Ticks Controller instance
        private readonly TicksController _ticksController;

        //Define private fields
        private CalendarService _calendarService;
        private Events previusEvents;

        //Define and create events
        public class CalendarEventArgs
        {
            public Events Events { get; private set; }

            public CalendarEventArgs(Events events)
            {
                this.Events = events;
            }
        }
        public EventHandler<CalendarEventArgs> EventsUpdated;

        public GoogleCalendar()
        {
            //Connect to Google Calendar API
            ConnectToCalendarApi();

            previusEvents = new Events();
            
            //Define and subscribe on Tick event
            _ticksController = InstancesContainer._TicksController;
            _ticksController.TickEvery5Min += OnUpdateEvents;
        }

        private void ConnectToCalendarApi()
        {
            ServiceAccountCredential credential;

            var certificate = new X509Certificate2(_credentialPath, "notasecret", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);

            credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer("callcontrollermain@callcontroller-1614994327804.iam.gserviceaccount.com").FromCertificate(certificate));

            _calendarService = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        private void OnUpdateEvents(object sender, TicksController.DefaultEventArgs e)
        {
            var events = GetEventsToday();
            EventsUpdated?.Invoke(this, new CalendarEventArgs(events));
        }

        public Events GetEventsByTime(DateTime startTime, DateTime endTime)
        {
            var request = _calendarService.Events.List(CalendarId);

            request.TimeMin = startTime;
            request.TimeMax = endTime;
            request.ShowDeleted = false;
            request.SingleEvents = true;

            Events events;
            
            try { events = request.Execute(); previusEvents = events; } catch { return previusEvents; }

            if (events.Items == null || events.Items.Count <= 0) return null;
            
            return events;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public Events GetEventsToday()
        {
            var request = _calendarService.Events.List(CalendarId);
            
            request.TimeMin = DateTime.Today;
            request.TimeMax = DateTime.Today.AddDays(1).AddMinutes(5);
            request.ShowDeleted = false;
            request.SingleEvents = true;

            Events events;
            
            try { events = request.Execute(); previusEvents = events; } catch { return previusEvents; }

            if (events.Items == null || events.Items.Count <= 0) return null;
            
            return events;
        }
    }
}
