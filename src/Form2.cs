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
		private ScreensaverSettings _Settings = new ScreensaverSettings();
		public Form2()
		{
			InitializeComponent();
			
			ChangeLanguage();

			_Settings.Load();

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
			SpeedUnD.Value = _Settings.Density;
		}
		private void ResetFields()
		{
			SpeedUnD.Value = ScreensaverSettings.D_Density;
		}

		private void MaxLengthUnD_ValueChanged(object sender, EventArgs e)
		{
			_Settings.Density = (int)SpeedUnD.Value;
		}




		private void ResetBtn_Click(object sender, EventArgs e)
		{
			ResetFields();
			_Settings = new ScreensaverSettings();
		}
		private void OkBtn_Click(object sender, EventArgs e)
		{
			_Settings.Save();
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
