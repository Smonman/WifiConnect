using WifiConnect.Wifi.URI.Parser.Validation.Generic;

namespace WifiConnect.Wifi.URI.Parser.Validation
{
    internal class IsNotEmptyValidator : IValidator<string>
    {
        public void ApplyTo(string input)
        {
            if (input == null || input.Length <= 0)
            {
                throw new ValidationException($"'{input}' must not be empty");
            }
        }
    }
}
