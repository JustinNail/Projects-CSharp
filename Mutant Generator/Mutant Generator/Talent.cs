using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dark_Heresy_Generator
{
	class Talent
	{
		public string Name { get; set; }
		public string Source { get; set; }

		public Talent(string n, string s)
		{
			Name = n;
			Source = s;
		}
	}
}
