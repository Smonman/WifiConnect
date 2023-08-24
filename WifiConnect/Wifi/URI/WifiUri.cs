namespace WifiConnect.Wifi.URI
{
    public class WifiUri
    {
        public const string SCHEME = "WIFI:";
        public const string DELIMITER = ":";
        public const string FIELD_DELIMITER = ";";
        public const string TYPE_FIELD = "T";
        public const string SSID_FIELD = "S";
        public const string PASS_FIELD = "P";
        public const string HIDDEN_FIELD = "H";
        public const string EAP_FIELD = "E";
        public const string ANONYMOUS_FIELD = "A";
        public const string IDENTITY_FIELD = "I";
        public const string PHASE_2_FIELD = "PH2";
        [Obsolete] public const string URI_PATTERN = @"^WIFI:S:\S+;T:(WEP|WPA|nopass);P:\S*;H:(true|false|blank);;$";
        public const string URI_PATTERN_NEW = @"^WIFI:((T|S|P|H|E|A|I|PH2):\S*;)*;$";

        public string? Type { get; private set; }
        public string? SSID { get; private set; }
        public string? Pass { get; private set; }
        public string? Hidden { get; private set; }
        public string? EAP { get; private set; }
        public string? Anonymous { get; private set; }
        public string? Identity { get; private set; }
        public string? Phase2 { get; private set; }

        private WifiUri() { }

        public static WifiUriBuilder Builder()
        {
            return new WifiUriBuilder();
        }

        public class WifiUriBuilder
        {
            private readonly WifiUri components;

            public WifiUriBuilder()
            {
                this.components = new WifiUri();
            }

            public WifiUriBuilder Type(string type)
            {
                this.components.Type = type;
                return this;
            }

            public WifiUriBuilder SSID(string SSID)
            {
                this.components.SSID = SSID;
                return this;
            }

            public WifiUriBuilder Pass(string pass)
            {
                this.components.Pass = pass;
                return this;
            }

            public WifiUriBuilder Hidden(string hidden)
            {
                this.components.Hidden = hidden;
                return this;
            }

            public WifiUriBuilder EAP(string EAP)
            {
                this.components.EAP = EAP;
                return this;
            }

            public WifiUriBuilder Anonymous(string anonymous)
            {
                this.components.Anonymous = anonymous;
                return this;
            }

            public WifiUriBuilder Identity(string identity)
            {
                this.components.Identity = identity;
                return this;
            }

            public WifiUriBuilder Phase2(string phase2)
            {
                this.components.Phase2 = phase2;
                return this;
            }

            public WifiUriBuilder Set(string key, string value)
            {
                switch (key)
                {
                    case TYPE_FIELD:
                        this.components.Type = value;
                        break;
                    case SSID_FIELD:
                        this.components.SSID = value;
                        break;
                    case PASS_FIELD:
                        this.components.Pass = value;
                        break;
                    case HIDDEN_FIELD:
                        this.components.Hidden = value;
                        break;
                    case EAP_FIELD:
                        this.components.EAP = value;
                        break;
                    case ANONYMOUS_FIELD:
                        this.components.Anonymous = value;
                        break;
                    case IDENTITY_FIELD:
                        this.components.Identity = value;
                        break;
                    case PHASE_2_FIELD:
                        this.components.Phase2 = value;
                        break;
                    default:
                        break;
                }
                return this;
            }

            public WifiUri Build()
            {
                return this.components;
            }
        }
    }
}
