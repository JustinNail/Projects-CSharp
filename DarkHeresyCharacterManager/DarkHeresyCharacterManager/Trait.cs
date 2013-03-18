using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarkHeresyCharacter
{
	[Serializable()]
	public class Trait
	{
		public string Name { get; set; }
		public string Effect { get; set; }
		public string Source { get; set; }

		public Trait()
		{
			Name = "";
			Effect = "";
			Source = "";
		}
		public Trait(string n, string e, string s)
		{
			Name = n;
			Effect = e;
			Source = s;
		}
	}
}
