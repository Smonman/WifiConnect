using WifiConnect.Wifi.URI.Parser.Validation;
using WifiConnect.Wifi.URI.Parser.Validation.Generic;

namespace WifiConnect.Wifi.URI.Field
{
    internal class WifiUriSsidField : WifiUriField
    {
        public WifiUriSsidField(string value)
            : base(WifiUri.FieldName.Ssid, value) { }

        public override IEnumerable<IValidator<string>> GetValidators()
        {
            return new List<IValidator<string>>
            {
                new IsNotEmptyValidator()
            };
        }
    }
}
