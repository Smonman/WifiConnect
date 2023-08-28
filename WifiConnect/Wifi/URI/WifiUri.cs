using WifiConnect.Wifi.URI.Field;
using WifiConnect.Wifi.URI.Parser.Validation.Generic;

namespace WifiConnect.Wifi.URI
{
    internal class WifiUri : Validateable<string>
    {
        public enum FieldName
        {
            Type,
            Ssid,
            Pass,
            Hidden,
            Eap,
            Anonymous,
            Identity,
            Phase2
        }

        public const string SCHEME = "WIFI:";
        public const string DELIMITER = ";";
        public const string FIELD_DELIMITER = ":";
        public const string URI_PATTERN = @"^WIFI:((T|P|H|E|A|I|PH2):\S*;)*(S:\S+;)((T|S|P|H|E|A|I|PH2):\S*;)*;$";

        public WifiUri()
        {
            this.Fields = new HashSet<WifiUriField>(Enum.GetNames<FieldName>().Length);
        }

        public HashSet<WifiUriField> Fields { get; }

        public override void Validate()
        {
            foreach (WifiUriField field in this.Fields)
            {
                ((IValidateable<string>)field).Validate();
            }
        }

        public WifiUriField? GetField(FieldName fieldName)
        {
            return this.Fields
                .FirstOrDefault(e => e?.Name == fieldName, null);
        }
    }
}
