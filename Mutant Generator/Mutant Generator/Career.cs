using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dark_Heresy_Generator
{
	[Serializable()]
	class Career
	{
		public string Name { get; set; }
		public string Base { get; set; }

		public Career(string n, string b)
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
