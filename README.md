# WifiConnect
Connect to an available Wifi network via a QR code on Windows.

Connect to a Wifi using a QR code utilizing [this format](https://en.wikipedia.org/wiki/QR_code#Joining_a_Wi%E2%80%91Fi_network), used by Android and IOS 11+ (further information [here](https://en.wikipedia.org/wiki/QR_code#Joining_a_Wi%E2%80%91Fi_network)).

## What does this do?

This tries to connect to an available wireless network, based on a QR code, containing required information.

## What do I need?

- A webcam

## Which platforms are supported?

- Windows (tested on Windows 10)

## Is this secure?

It is important to note, that information contained in the QR code will be used as-is by this application. The application never shows this information explicitly (apart from the networks name), nor does it persit it outside of its livecycle.
