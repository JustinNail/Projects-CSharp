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
	public partial class DialogBox3 : Form
	{
		public DialogBox3(string choice1, string choice2, string choice3)
		{
			InitializeComponent();
			Choice1_Button.Text = choice1;
			Choice2_Button.Text = choice2;
			Choice3_Button.Text = choice3;
		}
	}
}
