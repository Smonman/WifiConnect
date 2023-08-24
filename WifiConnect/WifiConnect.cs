﻿using WifiConnect.Decoder;
using WifiConnect.Wifi.Manager;
using WifiConnect.Wifi.URI.Parser;
using static WifiConnect.Wifi.Manager.IWifiManager;

namespace WifiConnect
{
    internal class WifiConnect
    {
        public event EventHandler<EventArgs>? DecoderStart;
        public event EventHandler<DecoderEventArgs>? DecoderEnd;
        public event EventHandler<EventArgs>? ManagerConnectingStart;
        public event EventHandler<WifiEventArgs>? ManagerConnectingEnd;
        public event EventHandler<EventArgs>? ParserFailure;

        private readonly IDecoder decoder;
        private readonly IWifiUriParser uriParser;
        private readonly IWifiManager wifiManager;

        public WifiConnect()
        {
            this.decoder = new Decoder.Decoder();
            this.uriParser = new WifiUriParser();
            this.wifiManager = new WifiManager();
            this.RegisterEvents();
        }

        ~WifiConnect()
        {
            this.DeregsterEvents();
        }

        public void TryToConnectFromImage(Bitmap? image)
        {
            if (image != null)
            {
                string? qrCodeResult = this.decoder.Decode(image);
                if (qrCodeResult != null)
                {
                    this.TryToConnectFromUri(qrCodeResult);
                }
            }
        }

        private void TryToConnectFromUri(string uri)
        {
            try
            {
                IWifiUriParser.WifiUriComponents components = this.uriParser.Parse(uri);
                this.wifiManager.Connect(components.SSID, components.Pass);
            }
            catch (InvalidFormatException)
            {
                this.OnParserFailure();
            }
        }

        private void RegisterEvents()
        {
            this.decoder.DecodingStart += this.Decoder_Start;
            this.decoder.DecodingEnd += this.Decoder_End;
            this.wifiManager.WifiConnectionStart += this.Manager_Start;
            this.wifiManager.WifiConnectionEnd += this.Manager_End;
        }

        private void DeregsterEvents()
        {
            this.decoder.DecodingStart -= this.Decoder_Start;
            this.decoder.DecodingEnd -= this.Decoder_End;
            this.wifiManager.WifiConnectionStart -= this.Manager_Start;
            this.wifiManager.WifiConnectionEnd -= this.Manager_End;
        }

        private void Decoder_Start(object? sender, EventArgs e)
        {
            this.DecoderStart?.Invoke(this, e);
        }

        private void Decoder_End(object? sender, DecoderEventArgs e)
        {
            this.DecoderEnd?.Invoke(this, e);
        }

        private void Manager_Start(object? sender, EventArgs e)
        {
            this.ManagerConnectingStart?.Invoke(this, e);
        }

        private void Manager_End(object? sender, WifiEventArgs e)
        {
            this.ManagerConnectingEnd?.Invoke(this, e);
        }

        private void OnParserFailure()
        {
            this.ParserFailure?.Invoke(this, EventArgs.Empty);
        }
    }
}
