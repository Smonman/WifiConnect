namespace WifiConnect.Wifi.URI.Parser.Validation
{
    [Serializable]
    internal class ValidationException : Exception
    {
        public ValidationException() { }

        public ValidationException(string message) : base(message) { }

        public ValidationException(string message, Exception inner) : base(message, inner) { }

        protected ValidationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
