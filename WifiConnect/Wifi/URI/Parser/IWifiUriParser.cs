using System.Text.RegularExpressions;

namespace WifiConnect.Wifi.URI.Parser
{
    internal partial interface IWifiUriParser
    {
        public WifiUri Parse(string uri);

        public bool IsWifiUri(string uri)
        {
            return Regex.Match(uri, WifiUri.URI_PATTERN).Success;
        }
    }
}
