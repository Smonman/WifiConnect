namespace WifiConnect.Wifi.Manager
{
    internal partial interface IWifiManager
    {
        public class WifiEventArgs : EventArgs
        {
            public WifiEventArgs(string ssid, bool connected)
            {
                this.Ssid = ssid;
                this.Connected = connected;
            }

            public string Ssid { get; private set; }
            public bool Connected { get; private set; }
        }
    }
}