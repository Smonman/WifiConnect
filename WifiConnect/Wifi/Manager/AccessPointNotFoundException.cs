namespace WifiConnect.Wifi.Manager
{
    [Serializable]
    internal class AccessPointNotFoundException : Exception
    {
        public AccessPointNotFoundException() : base() { }

        public AccessPointNotFoundException(string message) : base(message) { }

        public AccessPointNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}