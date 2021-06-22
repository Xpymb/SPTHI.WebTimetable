using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using ScheduleController.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading;

namespace ScheduleController.GoogleSheets
{
    public static class SheetsAPI
    {
        private static readonly string[] _scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        private static readonly string _applicationName = "Google Sheets API .NET Quickstart";
        private static readonly string _credentialPath = "GoogleSheets/Credentials/credentials.p12";
        private static readonly string _tokenResponses = "GoogleSheets/Credentials/Google.Apis.Auth.OAuth2.Responses.TokenResponse-user";

        private static SheetsService _service;

        public static string SpreadSheetId { get; } = "1l4y2eBdwInbG3C7hKObvwRYqduNgX9px06GL7kYK4N0";
        public static string RangeValues { get; } = "Лист1!A2:H";

        public static void ConnectToSheetsAPI()
        {
            ServiceAccountCredential credential;

            TokenSerializable tokenResponse;

            /*using (var stream = new StreamReader(_tokenResponses))
            {
                var token = stream.ReadToEndAsync().Result;

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                tokenResponse = JsonSerializer.Deserialize<TokenSerializable>(token, options);
            }*/

            var certificate = new X509Certificate2(_credentialPath, "notasecret", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);

            credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(AppSettings.ServiceAccountEmail).FromCertificate(certificate));

            credential.Token = new Google.Apis.Auth.OAuth2.Responses.TokenResponse()
            {
                //Scope = tokenResponse.Scope
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
