using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void MyButton_Click(object sender, EventArgs e)
		{
			MyLabel.Text = "Hello " + MyTextBox.Text;
		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			MyLabel.ForeColor = Color.Blue;
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
			MyLabel.ForeColor = Color.Red;
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}
	}
}
