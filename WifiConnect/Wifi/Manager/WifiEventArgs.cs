namespace WifiConnect.Wifi.Manager
{
    internal partial interface IWifiManager
    {
        public class WifiEventArgs : EventArgs
        {
            public string SSID { get; private set; }
            public bool Connected { get; private set; }

            public WifiEventArgs(string SSID, bool connected) : base()
            {
                this.SSID = SSID;
                this.Connected = connected;
            }
        }
    }
}
