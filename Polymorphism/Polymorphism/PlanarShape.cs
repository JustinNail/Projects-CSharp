using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polymorphism
{
	class PlanarShape
	{
		public int X { get; private set; }
		public int Y { get; private set; }

		public virtual void Draw()
		{
			Console.WriteLine("Performing base class drawing tasks");
		}
	}
}
