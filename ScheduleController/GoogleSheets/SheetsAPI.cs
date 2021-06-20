using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ScheduleController.GoogleSheets
{
    public static class SheetsAPI
    {
        private static readonly string[] _scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        private static readonly string _applicationName = "Google Sheets API .NET Quickstart";

        private static SheetsService _service;

        public static string SpreadSheetId { get; } = "1l4y2eBdwInbG3C7hKObvwRYqduNgX9px06GL7kYK4N0";
        public static string RangeValues { get; } = "Лист1!A2:H";

        public static void ConnectToSheetsAPI()
        {
            UserCredential credential;

            using (var stream = new FileStream("GoogleSheets/Credentials/credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    _scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

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
                SpreadsheetsResource.ValuesResource.GetRequest request =
                    _service.Spreadsheets.Values.Get(spreadSheetId, rangeValues);

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
