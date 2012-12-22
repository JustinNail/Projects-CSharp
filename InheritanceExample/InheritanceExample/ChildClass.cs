using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InheritanceExample
{
	class ChildClass : ParentClass
	{
		public ChildClass()
			: base("From Derived Class")
		{
			Console.WriteLine("Child Constructor");
		}
		internal void Print()
		{
			Console.WriteLine("Print in Child Class");
		}
	}
}
