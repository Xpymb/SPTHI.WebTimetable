using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using ScheduleController.Settings;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace ScheduleController.GoogleSheets
{
    public static class SheetsAPI
    {
        private static readonly string[] _scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        private static readonly string _applicationName = "ScheduleController API";
        private static readonly string _credentialPath = "GoogleSheets/Credentials/credentials.p12";

        private static SheetsService _service;

        public static string SpreadSheetId { get; } = "1l4y2eBdwInbG3C7hKObvwRYqduNgX9px06GL7kYK4N0";
        public static string RangeValues { get; } = "Лист1!A2:H";

        public static void ConnectToSheetsAPI()
        {
            ServiceAccountCredential credential;

            var certificate = new X509Certificate2(_credentialPath, "notasecret", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);

            credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(AppSettings.ServiceAccountEmail).FromCertificate(certificate));

            credential.Token = new Google.Apis.Auth.OAuth2.Responses.TokenResponse()
            {
                Scope = _scopes[0]
            };

            _service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _applicationName,
            });
        }

        public static IList<IList<object>> GetSheetValues(string spreadSheetId, string rangeValues)
        {
            try
            {
                var request = _service.Spreadsheets.Values.Get(spreadSheetId, rangeValues);
                _service.Spreadsheets.Values.Get(spreadSheetId, rangeValues);

                var sheet = _service.Spreadsheets.Get(spreadSheetId);

                ValueRange response = request.Execute();
                return response.Values;
            }
            catch
            {
                return null;
            }
        }
    }
}
