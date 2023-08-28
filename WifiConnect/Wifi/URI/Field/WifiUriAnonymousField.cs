using WifiConnect.Wifi.URI.Parser.Validation;
using WifiConnect.Wifi.URI.Parser.Validation.Generic;

namespace WifiConnect.Wifi.URI.Field
{
    internal class WifiUriAnonymousField : WifiUriField
    {
        public WifiUriAnonymousField(string value)
            : base(WifiUri.FieldName.Anonymous, value) { }

        public override IEnumerable<IValidator<string>> GetValidators()
        {
            return new List<IValidator<string>>
            {
                new IsNotEmptyValidator()
            };
        }
    }
}