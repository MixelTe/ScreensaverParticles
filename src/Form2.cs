using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenSaverConections
{
	public partial class Form2 : Form
	{
		public Form2()
		{
			InitializeComponent();
			
			ChangeLanguage();

			RegSerializer.Load(Program.KeyName, Program.Settings);

			SetFields();
		}
		private void ChangeLanguage()
		{
			if (CultureInfo.CurrentUICulture.Name == "ru-RU")
			{
				Text = "ScreenSaver";

				SpeecLbl.Text = "Скорость";

				ResetBtn.Text = "Сбросить";
				OkBtn.Text = "ОК";
			}
		}
		private void SetFields()
		{
			SpeedUnD.Value = Program.Settings.Density;
		}

		private void MaxLengthUnD_ValueChanged(object sender, EventArgs e)
		{
			Program.Settings.Density = (int)SpeedUnD.Value;
		}




		private void ResetBtn_Click(object sender, EventArgs e)
		{
			Program.Settings = new Settings();
			SetFields();
		}
		private void OkBtn_Click(object sender, EventArgs e)
		{
			RegSerializer.Save(Program.KeyName, Program.Settings);
			Close();
		}

		private void PictureBoxGitHub_Click(object sender, EventArgs e)
		{
			var link = "https://github.com/MixelTe";
			try
			{
				Process.Start(link);
			}
			catch (Exception)
			{
				MessageBox.Show(link + "\n\nCopied to clipboard", "StarScreen: Source code", MessageBoxButtons.OK, MessageBoxIcon.Information);
				Clipboard.SetText(link);
			}
		}
	}
}
