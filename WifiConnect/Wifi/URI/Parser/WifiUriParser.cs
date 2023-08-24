namespace WifiConnect.Wifi.URI.Parser
{
    internal class WifiUriParser : IWifiUriParser
    {
        public IWifiUriParser.WifiUriComponents Parse(string uri)
        {
            if (!((IWifiUriParser)this).IsWifiUri(uri))
            {
                throw new InvalidFormatException();
            }
            string[] fields = uri[IWifiUriParser.SCHEME.Length..].Split(IWifiUriParser.FIELD_DELIMITER);
            IWifiUriParser.WifiUriComponents uriComponents = new();
            foreach (string field in fields)
            {
                ParseField(field, uriComponents);
            }
            return uriComponents;
        }

        private static void ParseField(string field, IWifiUriParser.WifiUriComponents uriComponents)
        {
            string[] fieldParts = field.Split(IWifiUriParser.DELIMITER);
            if (fieldParts.Length == 2)
            {
                uriComponents.SetComponent(fieldParts[0], fieldParts[1]);
            }
        }
    }
}
