using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;

using DarkHeresyCharacter;

namespace DarkHeresyCharacterManager
{
	public partial class Form1 : Form
	{
		DHCharacter Character = new DHCharacter();
		public Form1()
		{
			InitializeComponent();
		}
		public void CharacterUpdate()
		{
			Name_Box.Text = Character.Name;

			#region Characteristics
			WS_Box.Text = Character.WS.Value.ToString();
			BS_Box.Text = Character.BS.Value.ToString();
			S_Box.Text = Character.S.Value.ToString();
			T_Box.Text = Character.T.Value.ToString();
			Ag_Box.Text = Character.Ag.Value.ToString();
			Int_Box.Text = Character.Int.Value.ToString();
			Per_Box.Text = Character.Per.Value.ToString();
			Wp_Box.Text = Character.Wp.Value.ToString();
			Fel_Box.Text = Character.Fel.Value.ToString();
			#endregion

			Wounds_Box.Text = Character.Wounds.ToString();
			Current_Wounds_Box.Text = Character.CurWounds.ToString();

			Fate_Box.Text = Character.Fate.ToString();
			Current_Fate_Box.Text = Character.CurFate.ToString();

			Insanity_Box.Text = Character.Insanity.ToString();
			Corruption_Box.Text = Character.Corruption.ToString();

			foreach (Trait t in Character.Traits)
			{
				Traits_Box.AppendText(t.Name + "\n");
				Traits_Box.AppendText(t.Effect + "\n\n");
			}
			foreach (Talent t in Character.Talents)
			{
				Talents_Box.AppendText(t.Name+"\n");
			}
			foreach (Skill s in Character.Skills)
			{
				Skills_Box.AppendText(s.Name + "\n");
			}
			foreach (Equipment e in Character.Gear)
			{
				Gear_Box.AppendText(e.Name + "\n");
			}
			Career_Box.Text = Character.Career.Base;
			Background_Box.Text = Character.Background.Name;
			XP_Box.Text = Character.XP_Spent.ToString();
		}

		private void Load_Button_Click(object sender, EventArgs e)
		{
			Stream LoadStream = null;
			OpenFileDialog openFileDialog1 = new OpenFileDialog();

			openFileDialog1.Filter = "Dark Heresy Character (*.DHC)|*.DHC";
			openFileDialog1.FilterIndex = 1;
			openFileDialog1.RestoreDirectory = true;

			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				try
				{
					if ((LoadStream = openFileDialog1.OpenFile()) != null)
					{
						using (LoadStream)
						{
							XmlSerializer serializer = new XmlSerializer(typeof(DHCharacter));
							Character = (DHCharacter)serializer.Deserialize(LoadStream);
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				CharacterUpdate();
			}

		}
	}
}

