using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarkHeresyCharacter
{
	[Serializable()]
	public class Equipment
	{
		public string Name { get; set; }
		public Source Source { get; set; }
		public Equipment()
		{
			Name = "";
		}
		public Equipment(string n, Source s)
		{
			Name = n;
			Source = s;
		}
	}
}
