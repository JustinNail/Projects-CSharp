using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Xml;

namespace LoadXmlNav
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button_LoadTree_Click(object sender, EventArgs e)
		{
			XmlDocument my_xml_doc = new XmlDocument();
			my_xml_doc.Load(@"C:\Users\Justin\Documents\School\C#\EmployeeList.xml");

			TreeNode root = new TreeNode(my_xml_doc.DocumentElement.Name);
			treeView1.Nodes.Add(root);

			foreach (XmlNode node in my_xml_doc.DocumentElement.ChildNodes)
			{
				TreeNode employee = new TreeNode(node.Attributes[0].Name + " " + node.Attributes[0].Value);
				root.Nodes.Add(employee);
				if (node.HasChildNodes)
				{
					foreach (XmlNode childnode in node.ChildNodes)
					{
						TreeNode employee_info = new TreeNode(childnode.Name + " : " + childnode.InnerText);
						employee.Nodes.Add(employee_info);
					}
				}
			}
		}
	}
}
