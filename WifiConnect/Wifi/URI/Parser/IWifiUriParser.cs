using System.Text.RegularExpressions;

namespace WifiConnect.Wifi.URI.Parser
{
    internal interface IWifiUriParser
    {
        protected const string SCHEME = "WIFI:";
        protected const string DELIMITER = ":";
        protected const string FIELD_DELIMITER = ";";
        protected const string SSID_FIELD = "S";
        protected const string TYPE_FIELD = "T";
        protected const string PASS_FIELD = "P";
        protected const string HIDDEN_FIELD = "H";
        protected const string URI_PATTERN = @"^WIFI:S:\S+;T:(WEP|WPA|nopass);P:\S*;H:(true|false|blank);;$";

        public WifiUriComponents Parse(string uri);

        public bool IsWifiUri(string uri)
        {
            return Regex.Match(uri, URI_PATTERN).Success;
        }

        public class WifiUriComponents
        {
            public string? SSID { get; private set; }
            public string? Type { get; private set; }
            public string? Pass { get; private set; }
            public string? Hidden { get; private set; }

            public WifiUriComponents() { }

            internal void SetComponent(string fieldName, string fieldValue)
            {
                switch (fieldName)
                {
                    case SSID_FIELD:
                        this.SSID = fieldValue;
                        break;
                    case TYPE_FIELD:
                        this.Type = fieldValue;
                        break;
                    case PASS_FIELD:
                        this.Pass = fieldValue;
                        break;
                    case HIDDEN_FIELD:
                        this.Hidden = fieldValue;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
