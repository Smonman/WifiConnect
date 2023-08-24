namespace WifiConnect.Decoder
{
    internal class DecoderEventArgs : EventArgs
    {
        public string? Result { get; private set; }
        public bool Success { get; private set; }

        public DecoderEventArgs(string? result) : base()
        {
            this.Success = result != null;
            this.Result = result;
        }

        public DecoderEventArgs() : this(null) { }
    }
}
