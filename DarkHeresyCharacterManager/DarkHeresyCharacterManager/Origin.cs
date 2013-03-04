using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarkHeresyCharacterManager
{
	[Serializable()]
	class Origin
	{
		public string Name { get; set; }
		public string Base { get; set; }

		public Origin(string n, string b)
		{
			Name = n;
			Base = b;
		}
		public void Reset()
		{
			Name = "";
			Base = "";
		}
	}
}
