using WifiConnect.Wifi.URI.Parser.Validation;
using WifiConnect.Wifi.URI.Parser.Validation.Generic;

namespace WifiConnect.Wifi.URI.Field
{
    internal class WifiUriEapField : WifiUriField
    {
        public WifiUriEapField(string value)
            : base(WifiUri.FieldName.Eap, value) { }

        public override IEnumerable<IValidator<string>> GetValidators()
        {
            return new List<IValidator<string>>
            {
                new IsNotEmptyValidator(),
                new IsContainedInValidator(new[] { "MD5", "TLS", "TTLS", "LEAP", "PEAP", "EAP-FAST", "PWD" })
            };
        }
    }
}
