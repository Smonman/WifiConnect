using AForge.Video;
using AForge.Video.DirectShow;
using WifiConnect.Decoder;
using static WifiConnect.Wifi.Manager.IWifiManager;

namespace WifiConnect
{
    public partial class MainForm : Form
    {
        private FilterInfoCollection? captureDevices;
        private VideoCaptureDevice? selectedDevice;
        private NewFrameEventHandler? newFrameEventHandler;
        private readonly WifiConnect wifiConnect;

        private const string STATUS_NONE = "";
        private const string STATUS_SCANNING = "scanning";
        private const string STATUS_DECODING = "decoding";
        private const string STATUS_CONNECTING = "connecting";
        private const string MESSAGE_NONE = "";
        private const string MESSAGE_DECODING_SUCCESS = "successfully decoded";
        private const string MESSAGE_DECODING_FAILURE = "cannot decode";
        private const string MESSAGE_PARSING_FAILURE = "unsupported format";
        private const string MESSAGE_CONNECTING_SUCCESS = "successfully connected";
        private const string MESSAGE_CONNECTING_FAILURE = "cannot connect";

        public MainForm()
        {
            this.InitializeComponent();
            this.wifiConnect = new WifiConnect();
        }

        public void StartScanning()
        {
            this.SwitchCaptureDevice();
            this.StatusLabel.Text = STATUS_SCANNING;
            this.Timer.Start();
        }

        public void StopScanning()
        {
            this.Timer.Stop();
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
            Bitmap image = (Bitmap)this.PictureBox.Image;
            this.wifiConnect.TryToConnectFromImage(image);
        }

        private void SelectedDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap newFrame = eventArgs.Frame;
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

        private void InitComponentValues()
        {
            this.StatusLabel.Text = STATUS_NONE;
            this.MessageLabel.Text = MESSAGE_NONE;
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
        }

        private void DeregisterEvents()
        {

            this.wifiConnect.DecoderStart -= this.Decoder_Start;
            this.wifiConnect.DecoderEnd -= this.Decoder_End;
            this.wifiConnect.ManagerConnectingStart -= this.Manager_Start;
            this.wifiConnect.ManagerConnectingEnd -= this.Manager_End;
            this.wifiConnect.ParserFailure -= this.Parser_Failure;
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
                this.newFrameEventHandler = new NewFrameEventHandler(this.SelectedDevice_NewFrame);
                this.selectedDevice = new VideoCaptureDevice(this.captureDevices[selectedDeviceIndex].MonikerString);
                this.selectedDevice.NewFrame += this.newFrameEventHandler;
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
            this.StatusLabel.Text = STATUS_DECODING;
            this.MessageLabel.Text = MESSAGE_NONE;
        }

        private void Decoder_End(object? sender, DecoderEventArgs e)
        {
            this.StatusLabel.Text = STATUS_SCANNING;
            this.MessageLabel.Text = e.Success ? MESSAGE_DECODING_SUCCESS : MESSAGE_DECODING_FAILURE;
        }

        private void Manager_Start(object? sender, EventArgs e)
        {
            this.StatusLabel.Text = STATUS_CONNECTING;
            this.MessageLabel.Text = MESSAGE_NONE;
            this.StopScanning();
        }

        private void Manager_End(object? sender, WifiEventArgs e)
        {
            if (e.Connected)
            {
                this.MessageLabel.Text = MESSAGE_CONNECTING_SUCCESS;
                DialogResult res = MessageBox.Show($"Successfully connected to Wifi: '{e.SSID}'.\nClose this application?", "Connection Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (res == DialogResult.Yes)
                {
                    Application.Exit();
                    return;
                }
            }
            else
            {
                this.MessageLabel.Text = MESSAGE_CONNECTING_FAILURE;
                DialogResult res = MessageBox.Show($"Could not connect to Wifi: '{e.SSID}'\nMake sure that the adapter is enabled.", "Connection Failure", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
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
            this.MessageLabel.Text = MESSAGE_PARSING_FAILURE;
        }
    }
}
