namespace WifiConnect.Decoder
{
    internal interface IDecoder
    {
        public event EventHandler<EventArgs>? DecodingStart;

        public event EventHandler<DecoderEventArgs>? DecodingEnd;

        public string? Decode(Bitmap image);
    }
}