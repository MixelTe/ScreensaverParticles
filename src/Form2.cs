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
			
			//ChangeLanguage();

			RegSerializer.Load(Program.KeyName, Program.Settings);

			SetFields();
		}
		private void ChangeLanguage()
		{
			if (CultureInfo.CurrentUICulture.Name == "ru-RU")
			{
				Text = "ScreenSaver";

				DensityLbl.Text = "Плотность";

				ResetBtn.Text = "Сбросить";
				OkBtn.Text = "ОК";
			}
		}
		private void SetFields()
		{
			DensityUnD.Value = Program.Settings.Density;
			BackgroundColorPB.BackColor = Program.Settings.BackgroundColor;
			DrawPointsCB.Checked = Program.Settings.DrawPoints;
			PointRadiusUnD.Value = Program.Settings.PointRadius;
			SpeedUnD.Value = (decimal)Program.Settings.SpeedMax;
			RotateSpeedUnD.Value = (decimal)Program.Settings.RotateSpeedMax;
			ChaoticUnD.Value = (int)ChaoticUnD.Maximum + 1 - Program.Settings.TimeMin;
			PointColorPB.BackColor = new HSL(Program.Settings.ColorMax - 1, 100, Program.Settings.ColorLMax * 100).HSLToRGB().RGBToColor(255);

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


		private void DensityUnD_ValueChanged(object sender, EventArgs e)
		{
			Program.Settings.Density = (int)DensityUnD.Value;
		}
		private void BackgroundColorPB_Click(object sender, EventArgs e)
		{
			colorDialog1.Color = Program.Settings.BackgroundColor;
			DialogResult result = colorDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
				BackgroundColorPB.BackColor = colorDialog1.Color;
				Program.Settings.BackgroundColor = colorDialog1.Color;
			}
		}
		private void DrawPointsCB_CheckedChanged(object sender, EventArgs e)
		{
			Program.Settings.DrawPoints = DrawPointsCB.Checked;
		}
		private void PointRadiusUnD_ValueChanged(object sender, EventArgs e)
		{
			Program.Settings.PointRadius = (int)PointRadiusUnD.Value;
		}
		private void SpeedUnD_ValueChanged(object sender, EventArgs e)
		{
			Program.Settings.SpeedMax = (double)SpeedUnD.Value;
		}
		private void RotateSpeedUnD_ValueChanged(object sender, EventArgs e)
		{
			Program.Settings.RotateSpeedMax = (double)RotateSpeedUnD.Value;
		}
		private void ChaoticUnD_ValueChanged(object sender, EventArgs e)
		{
			Program.Settings.TimeMin = (int)ChaoticUnD.Maximum + 1 - (int)ChaoticUnD.Value;
			Program.Settings.TimeMax = Program.Settings.TimeMin * 10;
		}
		private void PointColorPB_Click(object sender, EventArgs e)
		{
			colorDialog1.Color = PointColorPB.BackColor;
			DialogResult result = colorDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
				Program.Settings.ColorMin = (int)colorDialog1.Color.GetHue();
				Program.Settings.ColorMax = (int)colorDialog1.Color.GetHue();
				Program.Settings.ColorLMin = colorDialog1.Color.GetBrightness();
				Program.Settings.ColorLMax = colorDialog1.Color.GetBrightness();
				PointColorPB.BackColor = colorDialog1.Color;
			}
		}
	}
}
