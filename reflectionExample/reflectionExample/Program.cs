using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;

namespace reflectionExample
{
	class Program
	{
		struct A
		{
			public int Field1;
			public string Field2;
			public double Field3;
		}
		class B
		{
			public int FieldNumber1;
			public string FieldNumber2;
			public double FieldNumber3;

			public void MyMethod()
			{
				Console.WriteLine("Greetings from class B");
			}
		}
		static void Main(string[] args)
		{
			int i = 5;
			System.Type type = i.GetType();
			System.Console.WriteLine("The type is: " + type.ToString());
			Console.WriteLine();

			A a_instance = new A() { Field1 = 1, Field2 = "2 is a string", Field3 = 3.0 };
			DisplayStructureClass<A>(a_instance);
			Console.WriteLine();

			B b_instance = new B() { FieldNumber1 = 4, FieldNumber2 = "5 is a string", FieldNumber3 = 6.0 };
			DisplayStructureClass<B>(b_instance);

			Console.ReadKey();
		}

		private static void DisplayStructureClass<T>(T the_structure_class)
		{
			Type the_type = the_structure_class.GetType();
			Console.WriteLine("The Type is: " + the_type.ToString());

			FieldInfo[] fields = the_type.GetFields
			(
				BindingFlags.Instance | BindingFlags.Public
			);

			foreach (FieldInfo field in fields)
			{
				Console.WriteLine("Field name is {0}", field.Name);
				Console.WriteLine("Field value is {0}", field.GetValue(the_structure_class));
			}
			MethodInfo[] methods = the_type.GetMethods
			(
				BindingFlags.DeclaredOnly 
				| BindingFlags.Public
				| BindingFlags.Instance
			);
			foreach(MethodInfo method in methods)
			{
				Console.WriteLine(method.Name);
				method.Invoke(the_structure_class, null);
			}
		}
		
	}
}
