using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Xml.Linq;
using System.IO;


namespace Dark_Heresy_Generator
{
	public partial class Form1 : Form
	{
		#region Initializations
		Random Dice = new Random();
		XDocument Origins;
		XDocument Careers;
		XDocument Names;
		private delegate void DivinationDel();
		DivinationDel DivDel;
		#endregion

		public Form1()
		{
			InitializeComponent();
			try
			{
				Origins = XDocument.Load("Resources/Origins.xml");
				Careers = XDocument.Load("Resources/Careers.xml");
				Names = XDocument.Load("Resources/Names.xml");
			}
			catch (Exception e)
			{
				//display error if file not found
				MessageBox.Show(e.Message);
			}
			Fill_Origin_comboBox();
		}

		private XElement GetOrigin()
		{
			//query variable
			IEnumerable<XElement> query;

			query = from origin in Origins.Element("root").Elements()
					where (string)(origin.Attribute("name").Value) == DHCharacter.Origin.Name
					select origin;

			return query.ElementAt(0);
		}
		private XElement GetCareer()
		{
			//query variable
			IEnumerable<XElement> query;

			query = from career in Careers.Element("root").Elements()
					where (((career.Attribute("name").Value) == DHCharacter.Career.Name) &&
							((career.Attribute("base").Value) == DHCharacter.Career.Base))
					select career;
			return query.ElementAt(0);
			
		}
		private XElement GetBackground()
		{
			IEnumerable<XElement> query;
			XElement career = GetCareer();

			query = from back in career.Element("backgrounds").Elements()
					where (string)(back.Attribute("name").Value) == DHCharacter.Background.Name
					select back;

			return query.ElementAt(0);
		}

		private void Fill_Base_Stats()
		{
			XElement origin = GetOrigin();
			DHCharacter.WS.Base = int.Parse(origin.Element("basestats").Element("WS").Value);
			DHCharacter.BS.Base = int.Parse(origin.Element("basestats").Element("BS").Value);
			DHCharacter.S.Base = int.Parse(origin.Element("basestats").Element("S").Value);
			DHCharacter.T.Base = int.Parse(origin.Element("basestats").Element("T").Value);
			DHCharacter.Ag.Base = int.Parse(origin.Element("basestats").Element("Ag").Value);
			DHCharacter.Int.Base = int.Parse(origin.Element("basestats").Element("Int").Value);
			DHCharacter.Per.Base = int.Parse(origin.Element("basestats").Element("Per").Value);
			DHCharacter.Wp.Base = int.Parse(origin.Element("basestats").Element("Wp").Value);
			DHCharacter.Fel.Base = int.Parse(origin.Element("basestats").Element("Fel").Value);
		}

		#region Combo Boxes
		private void Fill_Name_comboBox()
		{
			Name_comboBox.Items.Clear();//clear the box
			//select every origin
			var names = from name in Names.Element("root").Elements()
						where (string)(name.Attribute("type").Value) ==
								(string)(DHCharacter.Sex)
						select name;
			names = from name in names.Elements()
						select name;
			//add each name type to box
			foreach (XElement name in names)
			{
				Name_comboBox.Items.Add(name.Attribute("type").Value);
			}
		}
		private void Fill_Origin_comboBox()
		{
			Origin_comboBox.Items.Clear();//clear the box
			//select every origin
			var origins = from origin in Origins.Element("root").Elements()
						  select origin;
			//add each origin's name to box
			foreach (XElement origin in origins)
			{
				Origin_comboBox.Items.Add(origin.Attribute("name").Value);
			}
		}
		private void Fill_Career_comboBox()
		{
			XElement origin = GetOrigin();

			IEnumerable<string> careers = from career in origin.Element("careers").Elements()
						where career.Attribute("available").Value == "True"
						select career.Attribute("name").Value;

			IEnumerable<XElement> query = from career in Careers.Element("root").Elements()
						where careers.Contains<string>(career.Attribute("base").Value)
						select career;

			foreach (XElement career in query)
			{
				bool available = true;
				if (career.Element("requirements").Elements().Count() > 0)
				{
					foreach (XElement requirement in career.Element("requirements").Elements())
					{
						#region Type Switch
						switch (requirement.Attribute("type").Value)
						{
							case "origin, specific":
								if (requirement.Attribute("name").Value !=
									origin.Attribute("name").Value)
								{
									available = false;
								}
								break;
							case "origin, general":
								IEnumerable<string> names = from name in requirement.Attributes()
															where (name.Name == "name" ||
																	name.Name == "name1" ||
																	name.Name == "name2")
															select name.Value;
								if (names.Contains<string>(origin.Attribute("base").Value))
								{
									available = false;
								}
								break;
							case "stat":
								#region Stat Switch
								switch (requirement.Attribute("name").Value)
								{
									case "WS":
										if (int.Parse(requirement.Attribute("amount").Value)
											> int.Parse(WS_Box.Text))
										{
											available = false;
										}
										break;
									case "BS":
										if (int.Parse(requirement.Attribute("amount").Value)
											> int.Parse(BS_Box.Text))
										{
											available = false;
										}
										break;
									case "S":
										if (int.Parse(requirement.Attribute("amount").Value)
											> int.Parse(S_Box.Text))
										{
											available = false;
										}
										break;
									case "T":
										if (int.Parse(requirement.Attribute("amount").Value)
											> int.Parse(T_Box.Text))
										{
											available = false;
										}
										break;
									case "Ag":
										if (int.Parse(requirement.Attribute("amount").Value)
											> int.Parse(Ag_Box.Text))
										{
											available = false;
										}
										break;
									case "Int":
										if (int.Parse(requirement.Attribute("amount").Value)
											> int.Parse(Int_Box.Text))
										{
											available = false;
										}
										break;
									case "Per":
										if (int.Parse(requirement.Attribute("amount").Value)
											> int.Parse(Per_Box.Text))
										{
											available = false;
										}
										break;
									case "Wp":
										if (int.Parse(requirement.Attribute("amount").Value)
											> int.Parse(Wp_Box.Text))
										{
											available = false;
										}
										break;
									case "Fel":
										if (int.Parse(requirement.Attribute("amount").Value)
											> int.Parse(Fel_Box.Text))
										{
											available = false;
										}
										break;
								}
								#endregion
								break;
						}
						#endregion
					}
				}
				if (available)
				{
					if (career.Attribute("name").Value != career.Attribute("base").Value)
					{
						Career_comboBox.Items.Add(career.Attribute("name").Value +
							"(" + career.Attribute("base").Value + ")");
					}
					else
					{
						Career_comboBox.Items.Add(career.Attribute("name").Value);
					}
				}
			}
		}
		private void Fill_Background_comboBox()
		{
			Background_comboBox.Items.Clear();
			
			XElement career = GetCareer();
			XElement origin = GetOrigin();

			var backs = from back in career.Element("backgrounds").Elements()
						where back.Attribute("origin").Value.Contains(origin.Attribute("base").Value)
					select back;
			//add each name type to box
			foreach (XElement back in backs)
			{
				Background_comboBox.Items.Add(back.Attribute("name").Value + ": " + 
					back.Attribute("cost").Value + "xp");
			}
			
		}
		private void Origin_comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			DHCharacter.Origin.Name = Origin_comboBox.SelectedItem.ToString();
			IEnumerable<string> query = from origin in Origins.Element("root").Elements()
										where origin.Attribute("name").Value == DHCharacter.Origin.Name
										select origin.Attribute("base").Value;
			DHCharacter.Origin.Base = query.First();


			Reset();
			Fill_Base_Stats();
			Add_Origin_Skills();
			Add_Origin_Talents();
			Add_Origin_Traits();

			Add_Origin_Insanity();
			Add_Origin_Corruption();

			Set_Sex();
			Add_Appearance();
			Fill_Name_comboBox();
			Roll_button.Enabled = true;
			CharacterUpdate();
		}
		private void Career_comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			string[] attributes = Career_comboBox.SelectedItem.ToString().Split('(',')');
			DHCharacter.Career.Name = attributes.First();
			DHCharacter.Career.Base = attributes.Last(s=> s != "");

			Add_Career_Skills();
			Add_Career_Talents();
			Add_Career_Traits();
			Add_Career_Gear();

			Add_Wealth();

			if (GetCareer().Attribute("base").Value == "Adepta Sororitas" &&
				DHCharacter.Sex != "Female")
			{
				#region Appearance
				Name_Box.Clear();
				Sex_Box.Clear();
				Build_Box.Clear();
				Age_Box.Clear();
				Skin_Box.Clear();
				Hair_Box.Clear();
				Eye_Box.Clear();
				Quirk_Box.Clear();
				#endregion
				DHCharacter.Sex = "Female";
				Add_Appearance();
			}

			var query = from trait in GetOrigin().Element("traits").Elements()
						where trait.Attribute("name").Value == "Fit for the Purpose"
						select trait;

			if (query.Count() > 0)
			{
				FitForThePurpose(GetCareer());
			}

			Divination_Roll();
			CharacterUpdate();
			Fill_Background_comboBox();
		}
		private void Name_comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			IEnumerable<XElement> query;

			query = from names in Names.Element("root").Elements()
					where (string)(names.Attribute("type").Value) ==
								(string)(Sex_Box.Text)
					select names;

			query = from name in query.Elements()
					where (string)(name.Attribute("type").Value) ==
								(string)(Name_comboBox.SelectedItem)
					select name;

			int Size = query.Elements().Count();
			DHCharacter.Name = query.Elements().ElementAt(Dice.Next(Size)).Value;
			CharacterUpdate();
		}
		private void Background_comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			string[] attributes = Background_comboBox.SelectedItem.ToString().Split(':');
			DHCharacter.Background.Name = attributes.First();
			DHCharacter.Background.Cost = attributes.Last(s => s != "");

			Add_Back_Stats();
			Add_Back_Skills();
			Add_Back_Talents();
			Add_Back_Traits();
			Add_Back_Gear();
			Add_Back_Wealth();
			Add_Back_Fate();
			Add_Back_Insanity();
			Add_Back_Corruption();
			CharacterUpdate();
		}
		#endregion

		#region Rolls
		private void WS_Roll()
		{
			DHCharacter.WS.Roll=Dice.Next(1, 11) + Dice.Next(1, 11);
		}
		private void BS_Roll()
		{
			DHCharacter.BS.Roll = Dice.Next(1, 11) + Dice.Next(1, 11);
		}
		private void S_Roll()
		{
			DHCharacter.S.Roll = Dice.Next(1, 11) + Dice.Next(1, 11);
		}
		private void T_Roll()
		{
			DHCharacter.T.Roll = Dice.Next(1, 11) + Dice.Next(1, 11);
		}
		private void Ag_Roll()
		{
			DHCharacter.Ag.Roll = Dice.Next(1, 11) + Dice.Next(1, 11);
		}
		private void Int_Roll()
		{
			DHCharacter.Int.Roll = Dice.Next(1, 11) + Dice.Next(1, 11);
		}
		private void Per_Roll()
		{
			DHCharacter.Per.Roll = Dice.Next(1, 11) + Dice.Next(1, 11);
		}
		private void Wp_Roll()
		{
			DHCharacter.Wp.Roll = Dice.Next(1, 11) + Dice.Next(1, 11);
		}
		private void Fel_Roll()
		{
			DHCharacter.Fel.Roll = Dice.Next(1, 11) + Dice.Next(1, 11);
		}

		private void Wound_Roll()
		{
			XElement origin = GetOrigin();
			DHCharacter.Wounds=Dice.Next(1, 6) + int.Parse(origin.Element("basestats").Element("Wounds").Value);
		}
		private void Fate_Roll()
		{
			int result = Dice.Next(1, 11);
			XElement origin = GetOrigin();
			foreach (XElement option in origin.Element("basestats").Element("FatePoints").Elements())
			{
				if (result >= int.Parse(option.Attribute("lower").Value) &&
					result <= int.Parse(option.Attribute("upper").Value))
				{
					DHCharacter.Fate = int.Parse(option.Attribute("amount").Value);
				}
			}
		}

		private void Insanity_Roll(int num, int size, int Base)
		{
			int roll=0;
			for (int i = 0; i < num; i++)
			{
				roll += Dice.Next(1, size + 1);
			}
			roll+=Base;
			DHCharacter.Insanity += roll;
		}
		private void Corruption_Roll(int num, int size, int Base)
		{
			int roll = 0;
			for (int i = 0; i < num; i++)
			{
				roll += Dice.Next(1, size + 1);
			}
			roll += Base;
			DHCharacter.Corruption += roll;
		}

		private void Divination_Roll()
		{
			switch (Dice.Next(1, 101))
			{
				case 1:
					Divination_Box.Text =
					"\"Mutation without, corruption within.\"" +
					"\nBegin play with one Minor Mutation";
					DivDel=delegate()
					{
						int result = Dice.Next(1,101);
						MinorMutation(result,"Divination");
					};
					break;
				case 2: case 3:
					Divination_Box.Text =
					"\"Only the insane have strength enough to prosper." +
					" Only those who prosper may judge what is sane.\"" +
					"\nBegin play with 2 Insanity Points.";
					DivDel = delegate()
					{
						DHCharacter.Insanity += 2;
					};
					break;
				case 4: case 5: case 6: case 7:
					Divination_Box.Text=
					"\"Sins hidden in the heart turn all to decay.\""+
					"\nBegin play with 3 Corruption Points.";
					DivDel = delegate()
					{
						DHCharacter.Corruption += 3;
					};
					break;
				case 8:
					Divination_Box.Text=
					"\"Innocence is an illusion.\""+
					"\nBegin play with 1 Insanity Point and 1 Corruption Point.";
					DivDel = delegate()
					{
						DHCharacter.Insanity += 1;
						DHCharacter.Corruption += 1;
					};
					break;
				case 9: case 10: case 11:
					Divination_Box.Text=
					"\"Dark dreams lie upon the heart.\""+
					"\nBegin play with 2 Corruption Points.";
					DivDel = delegate()
					{
						DHCharacter.Corruption += 2;
					};
					break;
				case 12: case 13: case 14: case 15:
					Divination_Box.Text=
					"\"The pain of the bullet is ecstasy compared to damnation.\""+
					"\nIncrease Toughness by +1.";
					DivDel = delegate()
					{
						DHCharacter.T.Base += 1;
					};
					break;
				case 16: case 17: case 18:
					Divination_Box.Text =
					"\"Kill the alien before it can speak its lies.\"" +
					"\nIncrease Agility by +2.";
					DivDel = delegate()
					{
						DHCharacter.Ag.Base += 2;
					};
					break;
				case 19: case 20: case 21:
					Divination_Box.Text =
					"\"Truth is subjective.\"" +
					"\nIncrease Intelligence by +3. Begin play with 3 Corruption Points.";
					DivDel = delegate()
					{
						DHCharacter.Int.Base += 3;
						DHCharacter.Corruption += 3;
					};
					break;
				case 22: case 23: case 24: case 25: case 26:
					Divination_Box.Text =
					"\"Know the mutant; kill the mutant.\"" +
					"\nIncrease Perception by +2.";
					DivDel = delegate()
					{
						DHCharacter.Per.Base += 2;
					};
					break;
				case 27: case 28: case 29: case 30:
					Divination_Box.Text=
					"\"Even a man who has nothing can still offer his life.\""+
					"\nIncrease Strength by +2.";
					DivDel = delegate()
					{
						DHCharacter.S.Base += 2;
					};
					break;
				case 31: case 32: case 33:
					Divination_Box.Text=
					"\"If a job is worth doing it is worth dying for.\""+
					"\nGain the Frenzy talent.";
					DivDel = delegate()
					{
						DHCharacter.Talents.Add(new Talent("Frenzy","Divination"));
					};
					break;
				case 34: case 35: case 36: case 37: case 38:
					Divination_Box.Text=
					"\"Only in death does duty end.\""+
					"\nGain 1 Wound.";
					DivDel = delegate()
					{
						DHCharacter.Wounds += 1;
					};
					break;
				case 39: case 40: case 41: case 42:
					Divination_Box.Text=
					"\"A mind without purpose will wander in dark places.\""+
					"\nGain 1 Fate Point.";
					DivDel = delegate()
					{
						DHCharacter.Fate += 1;
					};
					break;
				case 43: case 44: case 45: case 46:
					Divination_Box.Text=
					"\"There are no civilians in the battle for survival.\""+
					"\nIncrease Toughness by +2 and gain 1 Wound.";
					DivDel = delegate()
					{
						DHCharacter.T.Base += 2;
						DHCharacter.Wounds += 1;
					};
					break;
				case 47: case 48: case 49: case 50:
					Divination_Box.Text=
					"\"Violence solves everything.\""+
					"\nIncrease Weapon Skill by +3.";
					DivDel = delegate()
					{
						DHCharacter.WS.Base += 3;
					};
					break;
				case 51: case 52: case 53: case 54:
					Divination_Box.Text =
					"\"To war is human.\"" +
					"\nIncrease Agility by +3.";
					DivDel = delegate()
					{
						DHCharacter.Ag.Base += 3;
					};
					break;
				case 55: case 56: case 57: case 58:
					Divination_Box.Text=
					"\"Die if you must, but not with your spirit broken.\""+
					"\nIncrease Willpower by +3.";
					DivDel = delegate()
					{
						DHCharacter.Wp.Base += 3;
					};
					break;
				case 59: case 60: case 61: case 62:
					Divination_Box.Text=
					"\"The gun is mightier than the sword.\""+
					"\nIncrease Ballistic Skill by +3.";
					DivDel = delegate()
					{
						DHCharacter.BS.Base += 3;
					};
					break;
				case 63: case 64: case 65: case 66:
					Divination_Box.Text=
					"\"Be a boon to your brothers and bane to your enemies.\""+
					"\nIncrease Fellowship by +3.";
					DivDel = delegate()
					{
						DHCharacter.Fel.Base += 3;
					};
					break;
				case 67: case 68: case 69: case 70:
					Divination_Box.Text=
					"\"Men must die so that Man endures.\""+
					"\nIncrease Toughness by +3.";
					DivDel = delegate()
					{
						DHCharacter.T.Base += 3;
					};
					break;
				case 71: case 72: case 73: case 74:
					Divination_Box.Text=
					"\"In the darkness, follow the light of Terra.\""+
					"\nIncrease Willpower by +3.";
					DivDel = delegate()
					{
						DHCharacter.Wp.Base += 3;
					};
					break;
				case 75: case 76: case 77: case 78: case 79:
					Divination_Box.Text=
					"\"The only true fear is of dying with your duty not done.\""+
					"\nGain 2 Wounds.";
					DivDel = delegate()
					{
						DHCharacter.Wounds += 2;
					};
					break;
				case 80: case 81: case 82: case 83: case 84: case 85:
					Divination_Box.Text=
					"\"Thought begets Heresy; Heresy begets Retribution.\""+
					"\nIncrease Strength by +3.";
					DivDel = delegate()
					{
						DHCharacter.S.Base += 3;
					};
					break;
				case 86: case 87: case 88: case 89: case 90:
					Divination_Box.Text=
					"\"The wise man learns from the deaths of others.\""+
					"\nIncrease Intelligence by +3.";
					DivDel = delegate()
					{
						DHCharacter.Int.Base += 3;
					};
					break;
				case 91: case 92: case 93: case 94:
					Divination_Box.Text=
					"\"The suspicious mind is a healthy mind.\""+
					"\nIncrease Perception by +3.";
					DivDel = delegate()
					{
						DHCharacter.Per.Base += 3;
					};
					break;
				case 95: case 96: case 97:
					Divination_Box.Text=
					"\"Trust in your fear.\""+
					"\nIncrease Agility by +2 and gain 1 Fate Point.";
					DivDel = delegate()
					{
						DHCharacter.Ag.Base += 2;
						DHCharacter.Fate += 1;
					};
					break;
				case 98: case 99:
					Divination_Box.Text=
					"\"There is no substitute for zeal.\""+
					"\nIncrease Toughness and Willpower each by +2.";
					DivDel = delegate()
					{
						DHCharacter.T.Base += 2;
						DHCharacter.Wp.Base += 2;
					};
					break;
				case 100:
					Divination_Box.Text=
					"\"Do not ask why you serve. Only ask how.\""+
					"\nIncrease Weapon Skill and Ballistic Skill each by +2.";
					DivDel = delegate()
					{
						DHCharacter.WS.Base += 2;
						DHCharacter.BS.Base += 2;
					};
					break;
			}
		}
		#endregion

		#region Buttons
		private void Export_Button_Click(object sender, EventArgs e)
		{
			Stream ExportStream;
			SaveFileDialog saveFileDialog1 = new SaveFileDialog();

			saveFileDialog1.FileName = Name_Box.Text + ".txt";
			saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
			saveFileDialog1.FilterIndex = 1;
			saveFileDialog1.RestoreDirectory = true;

			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				if ((ExportStream = saveFileDialog1.OpenFile()) != null)
				{
					StreamWriter sw = new StreamWriter(ExportStream);
					sw.WriteLine("Name: {0}", Name_Box.Text);
					sw.WriteLine("Origin: {0}  Career: {1}",
						Origin_comboBox.SelectedItem, Career_comboBox.SelectedItem);
					sw.WriteLine("Background: {0}",Background_comboBox.SelectedItem);
					sw.WriteLine("==================================================");
					sw.WriteLine("Sex: {0}  Age: {1}  Build: {2}",
						Sex_Box.Text,Age_Box.Text,Build_Box.Text);
					sw.WriteLine("Skin: {0}  Eye: {1}  Hair: {2}",
						Skin_Box.Text, Eye_Box.Text, Hair_Box.Text);
					sw.WriteLine("Quirk: {0}",Quirk_Box.Text);
					sw.WriteLine("==================================================");
					sw.WriteLine("Divination: {0}", Divination_Box.Text);
					sw.WriteLine("Bio: {0}", Bio_Box.Text);
					sw.WriteLine("==================================================");
					sw.WriteLine();
					sw.WriteLine("WS: {0}", WS_Box.Text);
					sw.WriteLine("BS: {0}", BS_Box.Text);
					sw.WriteLine("S: {0}", S_Box.Text);
					sw.WriteLine("T: {0}", T_Box.Text);
					sw.WriteLine("Ag: {0}", Ag_Box.Text);
					sw.WriteLine("Int: {0}", Int_Box.Text);
					sw.WriteLine("Per: {0}", Per_Box.Text);
					sw.WriteLine("Wp: {0}", Wp_Box.Text);
					sw.WriteLine("Fel: {0}", Fel_Box.Text);
					sw.WriteLine();
					sw.WriteLine("Wounds: {0}  Fate Points: {1}", Wounds_Box.Text, Fate_Box.Text);
					sw.WriteLine("Insanity: {0}  Corruption: {1}", Insanity_Box.Text, Corruption_Box.Text);
					sw.WriteLine();
					sw.WriteLine("Traits:");
					sw.WriteLine(Traits_Box.Text);
					sw.WriteLine();
					sw.WriteLine("Skills:");
					sw.WriteLine(Skills_Box.Text);
					sw.WriteLine();
					sw.WriteLine("Talents:");
					sw.WriteLine(Talents_Box.Text);
					sw.WriteLine();
					sw.WriteLine("Starting Gear:");
					sw.WriteLine(Gear_Box.Text);
					sw.WriteLine();
					sw.WriteLine("Thrones: {0}", Money_Box.Text);
					sw.WriteLine("Income: {0}", Income_Box.Text);
					sw.Close();
					ExportStream.Close();
				}
			}
		}
		private void Reset_Button_Click(object sender, EventArgs e)
		{
			Reset();
		}
		private void Roll_button_Click(object sender, EventArgs e)
		{
			Career_comboBox.Items.Clear();
			WS_Roll();
			BS_Roll();
			S_Roll();
			T_Roll();
			Ag_Roll();
			Int_Roll();
			Per_Roll();
			Wp_Roll();
			Fel_Roll();

			Wound_Roll();
			Fate_Roll();

			Reroll_Enable();
			CharacterUpdate();
			Fill_Career_comboBox();
		}
		private void WS_Reroll_Button_Click(object sender, EventArgs e)
		{
			WS_Roll();
			CharacterUpdate();
			Reroll_Disable();
		}
		private void BS_Reroll_Button_Click(object sender, EventArgs e)
		{
			BS_Roll();
			CharacterUpdate();
			Reroll_Disable();
		}
		private void S_Reroll_Button_Click(object sender, EventArgs e)
		{
			S_Roll();
			CharacterUpdate();
			Reroll_Disable();
		}
		private void T_Reroll_Button_Click(object sender, EventArgs e)
		{
			T_Roll();
			CharacterUpdate();
			Reroll_Disable();
		}
		private void Ag_Reroll_Button_Click(object sender, EventArgs e)
		{
			Ag_Roll();
			CharacterUpdate();
			Reroll_Disable();
		}
		private void Int_Reroll_Button_Click(object sender, EventArgs e)
		{
			Int_Roll();
			CharacterUpdate();
			Reroll_Disable();
		}
		private void Per_Reroll_Button_Click(object sender, EventArgs e)
		{
			Per_Roll();
			CharacterUpdate();
			Reroll_Disable();
		}
		private void Wp_Reroll_Button_Click(object sender, EventArgs e)
		{
			Wp_Roll();
			CharacterUpdate();
			Reroll_Disable();
		}
		private void Fel_Reroll_Button_Click(object sender, EventArgs e)
		{
			Fel_Roll();
			CharacterUpdate();
			Reroll_Disable();
		}
		private void Reroll_Disable()
		{
			WS_Reroll_Button.Enabled = false;
			BS_Reroll_Button.Enabled = false;
			S_Reroll_Button.Enabled = false;
			T_Reroll_Button.Enabled = false;
			Ag_Reroll_Button.Enabled = false;
			Int_Reroll_Button.Enabled = false;
			Per_Reroll_Button.Enabled = false;
			Wp_Reroll_Button.Enabled = false;
			Fel_Reroll_Button.Enabled = false;
		}
		private void Reroll_Enable()
		{
			WS_Reroll_Button.Enabled = true;
			BS_Reroll_Button.Enabled = true;
			S_Reroll_Button.Enabled = true;
			T_Reroll_Button.Enabled = true;
			Ag_Reroll_Button.Enabled = true;
			Int_Reroll_Button.Enabled = true;
			Per_Reroll_Button.Enabled = true;
			Wp_Reroll_Button.Enabled = true;
			Fel_Reroll_Button.Enabled = true;
		}
		private void Divination_Button_Click(object sender, EventArgs e)
		{
			DivDel();
			DivDel = delegate() { };
			CharacterUpdate();
		}
		#endregion

		#region Adding Functions
		private void Add_Skill(XElement skill)
		{
			if (skill.Attributes().Count() > 1)
			{
				List<string> SkillList = new List<string>();
				foreach (XAttribute choice in skill.Attributes())
				{
					SkillList.Add(choice.Value);
				}
				DialogBoxList SkillDialog = new DialogBoxList(SkillList);
				SkillDialog.ShowDialog();
				DHCharacter.Skills.Add(new Skill(SkillDialog.selection, skill.Parent.Parent.Name.ToString()));
			}
			else
			{
				DHCharacter.Skills.Add(new Skill(skill.Attribute("choice").Value, skill.Parent.Parent.Name.ToString()));
			}
		}
		private void Add_Talent(XElement talent)
		{
			if (talent.Attributes().Count() > 1)
			{
				List<string> TalentList = new List<string>();
				foreach (XAttribute choice in talent.Attributes())
				{
					TalentList.Add(choice.Value);
				}
				DialogBoxList TalentDialog = new DialogBoxList(TalentList);
				TalentDialog.ShowDialog();
				DHCharacter.Talents.Add(new Talent(TalentDialog.selection, talent.Parent.Parent.Name.ToString()));
			}
			else
			{
				DHCharacter.Talents.Add(new Talent(talent.Attribute("choice").Value, talent.Parent.Parent.Name.ToString()));
			}
		}
		private void Add_Trait(XElement trait)
		{
			DHCharacter.Traits.Add(new Trait(trait.Attribute("name").Value,trait.Attribute("effect").Value,
				trait.Parent.Parent.Name.ToString()));
			if (trait.Attribute("name").Value == "Sanctioned Psyker")
			{
				string SideEffect = SanctioningSideEffects();
				DHCharacter.Traits.Add(new Trait("Sanctioning Side Effect:", SideEffect, 
					trait.Parent.Parent.Name.ToString()));
			}
			if (trait.Attribute("name").Value == "Twist")
			{
				#region Twist
				DialogBox2 Dialog = new DialogBox2("Choose 2 Minor Mutations", "1 random Minor, and 1 random Major Mutation");
				DialogResult result = Dialog.ShowDialog();
				if (result == DialogResult.Yes)
				{
					List<string> list = new List<string>();
					list.Add("Grotesque");
					list.Add("Tough Hide");
					list.Add("Misshapen");
					list.Add("Feels No Pain");
					list.Add("Brute");
					list.Add("Nightsider");
					list.Add("Big Eyes");
					list.Add("Malformed Hands");
					list.Add("Tox Blood");
					list.Add("Wyrdling");
					DialogBoxList MutationDialog = new DialogBoxList(list);
					DialogResult MutationResult = MutationDialog.ShowDialog();
					switch (MutationDialog.selection)
					{
						case ("Grotesque"):
							MinorMutation(1, "Origin");
							break;
						case("Tough Hide"):
							MinorMutation(21, "Origin");
							break;
						case("Misshapen"):
							MinorMutation(31, "Origin");
							break;
						case("Feels No Pain"):
							MinorMutation(41, "Origin");
							break;
						case("Brute"):
							MinorMutation(51, "Origin");
							break;
						case("Nightsider"):
							MinorMutation(61, "Origin");
							break;
						case("Big Eyes"):
							MinorMutation(71, "Origin");
							break;
						case("Malformed Hands"):
							MinorMutation(81, "Origin");
							break;
						case("Tox Blood"):
							MinorMutation(86, "Origin");
							break;
						case("Wyrdling"):
							MinorMutation(90, "Origin");
							break;
					}
					List<string> list1 = new List<string>();
					list1.Add("Grotesque");
					list1.Add("Tough Hide");
					list1.Add("Misshapen");
					list1.Add("Feels No Pain");
					list1.Add("Brute");
					list1.Add("Nightsider");
					list1.Add("Big Eyes");
					list1.Add("Malformed Hands");
					list1.Add("Tox Blood");
					list1.Add("Wyrdling");
					for (int i = 0; i < list1.Count; i++)
					{
						if (list1.ElementAt(i) == MutationDialog.selection)
						{
							list1.RemoveAt(i);
						}
					}
					DialogBoxList MutationDialog1 = new DialogBoxList(list1);
					DialogResult MutationResult1 = MutationDialog1.ShowDialog();
					switch (MutationDialog1.selection)
					{
						case ("Grotesque"):
							MinorMutation(1, "Origin");
							break;
						case ("Tough Hide"):
							MinorMutation(21, "Origin");
							break;
						case ("Misshapen"):
							MinorMutation(31, "Origin");
							break;
						case ("Feels No Pain"):
							MinorMutation(41, "Origin");
							break;
						case ("Brute"):
							MinorMutation(51, "Origin");
							break;
						case ("Nightsider"):
							MinorMutation(61, "Origin");
							break;
						case ("Big Eyes"):
							MinorMutation(71, "Origin");
							break;
						case ("Malformed Hands"):
							MinorMutation(81, "Origin");
							break;
						case ("Tox Blood"):
							MinorMutation(86, "Origin");
							break;
						case ("Wyrdling"):
							MinorMutation(90, "Origin");
							break;
					}
				}
				else
				{
					MinorMutation(Dice.Next(1, 101),"Origin");
					MajorMutation(Dice.Next(1, 101),"Origin");
				}
				#endregion
			}
			if (trait.Attribute("name").Value == "Mysterious Lineage")
			{
				MysteriousLineage();
			}
		}
		private void Add_Gear(XElement equip)
		{
			if (equip.Attributes().Count() > 1)
			{
				List<string> EquipList = new List<string>();
				foreach (XAttribute choice in equip.Attributes())
				{
					EquipList.Add(choice.Value);
				}
				DialogBoxList EquipDialog = new DialogBoxList(EquipList);
				EquipDialog.ShowDialog();
				DHCharacter.Gear.Add(new Equipment(EquipDialog.selection, equip.Parent.Parent.Name.ToString()));
			}
			else
			{
				DHCharacter.Gear.Add(new Equipment(equip.Attribute("choice").Value, equip.Parent.Parent.Name.ToString()));
			}
		}
		#endregion

		#region Origin Additions
		private void Add_Origin_Traits()
		{
			XElement origin = GetOrigin();
			foreach (XElement trait in origin.Element("traits").Elements())
			{
				Add_Trait(trait);
			}
		}
		private void Add_Origin_Talents()
		{
			XElement origin = GetOrigin();
			foreach (XElement talent in origin.Element("talents").Elements())
			{
				Add_Talent(talent);
			}
		}
		private void Add_Origin_Skills()
		{
			XElement origin = GetOrigin();
			foreach (XElement skill in origin.Element("skills").Elements())
			{
				Add_Skill(skill);
			}
		}

		private void Add_Origin_Insanity()
		{
			XElement origin = GetOrigin();
			foreach (XElement points in origin.Element("basestats").Element("insanity").Elements())
			{
				int num = int.Parse(points.Attribute("num").Value);
				int size = int.Parse(points.Attribute("size").Value);
				int Base = int.Parse(points.Attribute("base").Value);
				Insanity_Roll(num,size,Base);
			}
		}
		private void Add_Origin_Corruption()
		{
			XElement origin = GetOrigin();
			foreach (XElement points in origin.Element("basestats").Element("corruption").Elements())
			{
				int num = int.Parse(points.Attribute("num").Value);
				int size = int.Parse(points.Attribute("size").Value);
				int Base = int.Parse(points.Attribute("base").Value);
				Corruption_Roll(num, size, Base);
				if (points.Attributes().Count() > 3)
				{
					var query = from mal in points.Attributes()
								where mal.Name == "mal"
								select mal.Value;
					if (query.ElementAt(0) == "true")
					{
						Malignancy();
					}
				}
			}
		}

		private void Add_Appearance()
		{
			XElement origin = GetOrigin();
			Set_Build(origin.Element("appearance").Element("builds"));
			Set_Skin(origin.Element("appearance").Element("skins"));
			Set_Age(origin.Element("appearance").Element("ages"));
			Set_Hair(origin.Element("appearance").Element("hairs"));
			Set_Eye(origin.Element("appearance").Element("eyes"));
			Set_Quirk(origin.Element("appearance").Element("quirks"));

		}
		#region Appearance
		private void Set_Sex()
		{
			int result = Dice.Next(2);
			if (result == 0)
			{
				DHCharacter.Sex = "Male";
			}
			else
			{
				DHCharacter.Sex = "Female";
			}
		}
		private void Set_Build(XElement builds)
		{
			int result = Dice.Next(1, 101);
			foreach (XElement build in builds.Elements())
			{
				if (result >= int.Parse(build.Attribute("lower").Value) &&
					result <= int.Parse(build.Attribute("upper").Value))
				{
					Build_Box.AppendText(build.Attribute("name").Value + ": ");
					if (DHCharacter.Sex == "Male")
					{
						DHCharacter.Build = build.Attribute("Male").Value;
					}
					else
					{
						DHCharacter.Build = build.Attribute("Female").Value;
					}
					break;
				}
			}
		}
		private void Set_Skin(XElement skins)
		{
			int result = Dice.Next(1, 101);
			foreach (XElement skin in skins.Elements())
			{
				if (result >= int.Parse(skin.Attribute("lower").Value) &&
					result <= int.Parse(skin.Attribute("upper").Value))
				{
					DHCharacter.Skin = skin.Attribute("name").Value;
					break;
				}
			}
		}
		private void Set_Age(XElement ages)
		{
			int result = Dice.Next(1, 101);
			foreach (XElement age in ages.Elements())
			{
				if (result >= int.Parse(age.Attribute("lower").Value) &&
					result <= int.Parse(age.Attribute("upper").Value))
				{
					int Age = int.Parse(age.Attribute("base").Value) + Dice.Next(1, 11);
					DHCharacter.Age = age.Attribute("name").Value + ": " + Age.ToString();
					break;
				}
			}
		}
		private void Set_Hair(XElement hairs)
		{
			int result = Dice.Next(1, 101);
			foreach (XElement hair in hairs.Elements())
			{
				if (result >= int.Parse(hair.Attribute("lower").Value) &&
					result <= int.Parse(hair.Attribute("upper").Value))
				{
					DHCharacter.Hair = hair.Attribute("name").Value;
					break;
				}
			}
		}
		private void Set_Eye(XElement eyes)
		{
			int result = Dice.Next(1, 101);
			foreach (XElement eye in eyes.Elements())
			{
				if (result >= int.Parse(eye.Attribute("lower").Value) &&
					result <= int.Parse(eye.Attribute("upper").Value))
				{
					DHCharacter.Eye = eye.Attribute("name").Value;
					break;
				}
			}
		}
		private void Set_Quirk(XElement quirks)
		{
			int result = Dice.Next(1, 101);
			foreach (XElement quirk in quirks.Elements())
			{
				if (result >= int.Parse(quirk.Attribute("lower").Value) &&
					result <= int.Parse(quirk.Attribute("upper").Value))
				{
					DHCharacter.Quirk = quirk.Attribute("name").Value;
					break;
				}
			}
		}
		#endregion
		#endregion

		#region Career Additions
		private void Add_Career_Skills()
		{
			XElement career = GetCareer();
			foreach (XElement skill in career.Element("skills").Elements())
			{
				Add_Skill(skill);			
			}
		}
		private void Add_Career_Talents()
		{
			XElement career = GetCareer();
			foreach (XElement talent in career.Element("talents").Elements())
			{
				Add_Talent(talent);
			}
		}
		private void Add_Career_Traits()
		{
			XElement career = GetCareer();
			foreach (XElement trait in career.Element("traits").Elements())
			{
				Add_Trait(trait);
			}
		}
		private void Add_Career_Gear()
		{
			XElement career = GetCareer();
			foreach (XElement equip in career.Element("gear").Elements())
			{
				Add_Gear(equip);
			}
		}

		private void Add_Career_Insanity()
		{
			XElement career = GetCareer();
			foreach (XElement points in career.Element("insanity").Elements())
			{
				int num = int.Parse(points.Attribute("num").Value);
				int size = int.Parse(points.Attribute("size").Value);
				int Base = int.Parse(points.Attribute("base").Value);
				Insanity_Roll(num, size, Base);
			}
		}
		private void Add_Career_Corruption()
		{
			XElement career = GetCareer();
			foreach (XElement points in career.Element("corruption").Elements())
			{
				int num = int.Parse(points.Attribute("num").Value);
				int size = int.Parse(points.Attribute("size").Value);
				int Base = int.Parse(points.Attribute("base").Value);
				Corruption_Roll(num, size, Base);
				if (points.Attributes().Count() > 3)
				{
					var query = from mal in points.Attributes()
								where mal.Name == "mal"
								select mal.Value;
					if (query.ElementAt(0) == "true")
					{
						Malignancy();
					}
				}
			}
		}

		private void Add_Wealth()
		{
			XElement career = GetCareer();
			XElement origin = GetOrigin();

			DHCharacter.Income = career.Element("wealth").Element("income").Attribute("name").Value;

			int thrones = 0;
			int num = int.Parse(career.Element("wealth").Element("thrones").Attribute("num").Value);
			int size = int.Parse(career.Element("wealth").Element("thrones").Attribute("size").Value);
			int Base = int.Parse(career.Element("wealth").Element("thrones").Attribute("base").Value);
			for (int i = 0; i < num; i++)
			{
				thrones += Dice.Next(1,size+1);
			}
			thrones += Base;

			if (origin.Element("wealth").Elements().Count() > 0)
			{
				switch (origin.Element("wealth").Element("thrones").Attribute("type").Value)
				{
					case "*":
						thrones *= int.Parse(origin.Element("wealth").Element("thrones").Attribute("mod").Value);
						break;
					case "/":
						thrones /= int.Parse(origin.Element("wealth").Element("thrones").Attribute("mod").Value);
						break;
					case "+":
						thrones += int.Parse(origin.Element("wealth").Element("thrones").Attribute("mod").Value);
						break;
					case "-":
						thrones -= int.Parse(origin.Element("wealth").Element("thrones").Attribute("mod").Value);
						break;
				}
				switch (origin.Element("wealth").Element("income").Attribute("type").Value)
				{
					case "replace":
						DHCharacter.Income = origin.Element("wealth").Element("income").Attribute("name").Value;
						break;
					case "add":
						DHCharacter.Income += (origin.Element("wealth").Element("income").Attribute("name").Value);
						break;
				}
			}
			DHCharacter.Thrones += thrones;
		}
		#endregion

		#region Background Additions
		private void Add_Back_Stats()
		{
			XElement back = GetBackground();
			foreach (XElement stat in back.Element("stats").Elements())
			{
				string choice;
				if(stat.Attributes().Count() == 3)
				{
					string choice1 = stat.Attribute("name").Value;
					string choice2 = stat.Attribute("name1").Value;
					string amount = stat.Attribute("amount").Value;
					DialogBox2 Dialog = new DialogBox2(choice1 +" +"+ amount, choice2 +" +"+ amount);
					DialogResult result = Dialog.ShowDialog();
					if (result == DialogResult.Yes)
					{
						choice = choice1;
					}
					else
					{
						choice = choice2;
					}
				}
				else
				{
					choice = stat.Attribute("name").Value;
				}
				 
				switch (choice)
				{
					case "WS":
						DHCharacter.WS.Base += (int.Parse(stat.Attribute("amount").Value));
						break;
					case "BS":
						DHCharacter.BS.Base += (int.Parse(stat.Attribute("amount").Value));
						break;
					case "S":
						DHCharacter.S.Base += (int.Parse(stat.Attribute("amount").Value));
						break;
					case "T":
						DHCharacter.T.Base += (int.Parse(stat.Attribute("amount").Value));
						break;
					case "Ag":
						DHCharacter.Ag.Base += (int.Parse(stat.Attribute("amount").Value));
						break;
					case "Int":
						DHCharacter.Int.Base += (int.Parse(stat.Attribute("amount").Value));
						break;
					case "Per":
						DHCharacter.Per.Base += (int.Parse(stat.Attribute("amount").Value));
						break;
					case "Wp":
						DHCharacter.Wp.Base += (int.Parse(stat.Attribute("amount").Value));
						break;
					case "Fel":
						DHCharacter.Fel.Base += (int.Parse(stat.Attribute("amount").Value));
						break;
				}
			}
		}
		private void Add_Back_Skills()
		{
			XElement back = GetBackground();
			foreach (XElement skill in back.Element("skills").Elements())
			{
				Add_Skill(skill);
			}
		}
		private void Add_Back_Talents()
		{
			XElement back = GetBackground();
			foreach (XElement talent in back.Element("talents").Elements())
			{
				Add_Talent(talent);
			}
		}
		private void Add_Back_Traits()
		{
			XElement back = GetBackground();
			foreach (XElement trait in back.Element("traits").Elements())
			{
				Add_Trait(trait);
			}
		}
		private void Add_Back_Gear()
		{
			XElement back = GetBackground();
			if(back.Element("gear").Elements().Count()>0)
			{
				Gear_Box.Clear();
				foreach (XElement equip in back.Element("gear").Elements())
				{
					Add_Gear(equip);
				}
			}
		}
		private void Add_Back_Wealth()
		{
			XElement back = GetBackground();
			
			if (back.Element("wealth").Elements().Count() > 0)
			{
				switch (back.Element("wealth").Element("thrones").Attribute("type").Value)
				{
					case "*":
						DHCharacter.Thrones *= int.Parse(back.Element("wealth").Element("thrones").Attribute("mod").Value);
						break;
					case "/":
						DHCharacter.Thrones /= int.Parse(back.Element("wealth").Element("thrones").Attribute("mod").Value);
						break;
					case "+":
						DHCharacter.Thrones += int.Parse(back.Element("wealth").Element("thrones").Attribute("mod").Value);
						break;
					case "-":
						DHCharacter.Thrones -= int.Parse(back.Element("wealth").Element("thrones").Attribute("mod").Value);
						break;
				}
				switch (back.Element("wealth").Element("income").Attribute("type").Value)
				{
					case "replace":
						DHCharacter.Income = back.Element("wealth").Element("income").Attribute("name").Value;
						break;
					case "add":
						DHCharacter.Income += (back.Element("wealth").Element("income").Attribute("name").Value);
						break;
				}
			}
		}
		private void Add_Back_Fate()
		{
			XElement back = GetBackground();
			if (back.Element("fate").Elements().Count() > 0)
			{
				switch (back.Element("fate").Element("points").Attribute("type").Value)
				{
					case "+":
						DHCharacter.Fate += int.Parse(back.Element("fate").Element("points").Attribute("amount").Value);
						break;
					case "-":
						DHCharacter.Fate -= int.Parse(back.Element("fate").Element("points").Attribute("amount").Value);
						break;
				}
			}
		}
		private void Add_Back_Insanity()
		{
			XElement back = GetBackground();
			foreach (XElement points in back.Element("insanity").Elements())
			{
				int num = int.Parse(points.Attribute("num").Value);
				int size = int.Parse(points.Attribute("size").Value);
				int Base = int.Parse(points.Attribute("base").Value);
				Insanity_Roll(num, size, Base);
			}
		}
		private void Add_Back_Corruption()
		{
			XElement back = GetBackground();
			foreach (XElement points in back.Element("corruption").Elements())
			{
				int num = int.Parse(points.Attribute("num").Value);
				int size = int.Parse(points.Attribute("size").Value);
				int Base = int.Parse(points.Attribute("base").Value);
				Corruption_Roll(num, size, Base);
				if (points.Attributes().Count() > 3)
				{
					var query = from mal in points.Attributes()
								where mal.Name == "mal"
								select mal.Value;
					if (query.ElementAt(0) == "true")
					{
						Malignancy();
					}
				}
			}
		}
		#endregion

		#region Special Case Functions
		private void FitForThePurpose(XElement career)
		{
			switch (career.Attribute("base").Value)
			{
				case "Adept":
					DHCharacter.Int.Base += 3;
					break;
				case "Assassin":
					DHCharacter.Ag.Base += 3;
					break;
				case "Guardsman":
					DHCharacter.BS.Base += 3;
					break;
				case "Scum":
					DHCharacter.Per.Base += 3;
					break;
				case "Tech-Priest":
					DHCharacter.Wp.Base += 3;
					break;
			}
		}
		private string SanctioningSideEffects()
		{
			int result = Dice.Next(1,101);

			if(result >= 1 && result <= 8)
			{
				int thrones=0;
				for(int i = 0; i < 5; i++)
				{
					thrones += Dice.Next(1,11);
				}
				DHCharacter.Int.Base += 3;
				DHCharacter.Thrones += thrones;
				return "Reconstructed Skull: You have large metal plates in your head, some"+
					" of which are clearly visible. Reduce your Intelligence by 3, but "+
					"gain 5d10 Throne Gelt in compensation";
			}
			else if (result >= 9 && result <= 14)
			{
				DHCharacter.Insanity += Dice.Next(1, 11);
				return "Hunted: You believe certain parts of your psyche, those amputated by"+
					" the sanctioners, have gained sentience and are tracking you down. Gain 1d10 Insanity Points";
			}
			else if (result >= 15 && result <= 25)
			{
				DHCharacter.Insanity += Dice.Next(1, 6);
				return "Unlovely Memories: Such was your sanctioning, that you visibly "+
					"twitch and grimace whenever Holy Terra is mentioned. Gain 1d5 Insanity Points";
			}
			else if (result >= 26 && result <= 35)
			{
				DHCharacter.Insanity += Dice.Next(1, 6);
				return "The Horror, the Horror: Your hair is pure white, you occasionally"+
					" gibber quietly to yourself and you endure terrible nightmares every night.  "+
					"Gain 1d5 Insanity Points.";
			}
			else if (result >= 36 && result <= 42)
			{
				return "Pain through Nerve Induction: The skin on the back of your right hand is"+
					" horribly scarred. You are uncomfortable around bald, robed women.";
			}
			else if (result >= 43 && result <= 49)
			{
				DHCharacter.Gear.Add(new Equipment("Carven Dentures (50 thrones)\n","Career"));
				return "Dental Probes: You no longer have any teeth in your head. You have a "+
					"set of carven dentures, They are of Good quality and worth 50 Thrones.";
			}
			else if (result >= 50 && result <= 57)
			{
				DHCharacter.Gear.Add(new Equipment("Common Cybernetic Eyes\n","Career"));
				return "Optical Rupture: Your sanctioning rituals have done great violence to your eyes."+
					" They have been removed and replaced with Common quality cybernetic senses";
			}
			else if (result >= 58 && result <= 63)
			{
				return "Screaming Devotions: Your ruined vocal cords have been replaced with a "+
					"vox inducer.";
			}
			else if (result >= 64 && result <= 70)
			{
				return "Irradience: You have seen the true power of the Golden Throne. "+
					"You have no hair anywhere upon your body, face or head.";
			}
			else if (result >= 71 && result <= 75)
			{
				return "Tongue Bound: Your lips, gums and soft palate are tattooed with "+
					"hexagrammatic wards. Hard (–20) Willpower Test to speak the names of "+
					"the Ruinous Ones, and you stutter terribly when speaking of daemons.";
			}
			else if (result >= 76 && result <= 88)
			{
				DHCharacter.Talents.Add(new Talent("Chem Geld","Career"));
				DHCharacter.Gear.Add(new Equipment("Chattallium Ring(100 thrones)","Career"));
				return "Throne Wed: You cleave only unto the Emperor. You gain the Chem "+
					"Geld talent, and a chattallium ring, worth 100 Thrones.";
			}
			else if (result >= 89 && result <= 94)
			{
				DHCharacter.T.Base += 3;
				return "Witch Prickling: Your body is covered in thousands of tiny scars. "+
					"You have a thorough dislike of needles. Increase your Toughness by 3.";
			}
 			else
			{
				DHCharacter.Wp.Base += 3;
				return "Hypno-doctrination: Powerful conditioning causes you to chant the "+
					"Litany of Protection in a whispered voice whenever you are asleep or "+
					"unconscious. Increase your Willpower by 3.";
			}
		}
		private void MinorMutation(int result, string Source)
		{
			if (result >= 1 && result <= 20)
			{
				DHCharacter.Traits.Add(new Trait("Grotesque",
					"The mutant is either badly deformed, scarred or bestial Fellowship Tests with"+
					" ‘normals’ are made at –20, but +10 to Intimidate Tests.",Source));
			}
			else if (result >= 21 && result <= 30)
			{
				DHCharacter.Traits.Add(new Trait("Tough Hide",
					"The mutant has 1 AP worth of Natural Armour thanks to dense skin and scar tissue.", Source));
			}
			else if (result >= 31 && result <= 40)
			{
				DHCharacter.Traits.Add(new Trait("Misshapen",
				"The mutant’s spine and/or limbs are horribly twisted, giving it a penalty of -1d10 to"+
				" its Agility.", Source));
				DHCharacter.Ag.Base -= Dice.Next(1, 11);
			}
			else if (result >= 41 && result <= 50)
			{
				DHCharacter.Traits.Add(new Trait("Feels No Pain",
					"The mutant cares little for injury or harm and gains +1 Wound.", Source));
				DHCharacter.Wounds += 1;
			}
			else if (result >= 51 && result <= 60)
			{
				DHCharacter.Traits.Add(new Trait("Brute", 
					"The mutant is physically powerful with deformed masses of muscle. "+
					"Apply +10 Strength, +10 Toughness and –10 Agility.", Source));
				DHCharacter.S.Base += 10;
				DHCharacter.T.Base += 10;
				DHCharacter.Ag.Base -= 10;
			}
			else if (result >= 61 && result <= 70)
			{
				DHCharacter.Traits.Add(new Trait("Nightsider",
					"The mutant gains the Dark Sight trait, but –10 penalty to all Actions in "+
					"bright light or daylight unless its eyes are shielded.", Source));
				DHCharacter.Traits.Add(new Trait("Dark Sight", 
					"See normally even in areas of total darkness and never takes a penalty in "+
					"dim or no lighting.", Source));
			}
			else if (result >= 71 && result <= 80)
			{
				DHCharacter.Traits.Add(new Trait("Big Eyes", 
					"The mutant’s eyes are virtually lidless and watery. Apply +10 Perception "+
					"and –10 Fellowship.", Source));
				DHCharacter.Per.Base += 10;
				DHCharacter.Fel.Base -= 10;
			}
			else if (result >= 81 && result <= 85)
			{
				DHCharacter.Traits.Add(new Trait("Malformed hands", 
					"–10 to WS and BS and the mutant suffers a –20 penalty to all tasks involving"+
					" fine physical manipulation.", Source));
				DHCharacter.WS.Base -= 10;
				DHCharacter.BS.Base -= 10;
			}
			else if (result >= 86 && result <= 89)
			{
				DHCharacter.Traits.Add(new Trait("Tox Blood",
					"The mutant’s system is saturated with toxic pollutants and poisonous chemicals. "+
					"+10 resistance to toxins and poisons, however it suffers a –1d10 penalty to its"+
					" Intelligence and Fellowship.", Source));
				DHCharacter.Int.Base -= Dice.Next(1, 11);
				DHCharacter.Fel.Base -= Dice.Next(1, 11);
			}
			else if (result >= 90 && result <= 99)
			{
				DHCharacter.Traits.Add(new Trait("Wyrdling", 
					"The mutant has Minor Psychic Powers that "+
					"it has so far been able to conceal. The mutant has a Psy Rating of 1.", Source));
				DHCharacter.Talents.Add(new Talent("Psy Rating 1",Source));
			}
			else
			{
				MajorMutation(Dice.Next(1,101),Source);
			}
			CharacterUpdate();
		}
		private void MajorMutation(int result, string Source)
		{
			if (result >= 1 && result <= 25)
			{
				DHCharacter.Traits.Add(new Trait("Vile Deformity",
					"The mutant is marked by some terrible deformity that shows the touch of the"+
					" warp and should not exist in a rational universe. The mutant gains the Disturbing "+
					"trait.", Source));
				DHCharacter.Traits.Add(new Trait("Disturbing", "Fear Rating 1.", Source));

			}
			else if (result >= 26 && result <= 35)
			{
				DHCharacter.Traits.Add(new Trait("Aberration",
					"The mutant has become a weird hybrid of man and animal (or reptile, insect, etc.) "+
					"Apply +10 Strength, +10 Agility, –1d10 Intelligence, -10 Fellowship and the Sprint "+
					"talent.", Source));
				DHCharacter.S.Base += 10;
				DHCharacter.Ag.Base += 10;
				DHCharacter.Int.Base -= Dice.Next(1,11);
				DHCharacter.Talents.Add(new Talent("Sprint", Source));
			}
			else if (result >= 36 && result <= 40)
			{
				DHCharacter.Traits.Add(new Trait("Degenerate Mind", 
					"The mutant’s mind is warped and inhuman. Apply –1d10 Intelligence +10 Fellowship, "+
					"roll 1d10 and apply the following Talents or Trait: 1–3: Frenzy, 4–7: Fearless, 8–0: "+
					"From Beyond.", Source));
				DHCharacter.Fel.Base += 10;
				DHCharacter.Int.Base -= Dice.Next(1,11);
				switch(Dice.Next(1,11))
				{
					case 1: case 2: case 3:
						DHCharacter.Talents.Add(new Talent("Frenzy", Source));
						break;
					case 4: case 5: case 6: case 7:
						DHCharacter.Talents.Add(new Talent("Fearless", Source));
						break;
					case 8: case 9: case 10:
						DHCharacter.Traits.Add(new Trait("From Beyond", 
							"Immune to Fear, Pinning, Insanity Points and Psychic Powers used to cloud, "+
							"control or delude its mind", Source));
						break;
				}
			}
			else if (result >= 41 && result <= 50)
			{
				DHCharacter.Traits.Add(new Trait("Ravaged Body", 
					"The mutant’s body has been entirely re-made by the warp. Gain 1d5 Minor Mutations,"+
					" re-rolling duplicate rolls. Such mutations, regardless of their nature, still show "+
					"the obvious taint of Chaos", Source));
				List<int> rolls = new List<int>();
				for(int i = 0; i < Dice.Next(1,6); i++)
				{
					while(true)
					{
						int roll = Dice.Next(1,101);
						if(!rolls.Contains(roll))
						{
							rolls.Add(roll);
							break;
						}
					}
				}
				foreach(int roll in rolls)
				{
					MinorMutation(roll,Source);
				}
			}
			else if (result >= 51 && result <= 60)
			{
				DHCharacter.Traits.Add(new Trait("Clawed/Fanged", 
					"The mutant gains razor claws, a fanged maw, barbed flesh or "+
					"some other form of Natural Weapon that inflicts 1d10 R or I Primitive damage "+
					"in close combat.", Source));
			}
			else if (result >= 61 && result <= 65)
			{
				DHCharacter.Traits.Add(new Trait("Necrophage", 
					"The mutant gains +10 Toughness and the Regeneration trait, "+
					"but must sustain itself on copious quantities of raw meat or starve.", Source));
				DHCharacter.T.Base += 10;
				DHCharacter.Traits.Add(new Trait("Regeneration", 
					"Each Round, at the start of its Turn, the creature Tests"+
					" Toughness to remove 1 point of Damage.", Source));
			}
			else if (result >= 66 && result <= 70)
			{
				DHCharacter.Traits.Add(new Trait("Corrupted Flesh", 
					"Beneath the mutants’ skin a blasphemous transformation has"+
					" taken place, exchanging living organs for writhing creatures and blood for "+
					"ichorous, maggot-ridden filth. If the mutant suffers Critical Damage, those "+
					"witnessing it must take a Fear Test at –10.", Source));
			}
			else if (result >= 71 && result <= 75)
			{
				DHCharacter.Traits.Add(new Trait("Vile Alacrity", "The mutant is constantly juddering and shaking unnaturally and "+
					"can move almost faster than sight. It gains the Unnatural Agility (x2) trait and "+
					"the Sprint talent, with a penalty of –10 to Weapon Skill and Ballistic Skill.", Source));

				DHCharacter.Traits.Add(new Trait("Unnatural Agility(x2)", "Agility Bonus x2", Source));
				DHCharacter.Talents.Add(new Talent("Sprint", Source));
				DHCharacter.WS.Base -= 10;
				DHCharacter.BS.Base -= 10;
			}
			else if (result >= 76 && result <= 80)
			{
				DHCharacter.Traits.Add(new Trait("Hideous Strength",
					"The mutant gains the Unnatural Strength (x2) trait.", Source));
				DHCharacter.Traits.Add(new Trait("Unnatural Strength(x2)", "Strength Bonus x2", Source));
			}
			else if (result >= 81 && result <= 85)
			{
				DHCharacter.Traits.Add(new Trait("Multiple Appendages", 
					"The mutant has sprouted additional functioning limbs in "+
					"the shape of arms, tentacles or a prehensile tail (or tails). Gain "+
					"Ambidextrous and Two-Weapon Wielder and +10 on Climb and Grapple.", Source));
				DHCharacter.Talents.Add(new Talent("Ambidextrous",Source));
				DHCharacter.Talents.Add(new Talent("Two-Weapon Wielder(Melee)",Source));
				DHCharacter.Talents.Add(new Talent("Two-Weapon Wielder(Ballistic)",Source));
			}
			else if (result >= 86 && result <= 90)
			{
				DHCharacter.Traits.Add(new Trait("Worm", 
					"The mutant’s lower limbs have fused together to form a "+
					"worm or snake-like tail. They gain the Crawler trait, +5 Wounds and the "+
					"Disturbing trait.", Source));
				DHCharacter.Traits.Add(new Trait("Crawler", 
					"The move for a creature with this Trait is half its Agility"+
					" Bonus, but it does not take penalties for moving over Difficult Terrain.", Source));
				DHCharacter.Wounds += 5;
				DHCharacter.Traits.Add(new Trait("Disturbing", "Fear Rating 1.", Source));
			}
			else if (result >= 91 && result <= 92)
			{
				DHCharacter.Traits.Add(new Trait("Nightmarish", 
					"So warped and horrific is the mutant’s appearance, it can "+
					"cause enemies to flee in fear. It gains the Frightening trait.", Source));
				DHCharacter.Traits.Add(new Trait("Frightening", "Fear Rating 2.", Source));
			}
			else if (result >= 93 && result <= 94)
			{
				DHCharacter.Traits.Add(new Trait("Malleable", 
					"The mutant possesses a sickeningly liquid flexibility and is"+
					" able to distend and flatten its body. Apply +10 Agility and +20 to Climb Tests, "+
					"and Grappling attacks. They may also fit through spaces only one-quarter its usual"+
					" body dimensions.", Source));
				DHCharacter.Ag.Base += 10;
			}
			else if (result >= 95 && result <= 96)
			{
				DHCharacter.Traits.Add(new Trait("Winged", 
					"The mutant’s body has warped to accommodate a pair of leathery "+
					"wings or the like. They gain the Flyer trait (1d10+5)", Source));
				int amount=Dice.Next(1,11)+5;
				DHCharacter.Traits.Add(new Trait("Flyer("+amount.ToString()+")", 
					"Can Fly "+amount.ToString()+" meters", Source));
			}
			else if (result >= 97 && result <= 98)
			{
				DHCharacter.Traits.Add(new Trait("Corpulent", "The mutant’s huge and bloated frame gives them +5 Wounds and "+
					"the Unnatural Toughness (x2) trait. The mutant can't run.", Source));
				DHCharacter.Traits.Add(new Trait("Unnatural Toughness(x2)", "Toughness Bonus x2", Source));
				DHCharacter.Wounds += 5;
			}
			else if (result == 99)
			{
				DHCharacter.Traits.Add(new Trait("Corrosive Bile", 
					"The mutant may vomit burning bile, flesh-eating grubs or some "+
					"other horrific substance instead of attacking normally in close combat. The "+
					"attack uses the mutant’s BS, is a Full Action and can be Dodged but not Parried. "+
					"This attack inflicts 1d10+5 R (or E) Tearing Damage", Source));
			}
			else
			{
				DHCharacter.Traits.Add(new Trait("Hellspawn", 
					"Saturated with the energies of the warp, the mutant is imbued"+
					" with Daemonic energies and gains the From Beyond, Frightening and Daemonic "+
					"traits and a Psy Rating of 2.", Source));

				DHCharacter.Traits.Add(new Trait("From Beyond", 
					"Immune to Fear, Pinning, Insanity Points and " +
					"Psychic Powers used to cloud, control or delude its mind", Source));
				
				DHCharacter.Traits.Add(new Trait("Frightening", "Fear Rating 2", Source));

				DHCharacter.Traits.Add(new Trait("Daemonic", 
					"Creatures with this Trait double their Toughness Bonus "+
					"against all Damage, except for Damage inflicted by force weapons, Psychic "+
					"Powers, holy attacks or other creatures with this Trait. Daemonic creatures "+
					"are also immune to poison and disease.", Source));
				DHCharacter.Talents.Add(new Talent("Psy Rating 2", Source));
			}
			CharacterUpdate();
		}
		private void Malignancy()
		{
			int result = Dice.Next(1,101);
			
			if(result >= 1 && result <= 10)
			{
				DHCharacter.Traits.Add(new Trait("Palsy",
					"The character suffers from numerous minor tics, shakes and tremors with no"+
					" medical cause. Reduce his Agility by 1d10.",""));
				DHCharacter.Ag.Base -= Dice.Next(1, 11);
			}
			else if(result >= 11 && result <= 15)
			{
				DHCharacter.Traits.Add(new Trait("Dark-hearted",
					"The character grows increasingly cruel, callous and vindictive. Reduce his "+
					"Fellowship by 1d10.",""));
				DHCharacter.Int.Base -= Dice.Next(1, 11);
			}
			else if(result >= 16 && result <= 20)
			{
				DHCharacter.Traits.Add(new Trait("Ill-fortuned",
					"Whenever the character uses a Fate Point roll a d10. On a score of 7, 8, 9 or 10"+
					" it has no effect but it is lost anyway.",""));
			}
			else if(result >= 21 && result <= 22)
			{
				DHCharacter.Traits.Add(new Trait("Skin Afflictions",
					"The character is plagued by boils, scabs, weeping sores and the like. He takes "+
					"a –20 penalty to all Charm Tests.",""));
			}
			else if(result >= 23 && result <= 25)
			{
				DHCharacter.Traits.Add(new Trait("Night Eyes",
					"Light pains the character, and unless he shields his eyes, he suffers a –10 "+
					"penalty on all Tests when in an area of bright light.",""));
			}
			else if(result >= 26 && result <= 30)
			{
				DHCharacter.Traits.Add(new Trait("Morbid",
					"The character finds it hard to concentrate as his mind is filled with macabre"+
					" visions and tortured, gloom-filled trains of thought. The character’s "+
					"Intelligence is reduced by 1d10.",""));
				DHCharacter.Int.Base -= Dice.Next(1, 11);
			}
			else if(result >= 31 && result <= 33)
			{
				DHCharacter.Traits.Add(new Trait("Witch-mark",
					"The character develops some minor physical deformity or easily concealable"+
					" mutation. It is small, but perhaps enough to consign him to the stake if found"+
					" out by a fanatical witch hunter. He must hide it well!",""));
				
			}
			else if(result >= 34 && result <= 45)
			{
				DHCharacter.Traits.Add(new Trait("Fell Obsession",
					"This is the same as the Obsession Disorder. However, in this case the character is"+
					" obsessed by a sinister or malign focus, such as collecting finger-bone trophies, "+
					"ritual scarification, carrying out meaningless vivisections, etc.",""));
			}
			else if(result >= 46 && result <= 50)
			{
				DHCharacter.Traits.Add(new Trait("Hatred",
					"The character develops an implacable hatred of a single group, individual or "+
					"social class. The character will never side with or aid them without explicit "+
					"orders or other vital cause, and even then grudgingly.",""));
			}
			else if(result >= 51 && result <= 55)
			{
				DHCharacter.Traits.Add(new Trait("Irrational Nausea",
					"The character feels sick at the sight or sound of some otherwise innocuous thing"+
					" such as prayer books and holy items, bare flesh, human laughter, fresh food, "+
					"shellfish, etc. When he encounters the object of his revulsion, he must Test "+
					"Toughness or suffer a –10 penalty to all Tests as long as he remains in its presence.",""));
			}
			else if(result >= 56 && result <= 60)
			{
				DHCharacter.Traits.Add(new Trait("Wasted Frame",
					"The character’s pallor becomes corpse-like and his muscles waste away. "+
					"The character’s Strength is reduced by 1d10.",""));
				DHCharacter.S.Base -= Dice.Next(1, 11);
			}
			else if(result >= 61 && result <= 63)
			{
				DHCharacter.Traits.Add(new Trait("Night Terrors",
					"The character is plagued by Daemonic visions in his sleep. See Horrific Nightmares.",""));
			}
			else if(result >= 64 && result <= 70)
			{
				DHCharacter.Traits.Add(new Trait("Poor Health",
					"The character constantly suffers petty illnesses and phantom pains, and his"+
					" wounds never seem to heal fully. The character’s Toughness is reduced by 1d10.",""));
				DHCharacter.T.Base -= Dice.Next(1, 11);
			}
			else if(result >= 71 && result <= 75)
			{
				DHCharacter.Traits.Add(new Trait("Distrustful",
					"The character cannot conceal the distrust and antipathy he has for others."+
					" He must take a –10 penalty to Fellowship Tests when dealing with strangers.",""));
			}
			else if(result >= 76 && result <= 80)
			{
				DHCharacter.Traits.Add(new Trait("Malign Sight", 
					"The world seems to darken, tarnish and rot if the character looks too long at"+
					" anything. The character’s Perception is reduced by 1d10.",""));
				DHCharacter.Per.Base -= Dice.Next(1, 11);
			}
			else if(result >= 81 && result <= 83)
			{
				DHCharacter.Traits.Add(new Trait("Ashen Taste",
					"Food and drink hold disgusting tastes and little sustenance for the character,"+
					" and he can barely stomach eating. The character doubles the negative effects "+
					"for levels of Fatigue.",""));
			}
			else if(result >= 84 && result <= 90)
			{
				DHCharacter.Traits.Add(new Trait("Bloodlust",
					"Murderous rage is never far from the character’s mind. After being wounded in"+
					" combat, he must Test Willpower to incapacitate or allow his enemies to flee,"+
					" rather than kill them outright, even if his intent is otherwise.",""));
			}
			else if(result >= 91 && result <= 93)
			{
				DHCharacter.Traits.Add(new Trait("Blackouts", "The character suffers from inexplicable"+
					" blackouts. When they occur and what happens during them is up to the GM.",""));
			}
			else if(result >= 94 && result <= 100)
			{
				DHCharacter.Traits.Add(new Trait("Strange Addiction", 
					"The character is addicted to some bizarre and unnatural substance, such as "+
					"eating rose petals, drinking blood, the taste of widows’ tears etc. This acts"+
					" like a Minor Compulsion, but is freakish enough to cause serious suspicion if "+
					"found out.",""));
				
			}
		}
		private void MysteriousLineage()
		{
			int result=0;
			switch (Dice.Next(1,11))
			{
				case 1: case 2:
					DHCharacter.Traits.Last().Effect=
						"You are extremely intelligent, though you are prone to peculiar headaches"+
						" and delusions. Increase your intelligence and insanity points by 1d10 "+
						"(roll only once and apply the result to both).";
					result = Dice.Next(1,11);
					DHCharacter.Int.Base += result;
					DHCharacter.Insanity += result;
					break;
				case 3: case 4:
					DHCharacter.Traits.Last().Effect=
						"The constant manipulation of your family's ancestry has rendered you a "+
						"mule, much to the disappointment of the Ordo Famulous. You gain the Chem "+
						"Geld talent.";
					DHCharacter.Talents.Add(new Talent("Chem Geld","origin"));
					break;
				case 5: case 6:
					DHCharacter.Traits.Last().Effect=
						"You are a naturally gifted orator and dissembler but are unaccustomed to"+
						" not being the centre of attention. You gain the Talented (Deceive) Talent"+
						" and a -10 to all Concealment and Move Silent tests.";
					DHCharacter.Talents.Add(new Talent("Talented(Deceive)","origin"));
					break;
				case 7: case 8:
					DHCharacter.Traits.Last().Effect=
						"Your health has been affected by an ancient genetic defect, but the "+
						"Sister Famulous nevertheless prepared you for your destiny. Lose 1 wound "+
						"and -5 Toughness but begin play with Forbidden Lore (Daemonology, Heresy, "+
						"Inquisition or Psykers).";
					DHCharacter.Wounds -= 1;
					DHCharacter.T.Base -= 5;
					List<string> list = new List<string>();
					list.Add("Forbidden Lore(Daemonology)(Int)");
					list.Add("Forbidden Lore(Heresy)(Int)");
					list.Add("Forbidden Lore(Inquisition)(Int)");
					list.Add("Forbidden Lore(Pyskers)(Int)");
					DialogBoxList SkillDialog = new DialogBoxList(list);
					DialogResult TalentResult = SkillDialog.ShowDialog();
					DHCharacter.Skills.Add(new Skill(SkillDialog.selection,"origin"));
					break;
				case 9: case 10:
					DHCharacter.Traits.Last().Effect=
						"You are astonishingly comely externally but at the cost of hidden imperfections."+
						"Increase your Fellowship and Corruption points by 1d10 (roll only once and apply"+
						" the result to both).";
					result = Dice.Next(1,11);
					DHCharacter.Fel.Base += result;
					DHCharacter.Corruption += result;
					break;
			}



		}
		#endregion

		private void CharacterUpdate()
		{
			

			#region Base Boxes
			WS_Base_Box.Text = DHCharacter.WS.Base.ToString();
			BS_Base_Box.Text = DHCharacter.BS.Base.ToString();
			S_Base_Box.Text = DHCharacter.S.Base.ToString();
			T_Base_Box.Text = DHCharacter.T.Base.ToString();
			Ag_Base_Box.Text = DHCharacter.Ag.Base.ToString();
			Int_Base_Box.Text = DHCharacter.Int.Base.ToString();
			Per_Base_Box.Text = DHCharacter.Per.Base.ToString();
			Wp_Base_Box.Text = DHCharacter.Wp.Base.ToString();
			Fel_Base_Box.Text = DHCharacter.Fel.Base.ToString();
			#endregion
			#region Roll Boxes
			WS_Roll_Box.Text = DHCharacter.WS.Roll.ToString();
			BS_Roll_Box.Text = DHCharacter.BS.Roll.ToString();
			S_Roll_Box.Text = DHCharacter.S.Roll.ToString();
			T_Roll_Box.Text = DHCharacter.T.Roll.ToString();
			Ag_Roll_Box.Text = DHCharacter.Ag.Roll.ToString();
			Int_Roll_Box.Text = DHCharacter.Int.Roll.ToString();
			Per_Roll_Box.Text = DHCharacter.Per.Roll.ToString();
			Wp_Roll_Box.Text = DHCharacter.Wp.Roll.ToString();
			Fel_Roll_Box.Text = DHCharacter.Fel.Roll.ToString();
			#endregion
			#region Total Boxes
			WS_Box.Text = DHCharacter.WS.Value.ToString();
			BS_Box.Text = DHCharacter.BS.Value.ToString();
			S_Box.Text = DHCharacter.S.Value.ToString();
			T_Box.Text = DHCharacter.T.Value.ToString();
			Ag_Box.Text = DHCharacter.Ag.Value.ToString();
			Int_Box.Text = DHCharacter.Int.Value.ToString();
			Per_Box.Text = DHCharacter.Per.Value.ToString();
			Wp_Box.Text = DHCharacter.Wp.Value.ToString();
			Fel_Box.Text = DHCharacter.Fel.Value.ToString();
			#endregion

			Wounds_Box.Text = DHCharacter.Wounds.ToString();
			Fate_Box.Text = DHCharacter.Fate.ToString();
			Insanity_Box.Text = DHCharacter.Insanity.ToString();
			Corruption_Box.Text = DHCharacter.Corruption.ToString();

			#region Tabs
			Traits_Box.Clear();
			foreach (Trait t in DHCharacter.Traits)
			{
				if (!Traits_Box.Text.Contains(t.Name))
				{
					Traits_Box.AppendText(t.Name + "\n");
					Traits_Box.AppendText(t.Effect + "\n\n");
					Traits_Box.AppendText("\n");
				}
			}
			Talents_Box.Clear();
			foreach (Talent t in DHCharacter.Talents)
			{
				if (!Talents_Box.Text.Contains(t.Name))
				{
					Talents_Box.AppendText(t.Name + "\n");
				}
			}
			Skills_Box.Clear();
			foreach (Skill s in DHCharacter.Skills)
			{
				if (!Skills_Box.Text.Contains(s.Name))
				{
					Skills_Box.AppendText(s.Name + "\n");
				}
			}
			Gear_Box.Clear();
			foreach (Equipment e in DHCharacter.Gear)
			{
				Gear_Box.AppendText(e.Name + "\n");
			}
			#endregion

			Money_Box.Text = DHCharacter.Thrones.ToString();
			Income_Box.Text = DHCharacter.Income;

			#region Appearance
			Name_Box.Text = DHCharacter.Name;
			Sex_Box.Text = DHCharacter.Sex;
			Build_Box.Text = DHCharacter.Build;
			Age_Box.Text = DHCharacter.Age;
			Skin_Box.Text = DHCharacter.Skin;
			Hair_Box.Text = DHCharacter.Hair;
			Eye_Box.Text = DHCharacter.Eye;
			Quirk_Box.Text = DHCharacter.Quirk;
			#endregion

			Bio_Box.Text = DHCharacter.Bio;

		}

		private void Reset()
		{
			DHCharacter.Reset();

			Divination_Box.Clear();
			DivDel = delegate() { };

			CharacterUpdate();

			Roll_button.Enabled = false;
			Reroll_Disable();
		}

		private void Eye_Box_TextChanged(object sender, EventArgs e)
		{

		}	
	}
}
