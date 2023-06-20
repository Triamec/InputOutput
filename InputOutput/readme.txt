$Id: readme.txt 36912 2021-02-16 08:14:04Z chm $
Copyright © 2011 Triamec Motion AG

Overview
--------

This "Input / Output" program is a .NET windows application
demonstrating the minimal steps to read inputs and write outputs
with the Triamec Automation and Motion software (TAM Software).


Hardware requirements
---------------------

- a Tria-Link PCI adapter (TL100 or TLC201) mounted to your PC,
- at least one of the TS151, TS351, TSP350 or TSP700, TIOB01 or TIOB02 products connected to the Tria-Link,
- a motor and encoder connected to the servo-drive,
- power supply for the servo-drive logic and motor power.


Hardware configuration adjustment
---------------------------------

The sample works with the first device found in the link. If you have attached several devices in the Tria-Link,
please make sure you test the I/Os of the correct device.

Depending on the product, another register layout may be present. If it is another than the expected,
the program will crash. You can change the expected ID by changing the using statement at the top of the
InputOutputForm.cs source file (using Triamec.Tam.Rlid4).


What the program does
---------------------

After the above adjustments, you can start the program in the Visual Studio Debugger (F5-key).

After the InoputOutputForm has loaded,
- a TAM topology is created,
- the Tria-Link gets booted,
- a device gets referenced.
After this, the program is ready for user interaction.

The user can
- set and reset the first two outputs of the device,
- see the state of the first four inputs of the device,
- change whether the inputs are simply polled or subscribed,
- open the TAM system explorer as a child form.