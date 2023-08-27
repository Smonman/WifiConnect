using WifiConnect.Wifi.URI.Parser.Validation;
using WifiConnect.Wifi.URI.Parser.Validation.Generic;

namespace WifiConnect.Wifi.URI.Field
{
    internal class WifiUriPhase2Field : WifiUriField
    {
        public WifiUriPhase2Field(string value)
            : base(WifiUri.FieldName.PHASE_2, value) { }

        public override IEnumerable<IValidator<string>> GetValidators()
        {
            return new List<IValidator<string>>()
            {
                new IsContainedInValidator(new string[] { "MSCHAPV2" })
            };
        }
    }
}
