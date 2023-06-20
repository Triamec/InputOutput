// Copyright © 2011 Triamec Motion AG

using Triamec.Tam.Subscriptions;

namespace Triamec.Tam.Samples {
	partial class InputOutputForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			try {
				TeardownListener();
			} catch (SubscriptionException) {
				// ignore and continue
			}

			if (disposing && (components != null)) {
				components.Dispose();
			}

			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.MenuStrip menuStrip;
			System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
			System.Windows.Forms.ToolStripMenuItem exitMenuItem;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputOutputForm));
			System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
			System.Windows.Forms.ToolStripMenuItem explorerMenuItem;
			System.Windows.Forms.ToolTip toolTip;
			System.Windows.Forms.RadioButton radioButtonPoll;
			System.Windows.Forms.RadioButton radioButtonListen;
			this._checkBoxOutput1 = new System.Windows.Forms.CheckBox();
			this._checkBoxOutput2 = new System.Windows.Forms.CheckBox();
			this._checkBoxInput1 = new System.Windows.Forms.CheckBox();
			this._checkBoxInput2 = new System.Windows.Forms.CheckBox();
			this._checkBoxInput4 = new System.Windows.Forms.CheckBox();
			this._checkBoxInput3 = new System.Windows.Forms.CheckBox();
			this.timer = new System.Windows.Forms.Timer(this.components);
			menuStrip = new System.Windows.Forms.MenuStrip();
			fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			explorerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolTip = new System.Windows.Forms.ToolTip(this.components);
			radioButtonPoll = new System.Windows.Forms.RadioButton();
			radioButtonListen = new System.Windows.Forms.RadioButton();
			menuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			fileToolStripMenuItem,
			viewToolStripMenuItem});
			resources.ApplyResources(menuStrip, "menuStrip");
			menuStrip.Name = "menuStrip";
			// 
			// fileToolStripMenuItem
			// 
			fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			exitMenuItem});
			fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			resources.ApplyResources(fileToolStripMenuItem, "fileToolStripMenuItem");
			// 
			// exitMenuItem
			// 
			exitMenuItem.Name = "exitMenuItem";
			resources.ApplyResources(exitMenuItem, "exitMenuItem");
			exitMenuItem.Click += new System.EventHandler(this.OnExitMenuItemClick);
			// 
			// viewToolStripMenuItem
			// 
			viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			explorerMenuItem});
			viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			resources.ApplyResources(viewToolStripMenuItem, "viewToolStripMenuItem");
			// 
			// explorerMenuItem
			// 
			explorerMenuItem.CheckOnClick = true;
			explorerMenuItem.Name = "explorerMenuItem";
			resources.ApplyResources(explorerMenuItem, "explorerMenuItem");
			explorerMenuItem.Click += new System.EventHandler(this.OnExplorerMenuItemClick);
			// 
			// _checkBoxOutput1
			// 
			resources.ApplyResources(this._checkBoxOutput1, "_checkBoxOutput1");
			this._checkBoxOutput1.Name = "_checkBoxOutput1";
			toolTip.SetToolTip(this._checkBoxOutput1, resources.GetString("_checkBoxOutput1.ToolTip"));
			this._checkBoxOutput1.UseVisualStyleBackColor = true;
			this._checkBoxOutput1.CheckedChanged += new System.EventHandler(this.OnCheckBoxOutput1CheckedChanged);
			// 
			// _checkBoxOutput2
			// 
			resources.ApplyResources(this._checkBoxOutput2, "_checkBoxOutput2");
			this._checkBoxOutput2.Name = "_checkBoxOutput2";
			toolTip.SetToolTip(this._checkBoxOutput2, resources.GetString("_checkBoxOutput2.ToolTip"));
			this._checkBoxOutput2.UseVisualStyleBackColor = true;
			this._checkBoxOutput2.CheckedChanged += new System.EventHandler(this.OnCheckBoxOutput2CheckedChanged);
			// 
			// radioButtonPoll
			// 
			resources.ApplyResources(radioButtonPoll, "radioButtonPoll");
			radioButtonPoll.Checked = true;
			radioButtonPoll.Name = "radioButtonPoll";
			radioButtonPoll.TabStop = true;
			toolTip.SetToolTip(radioButtonPoll, resources.GetString("radioButtonPoll.ToolTip"));
			radioButtonPoll.UseVisualStyleBackColor = true;
			radioButtonPoll.CheckedChanged += new System.EventHandler(this.OnRadioButtonPollCheckedChanged);
			// 
			// radioButtonListen
			// 
			resources.ApplyResources(radioButtonListen, "radioButtonListen");
			radioButtonListen.Name = "radioButtonListen";
			toolTip.SetToolTip(radioButtonListen, resources.GetString("radioButtonListen.ToolTip"));
			radioButtonListen.UseVisualStyleBackColor = true;
			// 
			// _checkBoxInput1
			// 
			resources.ApplyResources(this._checkBoxInput1, "_checkBoxInput1");
			this._checkBoxInput1.Name = "_checkBoxInput1";
			this._checkBoxInput1.UseVisualStyleBackColor = true;
			// 
			// _checkBoxInput2
			// 
			resources.ApplyResources(this._checkBoxInput2, "_checkBoxInput2");
			this._checkBoxInput2.Name = "_checkBoxInput2";
			this._checkBoxInput2.UseVisualStyleBackColor = true;
			// 
			// _checkBoxInput4
			// 
			resources.ApplyResources(this._checkBoxInput4, "_checkBoxInput4");
			this._checkBoxInput4.Name = "_checkBoxInput4";
			this._checkBoxInput4.UseVisualStyleBackColor = true;
			// 
			// _checkBoxInput3
			// 
			resources.ApplyResources(this._checkBoxInput3, "_checkBoxInput3");
			this._checkBoxInput3.Name = "_checkBoxInput3";
			this._checkBoxInput3.UseVisualStyleBackColor = true;
			// 
			// timer
			// 
			this.timer.Enabled = true;
			this.timer.Interval = 2000;
			this.timer.Tick += new System.EventHandler(this.OnTimerTick);
			// 
			// InputOutputForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(radioButtonListen);
			this.Controls.Add(radioButtonPoll);
			this.Controls.Add(this._checkBoxOutput2);
			this.Controls.Add(this._checkBoxInput3);
			this.Controls.Add(this._checkBoxInput4);
			this.Controls.Add(this._checkBoxInput2);
			this.Controls.Add(this._checkBoxInput1);
			this.Controls.Add(this._checkBoxOutput1);
			this.Controls.Add(menuStrip);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainMenuStrip = menuStrip;
			this.Name = "InputOutputForm";
			menuStrip.ResumeLayout(false);
			menuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.CheckBox _checkBoxOutput1;
		private System.Windows.Forms.CheckBox _checkBoxOutput2;
		private System.Windows.Forms.CheckBox _checkBoxInput1;
		private System.Windows.Forms.CheckBox _checkBoxInput2;
		private System.Windows.Forms.CheckBox _checkBoxInput4;
		private System.Windows.Forms.CheckBox _checkBoxInput3;
		private System.Windows.Forms.Timer timer;
	}
}

