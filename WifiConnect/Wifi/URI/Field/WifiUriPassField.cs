using WifiConnect.Wifi.URI.Parser.Validation;
using WifiConnect.Wifi.URI.Parser.Validation.Generic;

namespace WifiConnect.Wifi.URI.Field
{
    internal class WifiUriPassField : WifiUriField
    {
        public WifiUriPassField(string value)
            : base(WifiUri.FieldName.PASS, value) { }

        public override IEnumerable<IValidator<string>> GetValidators()
        {
            return new List<IValidator<string>>()
            {
                new IsNotEmptyValidator()
            };
        }
    }
}
