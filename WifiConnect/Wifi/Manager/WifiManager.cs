using SimpleWifi;
using static WifiConnect.Wifi.Manager.IWifiManager;

namespace WifiConnect.Wifi.Manager
{
    internal class WifiManager : IWifiManager
    {
        private readonly SimpleWifi.Wifi wifi;

        public WifiManager()
        {
            this.wifi = new SimpleWifi.Wifi();
        }

        public event EventHandler<EventArgs>? WifiConnectionStart;
        public event EventHandler<WifiEventArgs>? WifiConnectionEnd;

        public void Connect(string ssid, string? pass)
        {
            if (this.wifi.NoWifiAvailable)
            {
                throw new InvalidOperationException("no wifi card available");
            }

            this.OnWifiConnectionStart();
            try
            {
                AccessPoint accessPoint = this.GetAccessPointBySsid(ssid);
                this.ConnectToAccessPoint(accessPoint, pass);
            }
            catch (Exception)
            {
                this.OnWifiConnectionEnd(ssid, false);
            }
        }

        private IEnumerable<AccessPoint> SearchForAccessPoints()
        {
            return this.wifi.GetAccessPoints().OrderByDescending(ap => ap.SignalStrength);
        }

        private AccessPoint GetAccessPointBySsid(string ssid)
        {
            IEnumerable<AccessPoint> accessPoints = this.SearchForAccessPoints();
            IEnumerable<AccessPoint> wantedAccessPoints = accessPoints.Where(ap => ap.Name.Equals(ssid));
            return wantedAccessPoints.Any() ? wantedAccessPoints.First() : throw new AccessPointNotFoundException(ssid);
        }

        private void ConnectToAccessPoint(AccessPoint accessPoint, string? pass)
        {
            AuthRequest authRequest = new(accessPoint);
            if (authRequest.IsPasswordRequired)
            {
                if (!accessPoint.HasProfile)
                {
                    if (authRequest.IsUsernameRequired)
                    {
                        throw new NotImplementedException();
                    }

                    if (authRequest.IsDomainSupported)
                    {
                        throw new NotImplementedException();
                    }

                    authRequest.Password = pass;
                }
            }

            accessPoint.ConnectAsync(authRequest, false,
                success => { this.OnWifiConnectionEnd(accessPoint.Name, success); });
        }

        private void OnWifiConnectionStart()
        {
            this.WifiConnectionStart?.Invoke(this, EventArgs.Empty);
        }

        private void OnWifiConnectionEnd(string ssid, bool success)
        {
            this.WifiConnectionEnd?.Invoke(this, new WifiEventArgs(ssid, success));
        }
    }
}
