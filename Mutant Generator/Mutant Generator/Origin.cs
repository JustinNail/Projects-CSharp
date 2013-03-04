using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dark_Heresy_Generator
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
