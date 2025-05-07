using System.Reflection;
using System.Runtime.CompilerServices;

namespace drushim.BHelpers
{
    public static class BLHelper
    {
        public static string GetCallingFunctionName([CallerMemberName] string functionName = "")
        {
            return functionName;
        }

        public static string GetStatusModeMessege(bool is_succesfull)
        {
            return  is_succesfull ? $"Date : {DateTime.Now.ToString()} The Process is done and succeced" : $"Date : {DateTime.Now.ToString()} The Process is done and failed";
        }
        public static string GetFileStartAudit(string facade, string function)
        {
            return string.Format("Date : {0} Application Process Start : {1}  , Method : {2}\n", DateTime.Now, facade, function);
        }

        public static string GetLogFileProcessStep(string facade, string function)
        {
            return string.Format("Date : {0} Application Process Step : {1}  , Method : {2}\n", DateTime.Now, facade, function);
        }
        public static string GetLogError(string facade, string fun, string message)
        {
            return string.Format("Error : " +  facade + " , " + fun + " , " + message + $" Date : {DateTime.Now.ToString()}" + "\n");
        }

        public static string GetLogInfo(string facade, string fun, string message)
        {
            return string.Format("Info : " + facade + " , " + fun + " , " + message + "\n");
        }

        public static string GetLogException(string facade, string fun, string message)
        {
            return string.Format("Exception : " + facade + " , " + fun + " , " + message +$" Date : {DateTime.Now.ToString()}" + "\n");
        }

        public static string GetLogProcessStatus(string facade, string fun, string message)
        {
            return string.Format("Status Process : " + facade + " , " + fun + " , " + message + "\n");
        }

        public static string Createfiledirectorey()
        {

            string siteName = "drushim"; 
            string logDirectory = Path.Combine(@"C:\data\logs", siteName); 

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory); 
            }

            return Path.Combine(logDirectory, ConstantOperation.logfile); 
        }

        public static async Task WriteFileLog(string facade, string method,string SelectFileSectionAudit , string LogMessege="")
        {
            string logFilePath = Createfiledirectorey();
            string message = string.Empty;

            if (string.IsNullOrEmpty(SelectFileSectionAudit))
              throw new ArgumentNullException(nameof(SelectFileSectionAudit), "SelectFileSectionAudit is null.");

             if(!DoesMethodExist(SelectFileSectionAudit))
                throw new ArgumentException("Method does not exist", nameof(SelectFileSectionAudit));

            switch (SelectFileSectionAudit)
            {
                case TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP:
                    message = GetFileStartAudit(facade, method);
                    break;

                case TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP:
                    message = GetLogFileProcessStep(facade, method);
                    break;

                case TraceEventTypeToLogFile.WRITE_LOG_FILE_ERROR:
                    message = GetLogError(facade, method, LogMessege);
                    break;

                case TraceEventTypeToLogFile.WRITE__LOG_FILE_INFO:
                    message = GetLogInfo(facade, method, LogMessege);
                    break;

                case TraceEventTypeToLogFile.WRITE_LOG_FILE_EXCEPTION:
                    message = GetLogException(facade, method, LogMessege);
                    break;

                case TraceEventTypeToLogFile.WRIT_LOG_FILE_PROCESS_STATUS:
                    message = GetLogProcessStatus(facade, method, LogMessege);
                    break;

                default:
                    break;
            }

            if(!string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(logFilePath))
                await System.IO.File.AppendAllTextAsync(logFilePath, message);
        }

        public static bool DoesMethodExist(string methodName)
        {
            return typeof(BLHelper).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public) != null;
        }

    }
}
