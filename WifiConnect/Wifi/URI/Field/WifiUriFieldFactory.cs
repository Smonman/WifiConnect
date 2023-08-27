namespace WifiConnect.Wifi.URI.Field
{
    internal class WifiUriFieldFactory
    {
        private WifiUriFieldFactory() { }

        public static WifiUriField Create(string name, string value)
        {
            bool success = Enum.TryParse(name, true, out WifiUri.FieldName fieldName);
            if (!success)
            {
                fieldName = name switch
                {
                    "T" => WifiUri.FieldName.TYPE,
                    "S" => WifiUri.FieldName.SSID,
                    "P" => WifiUri.FieldName.PASS,
                    "H" => WifiUri.FieldName.HIDDEN,
                    "E" => WifiUri.FieldName.EAP,
                    "A" => WifiUri.FieldName.ANONYMOUS,
                    "I" => WifiUri.FieldName.IDENTITY,
                    "PH2" => WifiUri.FieldName.PHASE_2,
                    _ => throw new ArgumentException("name must correspond to a valid field name", nameof(name)),
                };
            }
            return WifiUriFieldFactory.Create(fieldName, value);
        }

        public static WifiUriField Create(WifiUri.FieldName name, string value)
        {
            return name switch
            {
                WifiUri.FieldName.TYPE => new WifiUriTypeField(value),
                WifiUri.FieldName.SSID => new WifiUriSSIDField(value),
                WifiUri.FieldName.PASS => new WifiUriPassField(value),
                WifiUri.FieldName.HIDDEN => new WifiUriHiddenField(value),
                WifiUri.FieldName.EAP => new WifiUriEAPField(value),
                WifiUri.FieldName.ANONYMOUS => new WifiUriAnonymousField(value),
                WifiUri.FieldName.IDENTITY => new WifiUriIdentityField(value),
                WifiUri.FieldName.PHASE_2 => new WifiUriPhase2Field(value),
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
