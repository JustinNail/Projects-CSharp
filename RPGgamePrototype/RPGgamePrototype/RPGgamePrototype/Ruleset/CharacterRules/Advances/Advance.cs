using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RPGgamePrototype.Ruleset;
using RPGgamePrototype.Ruleset.CharacterRules.CharacterAttributes;
using RPGgamePrototype.Ruleset.CharacterRules.CharacterAttributes.Skills;
using RPGgamePrototype.Ruleset.CharacterRules.CharacterAttributes.Characteristics;

namespace RPGgamePrototype.Ruleset.CharacterRules.Advances
{
	public abstract partial class Advance:CharacterAttribute
	{
		int xpPrice;
		public int XpPrice { get { return xpPrice; } }
		CharacterAttribute type;

		public Advance(string name, int xpPrice, CharacterAttribute type)
			: base(name)
		{
			this.xpPrice = xpPrice;
			this.type = type;
		}

		public virtual void Take(Character c)
		{
			if(type is Skill)
			{
				
			}
		}
	}
}
