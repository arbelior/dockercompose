using System.Diagnostics;
using System.Reflection;

namespace drushim.BHelpers
{
    public class ConstantOperation
    {
        public class create_error
        {
            public string facade { get; set; }
            public string function { get; set; }
            public string description { get; set; }

            public create_error(string Facade, string Function, string Description)
            {
                facade = Facade;
                function = Function;
                description = Description;
            }

        }
        public static string GetErrorMessage(create_error err)
        {
            return $" Facade : {err.facade} , Function : {err.function} , Description : {err.description}";
        }

        public string GetCallingFunctionName()
        {
            // Create a stack trace to inspect the call stack
            StackTrace stackTrace = new StackTrace();

            // Get the method information from the calling method (skip 1 frame for the current method)
            StackFrame callingFrame = stackTrace.GetFrame(1);
            MethodBase callingMethod = callingFrame.GetMethod();

            // Return the name of the calling method
            return callingMethod.Name;
        }

        public const string FAILED_CONVERT_JSON_SETTING = "faield while try to convert data to json";
        public const string logfile = "log.txt";
        public const string NOT_GET_HREF = "Not get href";


    }

    public static class HrefHeaders
    {
        public const string GetOrdersList = "GetOrdersList";
        public const string GetAdvertisingDestinations = "GetAdvertisingDestinations";
        public const string GetCitiesList = "GetCitiesList";
        public const string GetJobScopes = "GetJobScopes";
        public const string GetBranches = "GetBranches";
        public const string GetCategories = "GetCategories";
        public const string GetArea = "GetArea";
        public const string GetProfession = "GetProfession";
        public const string GetSubProfession = "GetSubProfession";
        public const string GetOrdersDetails = "GetOrdersDetails";
    }

    public static class TraceEventTypeToLogFile
    {
        public const string WRITE_LOG_FILE_START_APP = "GetFileStartAudit";
        public const string WRITE_LOG_FILE_PROCESS_STEP = "GetLogFileProcessStep";
        public const string WRITE_LOG_FILE_ERROR = "GetLogError";
        public const string WRITE__LOG_FILE_INFO = "GetLogInfo";
        public const string WRITE_LOG_FILE_EXCEPTION = "GetLogException";
        public const string WRIT_LOG_FILE_PROCESS_STATUS = "GetLogProcessStatus";
    }

}
