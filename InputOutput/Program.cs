// Copyright © 2011 Triamec Motion AG

using System;
using System.Windows.Forms;

namespace Triamec.Tam.Samples {
	using static Application;

	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			EnableVisualStyles();
			SetCompatibleTextRenderingDefault(false);
#pragma warning disable CA2000 // Dispose objects before losing scope: Application.Run disposes form
			Run(new InputOutputForm());
#pragma warning restore CA2000
		}
	}
}
