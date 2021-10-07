using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KoreanPass
{
	public partial class License : Form
	{
		public License()
		{
			InitializeComponent();
		}

		private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			var ps = new ProcessStartInfo(e.LinkText)
			{
				UseShellExecute = true,
				Verb = "open"
			};
			Process.Start(ps);
		}
	}
}
