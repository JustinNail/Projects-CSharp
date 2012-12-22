using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RPGgamePrototype.Ruleset;
using RPGgamePrototype.Ruleset.CharacterRules.CharacterAttributes;
using RPGgamePrototype.Ruleset.CharacterRules.CharacterAttributes.Skills;
using RPGgamePrototype.Ruleset.CharacterRules.CharacterAttributes.Characteristics;

namespace RPGgamePrototype.Ruleset.CharacterRules.CharacterAttributes.Talents
{
	public class Talent:CharacterAttribute
	{
		List<CharacterAttribute> prerequisites;
		Character ownerRef;

		public Talent(string name, Character c, List<CharacterAttribute> prereqs )
			: base(name)
		{
			ownerRef = c;
			prerequisites = prereqs;
		}

		private bool MeetsPrereqs()
		{
			if (prerequisites == null)//no prereqs
			{
				return true;
			}
			foreach (CharacterAttribute prereq in prerequisites)
			{
				if (prereq is Characteristic)
				{
					foreach (Characteristic c in ownerRef.Characteristics)
					{
						if (prereq.Name == c.Name)
						{
							return ((Characteristic)prereq).Value >= c.Value;
						}
					}
				}
				if (prereq is Talent)
				{
					foreach (Talent t in ownerRef.Talents)
					{
						if (prereq.Name == t.Name)
						{
							return true;
						}
					}
				}
				if (prereq is Skill)
				{
					foreach (Skill s in ownerRef.Skills)
					{
						if (prereq.Name == s.Name)
						{
							return s.Trained && s.TrainingLevel >= ((Skill)prereq).TrainingLevel;
						}
					}
				}
			}
			return false;
		}
	}
}
