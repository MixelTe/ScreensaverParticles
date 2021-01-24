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
				Text = "Screensaver";

				DensityLbl.Text = "Плотность";
				BackgroundColorLbl.Text = "Цвет фона";

				ClockModeGB.Text = "Режим часов";
				ClockModeCB.Text = "Режим часов";
				ClockSizeLbl.Text = "Размер часов";

				PointsGB.Text = "Точки";
				DrawPointsCB.Text = "Рисовать точки";
				PointRadiusLbl.Text = "Радиус";
				SpeedLbl.Text = "Скорость";
				RotateSpeedLbl.Text = "Скороть поворота";
				ChaoticLbl.Text = "Хаотичность";
				PointColorLbl.Text = "Цвет";

				ConnectionsGB.Text = "Cоеденения";
				DrawConCB.Text = "Рисовать соеденения";
				DistanceLbl.Text = "Макс. длина";
				ShadingLbl.Text = "Начало затухания";
				LineWidthLbl.Text = "Толщина линии";
				ConnectionsAlphaLbl.Text = "Прозрачность линии";
				ConnectionsColorLbl.Text = "Цвет";

				ResetBtn.Text = "Сбросить";
				OkBtn.Text = "ОК";
			}
		}
		private void SetFields()
		{
			DensityUnD.Value = Program.Settings.Density;
			BackgroundColorPB.BackColor = Program.Settings.BackgroundColor;
			ClockModeCB.Checked = Program.Settings.ClockMode;
			ClockSizeUnD.Value = (decimal)Program.Settings.ClockSize;

			DrawPointsCB.Checked = Program.Settings.DrawPoints;
			PointRadiusUnD.Value = Program.Settings.PointRadius;
			SpeedUnD.Value = (decimal)Program.Settings.SpeedMax;
			RotateSpeedUnD.Value = (decimal)Program.Settings.RotateSpeedMax;
			ChaoticUnD.Value = (int)ChaoticUnD.Maximum + 1 - Program.Settings.TimeMin;
			PointColorPB.BackColor = new HSL(Program.Settings.ColorMax - 1, 100, Program.Settings.ColorLMax * 100).HSLToRGB().RGBToColor(255);

			DrawConCB.Checked = Program.Settings.DrawConections;
			DistanceUnD.Value = Program.Settings.DistanceMax;
			ShadingUnD.Maximum = Program.Settings.DistanceMax;
			ShadingUnD.Value = Program.Settings.DistanceShading;
			LineWidthUnD.Value = Program.Settings.ConnectionsWidth;
			ConnectionsAlphaUnd.Value = (decimal)Program.Settings.LineAlpha;
			ConnectionsColorPB.BackColor = Program.Settings.ConnectionsColor;
			PointColorPB_SetImage();
		}
		private void PointColorPB_SetImage()
		{
			var rect = new Rectangle(0, 0, PointColorPB.Width, PointColorPB.Height);
			var background = new Bitmap(rect.Width, rect.Height);
			var tileWidth = 2;
			using (var g = Graphics.FromImage(background))
			{
				g.FillRectangle(Brushes.White, rect);
				var titleCount = rect.Width / tileWidth;
				var colorStart = Program.Settings.ColorMin;
				var colorEnd = Program.Settings.ColorMax;
				var colorStep = (colorEnd - colorStart) / (float)titleCount;
				for (int i = 0; i < titleCount; i++)
				{
					using (var brush = new SolidBrush(new HSL((int)(colorStep * i) + colorStart, 100, 50).HSLToRGB().RGBToColor(255)))
					{
						g.FillRectangle(brush, tileWidth * i, 0, tileWidth, rect.Height);
					}
				}
			}
			PointColorPB.Image = background;
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
			var link = "https://github.com/MixelTe/ScreensaverParticles";
			try
			{
				Process.Start(link);
			}
			catch (Exception)
			{
				MessageBox.Show(link + "\n\nCopied to clipboard", "ScreensaverParticles: Source code", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
			Program.Settings.SpeedMax = (float)SpeedUnD.Value;
		}
		private void RotateSpeedUnD_ValueChanged(object sender, EventArgs e)
		{
			Program.Settings.RotateSpeedMax = (float)RotateSpeedUnD.Value;
		}
		private void ChaoticUnD_ValueChanged(object sender, EventArgs e)
		{
			Program.Settings.TimeMin = (int)ChaoticUnD.Maximum + 1 - (int)ChaoticUnD.Value;
			Program.Settings.TimeMax = Program.Settings.TimeMin * 10;
		}
		private void PointColorPB_Click(object sender, EventArgs e)
		{
			DialogResult result = new Form3().ShowDialog();
			if (result == DialogResult.OK)
			{
				PointColorPB.BackColor = new HSL(Program.Settings.ColorMax - 1, 100, Program.Settings.ColorLMax * 100).HSLToRGB().RGBToColor(255);
				PointColorPB_SetImage();
			}
		}
		private void DrawConCB_CheckedChanged(object sender, EventArgs e)
		{
			Program.Settings.DrawConections = DrawConCB.Checked;
		}
		private void DistanceUnD_ValueChanged(object sender, EventArgs e)
		{
			Program.Settings.DistanceMax = (int)DistanceUnD.Value;
			ShadingUnD.Maximum = Program.Settings.DistanceMax;
		}
		private void ShadingUnD_ValueChanged(object sender, EventArgs e)
		{
			Program.Settings.DistanceShading = (int)ShadingUnD.Value;
		}
		private void LineWidthUnD_ValueChanged(object sender, EventArgs e)
		{
			Program.Settings.ConnectionsWidth = (int)LineWidthUnD.Value;
		}
		private void ConnectionsAlphaUnd_ValueChanged(object sender, EventArgs e)
		{
			Program.Settings.LineAlpha = (float)ConnectionsAlphaUnd.Value;
		}
		private void ConnectionsColorPB_Click(object sender, EventArgs e)
		{
			colorDialog1.Color = Program.Settings.ConnectionsColor;
			DialogResult result = colorDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
				ConnectionsColorPB.BackColor = colorDialog1.Color;
				Program.Settings.ConnectionsColor = colorDialog1.Color;
			}
		}
		private void ClockModeCB_CheckedChanged(object sender, EventArgs e)
		{
			Program.Settings.ClockMode = ClockModeCB.Checked;
		}
		private void ClockSizeUnD_ValueChanged(object sender, EventArgs e)
		{
			Program.Settings.ClockSize = (float)ClockSizeUnD.Value;
		}
	}
}
