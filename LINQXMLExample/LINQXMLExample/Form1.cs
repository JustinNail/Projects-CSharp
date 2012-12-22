using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Xml.Linq;

namespace LINQXMLExample
{
	public partial class Form1 : Form
	{
		XDocument x_document;
		public Form1()
		{
			InitializeComponent();
		}

		private void button_Load_Click(object sender, EventArgs e)
		{
			try
			{
				if (radioButton_File.Checked)
				{
					x_document = XDocument.Load(textBox_Load.Text);
				}

				if (radioButton_String.Checked)
				{
					x_document = XDocument.Parse(textBox_Load.Text);
				}

				if (radioButton_Internal.Checked)
				{
					x_document = new XDocument( new XElement("employees",
													new XElement("employee",
														new XElement("firstname","Darth"),
														new XElement("lastname","Vader"),
														new XElement("homephone","(304) Death Star"),
														new XElement("note","I am your father, Luke"),
														new XAttribute("employeeid","-1")))
												);
				}
				button_LoadTree.Enabled = true;

				var query_records = from record 
										in x_document.Element("employees").Descendants("employee")
									select record;
				var query_fields = from field in (query_records.ToArray())[0].Descendants()
								   orderby field.Name.LocalName ascending
								   select field.Name.LocalName;

				comboBox_Field.Items.Clear();
				foreach (string field in query_fields)
				{
					comboBox_Field.Items.Add(field);
				}
				comboBox_Field.Text = (string)comboBox_Field.Items[0];
				button_Search.Enabled = true;
			}
			catch(Exception ev)
			{
				MessageBox.Show(ev.Message);
			}
		}

		private void button_LoadTree_Click(object sender, EventArgs e)
		{
			string x_document_root = x_document.Root.Name.LocalName;
			TreeNode root_node = new TreeNode(x_document_root);

			treeView1.Nodes.Clear();
			treeView1.Nodes.Add(root_node);

			foreach (XElement employee in x_document.Element(x_document_root).Elements())
			{
				TreeNode employee_node = new TreeNode("Employee ID: " + 
					employee.Attribute("employeeid").Value);

				root_node.Nodes.Add(employee_node);
				if (employee.HasElements)
				{
					foreach(XElement employee_child in employee.Descendants())
					{
						TreeNode child_node = new TreeNode(employee_child.Value);
						employee_node.Nodes.Add(child_node);
					}
				}
			}
		}

		private void button_Search_Click(object sender, EventArgs e)
		{
			string search_text = textBox_Search.Text;

			string search_field = comboBox_Field.Text;
			var query = from text in x_document.Element("employees").Descendants(search_field)
						where text.Value.Contains(search_text)
						select text;
			if (query.Count() == 0)
			{
				MessageBox.Show("No matching items found");
				return;
			}
			richTextBox_Results.Clear();
			foreach (string s in query)
			{
				richTextBox_Results.Text += s + "\n";
			}
		}
	}
}
