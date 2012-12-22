using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InheritanceExample
{
	class ParentClass
	{
		string parent_string;
		public ParentClass()
		{
			Console.WriteLine("Parent Constructor");
		}
		public ParentClass(string my_string)
		{
			parent_string = my_string;
			Console.WriteLine("In Parent Constructor: " + parent_string);
		}
		public void Print()
		{
			Console.WriteLine("Print in Parent class");
		}
	}
}
