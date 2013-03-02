using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
		}

		private void UnboundRoll()
		{
			UnboundStats();
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
		}

		private void UnholyChanges(int roll)
		{
			if(roll >= 1 && roll <= 4)
			{Vestigial Horns: Two small horns project from the Daemonhost’s forehead.

			}
			else if(roll >= 5 && roll <= 8)
			{Glowing Eyes: The Daemonhost’s eyes glow with a malign light.

			}
			else if(roll >= 9 && roll <= 12)
			{Boils: The flesh of the Daemonhost is covered with weeping boils and sores.

			}
			else if(roll >= 13 && roll <= 16)
			{Cat’s Eyes: The Daemonhost’s eyes are yellow and the pupils are black slits.

			}
			else if(roll >= 17 && roll <= 20)
			{Horns: A set of horns, like those of a ram or goat, spring from the Daemonhost’s head. It gains the Natural Weapon (Horns) trait.

			}
			else if(roll >= 21 && roll <= 24)
			{Claws or Blades: The Daemonhost’s fingers are elongated and sharpened into razor claws, or fused into blades as sharp as sin. It gains the 
   Natural Weapon (Claws) trait with the Warp Weapon quality.

			}
			else if(roll >= 25 && roll <= 28)
			{Broken Form: The Daemonhost’s body is permanently contorted, its body folded in unnatural ways.

			}
			else if(roll >= 29 && roll <= 32)
			{Snake Tongue: A long forked tongue flicks from between the Daemonhost’s teeth.

			}
 			else if(roll >= 33 && roll <= 36)
			{Fluid Form: The Daemonhost’s flesh flows continually: limbs, screaming faces and other terrible things pushing out from its body before 
   being reabsorbed.

			}
			else if(roll >= 37 && roll <= 40)
			{Wings: Great wings of feathers or stretched skin have sprung from the Daemonhost’s back. It gains the Flyer trait at a rate equal to twice its 
   Agility Bonus.

			}
			else if(roll >= 41 && roll <= 44)
			{Bleeding Mouth and Eyes: The Daemonhost’s eyes continually weep blood, while viscous gore seeps from between its lips.

			}
			else if(roll >= 45 && roll <= 48)
			{Covered in Eyes: The Daemonhost’s flesh is covered in eyes. The Daemonhost gains a +20 bonus to Awareness Tests involving vision.

			}
			else if(roll >= 49 && roll <= 52)
			{Quills: The Daemonhost’s flesh has sprouted long avian quills. It gains the Natural Armour 2 trait.

			}
			else if(roll >= 53 && roll <= 56)
			{Un-fleshed: The Daemonhost has no skin, its glistening muscles and sinews are exposed.

			}
			else if(roll >= 57 && roll <= 60)
			{Inner Fire: An unearthly fire burns within the Daemonhost, glowing through its flesh, veins and skin. Its natural attacks inflict Energy 
   Damage.

			}
			else if(roll >= 61 && roll <= 64)
			{Bloated Form: The host body is grossly bloated.

			}
			else if(roll >= 65 && roll <= 68)
			{Snake Nest: Snakes coil around the Daemonhost, flowing from its mouth and rents in its flesh. Whenever it deals Damage, the target must 
   succeed on a Hard (-20) Toughness Test or be affected as if by a hallucinogen grenade for 1d5 Rounds.

			}
			else if(roll >= 69 && roll <= 72)
			{Insect Hive: The Daemonhost’s body is a hive for a mass of insects that crawl across it. It gains 1d10 Wounds.

			}
			else if(roll >= 73 && roll <= 76)
			{Corpse-Host: The host’s body has expired during the ritual and is visibly rotting—yet it still lives! It increases its Toughness by 1d10.

			}
			else if(roll >= 77 && roll <= 80)
			{Elongated limbs: The Daemonhost’s limbs are distorted and elongated. The Daemonhost can attack creatures up to three metres away in 
   close combat.

			}
			else if(roll >= 81 && roll <= 84)
			{Scales: The Daemonhost’s body is covered in a fine layer of snake-like scales. It gains the Natural Armour 3 trait.

			}
			else if(roll >= 85 && roll <= 88)
			{Animalistic: The Daemonhost’s body has bestial features, such as the head of a goat, bull or avian, backwards-jointed limbs or fur.

			}
			else if(roll >= 89 && roll <= 92)
			{Featureless Face: Though it has no effect on its senses, the Daemonhost’s head is smooth, featureless flesh.

			}
			else if(roll >= 93 && roll <= 96)
			{Charred Form: The host body appears horrifically burnt. The Daemonhost is immune to all forms of fire and heat damage, even psychic 
   fire (but not holy flame).

			}
			else
			{97–00  Seeming Normality: If you roll this result, do not roll any further on this table. Also, any results already rolled are removed. The host body 
   seems perfectly normal, apart from the instruments of its binding. It adds 1d10 to its Fellowship.
			}
				
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
			Talents_Box.AppendText("Psy Rating 8");
		}

		private void OnceBoundRoll()
		{
			throw new NotImplementedException();
		}

		private void TwiceBoundRoll()
		{
			throw new NotImplementedException();
		}

		private void ThriceBoundRoll()
		{
			throw new NotImplementedException();
		}
	}
}
