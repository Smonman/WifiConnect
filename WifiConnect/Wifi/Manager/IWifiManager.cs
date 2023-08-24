namespace WifiConnect.Wifi.Manager
{
    internal partial interface IWifiManager
    {
        public event EventHandler<EventArgs> WifiConnectionStart;

        public event EventHandler<WifiEventArgs> WifiConnectionEnd;

        public void Connect(string SSID, string? pass);
    }
}
