using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Xml;

namespace ModifyXML1
{
	public partial class Form1 : Form
	{
		XmlDocument my_xml_doc = new XmlDocument();
		int current_node_index = 0;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			my_xml_doc.Load(@"C:\Users\Justin\Documents\School\C#\EmployeeList.xml");

			foreach (XmlNode node in my_xml_doc.DocumentElement.ChildNodes)
			{
				combo_EmployeeID.Items.Add(node.Attributes["employeeid"].Value);
			}
			Fill_Controls();
		}

		private void Fill_Controls()
		{
			XmlNode node = my_xml_doc.DocumentElement.ChildNodes[current_node_index];



			combo_EmployeeID.Text = node.Attributes["employeeid"].Value;
			textBox_FirstName.Text = node.ChildNodes[0].InnerText;
			textBox_LastName.Text = node.ChildNodes[1].InnerText;
			textBox_Phone.Text = node.ChildNodes[2].InnerText;
			textBox_Notes.Text = node.ChildNodes[3].InnerText;

			Update_Label();
		}

		private void Save_XML()
		{
			my_xml_doc.Save(@"C:\Users\Justin\Documents\School\C#\EmployeeList.xml");
		}
		private void Update_Label()
		{
			label_Count.Text = "Employee " + (current_node_index + 1) + 
				" of " + my_xml_doc.DocumentElement.ChildNodes.Count;
		}

		//go to first record
		private void button_First_Click(object sender, EventArgs e)
		{
			current_node_index = 0;
			Fill_Controls();
		}
		//go to previous record
		private void button_Prev_Click(object sender, EventArgs e)
		{
			current_node_index--;
			if (current_node_index < 0)
			{
				current_node_index = 0;
			}
			Fill_Controls();
		}

		//go to next record
		private void button_Next_Click(object sender, EventArgs e)
		{
			current_node_index++;
			if (current_node_index >= my_xml_doc.DocumentElement.ChildNodes.Count)
			{
				current_node_index = my_xml_doc.DocumentElement.ChildNodes.Count - 1;
			}
			Fill_Controls();
		}

		//go to last record
		private void button_Last_Click(object sender, EventArgs e)
		{
			current_node_index = my_xml_doc.DocumentElement.ChildNodes.Count - 1;

			Fill_Controls();
		}

		private void button_Update_Click(object sender, EventArgs e)
		{
			Update();
		}

		private void Update()
		{
			XmlNode node = my_xml_doc.SelectSingleNode(
				"//employee[@employeeid='" + combo_EmployeeID.SelectedItem + "']");

			if (node != null)
			{
				node.ChildNodes[0].InnerText = textBox_FirstName.Text;
				node.ChildNodes[1].InnerText = textBox_LastName.Text;
				node.ChildNodes[2].InnerText = textBox_Phone.Text;
				XmlCDataSection notes = my_xml_doc.CreateCDataSection(textBox_Notes.Text);
				node.ChildNodes[3].ReplaceChild(notes, node.ChildNodes[3].ChildNodes[0]);
			}

			Save_XML();

			MessageBox.Show("Yay!! You updated this record");
		}

		private void button_Delete_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show("Are you sure you want to delete this record?", 
				"Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			if (result == DialogResult.Yes)
			{
				XmlNode node = my_xml_doc.SelectSingleNode(
					"//employee[@employeeid='" + combo_EmployeeID.SelectedItem + "']");

				if (node != null)
				{
					my_xml_doc.DocumentElement.RemoveChild(node);
				}

				combo_EmployeeID.Items.Remove(combo_EmployeeID.SelectedItem);

				Save_XML();
				Update_Label();
				MessageBox.Show("Yay!!! you deleted one!!!");
			}
		}

		private void button_Add_Click(object sender, EventArgs e)
		{
			XmlAttribute employeeid = my_xml_doc.CreateAttribute("employeeid");
			employeeid.Value = combo_EmployeeID.Text;

			foreach (XmlNode node in my_xml_doc.DocumentElement.ChildNodes)
			{
				if (employeeid.Value == node.Attributes["employeeid"].Value)
				{
					var result = MessageBox.Show("Are you sure you want to overwrite this record?",
						"Overwrite Record", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
					if (result == DialogResult.OK)
					{
						Update();
					}
					return;
				}
			}
			
			XmlElement employee = my_xml_doc.CreateElement("employee");
			XmlElement firstname = my_xml_doc.CreateElement("firstname");
			XmlElement lastname = my_xml_doc.CreateElement("lastname");
			XmlElement homephone = my_xml_doc.CreateElement("homephone");
			XmlElement notes = my_xml_doc.CreateElement("notes");

			XmlText firstnametext = my_xml_doc.CreateTextNode(textBox_FirstName.Text);
			XmlText lastnametext = my_xml_doc.CreateTextNode(textBox_LastName.Text);
			XmlText homephonetext = my_xml_doc.CreateTextNode(textBox_Phone.Text);
			XmlCDataSection notestext = my_xml_doc.CreateCDataSection(textBox_Notes.Text);

			employee.Attributes.Append(employeeid);
			employee.AppendChild(firstname);
			employee.AppendChild(lastname);
			employee.AppendChild(homephone);
			employee.AppendChild(notes);

			firstname.AppendChild(firstnametext);
			lastname.AppendChild(lastnametext);
			homephone.AppendChild(homephonetext);
			notes.AppendChild(notestext);

			my_xml_doc.DocumentElement.AppendChild(employee);

			combo_EmployeeID.Items.Add(employeeid.Value);

			Save_XML();
			Update_Label();

			MessageBox.Show("Yay! You added an employee!!!");
		}

		private void combo_EmployeeID_SelectedIndexChanged(object sender, EventArgs e)
		{
			current_node_index = combo_EmployeeID.SelectedIndex;
			Fill_Controls();
		}
	}
}
