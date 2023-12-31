# InputOutput

[![TAM - API](https://img.shields.io/static/v1?label=TAM&message=API&color=b51839)](https://www.triamec.com/en/tam-api.html)

The InputOutput sample is a .NET Windows Forms application demonstrating how to read inputs to and write outputs from your devices using the Triamec Advanced Motion (TAM) Software.

For the inputs two different methods are shown: Polling and cyclic subscription via a listener. 
Due to Windows Timer limitations, the polling event runs at up to 100Hz, while the listening event can reach 10kHz.


**Check the respective Hardware Manual for the input and output limits at https://www.triamec.com/en/documents.html**

<img src="./doc/Screenshot_InputOutput.png" width="50%" height="50%">

## Hardware Prerequisites

- *Triamec* drive with a voltage source to apply voltage to at least one input (power supply and switch) and at least one possibility to verify voltage at the output (LED, multimeter etc.)
- Connection to the drive by *Tria-Link* via PCI adapter (built-in or by USB)

## Hardware Configuration Adjustment

The sample works with the first device found in the link. If you have attached several devices in the Tria-Link,
please make sure you test the I/Os of the correct device.

Depending on the product, another register layout may be present. If it is another than the expected,
the program will crash. You can change the expected ID by changing the using statement at the top of the
`InputOutputForm.cs` source file (`using Triamec.Tam.Rlid19`).

## Software Prerequisites

This project is made and built with [Microsoft Visual Studio](https://visualstudio.microsoft.com/en/).

In addition you need [TAM Software](https://www.triamec.com/en/tam-software-support.html) installation.

## Run the *InputOutput* Application

1. Start `InputOutput.sln`.

## Operate the *InputOutput* Application

- Click **Output 1** and **Output 2** to toggle between high and low state of the respective output 
- Apply voltage to Input 1 or Input 2 to change the state of the respective input box in the GUI
- Switch between **Poll** and **Listen** to update the Input states
