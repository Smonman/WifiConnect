namespace WifiConnect.Wifi.URI.Parser.Validation.Generic
{
    internal interface IValidateable<in T>
    {
        public IEnumerable<IValidator<T>> GetValidators();

        public void Validate();
    }
}
