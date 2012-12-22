using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RPGgamePrototype.Ruleset;
using RPGgamePrototype.Ruleset.CharacterRules;

namespace RPGgamePrototype.Ruleset.CharacterRules.CharacterAttributes
{
	public abstract partial class CharacterAttribute
	{
		string name;
		public string Name { get { return name; } }

		public CharacterAttribute(string name)
		{
			this.name = name;
		}

		
	}
}
