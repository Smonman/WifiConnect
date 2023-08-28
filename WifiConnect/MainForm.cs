using AForge.Video;
using AForge.Video.DirectShow;
using WifiConnect.Decoder;
using static WifiConnect.Wifi.Manager.IWifiManager;

namespace WifiConnect
{
    public partial class MainForm : Form
    {
        private const string StatusNone = "";
        private const string StatusScanning = "scanning";
        private const string StatusDecoding = "decoding";
        private const string StatusConnecting = "connecting";
        private const string MessageNone = "";
        private const string MessageDecodingSuccess = "successfully decoded";
        private const string MessageDecodingFailure = "cannot decode";
        private const string MessageParsingFailure = "unsupported format";
        private const string MessageConnectingSuccess = "successfully connected";
        private const string MessageConnectingFailure = "cannot connect";
        private readonly VideoSourceErrorEventHandler? videoSourceErrorEventHandler;
        private readonly WifiConnect wifiConnect;
        private FilterInfoCollection? captureDevices;
        private NewFrameEventHandler? newFrameEventHandler;
        private VideoCaptureDevice? selectedDevice;

        public MainForm()
        {
            this.InitializeComponent();
            this.wifiConnect = new WifiConnect();
            this.videoSourceErrorEventHandler = this.VideoSource_Error;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.InitComponentValues();
            this.RegisterEvents();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.StopCaptureDevice();
            this.ClearPictureBox();
            this.StopScanning();
            this.ClearTimer();
            this.DeregisterEvents();
        }

        private void CamerasComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.StartScanning();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Bitmap? image = (Bitmap)this.PictureBox.Image;
            this.wifiConnect.TryToConnectFromImage(image);
        }

        private void SelectedDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap? newFrame = eventArgs.Frame;
            if (newFrame == null)
            {
                this.ClearPictureBox();
            }
            else
            {
                newFrame.RotateFlip(RotateFlipType.RotateNoneFlipX);
                this.PictureBox.Image = (Image)newFrame.Clone();
            }
        }

        private void StartScanning()
        {
            this.SwitchCaptureDevice();
            this.StatusLabel.Text = StatusScanning;
            this.Timer.Start();
        }

        private void StopScanning()
        {
            this.Timer.Stop();
        }

        private void InitComponentValues()
        {
            this.StatusLabel.Text = StatusNone;
            this.MessageLabel.Text = MessageNone;
            this.InitCameraSelection();
        }

        private void InitCameraSelection()
        {
            this.captureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in this.captureDevices)
            {
                _ = this.CamerasComboBox.Items.Add(device.Name);
            }

            if (this.captureDevices.Count > 0)
            {
                this.CamerasComboBox.SelectedIndex = 0;
            }
        }

        private void RegisterEvents()
        {
            this.wifiConnect.DecoderStart += this.Decoder_Start;
            this.wifiConnect.DecoderEnd += this.Decoder_End;
            this.wifiConnect.ManagerConnectingStart += this.Manager_Start;
            this.wifiConnect.ManagerConnectingEnd += this.Manager_End;
            this.wifiConnect.ParserFailure += this.Parser_Failure;
            this.wifiConnect.ConnectionFailure += this.Connection_Failure;
        }

        private void DeregisterEvents()
        {
            this.wifiConnect.DecoderStart -= this.Decoder_Start;
            this.wifiConnect.DecoderEnd -= this.Decoder_End;
            this.wifiConnect.ManagerConnectingStart -= this.Manager_Start;
            this.wifiConnect.ManagerConnectingEnd -= this.Manager_End;
            this.wifiConnect.ParserFailure -= this.Parser_Failure;
            this.wifiConnect.ConnectionFailure -= this.Connection_Failure;
        }

        private void SwitchCaptureDevice()
        {
            this.StopCaptureDevice();
            this.StartCaptureDevice();
        }

        private void StartCaptureDevice()
        {
            if (this.captureDevices != null && this.captureDevices.Count > 0)
            {
                int selectedDeviceIndex = this.CamerasComboBox.SelectedIndex;
                this.selectedDevice = new VideoCaptureDevice(this.captureDevices[selectedDeviceIndex].MonikerString);
                this.newFrameEventHandler = this.SelectedDevice_NewFrame;
                this.selectedDevice.NewFrame += this.newFrameEventHandler;
                this.selectedDevice.VideoSourceError += this.videoSourceErrorEventHandler;
                this.selectedDevice.Start();
            }
        }

        private void StopCaptureDevice()
        {
            if (this.selectedDevice != null && this.selectedDevice.IsRunning)
            {
                if (this.newFrameEventHandler != null)
                {
                    this.selectedDevice.NewFrame -= this.newFrameEventHandler;
                    this.newFrameEventHandler = null;
                }

                this.selectedDevice.VideoSourceError -= this.videoSourceErrorEventHandler;

                this.selectedDevice.SignalToStop();
                this.selectedDevice.WaitForStop();
                this.selectedDevice = null;
                this.ClearPictureBox();
            }
        }

        private void ClearPictureBox()
        {
            if (this.PictureBox.Image != null)
            {
                this.PictureBox.Image.Dispose();
                this.PictureBox.Image = null;
            }
        }

        private void ClearTimer()
        {
            if (this.Timer != null)
            {
                this.Timer.Stop();
                this.Timer.Dispose();
            }
        }

        private void Decoder_Start(object? sender, EventArgs e)
        {
            this.StatusLabel.Text = StatusDecoding;
            this.MessageLabel.Text = MessageNone;
        }

        private void Decoder_End(object? sender, DecoderEventArgs e)
        {
            this.StatusLabel.Text = StatusScanning;
            this.MessageLabel.Text = e.Success ? MessageDecodingSuccess : MessageDecodingFailure;
        }

        private void Manager_Start(object? sender, EventArgs e)
        {
            this.StatusLabel.Text = StatusConnecting;
            this.MessageLabel.Text = MessageNone;
            this.StopScanning();
        }

        private void Manager_End(object? sender, WifiEventArgs e)
        {
            if (e.Connected)
            {
                this.MessageLabel.Text = MessageConnectingSuccess;
                DialogResult res = MessageBox.Show(
                    $"Successfully connected to Wifi: '{e.Ssid}'.\nClose this application?",
                    "Connection Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
                if (res == DialogResult.Yes)
                {
                    Application.Exit();
                    return;
                }
            }
            else
            {
                this.MessageLabel.Text = MessageConnectingFailure;
                DialogResult res = MessageBox.Show(
                    $"Could not connect to Wifi: '{e.Ssid}'\nMake sure that the adapter is enabled.",
                    "Connection Failure", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);
                if (res == DialogResult.Cancel)
                {
                    Application.Exit();
                    return;
                }
            }

            this.StartScanning();
        }

        private void Parser_Failure(object? sender, EventArgs e)
        {
            this.MessageLabel.Text = MessageParsingFailure;
        }

        private void Connection_Failure(object? sender, EventArgs e)
        {
            this.MessageLabel.Text = MessageConnectingFailure;
            if (MessageBox.Show("Could not connect to the network.\nMake sure to have the Wifi option enabled.",
                    "Error",
                    MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) ==
                DialogResult.Cancel)
            {
                Application.Exit();
            }
        }

        private void VideoSource_Error(object? sender, VideoSourceErrorEventArgs e)
        {
            MessageBox.Show(
                $"An unexpected error occured:\n{e.Description}\nPlease try again later or with a different camera.",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
