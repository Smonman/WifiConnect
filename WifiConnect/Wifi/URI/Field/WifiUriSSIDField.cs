using WifiConnect.Wifi.URI.Parser.Validation;
using WifiConnect.Wifi.URI.Parser.Validation.Generic;

namespace WifiConnect.Wifi.URI.Field
{
    internal class WifiUriSSIDField : WifiUriField
    {
        public WifiUriSSIDField(string value)
            : base(WifiUri.FieldName.SSID, value) { }

        public override IEnumerable<IValidator<string>> GetValidators()
        {
            return new List<IValidator<string>>()
            {
                new IsNotEmptyValidator()
            };
        }
    }
}
