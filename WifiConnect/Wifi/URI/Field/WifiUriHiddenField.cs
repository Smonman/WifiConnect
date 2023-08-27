﻿using WifiConnect.Wifi.URI.Parser.Validation;
using WifiConnect.Wifi.URI.Parser.Validation.Generic;

namespace WifiConnect.Wifi.URI.Field
{
    internal class WifiUriHiddenField : WifiUriField
    {
        public WifiUriHiddenField(string value)
            : base(WifiUri.FieldName.HIDDEN, value) { }

        public override IEnumerable<IValidator<string>> GetValidators()
        {
            return new List<IValidator<string>>()
            {
                new IsNotEmptyValidator(),
                new IsContainedInValidator(new string[] { "true", "false", "blank"})
            };
        }
    }
}
