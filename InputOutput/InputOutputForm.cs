// Copyright © 2011 Triamec Motion AG

using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Forms;
using Triamec.Tam.Registers;
using Triamec.Tam.Rlid19;
using Triamec.Tam.Samples.Properties;
using Triamec.Tam.Subscriptions;
using Triamec.Tam.UI;
using Triamec.TriaLink;

namespace Triamec.Tam.Samples {
	/// <summary>
	/// The main form of the TAM "InputOutput" application.
	/// </summary>
	internal partial class InputOutputForm : Form {
		#region Fields
		readonly int _inputBit1, _inputBit2, _inputBit3, _inputBit4;

		TamTopology _topology;

		ITamDevice _device;

		TamExplorerForm _tamExplorerForm;

		ITamRegister<bool> _output1Register, _output2Register;
        ITamReadonlyRegister<bool> _input1Register, _input2Register;

		IClientSubscription _listener;

		int _packet1ValueIndex, _packet2ValueIndex;

		#endregion Fields

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="InputOutputForm"/> class.
		/// </summary>
		public InputOutputForm() {
			InitializeComponent();

			// create input bit masks
			_inputBit1 = BitVector32.CreateMask();
			_inputBit2 = BitVector32.CreateMask(_inputBit1);
			_inputBit3 = BitVector32.CreateMask(_inputBit2);
			_inputBit4 = BitVector32.CreateMask(_inputBit3);
		}
		#endregion Constructor

		#region InputOutput code
		/// <summary>
		/// Prepares the TAM system.
		/// </summary>
		/// <remarks>
		/// 	<list type="bullet">
		/// 		<item><description>Creates a TAM topology,</description></item>
		/// 		<item><description>boots the Tria-Link,</description></item>
		/// 		<item><description>searches for a device.</description></item>
		/// 	</list>
		/// </remarks>
		/// <exception cref="TamException">Startup failed.</exception>
		void Startup() {

			// Create the root object representing the topology of the TAM hardware.
			// We will dispose this object via components.
			_topology = new TamTopology();
			components.Add(_topology);

			// Add the local TAM system on this PC to the topology.
			var system = _topology.AddLocalSystem();

			// Get the (first) Tria-Link on the (first) PCI Adapter of the local TAM system.
			var link = system[0][0];

			// Boot the Tria-Link so that it learns about connected stations.
			link.Identify();

			// Get the device from the first station in the link which has the right register type
			_device = link.First(station => station.Device?.Register.GetType() == typeof(Register)).Device;

			// Get the register layout of the drive
			// and cast it to the RLID-specific register layout.
			var register = _device.Register as Register;

			// cache the I/O registers
			_output1Register = register.Axes[0].Commands.General.DigitalOut1;
			_output2Register = register.Axes[0].Commands.General.DigitalOut2;
            _input1Register = register.Axes[0].Signals.General.DigitalInputBits.DigIn1;
			_input2Register = register.Axes[0].Signals.General.DigitalInputBits.DigIn2;
		}

		/// <summary>
		/// Sets the value of the first output.
		/// </summary>
		/// <param name="value">Whether the output should be set.</param>
		/// <exception cref="TimeoutException">A timeout occurred.</exception>
		void SetOutput1(bool value) => _output1Register.Write(value);

		/// <summary>
		/// Sets the value of the second output.
		/// </summary>
		/// <param name="value">Whether the output should be set.</param>
		/// <exception cref="TimeoutException">A timeout occurred.</exception>
		void SetOutput2(bool value) => _output2Register.Write(value);

		/// <summary>
		/// Reads the first output.
		/// </summary>
		/// <returns>Whether the first output is set.</returns>
		/// <exception cref="TimeoutException">A timeout occurred.</exception>
		bool ReadOutput1() => _output1Register.Read();

		/// <summary>
		/// Reads the second output.
		/// </summary>
		/// <returns>Whether the second output is set.</returns>
		/// <exception cref="TimeoutException">A timeout occurred.</exception>
		bool ReadOutput2() => _output2Register.Read();

		/// <summary>
		/// Reads the input register and updates the view appropriately.
		/// </summary>
		/// <exception cref="TimeoutException">A timeout occurred.</exception>
		void PollInputs() {
			bool input1 = _input1Register.Read();
			bool input2 = _input2Register.Read();

			// update the view. See there for how to extract the bits
			UpdateInputs(input1, input2);
		}

		/// <summary>
		/// Called when new data from the listener subscription gets available. Updates the view as appropriate.
		/// </summary>
		void OnPacketsAvailable(object sender, EventArgs e) {

			// Get all available raw packets.
			// The values are transported as raw TamValue32 structures
			// allowing to interpret them AsInt32, AsSingle or AsBoolean.
			TamValue32[][] packets = _listener.PacketSender.Dequeue();

			if (packets.Length > 0) {
				// only consider most recent packet in case of multiple packets
				TamValue32[] packet = packets[packets.Length - 1];

				// get the register value from the packet, using the previously cached index 
				bool input1 = packet[_packet1ValueIndex].AsBoolean;
				bool input2 = packet[_packet2ValueIndex].AsBoolean;

				// update the view. See there for how to extract the bits
				UpdateInputs(input1, input2);
			}
		}

		/// <summary>
		/// Creates the listener subscription.
		/// </summary>
		/// <exception cref="SubscriptionException">Could not set the listener down.</exception>
		void SetupListener() {
			if (_listener == null) {

				// navigate upwards
				var link = _device.Station.Link;

				// subscriptions are organized within the link
				var subscriptionManager = link.SubscriptionManager;

				// let the inputs register be published at a low rate of 10000 Hz / 500 = 20 Hz
				var publisher = new Publisher(500, _input1Register, _input2Register);
				_packet1ValueIndex = publisher.GetValueIndex(_input1Register);
                _packet2ValueIndex = publisher.GetValueIndex(_input2Register);

                // create the subsription
                _listener = subscriptionManager.SubscribeEvent(publisher);

				// subscribe to the data stream
				_listener.PacketSender.PacketsAvailable += OnPacketsAvailable;

				// Enable the subscription.
				_listener.Enable();
			}
		}

		/// <summary>
		/// Dissolves the listener subscription.
		/// </summary>
		/// <exception cref="SubscriptionException">Could not tear the listener down.</exception>
		void TeardownListener() {
			if (_listener != null) {

				// switch data transmission off
				_listener.Disable();

				// unsubscribe from the device
				_listener.Unsubscribe();

				// unsubscribe from the manager
				_listener.Dispose();

				_listener = null;
			}
		}
		#endregion InputOutput code

		#region GUI methods

		void UpdateInputs(bool input1, bool input2) {

			// Controls must only be updated on the main thread
			if (InvokeRequired) {

				// Make this method called on the main thread
				BeginInvoke(new Action<bool,bool>(UpdateInputs), input1, input2);
			} else {
				_checkBoxInput1.Checked = input1;
				_checkBoxInput2.Checked = input2;
				//_checkBoxInput3.Checked = inputBits[_inputBit3];
				//_checkBoxInput4.Checked = inputBits[_inputBit4];
			}
		}

		#region Form handler methods

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);

			try {
				Startup();

				// Read output registers
				_checkBoxOutput1.Checked = ReadOutput1();
				_checkBoxOutput2.Checked = ReadOutput2();
			} catch (TamException ex) {
				MessageBox.Show(this, ex.Message, Resources.StartupErrorCaption, MessageBoxButtons.OK,
					MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0);
			}
		}

		#endregion Form handler methods

		#region Control callbacks
		void OnCheckBoxOutput1CheckedChanged(object sender, EventArgs e) {
			try {
				SetOutput1(_checkBoxOutput1.Checked);
			} catch (TimeoutException ex) {
				MessageBox.Show(ex.Message, Resources.SetOutputErrorCaption, MessageBoxButtons.OK,
					MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0);
			}
		}

		void OnCheckBoxOutput2CheckedChanged(object sender, EventArgs e) {
			try {
				SetOutput2(_checkBoxOutput2.Checked);
			} catch (TimeoutException ex) {
				MessageBox.Show(ex.Message, Resources.SetOutputErrorCaption, MessageBoxButtons.OK,
					MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0);
			}
		}

		void OnRadioButtonPollCheckedChanged(object sender, EventArgs e) {
			var button = (RadioButton)sender;
			if (button.Checked) {
				timer.Start();
				try {
					TeardownListener();
				} catch (SubscriptionException ex) {
					MessageBox.Show(ex.Message, Resources.ListenerTeardownErrorCaption, MessageBoxButtons.OK,
						MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0);
				}
			} else {
				timer.Stop();
				try {
					SetupListener();
				} catch (SubscriptionException ex) {
					MessageBox.Show(ex.Message, Resources.ListenerSetupErrorCaption, MessageBoxButtons.OK,
						MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0);
				}
			}
		}
		#endregion Control callbacks

		#region Component callbacks
		void OnTimerTick(object sender, EventArgs e) {
			try {
				PollInputs();
			} catch (TimeoutException ex) {
				timer.Enabled = false;
				MessageBox.Show(ex.Message, Resources.GetInputErrorCaption, MessageBoxButtons.OK,
					MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0);
				timer.Enabled = true;
			}
		}
		#endregion Component callbacks

		#region Menu handler methods

		void OnExitMenuItemClick(object sender, EventArgs e) => Close();

		void OnExplorerMenuItemClick(object sender, EventArgs e) {
			var menuItem = (ToolStripMenuItem)sender;

			FormClosedEventHandler onExplorerClosed = null;
			onExplorerClosed = (s, a) => {

				// uncheck the menu item whenever the form is closed
				menuItem.Checked = false;

				var form = (Form)s;
				form.Dispose();
				form.FormClosed -= onExplorerClosed;
			};

			if ((_tamExplorerForm == null) || _tamExplorerForm.IsDisposed) {

				// Create the TAM system explorer as a child window.
				_tamExplorerForm = new TamExplorerForm {

					// Skip loading the TAM configuration when the explorer opens
					// because we already did this ourselves.
					AutoLoadTamConfiguration = false,

					// Tell the TAM system explorer the business object to work with.
					Topology = _topology
				};

				_tamExplorerForm.FormClosed += onExplorerClosed;
			}

			// Toggle the display of the TAM system explorer window.
			_tamExplorerForm.Visible = menuItem.Checked;
		}
		#endregion Menu handler methods
		#endregion GUI methods
	}
}
