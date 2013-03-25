using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarkHeresyCharacter
{
	[Serializable()]
	public class Skill
	{
		public string Name { get; set; }
		public Source Source { get; set; }

		public Skill()
		{
			Name = "";
		}
		public Skill(string n, Source s)
		{
			Name = n;
			Source = s;
		}
	}
}
