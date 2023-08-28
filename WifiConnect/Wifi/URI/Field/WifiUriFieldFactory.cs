namespace WifiConnect.Wifi.URI.Field
{
    internal static class WifiUriFieldFactory
    {
        public static WifiUriField Create(string name, string value)
        {
            bool success = Enum.TryParse(name, true, out WifiUri.FieldName fieldName);
            if (!success)
            {
                fieldName = name switch
                {
                    "T"   => WifiUri.FieldName.Type,
                    "S"   => WifiUri.FieldName.Ssid,
                    "P"   => WifiUri.FieldName.Pass,
                    "H"   => WifiUri.FieldName.Hidden,
                    "E"   => WifiUri.FieldName.Eap,
                    "A"   => WifiUri.FieldName.Anonymous,
                    "I"   => WifiUri.FieldName.Identity,
                    "PH2" => WifiUri.FieldName.Phase2,
                    _     => throw new ArgumentException("name must correspond to a valid field name", nameof(name))
                };
            }

            return Create(fieldName, value);
        }

        public static WifiUriField Create(WifiUri.FieldName name, string value)
        {
            return name switch
            {
                WifiUri.FieldName.Type      => new WifiUriTypeField(value),
                WifiUri.FieldName.Ssid      => new WifiUriSsidField(value),
                WifiUri.FieldName.Pass      => new WifiUriPassField(value),
                WifiUri.FieldName.Hidden    => new WifiUriHiddenField(value),
                WifiUri.FieldName.Eap       => new WifiUriEapField(value),
                WifiUri.FieldName.Anonymous => new WifiUriAnonymousField(value),
                WifiUri.FieldName.Identity  => new WifiUriIdentityField(value),
                WifiUri.FieldName.Phase2    => new WifiUriPhase2Field(value),
                _                           => throw new InvalidOperationException()
            };
        }
    }
}
