using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dark_Heresy_Generator
{
	class Background
	{
		public string Name { get; set; }
		public string Cost { get; set; }

		public void Reset()
		{
			Name = "";
			Cost = "";
		}
	}
}
