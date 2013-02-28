using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dark_Heresy_Generator
{
	public partial class DialogBox2 : Form
	{
		public DialogBox2(string Choice1, string Choice2)
		{
			InitializeComponent();
			Choice1_Button.Text = Choice1;
			Choice2_Button.Text = Choice2;
		}

		private void DialogBox_Load(object sender, EventArgs e)
		{

		}

		private void Choice1_Button_Click(object sender, EventArgs e)
		{

		}
	}
}
