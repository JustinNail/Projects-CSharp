using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Xml.Linq;

namespace DentistOfficeForm
{
	public partial class Form1 : Form
	{
		XDocument x_document;
		public Form1()
		{
			InitializeComponent();
			
			try
			{
				//always load the same xml file
				x_document = XDocument.Load(".../.../Resources/PatientData.xml");
				
				//load the field names into the search combo box
				var query_records = from record
										in x_document.Element("patients").Descendants("patient")
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
			}
			catch (Exception e)
			{
				//display error if file not found
				MessageBox.Show(e.Message);
			}
			//update the modify combo box with the patient ids
			update_comboBox();
		}

		private void update_comboBox()
		{
			comboBox_ID.Items.Clear();//clear the box
			//select every patient
			var patients = from patient in x_document.Element("patients").Elements()
					  select patient;
			//add each patient's id to box
			foreach (XElement patient in patients)
			{
				comboBox_ID.Items.Add(patient.Attribute("patientid").Value);
			}
		}

		private void fill_controls()
		{
			//select only the patient with the correct ID
			var selected = from patient in x_document.Element("patients").Elements()
						   where patient.Attribute("patientid").Value == comboBox_ID.SelectedItem.ToString()
						   select patient;

			//fill the textBoxes with values from the selected patient
			textBox_FirstName.Text = (selected.ToArray()[0]).Element("firstname").Value;
			textBox_LastName.Text = (selected.ToArray()[0]).Element("lastname").Value;
			textBox_Age.Text = (selected.ToArray()[0]).Element("age").Value;
			textBox_HomePhone.Text = (selected.ToArray()[0]).Element("homephone").Value;
			textBox_CellPhone.Text = (selected.ToArray()[0]).Element("cellphone").Value;
			textBox_Email.Text = (selected.ToArray()[0]).Element("email").Value;
			textBox_Date.Text = (selected.ToArray()[0]).Element("appointmentdate").Value;
			textBox_Time.Text = (selected.ToArray()[0]).Element("appointmenttime").Value;
			textBox_Notes.Text = (selected.ToArray()[0]).Element("appointmentnotes").Value;
		}

		private void button_Search_Click(object sender, EventArgs e)
		{
			//search parameters
			string search_text = textBox_Search.Text;
			string search_field = comboBox_Field.Text;

			//query variable
			IEnumerable<XElement> query;

			//handle the special search cases
			if (search_field == "age")
			{
				//age > search text
				if (radioButton_Old.Checked)
				{
					query = from patient in x_document.Element("patients").Elements()
							where int.Parse(patient.Element(search_field).Value) > int.Parse(search_text)
							select patient;
				}
				//age < search text
				else if (radioButton_Young.Checked)
				{
					query = from patient in x_document.Element("patients").Elements()
							where int.Parse(patient.Element(search_field).Value) < int.Parse(search_text)
							select patient;
				}
				//age == search text
				else
				{
					query = from patient in x_document.Element("patients").Elements()
							where patient.Element(search_field).Value == search_text
							select patient;
				}
			}
			else if(search_field == "appointmentdate")
			{
				if (radioButton_Month.Checked)//look only in the month section of date string
				{
					query = from patient in x_document.Element("patients").Elements()
							where patient.Element(search_field).Value.Substring(0,2) == search_text
							select patient;
				}
				else if (radioButton_Year.Checked)//look only in the year section of date string
				{
					query = from patient in x_document.Element("patients").Elements()
							where patient.Element(search_field).Value.Substring(6) == search_text
							select patient;
				}
				else//look at whole string
				{
					query = from patient in x_document.Element("patients").Elements()
							where patient.Element(search_field).Value == search_text
							select patient;
				}
			}
			else//not a special case
			{
				query = from patient in x_document.Element("patients").Elements()
						where patient.Element(search_field).Value == search_text
						select patient;
			}

			if (query.Count() == 0)//nothing found
			{
				MessageBox.Show("No matching items found");
			}
			else
			{
				//make a tree view of every patient that met search criteria
				string x_document_root = x_document.Root.Name.LocalName;
				TreeNode root_node = new TreeNode(x_document_root);

				treeView1.Nodes.Clear();//clear the old tree
				treeView1.Nodes.Add(root_node);//add the root node
				
				//add the rest of the nodes for the qualifing patients
				foreach (XElement patient in query)
				{
					//add the id node to the root
					TreeNode patient_node = new TreeNode("Patient ID: " +
						patient.Attribute("patientid").Value);
					root_node.Nodes.Add(patient_node);

					//add the rest of the nodes to the id node
					if (patient.HasElements)
					{
						foreach (XElement patient_child in patient.Descendants())
						{
							TreeNode child_node = new TreeNode(patient_child.Name +": "+
								patient_child.Value);
							patient_node.Nodes.Add(child_node);
						}
					}
				}
			}
		}

		private void comboBox_Field_SelectedIndexChanged(object sender, EventArgs e)
		{
			//disable the date buttons, enable the age buttons
			if ((string)comboBox_Field.SelectedItem == "age")
			{
				radioButton_Equal.Enabled = true;
				radioButton_Old.Enabled = true;
				radioButton_Young.Enabled = true;

				radioButton_Year.Enabled = false;
				radioButton_Date.Enabled = false;
				radioButton_Month.Enabled = false;
			}
			//disable the age buttons, enable the date buttons
			else if ((string)comboBox_Field.SelectedItem == "appointmentdate")
			{
				radioButton_Year.Enabled = true;
				radioButton_Date.Enabled = true;
				radioButton_Month.Enabled = true;

				radioButton_Equal.Enabled = false;
				radioButton_Old.Enabled = false;
				radioButton_Young.Enabled = false;

			}
			//disable all the buttons
			else
			{
				radioButton_Equal.Enabled = false;
				radioButton_Old.Enabled = false;
				radioButton_Young.Enabled = false;

				radioButton_Year.Enabled = false;
				radioButton_Date.Enabled = false;
				radioButton_Month.Enabled = false;
			}
		}
		private void comboBox_ID_SelectedIndexChanged(object sender, EventArgs e)
		{
			fill_controls();
		}
		private void button_Add_Click(object sender, EventArgs e)
		{
			//get the value of the last id number
			int lastid = int.Parse(x_document.Element("patients").Elements().Last().Attribute("patientid").Value);

			//create a new node from text box values, incrementing the last id for the new patient id
			x_document.Root.Add(new XElement("patient",
							new XAttribute("patientid", lastid + 1),
							new XElement("firstname", textBox_FirstName.Text),
							new XElement("lastname", textBox_LastName.Text),
							new XElement("age", textBox_Age.Text),
							new XElement("homephone", textBox_HomePhone.Text),
							new XElement("cellphone", textBox_CellPhone.Text),
							new XElement("email", textBox_Email.Text),
							new XElement("appointmentdate", textBox_Date.Text),
							new XElement("appointmenttime", textBox_Time.Text),
							new XElement("appointmentnotes", textBox_Notes.Text))
							);
			update_comboBox();//update the combo box with the new id
			x_document.Save(".../.../Resources/PatientData.xml");//save the document
			MessageBox.Show("Patient Added Successfully");//tell user patient was added
		}
		private void button_Edit_Click(object sender, EventArgs e)
		{
			//select only the patient with the correct ID
			var selected = from patient in x_document.Element("patients").Elements()
						   where patient.Attribute("patientid").Value == comboBox_ID.SelectedItem.ToString()
						   select patient;

			//replace the node with a new node created from the (potentially changed) text box values
			(selected.ToArray()[0]).ReplaceWith(new XElement("patient",
													new XAttribute("patientid", comboBox_ID.SelectedItem.ToString()),
													new XElement("firstname", textBox_FirstName.Text),
													new XElement("lastname", textBox_LastName.Text),
													new XElement("age", textBox_Age.Text),
													new XElement("homephone", textBox_HomePhone.Text),
													new XElement("cellphone", textBox_CellPhone.Text),
													new XElement("email", textBox_Email.Text),
													new XElement("appointmentdate", textBox_Date.Text),
													new XElement("appointmenttime", textBox_Time.Text),
													new XElement("appointmentnotes", textBox_Notes.Text))
												);

			x_document.Save(".../.../Resources/PatientData.xml");//save document
			MessageBox.Show("Patient Edited Successfully");//tell user edit was successful
		}

		private void button_Delete_Click(object sender, EventArgs e)
		{
			//select only the patient with the correct ID
			var selected = from patient in x_document.Element("patients").Elements()
						   where patient.Attribute("patientid").Value == comboBox_ID.SelectedItem.ToString()
						   select patient;

			(selected.ToArray()[0]).Remove();//remove the patient
			update_comboBox();//remove the id from the box
			x_document.Save(".../.../Resources/PatientData.xml");//save the document
			MessageBox.Show("Patient Removed Successfully");//tell user it was removed
		}

		
	}
}
