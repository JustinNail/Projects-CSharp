using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dark_Heresy_Generator
{
	[Serializable()]
	class Stat
	{
		public int Value 
		{ 
			get
			{
				return Base + Roll;
			}
		}
		public int Base { get; set; }
		public int Roll { get; set; }
		public Stat( int b, int r)
		{
			Base = b;
			Roll = r;
		}

		public void Reset()
		{
			Base = 0;
			Roll = 0;
		}
	}
}
