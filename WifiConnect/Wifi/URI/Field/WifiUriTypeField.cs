﻿using WifiConnect.Wifi.URI.Parser.Validation;
using WifiConnect.Wifi.URI.Parser.Validation.Generic;

namespace WifiConnect.Wifi.URI.Field
{
    internal class WifiUriTypeField : WifiUriField
    {
        public WifiUriTypeField(string value)
            : base(WifiUri.FieldName.TYPE, value) { }

        public override IEnumerable<IValidator<string>> GetValidators()
        {
            return new List<IValidator<string>>()
            {
                new IsNotEmptyValidator(),
                new IsContainedInValidator(new string[] { "WEP", "WPA", "WPA-2-EAP", "nopass" })
            };
        }
    }
}
