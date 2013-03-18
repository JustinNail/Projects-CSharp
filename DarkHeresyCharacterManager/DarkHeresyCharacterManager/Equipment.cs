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
		public string Source { get; set; }
		public Equipment()
		{
			Name = "";
			Source = "";
		}
		public Equipment(string n, string s)
		{
			Name = n;
			Source = s;
		}
	}
}
