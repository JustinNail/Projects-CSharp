using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dark_Heresy_Generator
{
	class Origin
	{
		public string Name { get; set; }
		public string Base { get; set; }

		public void Reset()
		{
			Name = "";
			Base = "";
		}
	}
}
