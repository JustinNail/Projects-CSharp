using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarkHeresyCharacterManager
{
	[Serializable()]
	class Skill
	{
		public string Name { get; set; }
		public string Source { get; set; }

		public Skill(string n, string s)
		{
			Name = n;
			Source = s;
		}
	}
}
