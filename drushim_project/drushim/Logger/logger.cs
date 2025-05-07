using drushim.BHelpers;
using System.Diagnostics;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.Json;
using drushim.Appsettings;
using Microsoft.Extensions.Configuration;

namespace drushim.Logger
{
    public class logger : Ilogger
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public IConfiguration iconfig;
        string elkurl = string.Empty;
        public AppSettingsConfig appSettingsConfig;
        public logger(IHttpClientFactory httpClientFactory, IConfiguration _iconfig)
        {
            _httpClientFactory = httpClientFactory;
            iconfig = _iconfig;
            appSettingsConfig = new AppSettingsConfig(iconfig);
            elkurl = Getelksetting() ?? throw new ArgumentException("Not found elk url setting", nameof(elkurl));
        }

        public string Getelksetting() 
        {
            //var appSettingsConfig = new AppSettingsConfig(iconfig);
            return appSettingsConfig.GetElkUrl();
        }


        public async void LogError(string message, string Facade, string function, string appID, string? request = null, string? response = null)
        {

            System.Net.Http.HttpClient client = _httpClientFactory.CreateClient();
            StringBuilder sbMessage = new StringBuilder();
            sbMessage.Append(Facade);
            sbMessage.Append(" ");
            sbMessage.Append(function);
            sbMessage.Append(" ");
            sbMessage.Append(message);
            Dictionary<string, string> logRequest = new Dictionary<string, string>();
            logRequest.Add("Title", appID);
            logRequest.Add("Message", sbMessage.ToString());
            logRequest.Add("TraceEventType", TraceEventType.Error.ToString());
            if (!string.IsNullOrEmpty(request))
            {
                logRequest.Add("Request", request);
            }
            if (!string.IsNullOrEmpty(response))
            {
                logRequest.Add("Response", response);
            }
            string json = JsonSerializer.Serialize(logRequest);
            client.DefaultRequestHeaders.Add
                        (HttpRequestHeader.Accept.ToString()
                        , "application/json, text/plain, */*");
            client.DefaultRequestHeaders.Add
                        (HttpRequestHeader.AcceptLanguage.ToString()
                        , "en-US,en;q=0.9");
            client.DefaultRequestHeaders.Add
                        (HttpRequestHeader.ContentType.ToString()
                        , "application/json;charset=UTF-8");
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var responseClient = await client.PostAsync(elkurl, httpContent);
            string logResponse = $"{responseClient.Content}  : {responseClient.StatusCode}";
        }

        public async void LogException(string message, string Facade, string function, Exception ex, string appID, string? request = null, string? response = null)
        {
            System.Net.Http.HttpClient client = _httpClientFactory.CreateClient();
            StringBuilder sbMessage = new StringBuilder();
            sbMessage.Append(Facade);
            sbMessage.Append(" ");
            sbMessage.Append(function);
            sbMessage.Append(" ");
            sbMessage.Append(message);
            sbMessage.Append(" the exception message is : ");
            sbMessage.Append(ex.Message);
            sbMessage.Append(" the exception stack trace is : ");
            sbMessage.Append(ex.StackTrace);
            if (ex.InnerException != null)
            {
                sbMessage.Append(" the inner exception message is : ");
                sbMessage.Append(ex.InnerException.Message);
                sbMessage.Append(" the inner exception stack trace is : ");
                sbMessage.Append(ex.InnerException.StackTrace);
            }

            Dictionary<string, string> logRequest = new Dictionary<string, string>();
            logRequest.Add("Title", appID);
            logRequest.Add("Message", sbMessage.ToString());
            logRequest.Add("TraceEventType", TraceEventType.Information.ToString());
            if (!string.IsNullOrEmpty(request))
            {
                logRequest.Add("Request", request);
            }
            if (!string.IsNullOrEmpty(response))
            {
                logRequest.Add("Response", response);
            }
            string json = JsonSerializer.Serialize(logRequest);
            client.DefaultRequestHeaders.Add
                        (HttpRequestHeader.Accept.ToString()
                        , "application/json, text/plain, */*");
            client.DefaultRequestHeaders.Add
                        (HttpRequestHeader.AcceptLanguage.ToString()
                        , "en-US,en;q=0.9");
            client.DefaultRequestHeaders.Add
                        (HttpRequestHeader.ContentType.ToString()
                        , "application/json;charset=UTF-8");
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var responseClient = await client.PostAsync(elkurl, httpContent);
            string logResponse = $"{responseClient.Content}  : {responseClient.StatusCode}";
        }

        public async void LogInfo(string message, string Facade, string function, string appID, string? request = null, string? response = null)
        {
            System.Net.Http.HttpClient client = _httpClientFactory.CreateClient();
            StringBuilder sbMessage = new StringBuilder();
            sbMessage.Append(Facade);
            sbMessage.Append(" ");
            sbMessage.Append(function);
            sbMessage.Append(" ");
            sbMessage.Append(message);
            Dictionary<string, string> logRequest = new Dictionary<string, string>();
            logRequest.Add("Title", appID);
            logRequest.Add("Message", sbMessage.ToString());
            logRequest.Add("TraceEventType", TraceEventType.Information.ToString());
            if (!string.IsNullOrEmpty(request))
            {
                logRequest.Add("Request", request);
            }
            if (!string.IsNullOrEmpty(response))
            {
                logRequest.Add("Response", response);
            }
            string json = JsonSerializer.Serialize(logRequest);
            client.DefaultRequestHeaders.Add
                        (HttpRequestHeader.Accept.ToString()
                        , "application/json, text/plain, */*");
            client.DefaultRequestHeaders.Add
                        (HttpRequestHeader.AcceptLanguage.ToString()
                        , "en-US,en;q=0.9");
            client.DefaultRequestHeaders.Add
                        (HttpRequestHeader.ContentType.ToString()
                        , "application/json;charset=UTF-8");
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");



            var responseClient = await client.PostAsync(elkurl, httpContent);
            string logResponse = $"{responseClient.Content}  : {responseClient.StatusCode}";
        }

        public async void LogCritical(string message, string Facade, string function, string appID, string? request = null, string? response = null)
        {
            System.Net.Http.HttpClient client = _httpClientFactory.CreateClient();
            StringBuilder sbMessage = new StringBuilder();
            sbMessage.Append(Facade);
            sbMessage.Append(" ");
            sbMessage.Append(function);
            sbMessage.Append(" ");
            sbMessage.Append(message);

            Dictionary<string, string> logRequest = new Dictionary<string, string>();
            logRequest.Add("Title", appID);
            logRequest.Add("Message", sbMessage.ToString());
            logRequest.Add("TraceEventType", TraceEventType.Information.ToString());
            if (!string.IsNullOrEmpty(request))
            {
                logRequest.Add("Request", request);
            }
            if (!string.IsNullOrEmpty(response))
            {
                logRequest.Add("Response", response);
            }
            string json = JsonSerializer.Serialize(logRequest);
            client.DefaultRequestHeaders.Add
                        (HttpRequestHeader.Accept.ToString()
                        , "application/json, text/plain, */*");
            client.DefaultRequestHeaders.Add
                        (HttpRequestHeader.AcceptLanguage.ToString()
                        , "en-US,en;q=0.9");
            client.DefaultRequestHeaders.Add
                        (HttpRequestHeader.ContentType.ToString()
                        , "application/json;charset=UTF-8");
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var responseClient = await client.PostAsync(elkurl, httpContent);
            string logResponse = $"{responseClient.Content}  : {responseClient.StatusCode}";
        }
    }
}
