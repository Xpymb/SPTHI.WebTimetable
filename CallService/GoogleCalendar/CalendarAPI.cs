using CallService.GoogleCalendar.Credentials;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace CallService.GoogleCalendar
{
    public static class CalendarAPI
    {
        private static string _certificatePath = "GoogleCalendar/Credentials/PrivateKey.json";
        private static string _applicationName = "CallService SPHTI";
        private static string _calendarId = "pit5gsmeoig0m329opvjh8f5o4@group.calendar.google.com";

        private static CalendarService _service;

        public static void ConnectToApi()
        {
            ServiceAccountCredential credential;

            var jsonCred = File.ReadAllText(_certificatePath);

            var cred = JsonSerializer.Deserialize<PersonalServiceAccountCred>(jsonCred);

            credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(cred.ClientEmail)
            {
                Scopes = new[] { CalendarService.Scope.CalendarEventsReadonly },
            }.FromPrivateKey(cred.PrivateKey));

            _service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _applicationName,
            });
        }

        public static IList<Event> GetEventsToday()
        {
            if(_service == null)
            {
                ConnectToApi();
            }

            var request = _service.Events.List(_calendarId);

            request.TimeMin = DateTime.Today;
            request.TimeMax = DateTime.Today.AddDays(1).AddMinutes(5);
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.TimeZone = "Etc/GMT+5";
            
            try 
            {
                var events = request.Execute().Items;

                if (events == null || events.Count == 0) return null;

                return events;
            } 
            catch 
            { 
                return null; 
            }
        }
    }
}
