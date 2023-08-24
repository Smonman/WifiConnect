namespace WifiConnect
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }

        // try to decode qr code
        // if success, try to connect to it

        // states:
        // 1. scanning
        //    if found -> 2. decoding
        // 2. decoding
        //    if success -> 3. connecting
        //    else -> 1. scanning
        // 3. connecting
        //    if success -> 1. scanning
        //    else -> 1. scanning  
    }
}