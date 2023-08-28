using WifiConnect.Wifi.URI.Field;
using WifiConnect.Wifi.URI.Parser.Validation.Generic;

namespace WifiConnect.Wifi.URI
{
    internal class WifiUri : Validateable<string>
    {
        public static readonly string SCHEME = "WIFI:";
        public static readonly string DELIMITER = ";";
        public static readonly string FIELD_DELIMITER = ":";
        public static readonly string URI_PATTERN = @"^WIFI:((T|P|H|E|A|I|PH2):\S*;)*(S:\S+;)((T|S|P|H|E|A|I|PH2):\S*;)*;$";

        public enum FieldName
        {
            TYPE,
            SSID,
            PASS,
            HIDDEN,
            EAP,
            ANONYMOUS,
            IDENTITY,
            PHASE_2
        }

        public HashSet<WifiUriField> Fields { get; private set; }

        public WifiUri()
        {
            this.Fields = new HashSet<WifiUriField>(Enum.GetNames<FieldName>().Length);
        }

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
                .Where(e => e != null)
                .FirstOrDefault(e => e?.Name == fieldName, null);
        }
    }
}
