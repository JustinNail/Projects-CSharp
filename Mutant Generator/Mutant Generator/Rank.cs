using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarkHeresyCharacter
{
	[Serializable()]
	public class Rank
	{
		public string Name { get; set; }
		public int Num { get; set; }
		public List<Advance> Advances;

		public Rank()
		{
			Name = "";
			Num = 0;
			Advances = new List<Advance>();
		}
		public Rank(string name, int num, List<Advance> advances)
		{
			Name = name;
			Num = num;
			Advances = advances;
		}
	}
}
