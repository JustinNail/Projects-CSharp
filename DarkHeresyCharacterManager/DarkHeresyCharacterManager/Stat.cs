using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarkHeresyCharacter
{
	[Serializable()]
	public class Stat
	{
		public int Value 
		{ 
			get
			{
				return Base + Roll;
			}
		}
		public int Base
		{
			get
			{
				return OriginBase + CareerMod + BackMod + DivMod;
			}
		}
		public int OriginBase { get; set; }
		public int CareerMod { get; set; }
		public int BackMod { get; set; }
		public int DivMod { get; set; }
		public int Roll { get; set; }
		public Stat()
		{
			OriginBase = 0;
			CareerMod = 0;
			BackMod = 0;
			DivMod = 0;
			Roll = 0;
		}
		public void Reset()
		{
			OriginBase = 0;
			CareerMod = 0;
			BackMod = 0;
			DivMod = 0;
			Roll = 0;
		}
	}
}
