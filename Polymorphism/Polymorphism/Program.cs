using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polymorphism
{
	class Program
	{
		static void Main(string[] args)
		{
			List<PlanarShape> shapes = new List<PlanarShape>();
			shapes.Add(new Rectangle());
			shapes.Add(new Triangle());
			shapes.Add(new Circle());
			
			foreach(PlanarShape s in shapes)
			{
				s.Draw();
				Console.WriteLine();
			}
			Console.ReadKey();
		}
	}
}
