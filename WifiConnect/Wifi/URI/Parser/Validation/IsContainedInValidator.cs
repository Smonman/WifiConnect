using WifiConnect.Wifi.URI.Parser.Validation.Generic;

namespace WifiConnect.Wifi.URI.Parser.Validation
{
    internal class IsContainedInValidator : IValidator<string>
    {
        private readonly IEnumerable<string> values;

        public IsContainedInValidator(IEnumerable<string> values)
        {
            this.values = values;
        }

        public void ApplyTo(string input)
        {
            if (!this.values.Contains(input))
            {
                throw new ValidationException($"'{input}' must be contained in [{String.Join(',', this.values)}]");
            }
        }
    }
}
