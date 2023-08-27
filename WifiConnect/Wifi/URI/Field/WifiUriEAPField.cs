using WifiConnect.Wifi.URI.Parser.Validation;
using WifiConnect.Wifi.URI.Parser.Validation.Generic;

namespace WifiConnect.Wifi.URI.Field
{
    internal class WifiUriEAPField : WifiUriField
    {
        public WifiUriEAPField(string value)
            : base(WifiUri.FieldName.EAP, value) { }

        public override IEnumerable<IValidator<string>> GetValidators()
        {
            return new List<IValidator<string>>()
            {
                new IsNotEmptyValidator(),
                new IsContainedInValidator(new string[]{ "MD5", "TLS", "TTLS", "LEAP", "PEAP", "EAP-FAST", "PWD" })
            };
        }
    }
}
