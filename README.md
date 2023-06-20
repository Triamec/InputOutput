# InputOutput

[![TAM - API](https://img.shields.io/static/v1?label=TAM&message=API&color=b51839)](https://www.triamec.com/en/tam-api.html)

The InputOutput sample is a .NET Windows Forms application demonstrating how to read inputs to and write outputs from your devices using the Triamec Advanced Motion (TAM) Software.

*Caution: you may harm your hardware when executing sample applications
without adjusting configuration values to your hardware environment.
Please read and follow the recommendations below
before executing any sample application.*

![TAM Acquisition](./doc/Acquisition_Movement.png)

## Hardware Prerequisites

- *Triamec* drive with a motor and encoder connected and configured with a stable position controller
- Connection to the drive by *Tria-Link* (via PCI adapter), *USB* or *Ethernet*

## Hardware Configuration Adjustment

The sample works with the first device found in the link. If you have attached several devices in the Tria-Link,
please make sure you test the I/Os of the correct device.

Depending on the product, another register layout may be present. If it is another than the expected,
the program will crash. You can change the expected ID by changing the using statement at the top of the
InputOutputForm.cs source file (using Triamec.Tam.Rlid19).

## Software Prerequisites

This project is made and built with [Microsoft Visual Studio](https://visualstudio.microsoft.com/en/).

In addition you need [TAM Software](https://www.triamec.com/en/tam-software-support.html) installation.

## Run the *Acquisition* Application

1. Open the `Acquisition.sln`.
2. Open the `AcquisitionForm.cs` (view code)
3. Set the name of the axis for `AxisName`. Double check it in the register *Axes[].Information.AxisName* using the *TAM System Explorer*.
4. Set the name of the network interface card for `NicName`. You can find this name using the *TAM System Explorer*. In the example below, `NicName = "Ethernet 2"`.

![TAM Acquisition](./doc/Network_NicName.png)

5. Set `_moveAxis` to `true` if you want to use the trigger for the acquisition
6. Select the correct connection to the drive by using comment/uncomment for setting the `access` variable 

## Operate the *Acquisition* Application

- Use the slider **Trigger** to adjust the velocity needed to start the acquisition. If `_moveAxis` is set to `false`, **Trigger** is ignored (continuous acquisition)
- Use the slider **Recording time** to adjust the length of the acquisition
