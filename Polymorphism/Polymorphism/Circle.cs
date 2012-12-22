using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polymorphism
{
	class Circle:PlanarShape
	{
		public override void Draw()
		{
			Console.WriteLine("Drawing a circle");
			base.Draw();
		}
	}
}
