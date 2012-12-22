using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InheritanceExample
{
	class Program
	{
		static void Main(string[] args)
		{
			SimpleChildClass simple_child = new SimpleChildClass();
			simple_child.Print();
			Console.WriteLine();

			ChildClass child = new ChildClass();
			child.Print();
			((ParentClass)child).Print();
			Console.WriteLine();

			Console.ReadKey();
		}
	}
}
