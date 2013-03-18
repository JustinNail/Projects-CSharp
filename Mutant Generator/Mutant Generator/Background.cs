using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarkHeresyCharacter
{
	[Serializable()]
	public class Background
	{
		public string Name { get; set; }
		public int Cost { get; set; }

		public Background()
		{
			Name = "";
			Cost = 0;
		}
		public Background(string name, int cost)
		{
			Name = name;
			Cost = cost;
		}

		public void Reset()
		{
			Name = "";
			Cost = 0;
		}
	}
}
