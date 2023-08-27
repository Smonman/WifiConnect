namespace WifiConnect.Wifi.URI.Parser.Validation.Generic
{
    internal interface IValidateable<T>
    {
        public IEnumerable<IValidator<T>> GetValidators();

        public void Validate();
    }
}
