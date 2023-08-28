namespace WifiConnect.Decoder
{
    internal class DecoderEventArgs : EventArgs
    {
        public DecoderEventArgs(string? result)
        {
            this.Success = result != null;
            this.Result = result;
        }

        public DecoderEventArgs() : this(null) { }

        public string? Result { get; private set; }
        public bool Success { get; private set; }
    }
}