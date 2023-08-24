using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;

namespace WifiConnect.Decoder
{
    internal class Decoder : IDecoder
    {
        private readonly IBarcodeReader<Bitmap> barcodeReader;

        public event EventHandler<EventArgs>? DecodingStart;
        public event EventHandler<DecoderEventArgs>? DecodingEnd;

        public Decoder()
        {
            this.barcodeReader = new BarcodeReader
            {
                AutoRotate = true,
                Options = new DecodingOptions { TryHarder = true }
            };
        }

        public string? Decode(Bitmap image)
        {
            this.OnDecodingStart();
            Result result = this.barcodeReader.Decode(image);
            string? resultString = result?.Text.Trim() ?? null;
            if (resultString != null && resultString.Length > 0)
            {
                this.OnDecodingSuccess(resultString);
                return resultString;
            }
            this.OnDecodingFailure();
            return null;
        }

        private void OnDecodingStart()
        {
            DecodingStart?.Invoke(this, EventArgs.Empty);
        }

        private void OnDecodingSuccess(string result)
        {
            DecodingEnd?.Invoke(this, new DecoderEventArgs(result));
        }

        private void OnDecodingFailure()
        {
            DecodingEnd?.Invoke(this, new DecoderEventArgs());
        }
    }
}
