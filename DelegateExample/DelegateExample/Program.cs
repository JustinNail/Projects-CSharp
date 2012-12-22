using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegateExample
{
	class Program
	{
		delegate void MyDelegate(string s1, string s2);

		static void Main(string[] args)
		{
			MyClass my_class = new MyClass();
			my_class.Method1("a", "b");
			my_class.Method2("a", "b");
			AnotherMethod("a", "b");
			Console.WriteLine();

			MyDelegate md1 = my_class.Method1;
			MyDelegate md2 = my_class.Method2;
			MyDelegate mda = AnotherMethod;

			md1("c", "d");
			md2("c", "d");
			mda("c", "d");
			Console.WriteLine();

			MyDelegate[] delegate_array = new MyDelegate[]{md1, md2, mda};
			foreach (MyDelegate md in delegate_array)
			{
				md("e", "f");
			}
			Console.WriteLine();

			MyDelegate delegate_sum = md1 + md2;
			delegate_sum("g", "h");
			delegate_sum += mda;
			delegate_sum("g", "h");
			Console.WriteLine();

			UseDelegate("i", "j", delegate_sum);

			Console.ReadKey();
		}
		static void AnotherMethod(string string1, string string2)
		{
			Console.WriteLine("AnotherMethod {0} + {1} = {2}",
				string1, string2, string1 + string2);
		}
		class MyClass
		{
			public void Method1(string string1, string string2)
			{
				Console.WriteLine("MyClass.Method1 {0} + {1} = {2}",
					string1, string2, string1 + string2);
			}

			public void Method2(string string1, string string2)
			{
				Console.WriteLine("MyClass.Method2 {0} + {1} = {2}",
					string1, string2, string1 + string2);
			}
		}
		static void UseDelegate(string s1, string s2, MyDelegate the_delegate)
		{
			the_delegate(s1, s2);
		}
	}
}
