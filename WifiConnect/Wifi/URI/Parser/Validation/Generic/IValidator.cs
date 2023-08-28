namespace WifiConnect.Wifi.URI.Parser.Validation.Generic
{
    internal interface IValidator<in T>
    {
        public void ApplyTo(T value);
    }
}
