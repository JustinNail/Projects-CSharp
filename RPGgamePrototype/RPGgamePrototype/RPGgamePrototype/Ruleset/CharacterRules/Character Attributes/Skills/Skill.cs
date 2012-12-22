using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RPGgamePrototype.Ruleset;
using RPGgamePrototype.Ruleset.CharacterRules.CharacterAttributes;
using RPGgamePrototype.Ruleset.CharacterRules.CharacterAttributes.Skills;
using RPGgamePrototype.Ruleset.CharacterRules.CharacterAttributes.Characteristics;

namespace RPGgamePrototype.Ruleset.CharacterRules.CharacterAttributes.Skills
{
	public class Skill:CharacterAttribute
	{
		#region Fields and Properties
		bool advanced;
		
		bool trained;
		public bool Trained { get { return trained; } }

		int trainingLevel;
		public int TrainingLevel { get { return trainingLevel; } }

		int modifier;

		Characteristic baseCharacteristic;

		
		public int Modifier
		{
			get { return modifier; }
			set 
			{
				if (value > 30)
				{
					modifier = 30;
				}
				else if (value < -30)
				{
					modifier = -30;
				}
				else
				{
					modifier = value;
				}
			}
		}
		public int Value
		{
			get
			{
				if (trained)
				{
					return baseCharacteristic.Value + (trainingLevel * 10) + modifier;
				}
				else
				{
					if (advanced)
					{
						return 0;
					}
					else
					{
						return (baseCharacteristic.Value / 2) + modifier;
					}
				}
			}
		}
		#endregion

		#region Constructors
		public Skill(string name, Characteristic baseCharacteristic, bool advanced)
			:base(name)
		{
			this.baseCharacteristic = baseCharacteristic;
			this.advanced = advanced;
			trained = false;
			trainingLevel = 0;
			Modifier = 0;
		}
		#endregion

		#region Methods
		public void Train()
		{
			if (!trained)
			{
				trained = true;
				return;
			}
			else
			{
				trainingLevel++;
				if (trainingLevel > 2)
				{
					trainingLevel = 2;
				}
			}
		}
		#endregion

	}
}
