using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RPGgamePrototype.Ruleset;
using RPGgamePrototype.Ruleset.CharacterRules.Advances;
using RPGgamePrototype.Ruleset.CharacterRules.CharacterAttributes;
using RPGgamePrototype.Ruleset.CharacterRules.CharacterAttributes.Skills;
using RPGgamePrototype.Ruleset.CharacterRules.CharacterAttributes.Talents;
using RPGgamePrototype.Ruleset.CharacterRules.CharacterAttributes.Characteristics;

namespace RPGgamePrototype.Ruleset
{
	public class Character
	{
		List<Characteristic> characteristics;
		public List<Characteristic> Characteristics { get { return characteristics; } }

		List<Skill> skills;
		public List<Skill> Skills { get { return skills; } }

		List<Talent> talents;
		public List<Talent> Talents { get { return talents; } }

		List<Advance> takenAdvances;
		public List<Advance> TakenAdvances { get { return takenAdvances; } }
	}
}
