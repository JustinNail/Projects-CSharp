using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RPGgamePrototype.Ruleset;
using RPGgamePrototype.Ruleset.CharacterRules.CharacterAttributes;
using RPGgamePrototype.Ruleset.CharacterRules.CharacterAttributes.Skills;
using RPGgamePrototype.Ruleset.CharacterRules.CharacterAttributes.Characteristics;

namespace RPGgamePrototype.Ruleset.CharacterRules.CharacterAttributes.Characteristics
{
	public class Characteristic:CharacterAttribute
	{
		#region Fields and Properties
		
		int min;
		int max;
		int trueValue;

		public int TempMod{get; set;}
		public int Value
		{
			get 
			{
				int value = trueValue + TempMod;
				if(value<min)
				{
					value=min;
				}
				if(value>max)
				{
					value=max;
				}
				return value;
			}
		}
		#endregion

		#region Constructors
		public Characteristic(string name, int initValue, int min, int max)
			:base(name)
		{
			this.min = min;
			this.max = max;

			TempMod = 0;

			if (initValue < min)
			{
				initValue = min;
			}
			if (initValue > max)
			{
				initValue = max;
			}
			trueValue = initValue;

		}

		public Characteristic(string name, int initValue)
			:base(name)
		{
			min = 0;
			max = 100;

			TempMod = 0;

			if (initValue < min)
			{
				initValue = min;
			}
			if (initValue > max)
			{
				initValue = max;
			}
			trueValue = initValue;
		}
		#endregion

		#region Methods
		public void ChangePermenant(int Change)
		{
			trueValue += Change;
		}
		#endregion

	}
}
