using WifiConnect.Wifi.URI.Parser.Validation.Generic;

namespace WifiConnect.Wifi.URI.Field
{
    internal abstract class WifiUriField : Validateable<string>
    {
        public WifiUri.FieldName Name { get; private set; }
        public string Value { get; private set; }

        protected WifiUriField(WifiUri.FieldName name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public override void Validate()
        {
            foreach (IValidator<string> validator in this.GetValidators())
            {
                validator.ApplyTo(this.Value);
            }
        }
    }
}
