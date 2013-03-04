using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarkHeresyCharacterManager
{
	[Serializable()]
	class Equipment
	{
		public string Name { get; set; }
		public string Source { get; set; }
		public Equipment(string n, string s)
		{
			Name = n;
			Source = s;
		}
	}
}
