namespace SeniorDesign.Core
{
    /// <summary>
    ///     A container for settings for StreamlineCore
    /// </summary>
    public static class CoreSettings
    {
        /// <summary>
        ///     The number of bytes that an input can take at a time
        /// </summary>
        public static int InputBuffer = 4096;

        /// <summary>
        ///     If debug mode is currently enabled
        /// </summary>
        public static bool DebugMode = true;
    }
}
