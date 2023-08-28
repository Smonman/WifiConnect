using WifiConnect.Wifi.URI.Parser.Validation;
using WifiConnect.Wifi.URI.Parser.Validation.Generic;

namespace WifiConnect.Wifi.URI.Field
{
    internal class WifiUriIdentityField : WifiUriField
    {
        public WifiUriIdentityField(string value)
            : base(WifiUri.FieldName.Identity, value) { }

        public override IEnumerable<IValidator<string>> GetValidators()
        {
            return new List<IValidator<string>>
            {
                new IsNotEmptyValidator()
            };
        }
    }
}