using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;

namespace DaemonhostGenerator
{
	public partial class Form1 : Form
	{
		Random Dice = new Random();
		public Form1()
		{
			InitializeComponent();
			Binding_comboBox.Items.Add("Unbound");
			Binding_comboBox.Items.Add("Once-bound");
			Binding_comboBox.Items.Add("Twice-bound");
			Binding_comboBox.Items.Add("Thrice-bound");
		}

		private void Binding_comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			Reset();
			switch (Binding_comboBox.SelectedItem.ToString())
			{
				case "Unbound":
					UnboundRoll();
					break;
				case "Once-bound":
					OnceBoundRoll();
					break;
				case "Twice-bound":
					TwiceBoundRoll();
					break;
				case "Thrice-bound":
					ThriceBoundRoll();
					break;
			}
			AddMinorPowers();
		}

		private void Reset()
		{
			WS_Box.Clear();
			BS_Box.Clear();
			S_Box.Clear();
			T_Box.Clear();
			Ag_Box.Clear();
			Int_Box.Clear();
			Per_Box.Clear();
			Wp_Box.Clear();
			Fel_Box.Clear();

			Wounds_Box.Clear();

			Traits_Box.Clear();
			Skills_Box.Clear();

			MinorPower_Box.Clear();
			MajorPower_Box.Clear();
		}

		private void DaemonPhenomena(int roll)
		{
			if (roll >= 1 && roll <= 4)
			{
				Traits_Box.AppendText("...people will feel nauseous.");
			}
			else if (roll >= 5 && roll <= 8)
			{
				Traits_Box.AppendText("...people perceive a sickly sweet smell.");
			}
			else if (roll >= 9 && roll <= 12)
			{
				Traits_Box.AppendText("...everyone tastes gritty ashes.");
			}
			else if (roll >= 13 && roll <= 16)
			{
				Traits_Box.AppendText("...people’s noses begin to bleed.");
			}
			else if (roll >= 17 && roll <= 20)
			{
				Traits_Box.AppendText("...the sound of scuttling can be heard.");
			}
			else if (roll >= 21 && roll <= 24)
			{
				Traits_Box.AppendText("...there is a smell of burnt paper and hot metal.");
			}
			else if (roll >= 25 && roll <= 28)
			{
				Traits_Box.AppendText("...a single high-pitched note can be heard.");
			}
			else if (roll >= 29 && roll <= 32)
			{
				Traits_Box.AppendText("...liquid falls upwards in drops, pooling on the ceiling.");
			}
			else if (roll >= 33 && roll <= 36)
			{
				Traits_Box.AppendText("...snatches of sound without connection can be heard.");
			}
			else if (roll >= 37 && roll <= 40)
			{
				Traits_Box.AppendText("...plants wither and die, food and drink spoils.");
			}
			else if (roll >= 41 && roll <= 44)
			{
				Traits_Box.AppendText("...the air is hot, as if standing in front of a furnace.");
			}
			else if (roll >= 45 && roll <= 48)
			{
				Traits_Box.AppendText("...the air is filled with the smell of ozone.");
			}
			else if (roll >= 49 && roll <= 52)
			{
				Traits_Box.AppendText("...there is a smell of burning flesh.");
			}
			else if (roll >= 53 && roll <= 56)
			{
				Traits_Box.AppendText("...paint peels as if burnt, metal rusts and wood rots. Once the Daemonhost has passed everything returns to normal.");
			}
			else if (roll >= 57 && roll <= 60)
			{
				Traits_Box.AppendText("...the air is filled with the smell of blood.");
			}
			else if (roll >= 61 && roll <= 64)
			{
				Traits_Box.AppendText("...the buzzing of flies can be heard, though none can be seen.");
			}
			else if (roll >= 65 && roll <= 68)
			{
				Traits_Box.AppendText("...shadows flicker and distort, seemingly to the silhouettes of strange figures.");
			}
			else if (roll >= 69 && roll <= 72)
			{
				Traits_Box.AppendText("...lights dim, candles snuff out, a gloom spreads.");
			}
			else if (roll >= 73 && roll <= 76)
			{
				Traits_Box.AppendText("...strange things are seen out of the corner of people’s eyes.");
			}
			else if (roll >= 77 && roll <= 80)
			{
				Traits_Box.AppendText("...the air is cold, breath forms in the air and surfaces are covered with a sheen of frost.");
			}
			else if (roll >= 81 && roll <= 84)
			{
				Traits_Box.AppendText("...a cold breeze swirls and screaming can be heard distantly.");
			}
			else if (roll >= 85 && roll <= 88)
			{
				Traits_Box.AppendText("...sparks arc across metal and ghostly radiance glimmers from flesh.");
			}
			else if (roll >= 89 && roll <= 92)
			{
				Traits_Box.AppendText("...muttering familiar voices can be heard dimly.");
			}
			else if (roll >= 93 && roll <= 96)
			{
				Traits_Box.AppendText("...everyone feels things scuttling across their skin; when they look, there is nothing there.");
			}
			else
			{
				Traits_Box.AppendText("...the taste of bile and of blood fills people’s mouths.");
			}
		}
		private void UnholyChanges(int roll)
		{
			if (roll >= 1 && roll <= 4)
			{
				Traits_Box.AppendText("Vestigial Horns:\nTwo small horns project from the " +
					"Daemonhost’s forehead.\n\n");
			}
			else if (roll >= 5 && roll <= 8)
			{
				Traits_Box.AppendText("Glowing Eyes:\nThe Daemonhost’s eyes glow with a" +
					" malign light.\n\n");
			}
			else if (roll >= 9 && roll <= 12)
			{
				Traits_Box.AppendText("Boils:\nThe flesh of the Daemonhost is covered with " +
					"weeping boils and sores.\n\n");
			}
			else if (roll >= 13 && roll <= 16)
			{
				Traits_Box.AppendText("Cat’s Eyes:\nThe Daemonhost’s eyes are yellow and the " +
					"pupils are black slits.\n\n");
			}
			else if (roll >= 17 && roll <= 20)
			{
				Traits_Box.AppendText("Horns:\nA set of horns, like those of a ram or goat, " +
					"spring from the Daemonhost’s head. It gains the Natural Weapon (Horns) trait.\n\n");
				int SB = int.Parse(S_Box.Text) / 10;
				Traits_Box.AppendText("Natural Weapon(Horns)\n 1d10 + SB, Primitive\n\n");
			}
			else if (roll >= 21 && roll <= 24)
			{
				Traits_Box.AppendText("Claws or Blades: \n The Daemonhost’s fingers are elongated and" +
					" sharpened into razor claws, or fused into blades as sharp as sin. It gains the " +
					"Natural Weapon (Claws) trait with the Warp Weapon quality.\n\n");
				int SB = int.Parse(S_Box.Text) / 10;
				Traits_Box.AppendText("Natural Weapon(Claws) \n 1d10 + SB, Warp Weapon\n\n");
			}
			else if (roll >= 25 && roll <= 28)
			{
				Traits_Box.AppendText("Broken Form \nThe Daemonhost’s body is permanently contorted," +
					" its body folded in unnatural ways. \n\n");
			}
			else if (roll >= 29 && roll <= 32)
			{
				Traits_Box.AppendText("Snake Tongue \nA long forked tongue flicks from between" +
					" the Daemonhost’s teeth. \n\n");
			}
			else if (roll >= 33 && roll <= 36)
			{
				Traits_Box.AppendText("Fluid Form \nThe Daemonhost’s flesh flows continually: limbs," +
					" screaming faces and other terrible things pushing out from its body before " +
					"being reabsorbed. \n\n");
			}
			else if (roll >= 37 && roll <= 40)
			{
				Traits_Box.AppendText("Wings \nGreat wings of feathers or stretched skin have sprung" +
					" from the Daemonhost’s back. It gains the Flyer trait at a rate equal to twice its" +
					" Agility Bonus. \n\n");
				Traits_Box.AppendText("Flyer(AB x2) \nCan fly \n\n");
			}
			else if (roll >= 41 && roll <= 44)
			{
				Traits_Box.AppendText("Bleeding Mouth and Eyes \nThe Daemonhost’s eyes continually" +
					" weep blood, while viscous gore seeps from between its lips. \n\n");
			}
			else if (roll >= 45 && roll <= 48)
			{
				Traits_Box.AppendText("Covered in Eyes \nThe Daemonhost’s flesh is covered in eyes." +
					" The Daemonhost gains a +20 bonus to Awareness Tests involving vision.\n\n");
			}
			else if (roll >= 49 && roll <= 52)
			{
				Traits_Box.AppendText("Quills \nThe Daemonhost’s flesh has sprouted long avian " +
					"quills. It gains the Natural Armour 2 trait. \n\n");
				Traits_Box.AppendText("Natural Armour 2 \n2 Armour Points on all locations \n\n");
			}
			else if (roll >= 53 && roll <= 56)
			{
				Traits_Box.AppendText("Un-fleshed \nThe Daemonhost has no skin, its glistening " +
					"muscles and sinews are exposed.\n\n");
			}
			else if (roll >= 57 && roll <= 60)
			{
				Traits_Box.AppendText("Inner Fire \nAn unearthly fire burns within the Daemonhost," +
					" glowing through its flesh, veins and skin. Its natural attacks inflict " +
					"Energy Damage.\n\n");
			}
			else if (roll >= 61 && roll <= 64)
			{
				Traits_Box.AppendText("Bloated Form \nThe host body is grossly bloated. \n\n");
			}
			else if (roll >= 65 && roll <= 68)
			{
				Traits_Box.AppendText("Snake Nest \nSnakes coil around the Daemonhost, flowing from" +
					" its mouth and rents in its flesh. Whenever it deals Damage, the target must" +
					" succeed on a Hard (-20) Toughness Test or be affected as if by a hallucinogen" +
					" grenade for 1d5 Rounds. \n\n");
			}
			else if (roll >= 69 && roll <= 72)
			{
				Traits_Box.AppendText("Insect Hive \nThe Daemonhost’s body is a hive for a mass of" +
					" insects that crawl across it. It gains 1d10 Wounds.\n\n");
				Wounds_Box.Text = (int.Parse(Wounds_Box.Text) + Dice.Next(1, 11)).ToString();
			}
			else if (roll >= 73 && roll <= 76)
			{
				Traits_Box.AppendText("Corpse-Host \nThe host’s body has expired during the ritual" +
					" and is visibly rotting—yet it still lives! It increases its Toughness by 1d10.\n\n");
				T_Box.Text = (int.Parse(T_Box.Text) + Dice.Next(1, 11)).ToString();
			}
			else if (roll >= 77 && roll <= 80)
			{
				Traits_Box.AppendText("Elongated limbs \nThe Daemonhost’s limbs are distorted and " +
					"elongated. The Daemonhost can attack creatures up to three metres away in " +
					"close combat.\n\n");
			}
			else if (roll >= 81 && roll <= 84)
			{
				Traits_Box.AppendText("Scales \nThe Daemonhost’s body is covered in a fine layer" +
					" of snake-like scales. It gains the Natural Armour 3 trait. \n\n");
				Traits_Box.AppendText("Natural Armour 2 \n2 Armour Points on all locations \n\n");
			}
			else if (roll >= 85 && roll <= 88)
			{
				Traits_Box.AppendText("Animalistic \nThe Daemonhost’s body has bestial features," +
					" such as the head of a goat, bull or avian, backwards-jointed limbs or fur.\n\n");
			}
			else if (roll >= 89 && roll <= 92)
			{
				Traits_Box.AppendText("Featureless Face \nThough it has no effect on its senses," +
					" the Daemonhost’s head is smooth, featureless flesh.\n\n");

			}
			else if (roll >= 93 && roll <= 96)
			{
				Traits_Box.AppendText("Charred Form \nThe host body appears horrifically burnt." +
					" The Daemonhost is immune to all forms of fire and heat damage, even psychic " +
					"fire (but not holy flame).\n\n");
			}
			else
			{
				Traits_Box.AppendText("Seeming Normality \nIf you roll this result, do not roll" +
					" any further on this table. Also, any results already rolled are removed. " +
					"The host body seems perfectly normal, apart from the instruments of its " +
					"binding. It adds 1d10 to its Fellowship. \n\n");
				Fel_Box.Text = (int.Parse(Fel_Box.Text) + Dice.Next(1, 11)).ToString();
			}

		}

		private void UnboundRoll()
		{
			UnboundStats();

			#region Unholy Changes
			int numrolls = Dice.Next(1, 6) + 1;
			List<int> rolls = new List<int>();
			while (true)
			{
				int roll = Dice.Next(1, 101);
				if (roll >= 97)
				{
					rolls.Clear();
					rolls.Add(roll);
					break;
				}
				else
				{
					if (!rolls.Contains(roll))
					{
						rolls.Add(roll);
					}
					if (rolls.Count >= numrolls)
					{
						break;
					}
				}
				
			}
			foreach (int roll in rolls)
			{
				UnholyChanges(roll);
			}
			#endregion

			#region Phenomena
			int radius = int.Parse(Wp_Box.Text) / 10;
			Traits_Box.AppendText("Daemonic Phenomena: \nEveryone in "+radius.ToString()+" experiences the following"+
				" phenomena and take -10 on all Willpower Tests\n");
			rolls = new List<int>();
			while (true)
			{
				int roll = Dice.Next(1, 101);
				
				if (!rolls.Contains(roll))
				{
					rolls.Add(roll);
				}
				if (rolls.Count >= 4)
				{
					break;
				}
				

			}
			foreach (int roll in rolls)
			{
				DaemonPhenomena(roll);
				Traits_Box.AppendText("\n");
			}
			Traits_Box.AppendText("\n");
			#endregion

			#region Skills
			Skills_Box.AppendText("Awareness (Per) +10\n");
			Skills_Box.AppendText("Deceive (Fel) +20\n");
			Skills_Box.AppendText("Forbidden Lore (Daemonology, Heresy, Warp, etc) (Int)+20\n");
			Skills_Box.AppendText("Psyniscience (Per) +20\n");
			Skills_Box.AppendText("Secret Tongue (any) (Int) +20\n");
			Skills_Box.AppendText("Speak Language (all) (Int) +20\n");
			#endregion

			#region Traits
			Traits_Box.AppendText("Daemonic \nx2 TB, except from Force Weapons, Psychic Powers, "+
				"Holy attacks, and other Daemonic creatures. Immune to Poison and Disease\n\n");
			Traits_Box.AppendText("Dark Sight \nSee normally in Darkness \n\n");
			Traits_Box.AppendText("Fear 4 \nTest Wp(-30) or roll on Shock Table\n\n");
			if(!Traits_Box.Text.Contains("Flyer"))
			{
				Traits_Box.AppendText("Flyer (AB) \nCan fly\n\n");
			}
			Traits_Box.AppendText("From Beyond \nImmune to Fear, Pinning, Insanity Points, and"+
				" Psychic Power used to cloud, control, or delude its mind\n\n");
			if(!Traits_Box.Text.Contains("Natural Weapon(Claws)"))
			{
				Traits_Box.AppendText("Natural Weapon (Claws/Fangs) \n1d10 + SB, Primitive \n\n");
			}
			Traits_Box.AppendText("Unnatural Strength (×3) \nStrength Bonus x3 \n\n");
			#endregion
		}
		private void UnboundStats()
		{
			WS_Box.Text = (20 + Dice.Next(1,11) + Dice.Next(1,11)).ToString();
			BS_Box.Text = (20 + Dice.Next(1,11) + Dice.Next(1,11)).ToString();
			S_Box.Text = (45 + Dice.Next(1,11) + Dice.Next(1,11)).ToString();
			T_Box.Text = (30 + Dice.Next(1,11) + Dice.Next(1,11)).ToString();
			Ag_Box.Text = (10 + Dice.Next(1,11) + Dice.Next(1,11)).ToString();
			Int_Box.Text = (70 + Dice.Next(1,11) + Dice.Next(1,11)).ToString();
			Per_Box.Text = (50 + Dice.Next(1,11) + Dice.Next(1,11)).ToString();
			Wp_Box.Text = (80 + Dice.Next(1,11) + Dice.Next(1,11)).ToString();
			Fel_Box.Text = (5 + Dice.Next(1,11) + Dice.Next(1,11)).ToString();
			Wounds_Box.Text = "30";
			PsyRating.Text = "8";
		}

		private void OnceBoundRoll()
		{
			OnceBoundStats();

			#region Unholy Changes
			int numrolls = Dice.Next(1, 6);
			List<int> rolls = new List<int>();
			while (true)
			{
				int roll = Dice.Next(1, 101);
				if (roll >= 97)
				{
					rolls.Clear();
					rolls.Add(roll);
					break;
				}
				else
				{
					if (!rolls.Contains(roll))
					{
						rolls.Add(roll);
					}
					if (rolls.Count >= numrolls)
					{
						break;
					}
				}

			}
			foreach (int roll in rolls)
			{
				UnholyChanges(roll);
			}
			#endregion

			#region Phenomena
			int radius = int.Parse(Wp_Box.Text) / 10;
			Traits_Box.AppendText("Daemonic Phenomena: \nEveryone in " + radius.ToString() + " experiences the following" +
				" phenomena and take -10 on all Willpower Tests\n");
			rolls = new List<int>();
			while (true)
			{
				int roll = Dice.Next(1, 101);
				
				if (!rolls.Contains(roll))
				{
					rolls.Add(roll);
				}
				if (rolls.Count >= 3)
				{
					break;
				}

			}
			foreach (int roll in rolls)
			{
				DaemonPhenomena(roll);
				Traits_Box.AppendText("\n");
			}
			Traits_Box.AppendText("\n");
			#endregion

			#region Skills
			Skills_Box.AppendText("Awareness (Per) +10\n");
			Skills_Box.AppendText("Deceive (Fel) +20\n");
			Skills_Box.AppendText("Forbidden Lore (Daemonology, Heresy, Warp, etc) (Int)+20\n");
			Skills_Box.AppendText("Psyniscience (Per) +20\n");
			Skills_Box.AppendText("Secret Tongue (any) (Int) +20\n");
			Skills_Box.AppendText("Speak Language (all) (Int) +20\n");
			#endregion

			#region Traits
			Traits_Box.AppendText("Daemonic \nx2 TB, except from Force Weapons, Psychic Powers, " +
				"Holy attacks, and other Daemonic creatures. Immune to Poison and Disease\n\n");
			Traits_Box.AppendText("Dark Sight \nSee normally in Darkness \n\n");
			Traits_Box.AppendText("Fear 4 \nTest Wp(-30) or roll on Shock Table\n\n");
			if (!Traits_Box.Text.Contains("Flyer"))
			{
				Traits_Box.AppendText("Flyer (AB) \nCan fly\n\n");
			}
			Traits_Box.AppendText("From Beyond \nImmune to Fear, Pinning, Insanity Points, and" +
				" Psychic Power used to cloud, control, or delude its mind\n\n");
			if (!Traits_Box.Text.Contains("Natural Weapon(Claws)"))
			{
				Traits_Box.AppendText("Natural Weapon (Claws/Fangs) \n1d10 + SB, Primitive \n\n");
			}
			Traits_Box.AppendText("Unnatural Strength (×3) \nStrength Bonus x3 \n\n");
			#endregion
		}
		private void OnceBoundStats()
		{
			WS_Box.Text = (20 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			BS_Box.Text = (20 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			S_Box.Text = (45 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			T_Box.Text = (35 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			Ag_Box.Text = (10 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			Int_Box.Text = (70 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			Per_Box.Text = (50 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			Wp_Box.Text = (70 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			Fel_Box.Text = (5 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			Wounds_Box.Text = "30";
			PsyRating.Text = "6";
		}

		private void TwiceBoundRoll()
		{
			TwiceBoundStats();

			#region Unholy Changes
			int numrolls = Dice.Next(1, 6)-1;
			if (numrolls < 1)
			{
				numrolls = 1;
			}
			List<int> rolls = new List<int>();
			while (true)
			{
				int roll = Dice.Next(1, 101);
				if (roll >= 97)
				{
					rolls.Clear();
					rolls.Add(roll);
					break;
				}
				else
				{
					if (!rolls.Contains(roll))
					{
						rolls.Add(roll);
					}
					if (rolls.Count >= numrolls)
					{
						break;
					}
				}

			}
			foreach (int roll in rolls)
			{
				UnholyChanges(roll);
			}
			#endregion

			#region Phenomena
			int radius = int.Parse(Wp_Box.Text) / 10;
			Traits_Box.AppendText("Daemonic Phenomena: \nEveryone in " + radius.ToString() + " experiences the following" +
				" phenomena and take -10 on all Willpower Tests\n");
			rolls = new List<int>();
			while (true)
			{
				int roll = Dice.Next(1, 101);
				
				if (!rolls.Contains(roll))
				{
					rolls.Add(roll);
				}
				if (rolls.Count >= 2)
				{
					break;
				}
				

			}
			foreach (int roll in rolls)
			{
				DaemonPhenomena(roll);
				Traits_Box.AppendText("\n");
			}
			Traits_Box.AppendText("\n");
			#endregion

			#region Skills
			Skills_Box.AppendText("Awareness (Per) +10\n");
			Skills_Box.AppendText("Deceive (Fel) +20\n");
			Skills_Box.AppendText("Forbidden Lore (Daemonology, Heresy, Warp, etc) (Int)+20\n");
			Skills_Box.AppendText("Psyniscience (Per) +20\n");
			Skills_Box.AppendText("Secret Tongue (any) (Int) +20\n");
			Skills_Box.AppendText("Speak Language (all) (Int) +20\n");
			#endregion

			#region Traits
			Traits_Box.AppendText("Daemonic \nx2 TB, except from Force Weapons, Psychic Powers, " +
				"Holy attacks, and other Daemonic creatures. Immune to Poison and Disease\n\n");
			Traits_Box.AppendText("Dark Sight \nSee normally in Darkness \n\n");
			Traits_Box.AppendText("Fear 4 \nTest Wp(-30) or roll on Shock Table\n\n");
			if (!Traits_Box.Text.Contains("Flyer"))
			{
				Traits_Box.AppendText("Flyer (AB) \nCan fly\n\n");
			}
			Traits_Box.AppendText("From Beyond \nImmune to Fear, Pinning, Insanity Points, and" +
				" Psychic Power used to cloud, control, or delude its mind\n\n");
			if (!Traits_Box.Text.Contains("Natural Weapon(Claws)"))
			{
				Traits_Box.AppendText("Natural Weapon (Claws/Fangs) \n1d10 + SB, Primitive \n\n");
			}
			Traits_Box.AppendText("Unnatural Strength (×2) \nStrength Bonus x2 \n\n");
			#endregion
		}
		private void TwiceBoundStats()
		{
			WS_Box.Text = (20 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			BS_Box.Text = (20 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			S_Box.Text = (40 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			T_Box.Text = (40 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			Ag_Box.Text = (10 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			Int_Box.Text = (70 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			Per_Box.Text = (50 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			Wp_Box.Text = (60 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			Fel_Box.Text = (5 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			Wounds_Box.Text = "30";
			PsyRating.Text = "5";
		}

		private void ThriceBoundRoll()
		{
			ThriceBoundStats();

			#region Unholy Changes
			int numrolls = Dice.Next(1, 6) - 2;
			if (numrolls < 1)
			{
				numrolls = 1;
			}
			List<int> rolls = new List<int>();
			while (true)
			{
				int roll = Dice.Next(1, 101);
				if (roll >= 97)
				{
					rolls.Clear();
					rolls.Add(roll);
					break;
				}
				else
				{
					if (!rolls.Contains(roll))
					{
						rolls.Add(roll);
					}
					if (rolls.Count >= numrolls)
					{
						break;
					}
				}

			}
			foreach (int roll in rolls)
			{
				UnholyChanges(roll);
			}
			#endregion

			#region Phenomena
			int radius = int.Parse(Wp_Box.Text) / 10;
			Traits_Box.AppendText("Daemonic Phenomena: \nEveryone in " + radius.ToString() + " experiences the following" +
				" phenomena and take -10 on all Willpower Tests\n");
			DaemonPhenomena(Dice.Next(1, 101));
			Traits_Box.AppendText("\n\n");
			#endregion

			#region Skills
			Skills_Box.AppendText("Awareness (Per) +10\n");
			Skills_Box.AppendText("Deceive (Fel) +20\n");
			Skills_Box.AppendText("Forbidden Lore (Daemonology, Heresy, Warp, etc) (Int)+20\n");
			Skills_Box.AppendText("Psyniscience (Per) +20\n");
			Skills_Box.AppendText("Secret Tongue (any) (Int) +20\n");
			Skills_Box.AppendText("Speak Language (all) (Int) +20\n");
			#endregion

			#region Traits
			Traits_Box.AppendText("Daemonic \nx2 TB, except from Force Weapons, Psychic Powers, " +
				"Holy attacks, and other Daemonic creatures. Immune to Poison and Disease\n\n");
			Traits_Box.AppendText("Dark Sight \nSee normally in Darkness \n\n");
			Traits_Box.AppendText("Fear 4 \nTest Wp(-30) or roll on Shock Table\n\n");
			if (!Traits_Box.Text.Contains("Flyer"))
			{
				Traits_Box.AppendText("Flyer (AB) \nCan fly\n\n");
			}
			Traits_Box.AppendText("From Beyond \nImmune to Fear, Pinning, Insanity Points, and" +
				" Psychic Power used to cloud, control, or delude its mind\n\n");
			if (!Traits_Box.Text.Contains("Natural Weapon(Claws)"))
			{
				Traits_Box.AppendText("Natural Weapon (Claws/Fangs) \n1d10 + SB, Primitive \n\n");
			}
			Traits_Box.AppendText("Unnatural Strength (×2) \nStrength Bonus x2 \n\n");
			#endregion
		}
		private void ThriceBoundStats()
		{
			WS_Box.Text = (20 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			BS_Box.Text = (20 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			S_Box.Text = (35 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			T_Box.Text = (40 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			Ag_Box.Text = (10 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			Int_Box.Text = (70 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			Per_Box.Text = (50 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			Wp_Box.Text = (50 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			Fel_Box.Text = (5 + Dice.Next(1, 11) + Dice.Next(1, 11)).ToString();
			Wounds_Box.Text = "30";
			PsyRating.Text = "4";
		}

		private void AddMinorPowers()
		{
			MinorPower_Box.AppendText("Call Creatures  PT:9  Full Action\n");
			MinorPower_Box.AppendText("Call Item  PT:5  Half Action  \n");
			MinorPower_Box.AppendText("Chameleon  PT:  Half Action  Sustained\n");
			MinorPower_Box.AppendText("Cipher Seed PT:9 Full Action \n");
			MinorPower_Box.AppendText("Déjà vu  PT:8  Half Action  \n");
			MinorPower_Box.AppendText("Death's Messenger  PT:15  Half Action\n");
			MinorPower_Box.AppendText("Disintigrating Directive PT:6 Full Action \n");
			MinorPower_Box.AppendText("Distort Vision  PT:8  Free Action  \n");
			MinorPower_Box.AppendText("Distortion PT:6 Full Action Sustained\n");
			MinorPower_Box.AppendText("Dull Pain  PT:8  Half Action\n");
			MinorPower_Box.AppendText("Endure Flames PT:8 Half Action Sustained\n");
			MinorPower_Box.AppendText("Familiar Bond PT:10 Full Action \n");
			MinorPower_Box.AppendText("Fearful Aura  PT:7  Full Action  Sustained\n");
			MinorPower_Box.AppendText("Flash Bang  PT:6  Half Action  \n");
			MinorPower_Box.AppendText("Float  PT:8  Half Action  Sustained\n");
			MinorPower_Box.AppendText("Forget Me  PT:6  Half Action  \n");
			MinorPower_Box.AppendText("Haywire PT:10 Full Action \n");
			MinorPower_Box.AppendText("Healer  PT:7  Full Action  \n");
			MinorPower_Box.AppendText("Inflict Pain  PT:8  Half Action  Sustained\n");
			MinorPower_Box.AppendText("Inspiring Aura  PT:6  Full Action  Sustained\n");
			MinorPower_Box.AppendText("Knack  PT:7  Half Action  \n");
			MinorPower_Box.AppendText("Lucky  PT:6  Half Action  \n");
			MinorPower_Box.AppendText("Mark of Flesh  PT:13  Full Action Sustained\n");
			MinorPower_Box.AppendText("Mutable Features PT:8 Full Action Sustained\n");
			MinorPower_Box.AppendText("Open Wounds PT:9 Half Action \n");
			MinorPower_Box.AppendText("Precognition  PT:6  Half Action  Sustained\n");
			MinorPower_Box.AppendText("Psychic Stench  PT:5  Half Action  \n");
			MinorPower_Box.AppendText("Resist Possession  PT:6  Reaction  \n");
			MinorPower_Box.AppendText("Sense Mechanism PT:7 Half Action Sustained\n");
			MinorPower_Box.AppendText("Sense Presence  PT:7  Half Action  Sustained\n");
			MinorPower_Box.AppendText("Space Slip PT:11 Half Action \n");
			MinorPower_Box.AppendText("Spasm  PT:7  Half Action  \n");
			MinorPower_Box.AppendText("Spectral Hands  PT:10  Full Action  \n");
			MinorPower_Box.AppendText("Staunch Bleeding  PT:8  Half Action  \n");
			MinorPower_Box.AppendText("Suggestion PT:9 Half Action \n");
			MinorPower_Box.AppendText("Time Fade PT:13 Full Action\n");
			MinorPower_Box.AppendText("Torch  PT:5  Half Action  Sustained\n");
			MinorPower_Box.AppendText("Touch of Madness  PT:11  Full Action  \n");
			MinorPower_Box.AppendText("Trick  PT:5  Half Action  Sustained\n");
			MinorPower_Box.AppendText("Trusting Aura PT:7 Full Action Sustained\n");
			MinorPower_Box.AppendText("Truth Seeker PT:6 Full Action Sustained\n");
			MinorPower_Box.AppendText("Twitch PT:5 Half Action Sustained\n");
			MinorPower_Box.AppendText("Unnatural Aim  PT:8  Half Action  \n");
			MinorPower_Box.AppendText("Wall Walk  PT:8  Half Action  Sustained\n");
			MinorPower_Box.AppendText("Warp Howl  PT:8  Full Action  \n");
			MinorPower_Box.AppendText("Weaken Veil  PT:9  Full Action  Sustained\n");
			MinorPower_Box.AppendText("Weapon Jinx  PT:8  Full Action  \n");
			MinorPower_Box.AppendText("Whispers of the Warp  PT:11  Half Action\n");
			MinorPower_Box.AppendText("White Noise  PT:8  Full Action  Sustained\n");
			MinorPower_Box.AppendText("Wither  PT:6  Full Action  \n");
			MinorPower_Box.AppendText("Without a Trace PT:6 Half Action Sustained\n");

		}
		private void AddMajorPowers(int num)
		{
			List<string> Powers = new List<string>();
			#region Powers
			Powers.Add("Agony PT:13 Full Action Sustained");
			Powers.Add("Beastmaster  PT:13  Half Action  Sustained");
			Powers.Add("Bio-lightning  PT:14  Half Action");
			Powers.Add("Blinding Flash  PT:11  Half Action  ");
			Powers.Add("Blood Boil  PT:19  Half Action Sustained");
			Powers.Add("Burning Fist  PT:10  Half Action  Sustained");
			Powers.Add("Call Flame  PT:8  Half Action  Sustained");
			Powers.Add("Catch Projectiles PT:16  Reaction  ");
			Powers.Add("Cellular Control  PT:16  Half Action Sustained");
			Powers.Add("Compel  PT:17  Half Action  ");
			Powers.Add("Constrict  PT:13  Half Action");
			Powers.Add("Create Door PT:28 Full Action ");
			Powers.Add("Daemon Wrack PT:23 Full Action ");
			Powers.Add("Disease PT:20 Half Action ");
			Powers.Add("Divine Shot  PT:15  Free Action  ");
			Powers.Add("Dominate  PT:24  Half Action  Sustained");
			Powers.Add("Douse Flames  PT:16  Half Action  Sustained");
			Powers.Add("Dowsing  PT:11  Full Action  Sustained");
			Powers.Add("Drain Vigour PT:20 Half Action Sustained");
			Powers.Add("Enhance Senses  PT:10  Half Action Sustained");
			Powers.Add("Exsanguine PT:18 Half Action ");
			Powers.Add("Far Sight  PT:17  Full Action  Sustained");
			Powers.Add("Fire Bolt  PT:11  Half Action  ");
			Powers.Add("Fire Storm  PT:16  Half Action  ");
			Powers.Add("Flail of Skulls PT:14 Half Action Sustained");
			Powers.Add("Flaming Word PT:18 Half Action ");
			Powers.Add("Flesh Like Iron PT:18 Half Action Sustained");
			Powers.Add("Fling  PT:14  Half Action  ");
			Powers.Add("Force Barrage  PT:21  Full Action  ");
			Powers.Add("Force Bolt  PT:13  Half Action  ");
			Powers.Add("Glimpse  PT:18  Half Action  ");
			Powers.Add("Hammerhand  PT:15  Full Action Sustained");
			Powers.Add("Hellish Blast PT:22 Half Action ");
			Powers.Add("Holocaust  PT:23  Full Action  Sustained");
			Powers.Add("Immunity PT:24 Half Action ");
			Powers.Add("Incinerate  PT:19  Full Action  Sustained");
			Powers.Add("Inspire  PT:9  Half Action  Sustained");
			Powers.Add("Leach Life PT:21 Full Action ");
			Powers.Add("Living Weapon PT:24 Full Action Sustained");
			Powers.Add("Malefic Curse PT:(16 Hex, 20 Blindness, 23 Death) Full Action ");
			Powers.Add("Mind Scan  PT:23  Full Action  ");
			Powers.Add("Molten Man PT:27 Full Action Sustained");
			Powers.Add("Open PT:14 Half Action ");
			Powers.Add("Personal Augury PT:14  Full Action  ");
			Powers.Add("Precision Telekinesis PT:23  Half Action  Sustained");
			Powers.Add("Precognitive Dodge PT:11  Free Action  ");
			Powers.Add("Precognitive Strike PT:17  Free Action  ");
			Powers.Add("Preternatural Awareness PT:9  Half Action  Sustained");
			Powers.Add("Projection  PT:21  Full Action  Sustained");
			Powers.Add("Psy Barrier PT:19 Half Action ");
			Powers.Add("Psychic Blade  PT:19  Half Action  Sustained");
			Powers.Add("Psychic Crush  PT:17  Half Action  ");
			Powers.Add("Psychic Shriek  PT:18  Half Action  ");
			Powers.Add("Psychokinetic Storm PT:12 Half Action Sustained");
			Powers.Add("Psychometry  PT:16  Full Action  Sustained");
			Powers.Add("Push  PT:13  Half Action  ");
			Powers.Add("Regenerate  PT:23  Full Action Sustained");
			Powers.Add("Sculpt Flame  PT:13  Half Action  Sustained");
			Powers.Add("Seal Wounds  PT:10  Half Action");
			Powers.Add("See Me t  PT:14  Half Action  Sustained");
			Powers.Add("Seed Mind PT:26 Extended ");
			Powers.Add("Shape Flesh  PT:19  Full Action Sustained");
			Powers.Add("Soul Killer PT:25 Full Action ");
			Powers.Add("Soul Sight  PT:23  Full Action  Sustained");
			Powers.Add("Summon Object PT:24 Full Action Sustained");
			Powers.Add("Telekinesis  PT:11  Half Action  Sustained");
			Powers.Add("Telekinetic Shield PT:17  Half Action  Sustained");
			Powers.Add("Telepathy  PT:11  Free Action  Sustained");
			Powers.Add("Terrify  PT:13  Half Action  ");
			Powers.Add("Toxic Siphon  PT:11  Half Action");
			Powers.Add("Transfix PT:19 Half Action ");
			Powers.Add("Wall of Fire  PT:17  Full Action  Sustained");
			Powers.Add("Wall of Souls PT:24 Full Action Sustained");
			Powers.Add("Warp Corruption PT:27 Full Action ");
			Powers.Add("Warp Lightning PT:21 Half Action ");
			Powers.Add("Warp Tongue PT:16 Half Action ");
			Powers.Add("Warp Vigour PT:14 Half Action ");
			Powers.Add("Zone of Compulsion PT:19 Half Action ");
			#endregion

			for(int i = 0; i < num; i++)
			{
				int roll = Dice.Next(Powers.Count);
				MajorPower_Box.AppendText(Powers.ElementAt(roll) + "\n");
				Powers.RemoveAt(roll);
			}
		}

		private void Reset_Button_Click(object sender, EventArgs e)
		{
			Reset();
		}

		private void Powers_Button_Click(object sender, EventArgs e)
		{
			MajorPower_Box.Clear();
			int num = 0;
			switch (Binding_comboBox.SelectedItem.ToString())
			{
				case "Unbound":
					num = Dice.Next(1, 6) + 4;
					break;
				case "Once-bound":
					num = Dice.Next(1, 6) + 2;
					break;
				case "Twice-bound":
					num = Dice.Next(1, 6) + 1;
					break;
				case "Thrice-bound":
					num = Dice.Next(1, 6);
					break;
			}
			AddMajorPowers(num);
		}

		private void Export_Button_Click(object sender, EventArgs e)
		{
			Stream ExportStream;
			SaveFileDialog saveFileDialog1 = new SaveFileDialog();

			saveFileDialog1.FileName = Binding_comboBox.SelectedItem.ToString()+" Daemonhost.txt";
			saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
			saveFileDialog1.FilterIndex = 1;
			saveFileDialog1.RestoreDirectory = true;

			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				if ((ExportStream = saveFileDialog1.OpenFile()) != null)
				{
					StreamWriter sw = new StreamWriter(ExportStream);
					sw.WriteLine("{0} Daemonhost", Binding_comboBox.SelectedItem.ToString());
					sw.WriteLine("===============================");
					sw.WriteLine("WS {0}", WS_Box.Text);
					sw.WriteLine("BS {0}", BS_Box.Text);
					sw.WriteLine("S {0}", S_Box.Text);
					sw.WriteLine("T {0}", T_Box.Text);
					sw.WriteLine("Ag {0}", Ag_Box.Text);
					sw.WriteLine("Int {0}", Int_Box.Text);
					sw.WriteLine("Per {0}", Per_Box.Text);
					sw.WriteLine("Wp {0}", Wp_Box.Text);
					sw.WriteLine("Fel {0}", Fel_Box.Text);
					sw.WriteLine();
					sw.WriteLine("Wounds {0}", Wounds_Box.Text);
					sw.WriteLine("===============================");
					sw.WriteLine(Traits_Box.Text);
					sw.WriteLine("===============================");
					sw.WriteLine(Skills_Box.Text);
					sw.WriteLine("===============================");
					sw.WriteLine("Psy Rating {0}", PsyRating.Text);
					sw.WriteLine("Major Powers:");
					sw.WriteLine(MajorPower_Box.Text);
					sw.WriteLine("===============================");
					sw.WriteLine("Minor Powers:");
					sw.WriteLine(MinorPower_Box.Text);
					sw.Close();
					ExportStream.Close();
				}
			}
		}
	}
}
