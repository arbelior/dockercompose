namespace drushim.BHelpers
{
    public class ErrorHelpers
    {
        public static string GetExecption(Exception ex)
        {
#if DEBUG
            return GetExtractMessage(ex);

#else
            return "מצטערים משהו השתבש אנא נסו מאוחר יותר";
#endif

        }

        public static string GetExtractMessage(Exception ex)
        {
            if (ex.InnerException == null)
                return ex.Message;

            return GetExtractMessage(ex.InnerException);
        }
    }
}
