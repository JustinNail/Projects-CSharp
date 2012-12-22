using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polymorphism
{
	class Rectangle:PlanarShape
	{
		public override void Draw()
		{
			Console.WriteLine("Drawing a Rectangle");
			base.Draw();
		}
	}
}
