using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarkHeresyCharacter
{
	[Serializable()]
	public class Origin
	{
		public string Name { get; set; }
		public string Base { get; set; }

		public Origin()
		{
			Name = "";
			Base = "";
		}
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
