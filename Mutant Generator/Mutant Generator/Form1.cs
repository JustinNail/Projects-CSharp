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


namespace Dark_Heresy_Generator
{
	public partial class Form1 : Form
	{
		#region Initializations
		Random Dice = new Random();
		DHCharacter Character = new DHCharacter();
		XDocument Origins;
		XDocument Careers;
		XDocument Names;
		private delegate void DivinationDel();
		DivinationDel DivDel = delegate() { };
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
					where (string)(origin.Attribute("name").Value) == Character.Origin.Name
					select origin;

			return query.ElementAt(0);
		}
		private XElement GetCareer()
		{
			//query variable
			IEnumerable<XElement> query;

			query = from career in Careers.Element("root").Elements()
					where (((career.Attribute("name").Value) == Character.Career.Name) &&
							((career.Attribute("base").Value) == Character.Career.Base))
					select career;
			return query.ElementAt(0);
			
		}
		private XElement GetBackground()
		{
			IEnumerable<XElement> query;
			XElement career = GetCareer();

			query = from back in career.Element("backgrounds").Elements()
					where (string)(back.Attribute("name").Value) == Character.Background.Name
					select back;

			return query.ElementAt(0);
		}

		private void Fill_Base_Stats()
		{
			XElement origin = GetOrigin();
			Character.WS.OriginBase = int.Parse(origin.Element("basestats").Element("WS").Value);
			Character.BS.OriginBase = int.Parse(origin.Element("basestats").Element("BS").Value);
			Character.S.OriginBase = int.Parse(origin.Element("basestats").Element("S").Value);
			Character.T.OriginBase = int.Parse(origin.Element("basestats").Element("T").Value);
			Character.Ag.OriginBase = int.Parse(origin.Element("basestats").Element("Ag").Value);
			Character.Int.OriginBase = int.Parse(origin.Element("basestats").Element("Int").Value);
			Character.Per.OriginBase = int.Parse(origin.Element("basestats").Element("Per").Value);
			Character.Wp.OriginBase = int.Parse(origin.Element("basestats").Element("Wp").Value);
			Character.Fel.OriginBase = int.Parse(origin.Element("basestats").Element("Fel").Value);
		}

		#region Combo Boxes
		private void Fill_Name_comboBox()
		{
			Name_comboBox.Items.Clear();
			XElement origin = GetOrigin();
			var names = from name in Names.Element("root").Elements()
						where (string)(name.Attribute("type").Value) ==
								(string)(Character.Sex)
						select name;
			if (origin.Attribute("name").Value == "Hive Mutant")
			{
				names = from name in names.Elements()
						select name;
			}
			else
			{
				names = from name in names.Elements()
						where name.Attribute("type").Value != "Mutant"
						select name;
			}
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
								if (!names.Contains<string>(origin.Attribute("base").Value))
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
			Character.Origin.Name = Origin_comboBox.SelectedItem.ToString();
			IEnumerable<string> query = from origin in Origins.Element("root").Elements()
										where origin.Attribute("name").Value == Character.Origin.Name
										select origin.Attribute("base").Value;
			Character.Origin.Base = query.First();

			Origin_Selection();

			Appearance_Button.Enabled = true;
		}
		private void Origin_Selection()
		{
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
			CareerReset();
			string[] attributes = Career_comboBox.SelectedItem.ToString().Split('(',')');
			Character.Career.Name = attributes.First();
			Character.Career.Base = attributes.Last(s=> s != "");
			Career_Selection();
		}
		private void Career_Selection()
		{
			Add_Career_Skills();
			Add_Career_Talents();
			Add_Career_Traits();
			Add_Career_Gear();

			Add_Wealth();

			if ((GetCareer().Attribute("base").Value == "Adepta Sororitas" &&
				Character.Sex != "Female") ||
				(GetCareer().Attribute("name").Value == "Sister Diagolous" &&
				Character.Sex != "Female") ||
				(GetCareer().Attribute("name").Value == "Sister Famulous" &&
				Character.Sex != "Female") ||
				(GetCareer().Attribute("name").Value == "Sister Hospitaller" &&
				Character.Sex != "Female"))
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
				Character.Sex = "Female";
				Add_Appearance();
			}
			if (GetCareer().Attribute("name").Value == "Fraternis Militia" &&
				Character.Sex != "Male")
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
				Character.Sex = "Male";
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
			Character.NameType = Name_comboBox.SelectedItem.ToString();
			Name_Selection();
		}
		private void Name_Selection()
		{
			if (Character.NameType == "Mutant")
			{
				Character.Name = MutantName();
			}
			else
			{
				IEnumerable<XElement> query;

				query = from names in Names.Element("root").Elements()
						where (string)(names.Attribute("type").Value) ==
									(string)(Character.Sex)
						select names;

				query = from name in query.Elements()
						where (string)(name.Attribute("type").Value) ==
									(string)(Character.NameType)
						select name;

				int Size = query.Elements().Count();
				Character.Name = query.Elements().ElementAt(Dice.Next(Size)).Value;
			}
			CharacterUpdate();
		}
		private void Background_comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			BackReset();
			string[] attributes = Background_comboBox.SelectedItem.ToString().Split(':');
			Character.Background.Name = attributes.First();
			string Cost = attributes.Last(s => s != "");
			Character.Background.Cost = int.Parse(Cost.Substring(0, 4));
			Background_Selection();
		}
		private void Background_Selection()
		{
			Character.XP_Spent = Character.Background.Cost;
			Add_Back_Stats();
			Add_Back_Skills();
			Add_Back_Talents();
			Add_Back_Traits();
			Add_Back_Gear();
			Add_Back_Wealth();
			Add_Back_Fate();
			Add_Back_Insanity();
			Add_Back_Corruption();
			if (Character.Background.Name == "Cold Trader")
			{
				ColdTraderChoice();
			}
			if (Character.Background.Name == "Hive Gang Member")
			{
				HiveGangChoice();
			}
			CharacterUpdate();
		}
		#endregion

		#region Rolls
		private void WS_Roll()
		{
			Character.WS.Roll=Dice.Next(1, 11) + Dice.Next(1, 11);
		}
		private void BS_Roll()
		{
			Character.BS.Roll = Dice.Next(1, 11) + Dice.Next(1, 11);
		}
		private void S_Roll()
		{
			Character.S.Roll = Dice.Next(1, 11) + Dice.Next(1, 11);
		}
		private void T_Roll()
		{
			Character.T.Roll = Dice.Next(1, 11) + Dice.Next(1, 11);
		}
		private void Ag_Roll()
		{
			Character.Ag.Roll = Dice.Next(1, 11) + Dice.Next(1, 11);
		}
		private void Int_Roll()
		{
			Character.Int.Roll = Dice.Next(1, 11) + Dice.Next(1, 11);
		}
		private void Per_Roll()
		{
			Character.Per.Roll = Dice.Next(1, 11) + Dice.Next(1, 11);
		}
		private void Wp_Roll()
		{
			Character.Wp.Roll = Dice.Next(1, 11) + Dice.Next(1, 11);
		}
		private void Fel_Roll()
		{
			Character.Fel.Roll = Dice.Next(1, 11) + Dice.Next(1, 11);
		}

		private void Wound_Roll()
		{
			XElement origin = GetOrigin();
			Character.Wounds.Roll = Dice.Next(1, 6);
			Character.Wounds.OriginBase= int.Parse(origin.Element("basestats").Element("Wounds").Value);
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
					Character.Fate.OriginBase = int.Parse(option.Attribute("amount").Value);
				}
			}
		}

		private void Insanity_Roll(int num, int size, int Base, Source source)
		{
			int roll=0;
			for (int i = 0; i < num; i++)
			{
				roll += Dice.Next(1, size + 1);
			}
			roll+=Base;
			switch (source)
			{
				case Source.Origin:
					Character.Insanity.OriginBase += roll;
					break;
				case Source.Career:
					Character.Insanity.CareerMod += roll;
					break;
				case Source.Background:
					Character.Insanity.BackMod += roll;
					break;
			}
		}
		private void Corruption_Roll(int num, int size, int Base, Source source)
		{
			int roll = 0;
			for (int i = 0; i < num; i++)
			{
				roll += Dice.Next(1, size + 1);
			}
			roll += Base;
			switch (source)
			{
				case Source.Origin:
					Character.Corruption.OriginBase += roll;
					break;
				case Source.Career:
					Character.Corruption.CareerMod += roll;
					break;
				case Source.Background:
					Character.Corruption.BackMod += roll;
					break;
			}
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
						MinorMutation(result,Source.Divination);
					};
					break;
				case 2: case 3:
					Divination_Box.Text =
					"\"Only the insane have strength enough to prosper." +
					" Only those who prosper may judge what is sane.\"" +
					"\nBegin play with 2 Insanity Points.";
					DivDel = delegate()
					{
						Character.Insanity.DivMod += 2;
					};
					break;
				case 4: case 5: case 6: case 7:
					Divination_Box.Text=
					"\"Sins hidden in the heart turn all to decay.\""+
					"\nBegin play with 3 Corruption Points.";
					DivDel = delegate()
					{
						Character.Corruption.DivMod += 3;
					};
					break;
				case 8:
					Divination_Box.Text=
					"\"Innocence is an illusion.\""+
					"\nBegin play with 1 Insanity Point and 1 Corruption Point.";
					DivDel = delegate()
					{
						Character.Insanity.DivMod += 1;
						Character.Corruption.DivMod += 1;
					};
					break;
				case 9: case 10: case 11:
					Divination_Box.Text=
					"\"Dark dreams lie upon the heart.\""+
					"\nBegin play with 2 Corruption Points.";
					DivDel = delegate()
					{
						Character.Corruption.DivMod += 2;
					};
					break;
				case 12: case 13: case 14: case 15:
					Divination_Box.Text=
					"\"The pain of the bullet is ecstasy compared to damnation.\""+
					"\nIncrease Toughness by +1.";
					DivDel = delegate()
					{
						Character.T.DivMod += 1;
					};
					break;
				case 16: case 17: case 18:
					Divination_Box.Text =
					"\"Kill the alien before it can speak its lies.\"" +
					"\nIncrease Agility by +2.";
					DivDel = delegate()
					{
						Character.Ag.DivMod += 2;
					};
					break;
				case 19: case 20: case 21:
					Divination_Box.Text =
					"\"Truth is subjective.\"" +
					"\nIncrease Intelligence by +3. Begin play with 3 Corruption Points.";
					DivDel = delegate()
					{
						Character.Int.DivMod += 3;
						Character.Corruption.DivMod += 3;
					};
					break;
				case 22: case 23: case 24: case 25: case 26:
					Divination_Box.Text =
					"\"Know the mutant; kill the mutant.\"" +
					"\nIncrease Perception by +2.";
					DivDel = delegate()
					{
						Character.Per.DivMod += 2;
					};
					break;
				case 27: case 28: case 29: case 30:
					Divination_Box.Text=
					"\"Even a man who has nothing can still offer his life.\""+
					"\nIncrease Strength by +2.";
					DivDel = delegate()
					{
						Character.S.DivMod += 2;
					};
					break;
				case 31: case 32: case 33:
					Divination_Box.Text=
					"\"If a job is worth doing it is worth dying for.\""+
					"\nGain the Frenzy talent.";
					DivDel = delegate()
					{
						Character.Talents.Add(new Talent("Frenzy",Source.Divination));
					};
					break;
				case 34: case 35: case 36: case 37: case 38:
					Divination_Box.Text=
					"\"Only in death does duty end.\""+
					"\nGain 1 Wound.";
					DivDel = delegate()
					{
						Character.Wounds.DivMod += 1;
					};
					break;
				case 39: case 40: case 41: case 42:
					Divination_Box.Text=
					"\"A mind without purpose will wander in dark places.\""+
					"\nGain 1 Fate Point.";
					DivDel = delegate()
					{
						Character.Fate.DivMod += 1;
					};
					break;
				case 43: case 44: case 45: case 46:
					Divination_Box.Text=
					"\"There are no civilians in the battle for survival.\""+
					"\nIncrease Toughness by +2 and gain 1 Wound.";
					DivDel = delegate()
					{
						Character.T.DivMod += 2;
						Character.Wounds.DivMod += 1;
					};
					break;
				case 47: case 48: case 49: case 50:
					Divination_Box.Text=
					"\"Violence solves everything.\""+
					"\nIncrease Weapon Skill by +3.";
					DivDel = delegate()
					{
						Character.WS.DivMod += 3;
					};
					break;
				case 51: case 52: case 53: case 54:
					Divination_Box.Text =
					"\"To war is human.\"" +
					"\nIncrease Agility by +3.";
					DivDel = delegate()
					{
						Character.Ag.DivMod += 3;
					};
					break;
				case 55: case 56: case 57: case 58:
					Divination_Box.Text=
					"\"Die if you must, but not with your spirit broken.\""+
					"\nIncrease Willpower by +3.";
					DivDel = delegate()
					{
						Character.Wp.DivMod += 3;
					};
					break;
				case 59: case 60: case 61: case 62:
					Divination_Box.Text=
					"\"The gun is mightier than the sword.\""+
					"\nIncrease Ballistic Skill by +3.";
					DivDel = delegate()
					{
						Character.BS.DivMod += 3;
					};
					break;
				case 63: case 64: case 65: case 66:
					Divination_Box.Text=
					"\"Be a boon to your brothers and bane to your enemies.\""+
					"\nIncrease Fellowship by +3.";
					DivDel = delegate()
					{
						Character.Fel.DivMod += 3;
					};
					break;
				case 67: case 68: case 69: case 70:
					Divination_Box.Text=
					"\"Men must die so that Man endures.\""+
					"\nIncrease Toughness by +3.";
					DivDel = delegate()
					{
						Character.T.DivMod += 3;
					};
					break;
				case 71: case 72: case 73: case 74:
					Divination_Box.Text=
					"\"In the darkness, follow the light of Terra.\""+
					"\nIncrease Willpower by +3.";
					DivDel = delegate()
					{
						Character.Wp.DivMod += 3;
					};
					break;
				case 75: case 76: case 77: case 78: case 79:
					Divination_Box.Text=
					"\"The only true fear is of dying with your duty not done.\""+
					"\nGain 2 Wounds.";
					DivDel = delegate()
					{
						Character.Wounds.DivMod += 2;
					};
					break;
				case 80: case 81: case 82: case 83: case 84: case 85:
					Divination_Box.Text=
					"\"Thought begets Heresy; Heresy begets Retribution.\""+
					"\nIncrease Strength by +3.";
					DivDel = delegate()
					{
						Character.S.DivMod += 3;
					};
					break;
				case 86: case 87: case 88: case 89: case 90:
					Divination_Box.Text=
					"\"The wise man learns from the deaths of others.\""+
					"\nIncrease Intelligence by +3.";
					DivDel = delegate()
					{
						Character.Int.DivMod += 3;
					};
					break;
				case 91: case 92: case 93: case 94:
					Divination_Box.Text=
					"\"The suspicious mind is a healthy mind.\""+
					"\nIncrease Perception by +3.";
					DivDel = delegate()
					{
						Character.Per.DivMod += 3;
					};
					break;
				case 95: case 96: case 97:
					Divination_Box.Text=
					"\"Trust in your fear.\""+
					"\nIncrease Agility by +2 and gain 1 Fate Point.";
					DivDel = delegate()
					{
						Character.Ag.DivMod += 2;
						Character.Fate.DivMod += 1;
					};
					break;
				case 98: case 99:
					Divination_Box.Text=
					"\"There is no substitute for zeal.\""+
					"\nIncrease Toughness and Willpower each by +2.";
					DivDel = delegate()
					{
						Character.T.DivMod += 2;
						Character.Wp.DivMod += 2;
					};
					break;
				case 100:
					Divination_Box.Text=
					"\"Do not ask why you serve. Only ask how.\""+
					"\nIncrease Weapon Skill and Ballistic Skill each by +2.";
					DivDel = delegate()
					{
						Character.WS.DivMod += 2;
						Character.BS.DivMod += 2;
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
					sw.WriteLine("Name: {0}", Character.Name);
					sw.WriteLine("Origin: {0}  Career: {1}",
						Character.Origin.Name, Character.Career.Name);
					sw.WriteLine("Background: {0} {1}",
						Character.Background.Name,Character.Background.Cost);
					sw.WriteLine("==================================================");
					sw.WriteLine("Sex: {0}  Age: {1}  Build: {2}",
						Character.Sex,Character.Age,Character.Build);
					sw.WriteLine("Skin: {0}  Eye: {1}  Hair: {2}",
						Character.Skin, Character.Eye, Character.Hair);
					sw.WriteLine("Quirk: {0}",Character.Quirk);
					sw.WriteLine("==================================================");
					sw.WriteLine("Divination: {0}",Divination_Box.Text);
					sw.WriteLine("Bio: {0}",Character.Bio);
					sw.WriteLine("==================================================");
					sw.WriteLine();
					sw.WriteLine("WS: {0}", Character.WS.Value);
					sw.WriteLine("BS: {0}", Character.BS.Value);
					sw.WriteLine("S: {0}", Character.S.Value);
					sw.WriteLine("T: {0}", Character.T.Value);
					sw.WriteLine("Ag: {0}", Character.Ag.Value);
					sw.WriteLine("Int: {0}", Character.Int.Value);
					sw.WriteLine("Per: {0}", Character.Per.Value);
					sw.WriteLine("Wp: {0}", Character.Wp.Value);
					sw.WriteLine("Fel: {0}", Character.Fel.Value);
					sw.WriteLine();
					sw.WriteLine("Wounds: {0}  Fate Points: {1}", 
						Character.Wounds, Character.Fate);
					sw.WriteLine("Insanity: {0}  Corruption: {1}", 
						Character.Insanity, Character.Corruption);
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
					sw.WriteLine("Thrones: {0}", Character.Thrones);
					sw.WriteLine("Income: {0}", Character.Income);
					sw.Close();
					ExportStream.Close();
				}
			}
		}
		private void Reset_Button_Click(object sender, EventArgs e)
		{
			Reset();
		}
		private void Random_Button_Click(object sender, EventArgs e)
		{
			Reset();

			#region Origin
			var origins = from s in Origins.Element("root").Elements()
						  select s;
			int selection = Dice.Next(origins.Count());
			string OName = origins.ElementAt(selection).Attribute("name").Value;
			string OBase = origins.ElementAt(selection).Attribute("base").Value;
			Character.Origin.Name = OName;
			Character.Origin.Base = OBase;

			Origin_comboBox.SelectedItem = Character.Origin.Name;
			Origin_comboBox.Update();
			#endregion
			Roll();
			#region Career
			List<Career> careerChoices = new List<Career>();
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
								if (!names.Contains<string>(origin.Attribute("base").Value))
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
					careerChoices.Add(new Career(career.Attribute("name").Value, career.Attribute("base").Value));
				}
			}
			Character.Career = careerChoices.ElementAt(Dice.Next(careerChoices.Count));

			string careername = "";
			if (Character.Career.Name != Character.Career.Base)
			{
				careername = Character.Career.Name + "(" + Character.Career.Base + ")";
			}
			else
			{
				careername = Character.Career.Name;
			}
			Career_comboBox.SelectedItem = careername;
			#endregion
			#region Background
			if (Dice.Next() % 2 == 0)
			{
				XElement career = GetCareer();
				var backs = from back in career.Element("backgrounds").Elements()
							where back.Attribute("origin").Value.Contains(origin.Attribute("base").Value)
							select back;
				XElement background = backs.ElementAt(Dice.Next(backs.Count()));
				Character.Background.Name = background.Attribute("name").Value;
				Character.Background.Cost = int.Parse(background.Attribute("cost").Value);

				Background_comboBox.SelectedItem = Character.Background.Name + ": " + Character.Background.Cost.ToString() + "xp";
			}
			#endregion
			#region Name
			if (Character.Origin.Name == "Hive Mutant")
			{
				Character.NameType = "Mutant";
			}
			else
			{
				var names = from name in Names.Element("root").Elements()
							where (string)(name.Attribute("type").Value) ==
									(string)(Character.Sex)
							select name;
				names = from name in names.Elements()
						where name.Attribute("type").Value != "Mutant"
						select name;

				Character.NameType = names.ElementAt(Dice.Next(names.Count())).Attribute("type").Value;
			}
			Name_comboBox.SelectedItem = Character.NameType;
			#endregion
		}
		private void Appearance_Button_Click(object sender, EventArgs e)
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
			Set_Sex();
			Add_Appearance();
			CharacterUpdate();
		}
		private void Save_Button_Click(object sender, EventArgs e)
		{
			Stream SaveStream;
			SaveFileDialog saveFileDialog1 = new SaveFileDialog();

			saveFileDialog1.FileName = Name_Box.Text + ".DHC";
			saveFileDialog1.Filter = "Dark Heresy Character (*.DHC)|*.DHC";
			saveFileDialog1.FilterIndex = 1;
			saveFileDialog1.RestoreDirectory = true;

			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				if ((SaveStream = saveFileDialog1.OpenFile()) != null)
				{
					try
					{
						XmlSerializer serializer = new XmlSerializer(typeof(DHCharacter));
						serializer.Serialize(SaveStream, Character);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
				}
				SaveStream.Close();
			}
		}	
		private void Roll_button_Click(object sender, EventArgs e)
		{
			Roll();
		}
		private void Roll()
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
				switch(skill.Parent.Parent.Name.LocalName)
				{
					case "origin":
						Character.Skills.Add(new Skill(SkillDialog.selection, Source.Origin));
						break;
					case "career":
						Character.Skills.Add(new Skill(SkillDialog.selection, Source.Career));
						break;
					case "background":
						Character.Skills.Add(new Skill(SkillDialog.selection, Source.Background));
						break;
					case "divination":
						Character.Skills.Add(new Skill(SkillDialog.selection, Source.Divination));
						break;
				}
			}
			else
			{
				switch (skill.Parent.Parent.Name.LocalName)
				{
					case "origin":
						Character.Skills.Add(new Skill(skill.Attribute("choice").Value, Source.Origin));
						break;
					case "career":
						Character.Skills.Add(new Skill(skill.Attribute("choice").Value, Source.Career));
						break;
					case "background":
						Character.Skills.Add(new Skill(skill.Attribute("choice").Value, Source.Background));
						break;
					case "divination":
						Character.Skills.Add(new Skill(skill.Attribute("choice").Value, Source.Divination));
						break;
				}
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
				switch(talent.Parent.Parent.Name.LocalName)
				{
					case "origin":
						Character.Talents.Add(new Talent(TalentDialog.selection, Source.Origin));
						break;
					case "career":
						Character.Talents.Add(new Talent(TalentDialog.selection, Source.Career));
						break;
					case "background":
						Character.Talents.Add(new Talent(TalentDialog.selection, Source.Background));
						break;
					case "divination":
						Character.Talents.Add(new Talent(TalentDialog.selection, Source.Divination));
						break;
				}
			}
			else
			{
				switch (talent.Parent.Parent.Name.LocalName)
				{
					case "origin":
						Character.Talents.Add(new Talent(talent.Attribute("choice").Value, Source.Origin));
						break;
					case "career":
						Character.Talents.Add(new Talent(talent.Attribute("choice").Value, Source.Career));
						break;
					case "background":
						Character.Talents.Add(new Talent(talent.Attribute("choice").Value, Source.Background));
						break;
					case "divination":
						Character.Talents.Add(new Talent(talent.Attribute("choice").Value, Source.Divination));
						break;
				}
			}
		}
		private void Add_Trait(XElement trait)
		{
			switch(trait.Parent.Parent.Name.LocalName)
			{
				case "background":
					Character.Traits.Add(new Trait(trait.Attribute("name").Value,trait.Attribute("effect").Value,
						Source.Background));
					break;
				case "origin":
					Character.Traits.Add(new Trait(trait.Attribute("name").Value,trait.Attribute("effect").Value,
						Source.Origin));
					break;
				case "career":
					Character.Traits.Add(new Trait(trait.Attribute("name").Value,trait.Attribute("effect").Value,
						Source.Career));
					break;
			}
			if (trait.Attribute("name").Value == "Sanctioned Psyker")
			{
				string SideEffect = SanctioningSideEffects();
				Character.Traits.Add(new Trait("Sanctioning Side Effect:", SideEffect, 
					Source.Career));
				
			}
			if (trait.Attribute("name").Value == "Twist")
			{
				#region Twist
				List<string> ChoiceList = new List<string>();
				ChoiceList.Add("Choose 2 Minor Mutations");
				ChoiceList.Add("1 random Minor, and 1 random Major Mutation");
				DialogBoxList Dialog = new DialogBoxList(ChoiceList);
				Dialog.ShowDialog();
				switch (Dialog.selection)
				{
					case "Choose 2 Minor Mutations":
						#region Minor
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
						MutationDialog.ShowDialog();
						switch (MutationDialog.selection)
						{
							case ("Grotesque"):
								MinorMutation(1, Source.Origin);
								break;
							case("Tough Hide"):
								MinorMutation(21, Source.Origin);
								break;
							case("Misshapen"):
								MinorMutation(31, Source.Origin);
								break;
							case("Feels No Pain"):
								MinorMutation(41, Source.Origin);
								break;
							case("Brute"):
								MinorMutation(51, Source.Origin);
								break;
							case("Nightsider"):
								MinorMutation(61, Source.Origin);
								break;
							case("Big Eyes"):
								MinorMutation(71, Source.Origin);
								break;
							case("Malformed Hands"):
								MinorMutation(81, Source.Origin);
								break;
							case("Tox Blood"):
								MinorMutation(86, Source.Origin);
								break;
							case("Wyrdling"):
								MinorMutation(90, Source.Origin);
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
						MutationDialog1.ShowDialog();
						switch (MutationDialog1.selection)
						{
							case ("Grotesque"):
								MinorMutation(1, Source.Origin);
								break;
							case ("Tough Hide"):
								MinorMutation(21, Source.Origin);
								break;
							case ("Misshapen"):
								MinorMutation(31, Source.Origin);
								break;
							case ("Feels No Pain"):
								MinorMutation(41, Source.Origin);
								break;
							case ("Brute"):
								MinorMutation(51, Source.Origin);
								break;
							case ("Nightsider"):
								MinorMutation(61, Source.Origin);
								break;
							case ("Big Eyes"):
								MinorMutation(71, Source.Origin);
								break;
							case ("Malformed Hands"):
								MinorMutation(81, Source.Origin);
								break;
							case ("Tox Blood"):
								MinorMutation(86, Source.Origin);
								break;
							case ("Wyrdling"):
								MinorMutation(90, Source.Origin);
								break;
						}
					#endregion
						break;
					case "1 random Minor, and 1 random Major Mutation":
						MinorMutation(Dice.Next(1, 101),Source.Origin);
						MajorMutation(Dice.Next(1, 101), Source.Origin);
						break;
				}				
				#endregion
			}
			if (trait.Attribute("name").Value == "Mysterious Lineage")
			{
				MysteriousLineage();
			}
			if (trait.Attribute("name").Value == "Guiding Hand of the Emperor")
			{
				GuidingHandEmperor();
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
				switch(equip.Parent.Parent.Name.LocalName)
				{
					case "background":
						Character.Gear.Add(new Equipment(EquipDialog.selection, Source.Background));
						break;
					case "career":
						Character.Gear.Add(new Equipment(EquipDialog.selection, Source.Career));
						break;
					case "origin":
						Character.Gear.Add(new Equipment(EquipDialog.selection, Source.Origin));
						break;
				}
			}
			else
			{
				switch (equip.Parent.Parent.Name.LocalName)
				{
					case "background":
						Character.Gear.Add(new Equipment(equip.Attribute("choice").Value, Source.Background));
						break;
					case "career":
						Character.Gear.Add(new Equipment(equip.Attribute("choice").Value, Source.Career));
						break;
					case "origin":
						Character.Gear.Add(new Equipment(equip.Attribute("choice").Value, Source.Origin));
						break;
				}
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
				Insanity_Roll(num,size,Base,Source.Origin);
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
				Corruption_Roll(num, size, Base, Source.Origin);
				if (points.Attributes().Count() > 3)
				{
					var query = from mal in points.Attributes()
								where mal.Name == "mal"
								select mal.Value;
					if (query.ElementAt(0) == "true")
					{
						Malignancy(Source.Origin);
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
				Character.Sex = "Male";
			}
			else
			{
				Character.Sex = "Female";
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
					Character.Build = build.Attribute("name").Value + ": ";
					if (Character.Sex == "Male")
					{
						Character.Build = Character.Build + build.Attribute("Male").Value;
					}
					else
					{
						Character.Build = Character.Build + build.Attribute("Female").Value;
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
					Character.Skin = skin.Attribute("name").Value;
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
					Character.Age = age.Attribute("name").Value + ": " + Age.ToString();
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
					Character.Hair = hair.Attribute("name").Value;
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
					Character.Eye = eye.Attribute("name").Value;
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
					Character.Quirk = quirk.Attribute("name").Value;
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
				Insanity_Roll(num, size, Base, Source.Career);
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
				Corruption_Roll(num, size, Base, Source.Career);
				if (points.Attributes().Count() > 3)
				{
					var query = from mal in points.Attributes()
								where mal.Name == "mal"
								select mal.Value;
					if (query.ElementAt(0) == "true")
					{
						Malignancy(Source.Career);
					}
				}
			}
		}

		private void Add_Wealth()
		{
			XElement career = GetCareer();
			XElement origin = GetOrigin();

			Character.Income = career.Element("wealth").Element("income").Attribute("name").Value;

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
						Character.Income = origin.Element("wealth").Element("income").Attribute("name").Value;
						break;
					case "add":
						Character.Income += (origin.Element("wealth").Element("income").Attribute("name").Value);
						break;
				}
			}
			Character.Thrones = thrones;
		}
		#endregion

		#region Background Additions
		private void Add_Back_Stats()
		{
			XElement back = GetBackground();
			string choice;
			foreach (XElement stat in back.Element("stats").Elements())
			{
				List<string> StatList = new List<string>();
				if (stat.Attributes().Count() > 2)
				{
					foreach (XAttribute x in stat.Attributes())
					{
						if (x.Name == "amount")
						{
							break;
						}
						StatList.Add(x.Value +" +"+ stat.Attribute("amount").Value);
					}
					DialogBoxList StatDialog = new DialogBoxList(StatList);
					StatDialog.ShowDialog();
					choice = StatDialog.selection.Split(' ').First();
				}
				else
				{
					choice = stat.Attribute("name").Value;
				}
				 
				switch (choice)
				{
					case "WS":
						Character.WS.BackMod += (int.Parse(stat.Attribute("amount").Value));
						break;
					case "BS":
						Character.BS.BackMod += (int.Parse(stat.Attribute("amount").Value));
						break;
					case "S":
						Character.S.BackMod += (int.Parse(stat.Attribute("amount").Value));
						break;
					case "T":
						Character.T.BackMod += (int.Parse(stat.Attribute("amount").Value));
						break;
					case "Ag":
						Character.Ag.BackMod += (int.Parse(stat.Attribute("amount").Value));
						break;
					case "Int":
						Character.Int.BackMod += (int.Parse(stat.Attribute("amount").Value));
						break;
					case "Per":
						Character.Per.BackMod += (int.Parse(stat.Attribute("amount").Value));
						break;
					case "Wp":
						Character.Wp.BackMod += (int.Parse(stat.Attribute("amount").Value));
						break;
					case "Fel":
						Character.Fel.BackMod += (int.Parse(stat.Attribute("amount").Value));
						break;
					case "Wounds":
						Character.Wounds.BackMod += (int.Parse(stat.Attribute("amount").Value));
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
				Character.Gear.Clear();
				foreach (XElement equip in back.Element("gear").Elements())
				{
					Add_Gear(equip);
				}
			}
		}
		private void Add_Back_Wealth()
		{
			XElement back = GetBackground();
			
			foreach (XElement element in back.Element("wealth").Elements())
			{
				switch (element.Name.ToString())
				{
					case "thrones":
						switch (back.Element("wealth").Element("thrones").Attribute("type").Value)
						{
							case "*":
								Character.Thrones *= int.Parse(back.Element("wealth").Element("thrones").Attribute("mod").Value);
								break;
							case "/":
								Character.Thrones /= int.Parse(back.Element("wealth").Element("thrones").Attribute("mod").Value);
								break;
							case "+":
								Character.Thrones += int.Parse(back.Element("wealth").Element("thrones").Attribute("mod").Value);
								break;
							case "-":
								Character.Thrones -= int.Parse(back.Element("wealth").Element("thrones").Attribute("mod").Value);
								break;
						}
						break;
					case "income":
						switch (back.Element("wealth").Element("income").Attribute("type").Value)
						{
							case "replace":
								Character.Income = back.Element("wealth").Element("income").Attribute("name").Value;
								break;
							case "add":
								Character.Income +=" And "+ (back.Element("wealth").Element("income").Attribute("name").Value);
								break;
						}
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
						Character.Fate.BackMod += int.Parse(back.Element("fate").Element("points").Attribute("amount").Value);
						break;
					case "-":
						Character.Fate.BackMod -= int.Parse(back.Element("fate").Element("points").Attribute("amount").Value);
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
				Insanity_Roll(num, size, Base, Source.Background);
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
				Corruption_Roll(num, size, Base, Source.Background);
				if (points.Attributes().Count() > 3)
				{
					var query = from mal in points.Attributes()
								where mal.Name == "mal"
								select mal.Value;
					if (query.ElementAt(0) == "true")
					{
						Malignancy(Source.Background);
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
					Character.Int.CareerMod += 3;
					break;
				case "Assassin":
					Character.Ag.CareerMod += 3;
					break;
				case "Guardsman":
					Character.BS.CareerMod += 3;
					break;
				case "Scum":
					if (GetCareer().Attribute("name").Value == "Lathesmaster")
					{
						Character.S.CareerMod += 3;
						Character.T.CareerMod += 3;
						Character.Ag.CareerMod -= 5;
					}
					else
					{
						Character.Per.CareerMod += 3;
					}
					break;
				case "Tech-Priest":
					Character.Wp.CareerMod += 3;
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
				Character.Int.CareerMod += 3;
				Character.Thrones += thrones;
				return "Reconstructed Skull: You have large metal plates in your head, some"+
					" of which are clearly visible. Reduce your Intelligence by 3, but "+
					"gain 5d10 Throne Gelt in compensation";
			}
			else if (result >= 9 && result <= 14)
			{
				Character.Insanity.CareerMod += Dice.Next(1, 11);
				return "Hunted: You believe certain parts of your psyche, those amputated by"+
					" the sanctioners, have gained sentience and are tracking you down. Gain 1d10 Insanity Points";
			}
			else if (result >= 15 && result <= 25)
			{
				Character.Insanity.CareerMod += Dice.Next(1, 6);
				return "Unlovely Memories: Such was your sanctioning, that you visibly "+
					"twitch and grimace whenever Holy Terra is mentioned. Gain 1d5 Insanity Points";
			}
			else if (result >= 26 && result <= 35)
			{
				Character.Insanity.CareerMod += Dice.Next(1, 6);
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
				Character.Gear.Add(new Equipment("Carven Dentures (50 thrones)\n",Source.Career));
				return "Dental Probes: You no longer have any teeth in your head. You have a "+
					"set of carven dentures, They are of Good quality and worth 50 Thrones.";
			}
			else if (result >= 50 && result <= 57)
			{
				Character.Gear.Add(new Equipment("Common Cybernetic Eyes\n",Source.Career));
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
				Character.Talents.Add(new Talent("Chem Geld",Source.Career));
				Character.Gear.Add(new Equipment("Chattallium Ring(100 thrones)",Source.Career));
				return "Throne Wed: You cleave only unto the Emperor. You gain the Chem "+
					"Geld talent, and a chattallium ring, worth 100 Thrones.";
			}
			else if (result >= 89 && result <= 94)
			{
				Character.T.CareerMod += 3;
				return "Witch Prickling: Your body is covered in thousands of tiny scars. "+
					"You have a thorough dislike of needles. Increase your Toughness by 3.";
			}
 			else
			{
				Character.Wp.CareerMod += 3;
				return "Hypno-doctrination: Powerful conditioning causes you to chant the "+
					"Litany of Protection in a whispered voice whenever you are asleep or "+
					"unconscious. Increase your Willpower by 3.";
			}
		}
		private void MinorMutation(int result, Source source)
		{
			if (result >= 1 && result <= 20)
			{
				Character.Traits.Add(new Trait("Grotesque",
					"The mutant is either badly deformed, scarred or bestial Fellowship Tests with"+
					" ‘normals’ are made at –20, but +10 to Intimidate Tests.",source));
			}
			else if (result >= 21 && result <= 30)
			{
				Character.Traits.Add(new Trait("Tough Hide",
					"The mutant has 1 AP worth of Natural Armour thanks to dense skin and scar tissue.", source));
			}
			else if (result >= 31 && result <= 40)
			{
				Character.Traits.Add(new Trait("Misshapen",
				"The mutant’s spine and/or limbs are horribly twisted, giving it a penalty of -1d10 to"+
				" its Agility.", source));
				switch (source)
				{
					case Source.Background:
						Character.Ag.BackMod -= Dice.Next(1, 11);
						break;
					case Source.Career:
						Character.Ag.CareerMod -= Dice.Next(1, 11);
						break;
					case Source.Divination:
						Character.Ag.DivMod -= Dice.Next(1, 11);
						break;
					case Source.Origin:
						Character.Ag.OriginBase -= Dice.Next(1, 11);
						break;
				}
			}
			else if (result >= 41 && result <= 50)
			{
				Character.Traits.Add(new Trait("Feels No Pain",
					"The mutant cares little for injury or harm and gains +1 Wound.", source));
				switch (source)
				{
					case Source.Background:
						Character.Wounds.BackMod += 1;
						break;
					case Source.Career:
						Character.Wounds.CareerMod += 1;
						break;
					case Source.Divination:
						Character.Wounds.DivMod += 1;
						break;
					case Source.Origin:
						Character.Wounds.OriginBase += 1;
						break;
				}
			}
			else if (result >= 51 && result <= 60)
			{
				Character.Traits.Add(new Trait("Brute", 
					"The mutant is physically powerful with deformed masses of muscle. "+
					"Apply +10 Strength, +10 Toughness and –10 Agility.", source));
				switch (source)
				{
					case Source.Background:
						Character.S.BackMod += 10;
						Character.T.BackMod += 10;
						Character.Ag.BackMod -= 10;
						break;
					case Source.Career:
						Character.S.CareerMod += 10;
						Character.T.CareerMod += 10;
						Character.Ag.CareerMod -= 10;
						break;
					case Source.Divination:
						Character.S.DivMod += 10;
						Character.T.DivMod += 10;
						Character.Ag.DivMod -= 10;
						break;
					case Source.Origin:
						Character.S.OriginBase += 10;
						Character.T.OriginBase += 10;
						Character.Ag.OriginBase -= 10;
						break;
				}
			}
			else if (result >= 61 && result <= 70)
			{
				Character.Traits.Add(new Trait("Nightsider",
					"The mutant gains the Dark Sight trait, but –10 penalty to all Actions in "+
					"bright light or daylight unless its eyes are shielded.", source));
				Character.Traits.Add(new Trait("Dark Sight", 
					"See normally even in areas of total darkness and never takes a penalty in "+
					"dim or no lighting.", source));
			}
			else if (result >= 71 && result <= 80)
			{
				Character.Traits.Add(new Trait("Big Eyes", 
					"The mutant’s eyes are virtually lidless and watery. Apply +10 Perception "+
					"and –10 Fellowship.", source));
				switch (source)
				{
					case Source.Background:
						Character.Per.BackMod += 10;
						Character.Fel.BackMod -= 10;
						break;
					case Source.Career:
						Character.Per.CareerMod += 10;
						Character.Fel.CareerMod -= 10;
						break;
					case Source.Divination:
						Character.Per.DivMod += 10;
						Character.Fel.DivMod -= 10;
						break;
					case Source.Origin:
						Character.Per.OriginBase += 10;
						Character.Fel.OriginBase -= 10;
						break;
				}
			}
			else if (result >= 81 && result <= 85)
			{
				Character.Traits.Add(new Trait("Malformed hands", 
					"–10 to WS and BS and the mutant suffers a –20 penalty to all tasks involving"+
					" fine physical manipulation.", source));
				switch (source)
				{
					case Source.Background:
						Character.WS.BackMod -= 10;
						Character.BS.BackMod -= 10;
						break;
					case Source.Career:
						Character.WS.CareerMod -= 10;
						Character.BS.CareerMod -= 10;
						break;
					case Source.Divination:
						Character.WS.DivMod -= 10;
						Character.BS.DivMod -= 10;
						break;
					case Source.Origin:
						Character.WS.OriginBase -= 10;
						Character.BS.OriginBase -= 10;
						break;
				}
			}
			else if (result >= 86 && result <= 89)
			{
				Character.Traits.Add(new Trait("Tox Blood",
					"The mutant’s system is saturated with toxic pollutants and poisonous chemicals. "+
					"+10 resistance to toxins and poisons, however it suffers a –1d10 penalty to its"+
					" Intelligence and Fellowship.", source));
				switch (source)
				{
					case Source.Background:
						Character.Int.BackMod -= Dice.Next(1, 11);
						Character.Fel.BackMod -= Dice.Next(1, 11);
						break;
					case Source.Career:
						Character.Int.CareerMod -= Dice.Next(1, 11);
						Character.Fel.CareerMod -= Dice.Next(1, 11);
						break;
					case Source.Divination:
						Character.Int.DivMod -= Dice.Next(1, 11);
						Character.Fel.DivMod -= Dice.Next(1, 11);
						break;
					case Source.Origin:
						Character.Int.OriginBase -= Dice.Next(1, 11);
						Character.Fel.OriginBase -= Dice.Next(1, 11);
						break;
				}
			}
			else if (result >= 90 && result <= 99)
			{
				Character.Traits.Add(new Trait("Wyrdling", 
					"The mutant has Minor Psychic Powers that "+
					"it has so far been able to conceal. The mutant has a Psy Rating of 1.", source));
				Character.Talents.Add(new Talent("Psy Rating 1",source));
			}
			else
			{
				MajorMutation(Dice.Next(1,101),source);
			}
			CharacterUpdate();
		}
		private void MajorMutation(int result, Source source)
		{
			if (result >= 1 && result <= 25)
			{
				Character.Traits.Add(new Trait("Vile Deformity",
					"The mutant is marked by some terrible deformity that shows the touch of the"+
					" warp and should not exist in a rational universe. The mutant gains the Disturbing "+
					"trait.", source));
				Character.Traits.Add(new Trait("Disturbing", "Fear Rating 1.", source));

			}
			else if (result >= 26 && result <= 35)
			{
				Character.Traits.Add(new Trait("Aberration",
					"The mutant has become a weird hybrid of man and animal (or reptile, insect, etc.) "+
					"Apply +10 Strength, +10 Agility, –1d10 Intelligence, -10 Fellowship and the Sprint "+
					"talent.", source));
				switch (source)
				{
					case Source.Origin:
						Character.S.OriginBase += 10;
						Character.Ag.OriginBase += 10;
						Character.Int.OriginBase -= Dice.Next(1, 11);
						break;
					case Source.Divination:
						Character.S.DivMod += 10;
						Character.Ag.DivMod += 10;
						Character.Int.DivMod -= Dice.Next(1, 11);
						break;
					case Source.Background:
						Character.S.BackMod += 10;
						Character.Ag.BackMod += 10;
						Character.Int.BackMod -= Dice.Next(1, 11);
						break;
					case Source.Career:
						Character.S.CareerMod += 10;
						Character.Ag.CareerMod += 10;
						Character.Int.CareerMod -= Dice.Next(1, 11);
						break;
				}
				Character.Talents.Add(new Talent("Sprint", source));
			}
			else if (result >= 36 && result <= 40)
			{
				Character.Traits.Add(new Trait("Degenerate Mind", 
					"The mutant’s mind is warped and inhuman. Apply –1d10 Intelligence +10 Fellowship, "+
					"roll 1d10 and apply the following Talents or Trait: 1–3: Frenzy, 4–7: Fearless, 8–0: "+
					"From Beyond.", source));
				switch (source)
				{
					case Source.Origin:
						Character.Fel.OriginBase += 10;
						Character.Int.OriginBase -= Dice.Next(1,11);
						break;
					case Source.Career:
						Character.Fel.CareerMod += 10;
						Character.Int.CareerMod -= Dice.Next(1, 11);
						break;
					case Source.Background:
						Character.Fel.BackMod += 10;
						Character.Int.BackMod -= Dice.Next(1, 11);
						break;
					case Source.Divination:
						Character.Fel.DivMod += 10;
						Character.Int.DivMod -= Dice.Next(1, 11);
						break;
				}
				switch(Dice.Next(1,11))
				{
					case 1: case 2: case 3:
						Character.Talents.Add(new Talent("Frenzy", source));
						break;
					case 4: case 5: case 6: case 7:
						Character.Talents.Add(new Talent("Fearless", source));
						break;
					case 8: case 9: case 10:
						Character.Traits.Add(new Trait("From Beyond", 
							"Immune to Fear, Pinning, Insanity Points and Psychic Powers used to cloud, "+
							"control or delude its mind", source));
						break;
				}
			}
			else if (result >= 41 && result <= 50)
			{
				Character.Traits.Add(new Trait("Ravaged Body", 
					"The mutant’s body has been entirely re-made by the warp. Gain 1d5 Minor Mutations,"+
					" re-rolling duplicate rolls. Such mutations, regardless of their nature, still show "+
					"the obvious taint of Chaos", source));
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
					MinorMutation(roll,source);
				}
			}
			else if (result >= 51 && result <= 60)
			{
				Character.Traits.Add(new Trait("Clawed/Fanged", 
					"The mutant gains razor claws, a fanged maw, barbed flesh or "+
					"some other form of Natural Weapon that inflicts 1d10 R or I Primitive damage "+
					"in close combat.", source));
			}
			else if (result >= 61 && result <= 65)
			{
				Character.Traits.Add(new Trait("Necrophage", 
					"The mutant gains +10 Toughness and the Regeneration trait, "+
					"but must sustain itself on copious quantities of raw meat or starve.", source));
				switch (source)
				{
					case Source.Background:
						Character.T.BackMod += 10;
						break;
					case Source.Career:
						Character.T.CareerMod += 10;
						break;
					case Source.Divination:
						Character.T.DivMod += 10;
						break;
					case Source.Origin:
						Character.T.OriginBase += 10;
						break;
				}
				Character.Traits.Add(new Trait("Regeneration", 
					"Each Round, at the start of its Turn, the creature Tests"+
					" Toughness to remove 1 point of Damage.", source));
			}
			else if (result >= 66 && result <= 70)
			{
				Character.Traits.Add(new Trait("Corrupted Flesh", 
					"Beneath the mutants’ skin a blasphemous transformation has"+
					" taken place, exchanging living organs for writhing creatures and blood for "+
					"ichorous, maggot-ridden filth. If the mutant suffers Critical Damage, those "+
					"witnessing it must take a Fear Test at –10.", source));
			}
			else if (result >= 71 && result <= 75)
			{
				Character.Traits.Add(new Trait("Vile Alacrity", "The mutant is constantly juddering and shaking unnaturally and "+
					"can move almost faster than sight. It gains the Unnatural Agility (x2) trait and "+
					"the Sprint talent, with a penalty of –10 to Weapon Skill and Ballistic Skill.", source));

				Character.Traits.Add(new Trait("Unnatural Agility(x2)", "Agility Bonus x2", source));
				Character.Talents.Add(new Talent("Sprint", source));
				switch (source)
				{
					case Source.Background:
						Character.WS.BackMod -= 10;
						Character.BS.BackMod -= 10;
						break;
					case Source.Career:
						Character.WS.CareerMod -= 10;
						Character.BS.CareerMod -= 10;
						break;
					case Source.Divination:
						Character.WS.DivMod -= 10;
						Character.BS.DivMod -= 10;
						break;
					case Source.Origin:
						Character.WS.OriginBase -= 10;
						Character.BS.OriginBase -= 10;
						break;
				}
			}
			else if (result >= 76 && result <= 80)
			{
				Character.Traits.Add(new Trait("Hideous Strength",
					"The mutant gains the Unnatural Strength (x2) trait.", source));
				Character.Traits.Add(new Trait("Unnatural Strength(x2)", "Strength Bonus x2", source));
			}
			else if (result >= 81 && result <= 85)
			{
				Character.Traits.Add(new Trait("Multiple Appendages", 
					"The mutant has sprouted additional functioning limbs in "+
					"the shape of arms, tentacles or a prehensile tail (or tails). Gain "+
					"Ambidextrous and Two-Weapon Wielder and +10 on Climb and Grapple.", source));
				Character.Talents.Add(new Talent("Ambidextrous",source));
				Character.Talents.Add(new Talent("Two-Weapon Wielder(Melee)",source));
				Character.Talents.Add(new Talent("Two-Weapon Wielder(Ballistic)",source));
			}
			else if (result >= 86 && result <= 90)
			{
				Character.Traits.Add(new Trait("Worm", 
					"The mutant’s lower limbs have fused together to form a "+
					"worm or snake-like tail. They gain the Crawler trait, +5 Wounds and the "+
					"Disturbing trait.", source));
				Character.Traits.Add(new Trait("Crawler", 
					"The move for a creature with this Trait is half its Agility"+
					" Bonus, but it does not take penalties for moving over Difficult Terrain.", source));
				switch (source)
				{
					case Source.Background:
						Character.Wounds.BackMod += 5;
						break;
					case Source.Career:
						Character.Wounds.CareerMod += 5;
						break;
					case Source.Divination:
						Character.Wounds.DivMod += 5;
						break;
					case Source.Origin:
						Character.Wounds.OriginBase += 5;
						break;
				}
				Character.Traits.Add(new Trait("Disturbing", "Fear Rating 1.", source));
			}
			else if (result >= 91 && result <= 92)
			{
				Character.Traits.Add(new Trait("Nightmarish", 
					"So warped and horrific is the mutant’s appearance, it can "+
					"cause enemies to flee in fear. It gains the Frightening trait.", source));
				Character.Traits.Add(new Trait("Frightening", "Fear Rating 2.", source));
			}
			else if (result >= 93 && result <= 94)
			{
				Character.Traits.Add(new Trait("Malleable", 
					"The mutant possesses a sickeningly liquid flexibility and is"+
					" able to distend and flatten its body. Apply +10 Agility and +20 to Climb Tests, "+
					"and Grappling attacks. They may also fit through spaces only one-quarter its usual"+
					" body dimensions.", source));
				switch (source)
				{
					case Source.Background:
						Character.Ag.BackMod += 10;
						break;
					case Source.Career:
						Character.Ag.CareerMod += 10;
						break;
					case Source.Divination:
						Character.Ag.DivMod += 10;
						break;
					case Source.Origin:
						Character.Ag.OriginBase += 10;
						break;
				}
			}
			else if (result >= 95 && result <= 96)
			{
				Character.Traits.Add(new Trait("Winged", 
					"The mutant’s body has warped to accommodate a pair of leathery "+
					"wings or the like. They gain the Flyer trait (1d10+5)", source));
				int amount=Dice.Next(1,11)+5;
				Character.Traits.Add(new Trait("Flyer("+amount.ToString()+")", 
					"Can Fly "+amount.ToString()+" meters", source));
			}
			else if (result >= 97 && result <= 98)
			{
				Character.Traits.Add(new Trait("Corpulent", "The mutant’s huge and bloated frame gives them +5 Wounds and "+
					"the Unnatural Toughness (x2) trait. The mutant can't run.", source));
				Character.Traits.Add(new Trait("Unnatural Toughness(x2)", "Toughness Bonus x2", source));
				switch (source)
				{
					case Source.Background:
						Character.Wounds.BackMod += 5;
						break;
					case Source.Career:
						Character.Wounds.CareerMod += 5;
						break;
					case Source.Divination:
						Character.Wounds.DivMod += 5;
						break;
					case Source.Origin:
						Character.Wounds.OriginBase += 5;
						break;
				}
			}
			else if (result == 99)
			{
				Character.Traits.Add(new Trait("Corrosive Bile", 
					"The mutant may vomit burning bile, flesh-eating grubs or some "+
					"other horrific substance instead of attacking normally in close combat. The "+
					"attack uses the mutant’s BS, is a Full Action and can be Dodged but not Parried. "+
					"This attack inflicts 1d10+5 R (or E) Tearing Damage", source));
			}
			else
			{
				Character.Traits.Add(new Trait("Hellspawn", 
					"Saturated with the energies of the warp, the mutant is imbued"+
					" with Daemonic energies and gains the From Beyond, Frightening and Daemonic "+
					"traits and a Psy Rating of 2.", source));

				Character.Traits.Add(new Trait("From Beyond", 
					"Immune to Fear, Pinning, Insanity Points and " +
					"Psychic Powers used to cloud, control or delude its mind", source));
				
				Character.Traits.Add(new Trait("Frightening", "Fear Rating 2", source));

				Character.Traits.Add(new Trait("Daemonic", 
					"Creatures with this Trait double their Toughness Bonus "+
					"against all Damage, except for Damage inflicted by force weapons, Psychic "+
					"Powers, holy attacks or other creatures with this Trait. Daemonic creatures "+
					"are also immune to poison and disease.", source));
				Character.Talents.Add(new Talent("Psy Rating 2", source));
			}
			CharacterUpdate();
		}
		private string MutantName()
		{
			List<string> namelist = new List<string>();
			var Mutations = from name in Names.Element("root").Elements()
							where name.Attribute("type").Value == Character.Sex
							select name;
			Mutations = from name in Mutations.Elements()
						where name.Attribute("type").Value == "Mutant"
						select name;
			foreach (Trait Mutation in Character.Traits)
			{
				foreach (XElement Mut in Mutations.Elements())
				{
					if (Mutation.Name == Mut.Attribute("name").Value)
					{
						var query = from name in Mut.Elements()
									select name;
						namelist.Add(query.ElementAt(Dice.Next(query.Count())).Value);
					}
				}
				if (namelist.Count == 2)
				{
					break;
				}
			}
			string selectedname = namelist.ElementAt(Dice.Next(namelist.Count));
			IEnumerable<XElement> Iname;

				Iname = from names in Names.Element("root").Elements()
						where (string)(names.Attribute("type").Value) ==
									(string)(Sex_Box.Text)
						select names;

				Iname = from name in Iname.Elements()
						where (string)(name.Attribute("type").Value) !=
									"Mutant"
						select name;
	
			string insertname = Iname.Elements().ElementAt(Dice.Next(Iname.Elements().Count())).Value;
			return selectedname.Replace("X", insertname);
		}
		private void Malignancy(Source source)
		{
			int result = Dice.Next(1,101);
			
			if(result >= 1 && result <= 10)
			{
				Character.Traits.Add(new Trait("Palsy",
					"The character suffers from numerous minor tics, shakes and tremors with no"+
					" medical cause. Reduce his Agility by 1d10.", source));
				switch(source)
				{
					case Source.Origin:
						Character.Ag.OriginBase -= Dice.Next(1, 11);
						break;
					case Source.Divination:
						Character.Ag.DivMod -= Dice.Next(1, 11);
						break;
					case Source.Career:
						Character.Ag.CareerMod -= Dice.Next(1, 11);
						break;
					case Source.Background:
						Character.Ag.BackMod -= Dice.Next(1, 11);
						break;
				}
				
			}
			else if(result >= 11 && result <= 15)
			{
				Character.Traits.Add(new Trait("Dark-hearted",
					"The character grows increasingly cruel, callous and vindictive. Reduce his "+
					"Fellowship by 1d10.",source));
				switch (source)
				{
					case Source.Origin:
						Character.Int.OriginBase -= Dice.Next(1, 11);
						break;
					case Source.Divination:
						Character.Int.DivMod -= Dice.Next(1, 11);
						break;
					case Source.Career:
						Character.Int.CareerMod -= Dice.Next(1, 11);
						break;
					case Source.Background:
						Character.Int.BackMod -= Dice.Next(1, 11);
						break;
				}
				
			}
			else if(result >= 16 && result <= 20)
			{
				Character.Traits.Add(new Trait("Ill-fortuned",
					"Whenever the character uses a Fate Point roll a d10. On a score of 7, 8, 9 or 10"+
					" it has no effect but it is lost anyway.",source));
			}
			else if(result >= 21 && result <= 22)
			{
				Character.Traits.Add(new Trait("Skin Afflictions",
					"The character is plagued by boils, scabs, weeping sores and the like. He takes "+
					"a –20 penalty to all Charm Tests.",source));
			}
			else if(result >= 23 && result <= 25)
			{
				Character.Traits.Add(new Trait("Night Eyes",
					"Light pains the character, and unless he shields his eyes, he suffers a –10 "+
					"penalty on all Tests when in an area of bright light.",source));
			}
			else if(result >= 26 && result <= 30)
			{
				Character.Traits.Add(new Trait("Morbid",
					"The character finds it hard to concentrate as his mind is filled with macabre"+
					" visions and tortured, gloom-filled trains of thought. The character’s "+
					"Intelligence is reduced by 1d10.",source));
				switch (source)
				{
					case Source.Origin:
						Character.Int.OriginBase -= Dice.Next(1, 11);
						break;
					case Source.Divination:
						Character.Int.DivMod -= Dice.Next(1, 11);
						break;
					case Source.Career:
						Character.Int.CareerMod -= Dice.Next(1, 11);
						break;
					case Source.Background:
						Character.Int.BackMod -= Dice.Next(1, 11);
						break;
				}
			}
			else if(result >= 31 && result <= 33)
			{
				Character.Traits.Add(new Trait("Witch-mark",
					"The character develops some minor physical deformity or easily concealable"+
					" mutation. It is small, but perhaps enough to consign him to the stake if found"+
					" out by a fanatical witch hunter. He must hide it well!",source));
				
			}
			else if(result >= 34 && result <= 45)
			{
				Character.Traits.Add(new Trait("Fell Obsession",
					"This is the same as the Obsession Disorder. However, in this case the character is"+
					" obsessed by a sinister or malign focus, such as collecting finger-bone trophies, "+
					"ritual scarification, carrying out meaningless vivisections, etc.",source));
			}
			else if(result >= 46 && result <= 50)
			{
				Character.Traits.Add(new Trait("Hatred",
					"The character develops an implacable hatred of a single group, individual or "+
					"social class. The character will never side with or aid them without explicit "+
					"orders or other vital cause, and even then grudgingly.",source));
			}
			else if(result >= 51 && result <= 55)
			{
				Character.Traits.Add(new Trait("Irrational Nausea",
					"The character feels sick at the sight or sound of some otherwise innocuous thing"+
					" such as prayer books and holy items, bare flesh, human laughter, fresh food, "+
					"shellfish, etc. When he encounters the object of his revulsion, he must Test "+
					"Toughness or suffer a –10 penalty to all Tests as long as he remains in its"+
					" presence.",source));
			}
			else if(result >= 56 && result <= 60)
			{
				Character.Traits.Add(new Trait("Wasted Frame",
					"The character’s pallor becomes corpse-like and his muscles waste away. "+
					"The character’s Strength is reduced by 1d10.",source));
				switch (source)
				{
					case Source.Origin:
						Character.S.OriginBase -= Dice.Next(1, 11);
						break;
					case Source.Divination:
						Character.S.DivMod -= Dice.Next(1, 11);
						break;
					case Source.Career:
						Character.S.CareerMod -= Dice.Next(1, 11);
						break;
					case Source.Background:
						Character.S.BackMod -= Dice.Next(1, 11);
						break;
				}
			}
			else if(result >= 61 && result <= 63)
			{
				Character.Traits.Add(new Trait("Night Terrors",
					"The character is plagued by Daemonic visions in his sleep. See Horrific "+
					"Nightmares.",source));
			}
			else if(result >= 64 && result <= 70)
			{
				Character.Traits.Add(new Trait("Poor Health",
					"The character constantly suffers petty illnesses and phantom pains, and his"+
					" wounds never seem to heal fully. The character’s Toughness is reduced by "+
					"1d10.",source));
				switch (source)
				{
					case Source.Origin:
						Character.T.OriginBase -= Dice.Next(1, 11);
						break;
					case Source.Divination:
						Character.T.DivMod -= Dice.Next(1, 11);
						break;
					case Source.Career:
						Character.T.CareerMod -= Dice.Next(1, 11);
						break;
					case Source.Background:
						Character.T.BackMod -= Dice.Next(1, 11);
						break;
				}
			}
			else if(result >= 71 && result <= 75)
			{
				Character.Traits.Add(new Trait("Distrustful",
					"The character cannot conceal the distrust and antipathy he has for others."+
					" He must take a –10 penalty to Fellowship Tests when dealing with strangers.",
					source));
			}
			else if(result >= 76 && result <= 80)
			{
				Character.Traits.Add(new Trait("Malign Sight", 
					"The world seems to darken, tarnish and rot if the character looks too long at"+
					" anything. The character’s Perception is reduced by 1d10.",source));
				switch (source)
				{
					case Source.Origin:
						Character.Per.OriginBase -= Dice.Next(1, 11);
						break;
					case Source.Divination:
						Character.Per.DivMod -= Dice.Next(1, 11);
						break;
					case Source.Career:
						Character.Per.CareerMod -= Dice.Next(1, 11);
						break;
					case Source.Background:
						Character.Per.BackMod -= Dice.Next(1, 11);
						break;
				}
			}
			else if(result >= 81 && result <= 83)
			{
				Character.Traits.Add(new Trait("Ashen Taste",
					"Food and drink hold disgusting tastes and little sustenance for the character,"+
					" and he can barely stomach eating. The character doubles the negative effects "+
					"for levels of Fatigue.",source));
			}
			else if(result >= 84 && result <= 90)
			{
				Character.Traits.Add(new Trait("Bloodlust",
					"Murderous rage is never far from the character’s mind. After being wounded in"+
					" combat, he must Test Willpower to incapacitate or allow his enemies to flee,"+
					" rather than kill them outright, even if his intent is otherwise.",source));
			}
			else if(result >= 91 && result <= 93)
			{
				Character.Traits.Add(new Trait("Blackouts", "The character suffers from inexplicable"+
					" blackouts. When they occur and what happens during them is up to the GM.",source));
			}
			else if(result >= 94 && result <= 100)
			{
				Character.Traits.Add(new Trait("Strange Addiction", 
					"The character is addicted to some bizarre and unnatural substance, such as "+
					"eating rose petals, drinking blood, the taste of widows’ tears etc. This acts"+
					" like a Minor Compulsion, but is freakish enough to cause serious suspicion if "+
					"found out.",source));
				
			}
		}
		private void MysteriousLineage()
		{
			int result=0;
			switch (Dice.Next(1,11))
			{
				case 1: case 2:
					Character.Traits.Last().Effect=
						"You are extremely intelligent, though you are prone to peculiar headaches"+
						" and delusions. Increase your intelligence and insanity points by 1d10 "+
						"(roll only once and apply the result to both).";
					result = Dice.Next(1,11);
					Character.Int.OriginBase += result;
					Character.Insanity.OriginBase += result;
					break;
				case 3: case 4:
					Character.Traits.Last().Effect=
						"The constant manipulation of your family's ancestry has rendered you a "+
						"mule, much to the disappointment of the Ordo Famulous. You gain the Chem "+
						"Geld talent.";
					Character.Talents.Add(new Talent("Chem Geld",Source.Origin));
					break;
				case 5: case 6:
					Character.Traits.Last().Effect=
						"You are a naturally gifted orator and dissembler but are unaccustomed to"+
						" not being the centre of attention. You gain the Talented (Deceive) Talent"+
						" and a -10 to all Concealment and Move Silent tests.";
					Character.Talents.Add(new Talent("Talented(Deceive)",Source.Origin));
					break;
				case 7: case 8:
					Character.Traits.Last().Effect=
						"Your health has been affected by an ancient genetic defect, but the "+
						"Sister Famulous nevertheless prepared you for your destiny. Lose 1 wound "+
						"and -5 Toughness but begin play with Forbidden Lore (Daemonology, Heresy, "+
						"Inquisition or Psykers).";
					Character.Wounds.OriginBase -= 1;
					Character.T.OriginBase -= 5;
					List<string> list = new List<string>();
					list.Add("Forbidden Lore(Daemonology)(Int)");
					list.Add("Forbidden Lore(Heresy)(Int)");
					list.Add("Forbidden Lore(Inquisition)(Int)");
					list.Add("Forbidden Lore(Pyskers)(Int)");
					DialogBoxList SkillDialog = new DialogBoxList(list);
					DialogResult TalentResult = SkillDialog.ShowDialog();
					Character.Skills.Add(new Skill(SkillDialog.selection,Source.Origin));
					break;
				case 9: case 10:
					Character.Traits.Last().Effect=
						"You are astonishingly comely externally but at the cost of hidden imperfections."+
						"Increase your Fellowship and Corruption points by 1d10 (roll only once and apply"+
						" the result to both).";
					result = Dice.Next(1,11);
					Character.Fel.OriginBase += result;
					Character.Corruption.OriginBase += result;
					break;
			}
		}
		private void ColdTraderChoice()
		{
			List<string> ChoiceList = new List<string>();
			ChoiceList.Add("Exotic Weapon Training(Needle Rifle) & Needle Rifle with 2 reloads");
			ChoiceList.Add("Exotic Weapon Training(Web Pistol) & Web Pistol with 2 reloads");
			ChoiceList.Add("Speak Language(Pick One Xenos) and a suit of Xeno Mesh armour");
			DialogBoxList ChoiceDialog = new DialogBoxList(ChoiceList);
			ChoiceDialog.ShowDialog();
			switch (ChoiceDialog.selection)
			{
				case "Exotic Weapon Training(Needle Rifle) & Needle Rifle w/ 2 reloads":
					Character.Talents.Add(new Talent("Exotic Weapon Training(Needle Rifle)", 
						Source.Background));
					Character.Gear.Add(new Equipment("Needle Rifle with 2 reloads",
						Source.Background));
					break;

				case "Exotic Weapon Training(Web Pistol) & Web Pistol with 2 reloads":
					Character.Talents.Add(new Talent("Exotic Weapon Training(Web Pistol)",
						Source.Background));
					Character.Gear.Add(new Equipment("Web Pistol with 2 reloads",
						Source.Background));
					break;

				case "Speak Language(Pick One Xenos) and Xeno Mesh":
					Character.Talents.Add(new Talent("Speak Language(Pick One Xenos)", 
						Source.Background));
					Character.Gear.Add(new Equipment("Xeno Mesh armour",
						Source.Background));
					break;
			}
		}
		private void HiveGangChoice()
		{
			List<string> list = new List<string>();
			list.Add("Awareness(Per)");
			list.Add("Dodge(Ag)");
			list.Add("Concealment(Ag)");
			list.Add("Gamble(Int)");
			list.Add("Evaluate(Int)");
			list.Add("Tech-Use(Int)");
			
			DialogBoxList ChoiceDialog = new DialogBoxList(list);
			ChoiceDialog.ShowDialog();

			Character.Skills.Add(new Skill(ChoiceDialog.selection, Source.Background));

			List<string> list1 = new List<string>();
			list1.Add("Awareness(Per)");
			list1.Add("Dodge(Ag)");
			list1.Add("Concealment(Ag)");
			list1.Add("Gamble(Int)");
			list1.Add("Evaluate(Int)");
			list1.Add("Tech-Use(Int)");
			for (int i = 0; i < list1.Count; i++)
			{
				if (list1.ElementAt(i) == ChoiceDialog.selection)
				{
					list1.RemoveAt(i);
				}
			}

			DialogBoxList ChoiceDialog1 = new DialogBoxList(list1);
			ChoiceDialog1.ShowDialog();

			Character.Skills.Add(new Skill(ChoiceDialog1.selection, Source.Background));
		}
		private void GuidingHandEmperor()
		{
			int result=0;
			switch (Dice.Next(1, 11))
			{
				case 1:
				case 2:
					Character.Traits.Last().Effect =
						"You have been blessed with visions of the Immortal Foe and given the knowledge" +
						" on how to defeat them, at the cost of their sanity. Gain Hatred(Daemons) " +
						"and 1d10 Insanity Points";
					Character.Talents.Add(new Talent("Hatred(Daemons)",
						Source.Background));
					result = Dice.Next(1, 11);
					Character.Insanity.BackMod += result;
					break;
				case 3:
				case 4:
					Character.Traits.Last().Effect =
						"The process has left the body broken, but the mind sharper. Reduce character's" +
						" Strength by 1d10, but increase Intelligence by the same amount";
					result = Dice.Next(1, 11);
					Character.S.BackMod -= result;
					Character.Int.BackMod += result;
					break;
				case 5:
				case 6:
					Character.Traits.Last().Effect =
						"The strength of the wards inscribed into your bones has filled you with the " +
						"Light of the Emperor. Unfortunately this inner fire steadily roasts your skin," +
						" which burns and sloughs off in large patches and heals slowly. Reduce " +
						"Fellowship by 1d10 and gain a Fate Point";
					result = Dice.Next(1, 11);
					Character.Fel.BackMod -= result;
					Character.Fate.BackMod += 1;
					break;
				case 7:
				case 8:
					Character.Traits.Last().Effect =
						"Your eyes are aglow with the power inscribed within, but this makes you a " +
						"beacon for any foe lashing out psychically. Gain Dark Sight, but are -10 to " +
						"any test to resist psychic powers used by non daemons";
					Character.Traits.Add(new Trait("Dark Sight",
					"See normally even in areas of total darkness and never takes a penalty in " +
					"dim or no lighting.", Source.Background));
					break;
				case 9:
				case 10:
					Character.Traits.Last().Effect =
						"The warding process left no outward markings upon the skin, and the only " +
						"visible sign of the process is a small mark on the tongue. The engraving " +
						"on your bones, however, is carefully designed to thwart the powers of a " +
						"specific warp entity. The GM chooses a daemonic entity and you are immune to " +
						"any fear or corruptive influence from this entity";
					break;
			}
		}
		#endregion

		private void CharacterUpdate()
		{
			#region Base Boxes
			WS_Base_Box.Text = Character.WS.Base.ToString();
			BS_Base_Box.Text = Character.BS.Base.ToString();
			S_Base_Box.Text = Character.S.Base.ToString();
			T_Base_Box.Text = Character.T.Base.ToString();
			Ag_Base_Box.Text = Character.Ag.Base.ToString();
			Int_Base_Box.Text = Character.Int.Base.ToString();
			Per_Base_Box.Text = Character.Per.Base.ToString();
			Wp_Base_Box.Text = Character.Wp.Base.ToString();
			Fel_Base_Box.Text = Character.Fel.Base.ToString();
			#endregion
			#region Roll Boxes
			WS_Roll_Box.Text = Character.WS.Roll.ToString();
			BS_Roll_Box.Text = Character.BS.Roll.ToString();
			S_Roll_Box.Text = Character.S.Roll.ToString();
			T_Roll_Box.Text = Character.T.Roll.ToString();
			Ag_Roll_Box.Text = Character.Ag.Roll.ToString();
			Int_Roll_Box.Text = Character.Int.Roll.ToString();
			Per_Roll_Box.Text = Character.Per.Roll.ToString();
			Wp_Roll_Box.Text = Character.Wp.Roll.ToString();
			Fel_Roll_Box.Text = Character.Fel.Roll.ToString();
			#endregion
			#region Total Boxes
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

			Wounds_Box.Text = Character.Wounds.Value.ToString();
			Character.CurWounds = Character.Wounds.Value;
			Fate_Box.Text = Character.Fate.Value.ToString();
			Character.CurFate = Character.Fate.Value;
			Insanity_Box.Text = Character.Insanity.Value.ToString();
			Corruption_Box.Text = Character.Corruption.Value.ToString();

			#region Tabs
			Traits_Box.Clear();
			foreach (Trait t in Character.Traits)
			{
				if (!Traits_Box.Text.Contains(t.Effect))
				{
					Traits_Box.AppendText(t.Name + "\n");
					Traits_Box.AppendText(t.Effect + "\n\n");
					Traits_Box.AppendText("\n");
				}
			}
			Talents_Box.Clear();
			foreach (Talent t in Character.Talents)
			{
				if (!Talents_Box.Text.Contains(t.Name))
				{
					Talents_Box.AppendText(t.Name + "\n");
				}
			}
			Skills_Box.Clear();
			foreach (Skill s in Character.Skills)
			{
				if (!Skills_Box.Text.Contains(s.Name))
				{
					Skills_Box.AppendText(s.Name + "\n");
				}
			}
			Gear_Box.Clear();
			foreach (Equipment e in Character.Gear)
			{
				Gear_Box.AppendText(e.Name + "\n");
			}
			#endregion

			Money_Box.Text = Character.Thrones.ToString();
			Income_Box.Text = Character.Income;

			#region Appearance
			Name_Box.Text = Character.Name;
			Sex_Box.Text = Character.Sex;
			Build_Box.Text = Character.Build;
			Age_Box.Text = Character.Age;
			Skin_Box.Text = Character.Skin;
			Hair_Box.Text = Character.Hair;
			Eye_Box.Text = Character.Eye;
			Quirk_Box.Text = Character.Quirk;
			#endregion

			Bio_Box.Text = Character.Bio;

		}

		private void Reset()
		{
			Character.Reset();

			//Origin_comboBox.ResetText();
			Career_comboBox.ResetText();
			Background_comboBox.ResetText();
			Name_comboBox.ResetText();

			Career_comboBox.Items.Clear();
			Background_comboBox.Items.Clear();
			Divination_Box.Clear();
			DivDel = delegate() { };

			CharacterUpdate();

			Roll_button.Enabled = false;
			Reroll_Disable();
		}
		private void BackReset()
		{
			Character.Background.Reset();

			#region Base Stats
			Character.WS.BackMod = 0;
			Character.BS.BackMod = 0;
			Character.S.BackMod = 0;
			Character.T.BackMod = 0;
			Character.Ag.BackMod = 0;
			Character.Int.BackMod = 0;
			Character.Per.BackMod = 0;
			Character.Wp.BackMod = 0;
			Character.Fel.BackMod = 0;
			#endregion

			Character.Wounds.BackMod = 0;
			Character.Fate.BackMod = 0;
			Character.Insanity.BackMod = 0;
			Character.Corruption.BackMod = 0;

			#region Lists
			#region Traits
			for (int i = 0; i < Character.Traits.Count; i++)
			{
				if (Character.Traits.ElementAt(i).Source == Source.Background)
				{
					Character.Traits.RemoveAt(i);
					i--;
				}
			}
			#endregion
			#region Talents
			for (int i = 0; i < Character.Talents.Count; i++)
			{
				if (Character.Talents.ElementAt(i).Source == Source.Background)
				{
					Character.Talents.RemoveAt(i);
					i--;
				}
			}
			#endregion
			#region Skills
			for (int i = 0; i < Character.Skills.Count; i++)
			{
				if (Character.Skills.ElementAt(i).Source == Source.Background)
				{
					Character.Skills.RemoveAt(i);
					i--;
				}
			}
			#endregion
			#region Gear
			bool gearRemoved=false;
			for (int i = 0; i < Character.Gear.Count; i++)
			{
				if (Character.Gear.ElementAt(i).Source == Source.Background)
				{
					Character.Gear.RemoveAt(i);
					gearRemoved = true;
					i--;
				}
			}
			if (gearRemoved)
			{
				Add_Career_Gear();
			}
			#endregion
			#endregion

			Add_Wealth();
		}
		private void CareerReset()
		{
			if (Character.Background.Name != "")
			{
				BackReset();
			}
			Character.Career.Reset();
			#region Base Stats
			Character.WS.CareerMod = 0;
			Character.BS.CareerMod = 0;
			Character.S.CareerMod = 0;
			Character.T.CareerMod = 0;
			Character.Ag.CareerMod = 0;
			Character.Int.CareerMod = 0;
			Character.Per.CareerMod = 0;
			Character.Wp.CareerMod = 0;
			Character.Fel.CareerMod = 0;
			#endregion

			Character.Wounds.CareerMod = 0;
			Character.Fate.CareerMod = 0;
			Character.Insanity.CareerMod = 0;
			Character.Corruption.CareerMod = 0;

			#region Lists
			#region Traits
			for (int i = 0; i < Character.Traits.Count; i++)
			{
				if (Character.Traits.ElementAt(i).Source == Source.Career)
				{
					Character.Traits.RemoveAt(i);
					i--;
				}
			}
			#endregion
			#region Talents
			for (int i = 0; i < Character.Talents.Count; i++)
			{
				if (Character.Talents.ElementAt(i).Source == Source.Career)
				{
					Character.Talents.RemoveAt(i);
					i--;
				}
			}
			#endregion
			#region Skills
			for (int i = 0; i < Character.Skills.Count; i++)
			{
				if (Character.Skills.ElementAt(i).Source == Source.Career)
				{
					Character.Skills.RemoveAt(i);
					i--;
				}
			}
			#endregion
			#region Gear
			for (int i = 0; i < Character.Gear.Count; i++)
			{
				if (Character.Gear.ElementAt(i).Source == Source.Career)
				{
					Character.Gear.RemoveAt(i);
					i--;
				}
			}
			#endregion
			#endregion
		}
		private void DivReset()
		{
			#region Base Stats
			Character.WS.DivMod = 0;
			Character.BS.DivMod = 0;
			Character.S.DivMod = 0;
			Character.T.DivMod = 0;
			Character.Ag.DivMod = 0;
			Character.Int.DivMod = 0;
			Character.Per.DivMod = 0;
			Character.Wp.DivMod = 0;
			Character.Fel.DivMod = 0;
			#endregion

			Character.Wounds.DivMod = 0;
			Character.Fate.DivMod = 0;
			Character.Insanity.DivMod = 0;
			Character.Corruption.DivMod = 0;

			#region Lists
			#region Traits
			for (int i = 0; i < Character.Traits.Count; i++)
			{
				if (Character.Traits.ElementAt(i).Source == Source.Divination)
				{
					Character.Traits.RemoveAt(i);
					i--;
				}
			}
			#endregion
			#region Talents
			for (int i = 0; i < Character.Talents.Count; i++)
			{
				if (Character.Talents.ElementAt(i).Source == Source.Divination)
				{
					Character.Talents.RemoveAt(i);
					i--;
				}
			}
			#endregion
			#region Skills
			for (int i = 0; i < Character.Skills.Count; i++)
			{
				if (Character.Skills.ElementAt(i).Source == Source.Divination)
				{
					Character.Skills.RemoveAt(i);
					i--;
				}
			}
			#endregion
			#region Gear
			for (int i = 0; i < Character.Gear.Count; i++)
			{
				if (Character.Gear.ElementAt(i).Source == Source.Divination)
				{
					Character.Gear.RemoveAt(i);
					i--;
				}
			}
			#endregion
			#endregion
		}

		private void UnDiv_Button_Click(object sender, EventArgs e)
		{
			DivReset();
			CharacterUpdate();
		}
	}
}
