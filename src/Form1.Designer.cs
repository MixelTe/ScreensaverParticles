namespace ScreenSaverParticles
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			timer1 = new System.Windows.Forms.Timer(components);
			SuspendLayout();
			// 
			// timer1
			// 
			timer1.Enabled = true;
			timer1.Tick += Timer1_Tick;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.Black;
			ClientSize = new Size(933, 519);
			FormBorderStyle = FormBorderStyle.None;
			Icon = (Icon)resources.GetObject("$this.Icon");
			Margin = new Padding(4, 3, 4, 3);
			Name = "Form1";
			Text = "Screensaver";
			WindowState = FormWindowState.Maximized;
			FormClosing += Form1_FormClosing;
			Load += Form1_Load;
			Click += Form1_Click;
			KeyDown += Form1_KeyDown;
			MouseMove += Form1_MouseMove;
			ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer timer1;
	}
}

