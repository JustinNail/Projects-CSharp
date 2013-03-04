using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dark_Heresy_Generator
{
	[Serializable()]
	class Background
	{
		public string Name { get; set; }
		public string Cost { get; set; }

		public Background(string n, string c)
		{
			Name = n;
			Cost = c;
		}

		public void Reset()
		{
			Name = "";
			Cost = "";
		}
	}
}
