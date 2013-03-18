using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarkHeresyCharacter
{
	[Serializable()]
	public class Advance
	{
		string Type;
		int Cost;
		string Name;

		public Advance()
		{
			Type = "";
			Cost = 0;
			Name = "";
		}
		public Advance(string type, int cost, string name)
		{
			Type = type;
			Cost = cost;
			Name = name;
		}
	}
}
