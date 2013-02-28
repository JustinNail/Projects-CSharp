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
	public partial class DialogBoxList : Form
	{
		public string selection;
		public DialogBoxList(List<string> list)
		{
			InitializeComponent();
			foreach (string s in list)
			{
				comboBox1.Items.Add(s);
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			selection = comboBox1.SelectedItem.ToString();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			comboBox1.Items.Clear();
		}
	}
}
