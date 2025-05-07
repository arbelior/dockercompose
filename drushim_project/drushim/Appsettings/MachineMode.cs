namespace drushim.Appsettings
{
    public static class MachineMode
    {
        public static bool IsDebug()
        {
        #if DEBUG
                    return true;
        #else
            return false;
        #endif
        }
    }
}
