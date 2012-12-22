using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polymorphism
{
	class Triangle:PlanarShape
	{
		public override void Draw()
		{
			Console.WriteLine("Drawing a Triangle");
			base.Draw();
		}
	}
}
