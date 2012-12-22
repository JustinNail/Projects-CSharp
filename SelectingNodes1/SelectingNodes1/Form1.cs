using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Xml.XPath;

namespace SelectingNodes1
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button_Execute_Click(object sender, EventArgs e)
		{
			XPathDocument doc = new XPathDocument(
				@"C:\Users\Justin\Documents\School\C#\EmployeeList.xml");

			XPathNavigator nav = doc.CreateNavigator();

			try
			{
				XPathNodeIterator iterator = nav.Select(textBox_Expression.Text);
				label_Selected.Text = "The Expressions returned " + iterator.Count + " nodes";
				if (iterator.Count > 0)
				{
					while (iterator.MoveNext())
					{
						textBox_Results.Text += iterator.Current.OuterXml;
					}
				}
				else
				{
					textBox_Results.Text = "No results";
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
