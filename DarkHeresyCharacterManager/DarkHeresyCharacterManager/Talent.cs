using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarkHeresyCharacter
{
	[Serializable()]
	public class Talent
	{
		public string Name { get; set; }
		public Source Source { get; set; }

		public Talent()
		{
			Name = "";
		}
		public Talent(string n, Source s)
		{
			Name = n;
			Source = s;
		}
	}
}
