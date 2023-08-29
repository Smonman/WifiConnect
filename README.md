# WifiConnect

![icon_small](https://github.com/Smonman/WifiConnect/assets/36928284/39ff8c41-5772-45c1-b064-06ba450e0662)

_Connect to an available Wifi network via a QR code on Windows._

Connect to a Wifi using a QR code
utilizing [this format](https://en.wikipedia.org/wiki/QR_code#Joining_a_Wi%E2%80%91Fi_network), used by Android and iOS
11+ (further
information [here](https://github.com/zxing/zxing/wiki/Barcode-Contents#wi-fi-network-config-android-ios-11)).

## What does this do?

This tries to connect to an available wireless network, based on a [QR code](https://en.wikipedia.org/wiki/QR_code),
containing required information.

## FAQ

### What do I need?

- A webcam
- Wifi capabilities

### How do I install this application?

You have three options:

1. [Download the source files, and compile it yourself](https://github.com/Smonman/WifiConnect/archive/refs/tags/v1.0.0.zip)
2. [Download the compiled files, and extract them to the a location of your choosing](https://github.com/Smonman/WifiConnect/releases/latest/download/WifiConnect_v1.0.0.zip)
3. [Let the wizard do the work for you](https://github.com/Smonman/WifiConnect/releases/latest/download/WifiConnectInstaller.msi)

All needed files are found [here](https://github.com/Smonman/WifiConnect/releases)

### Which platforms are supported?

- Windows (tested on Windows 10)

### Is this secure?

It is important to note, that information contained in the QR code will be used as-is by this application. The
application never shows this information explicitly (apart from the networks name), nor does it persit it outside of its
lifecycle.

### What must the QR code contain?

Take a look at [this format](https://en.wikipedia.org/wiki/QR_code#Joining_a_Wi%E2%80%91Fi_network).

## Known Issues

This application does not support the [OBS virtual camera](https://obsproject.com/kb/virtual-camera-guide). This won't be fixed in the foreseeable future, sorry.
