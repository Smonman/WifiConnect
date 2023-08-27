namespace WifiConnect.Wifi.URI.Parser.Validation.Generic
{
    internal interface IValidator<T>
    {
        public void ApplyTo(T value);
    }
}
