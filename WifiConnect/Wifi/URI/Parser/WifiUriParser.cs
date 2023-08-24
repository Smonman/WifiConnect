namespace WifiConnect.Wifi.URI.Parser
{
    internal class WifiUriParser : IWifiUriParser
    {
        public WifiUri Parse(string uri)
        {
            if (!((IWifiUriParser)this).IsWifiUri(uri))
            {
                throw new InvalidFormatException();
            }
            string[] fields = uri[WifiUri.SCHEME.Length..].Split(WifiUri.FIELD_DELIMITER);
            WifiUri.WifiUriBuilder builder = WifiUri.Builder();
            foreach (string field in fields)
            {
                ParseField(field, builder);
            }
            return builder.Build();
        }

        private static void ParseField(string field, WifiUri.WifiUriBuilder builder)
        {
            string[] fieldParts = field.Split(WifiUri.DELIMITER);
            if (fieldParts.Length == 2)
            {
                _ = builder.Set(fieldParts[0], fieldParts[1]);
            }
        }

        WifiUri IWifiUriParser.Parse(string uri)
        {
            throw new NotImplementedException();
        }
    }
}
