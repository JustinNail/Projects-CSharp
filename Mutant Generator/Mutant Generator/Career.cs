using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarkHeresyCharacter
{
	[Serializable()]
	public class Career
	{
		public string Name { get; set; }
		public string Base { get; set; }

		public Career()
		{
			Name = "";
			Base = "";
		}
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
