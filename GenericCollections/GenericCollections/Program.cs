using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericCollections
{
	class Program
	{
		class student
		{
			public int ID;
			public string Name;
			public student(int ID, string Name)
			{
				this.ID = ID;
				this.Name = Name;
			}
		}
		static void Main(string[] args)
		{
			List<string> string_list = new List<string>();
			string_list.Add("String #1");
			string_list.Add("String #2");
			string_list.Add("String #3");
			for (int i = 0; i < string_list.Count; i++)
			{
				Console.WriteLine(string_list[i]);
			}
			Console.WriteLine();

			Dictionary<int, student> student_dictionary = new Dictionary<int, student>();
			student student1 = new student(1, "Student #1");
			student student2 = new student(2, "Student #2");
			student student3 = new student(3, "Student #3");
			student_dictionary.Add(student1.ID, student1);
			student_dictionary.Add(student2.ID, student2);
			student_dictionary.Add(student3.ID, student3);
			foreach (KeyValuePair<int, student> student_entry in student_dictionary)
			{
				Console.WriteLine("ID={0}, Name={1}", student_entry.Key, student_entry.Value.Name);
			}
			Console.WriteLine();

			Stack<student> student_stack = new Stack<student>();
			student_stack.Push(student1);
			student_stack.Push(student2);
			student_stack.Push(student3);
			while (student_stack.Count > 0)
			{
				student s = student_stack.Pop();
				Console.WriteLine("ID={0}, Name={1}", s.ID,s.Name);
			}
			Console.WriteLine();

			Queue<student> student_queue = new Queue<student>();
			student_queue.Enqueue(student1);
			student_queue.Enqueue(student2);
			student_queue.Enqueue(student3);
			while (student_queue.Count > 0)
			{
				student s = student_queue.Dequeue();
				Console.WriteLine("ID={0}, Name={1}", s.ID, s.Name);
			}
			Console.WriteLine();

			LinkedList<student> student_linked_list = new LinkedList<student>();
			LinkedListNode<student> current_student = student_linked_list.AddFirst(student2);
			current_student = student_linked_list.AddAfter(current_student, student3);
			current_student = student_linked_list.AddAfter(current_student, student1);
			foreach (student s in student_linked_list)
			{
				Console.WriteLine("ID={0}, Name={1}", s.ID, s.Name);
			}
			Console.WriteLine();
			Console.ReadKey();
		}
	}
}
