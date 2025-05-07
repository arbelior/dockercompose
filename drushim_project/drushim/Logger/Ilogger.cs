namespace drushim.Logger
{
    public interface Ilogger
    {
        void LogError(string message, string Facade, string function, string appID, string? request = null, string? response = null);

        void LogException(string message, string Facade, string function, Exception ex, string appID, string? request = null, string? response = null);

        void LogInfo(string message, string Facade, string function, string appID, string? request = null, string? response = null);

        void LogCritical(string message, string Facade, string function, string appID, string? request = null, string? response = null);
    }
}
