using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using WifiConnect.Wifi.URI.Field;
using WifiConnect.Wifi.URI.Parser.Validation.Generic;

namespace WifiConnect.Wifi.URI.Parser
{
    internal partial class WifiUriParser : Validateable<string>, IWifiUriParser
    {
        private static readonly char[] escapedCharacters = { '\\', ';', ',', '"', ':' };
        private string? uri;
        private WifiUri? wifiUri;

        public WifiUri Parse(string uri)
        {
            this.uri = uri;
            try
            {
                IEnumerable<string> fields = GetFields(uri);
                this.wifiUri = ToWifiUri(fields);
                this.Validate();
                return this.wifiUri;
            }
            catch (ValidationException e)
            {
                throw new InvalidFormatException("not a valid format", e);
            }
        }

        [GeneratedRegex("(?=[A-Z0-9]+:)")]
        private static partial Regex WifiUriFieldPattern();

        private static IEnumerable<string> GetFields(string rawUri)
        {
            string innerUri = rawUri[WifiUri.SCHEME.Length..^1];
            string[] fields = WifiUriFieldPattern().Split(innerUri);
            return SanitizeFields(fields);
        }

        private static IEnumerable<string> SanitizeFields(IEnumerable<string> rawFields)
        {
            return rawFields
                .Select(e => e.TrimEnd(new char[] { ';' }))
                .Select(ReplaceEscapedCharacters)
                .Where(e => e.Length > 0);
        }

        private static string ReplaceEscapedCharacters(string rawField)
        {
            foreach (char escaped in WifiUriParser.escapedCharacters)
            {
                rawField = rawField.Replace($"\\{escaped}", escaped.ToString());
            }
            return rawField;
        }

        private static WifiUri ToWifiUri(IEnumerable<string> fields)
        {
            WifiUri result = new();
            IEnumerable<WifiUriField> uriFields = fields
                .Select(e => e.Split(WifiUri.FIELD_DELIMITER))
                .Where(e => e.Length == 2)
                .Select(e => WifiUriFieldFactory.Create(e[0], e[1]));
            foreach (WifiUriField uf in uriFields)
            {
                _ = result.Fields.Add(uf);
            }
            return result;
        }

        public override void Validate()
        {
            if (this.uri != null && this.wifiUri != null)
            {
                if (!Regex.Match(this.uri, WifiUri.URI_PATTERN).Success)
                {
                    throw new ValidationException("invalid structure");
                }
                this.wifiUri.Validate();
            }
        }
    }
}
