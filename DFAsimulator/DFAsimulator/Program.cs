using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DFAsimulator
{
	class Program
	{
		static void Main(string[] args)
		{
			DFA M = new DFA();//initialize the DFA

			//prompt for filename
			Console.WriteLine("Input filename");
			string filename = Console.ReadLine();
			
			M.ReadDFA(filename);//converts from file to the DFA states

			//prompt for input
			Console.WriteLine("Input test string, or quit");
			string input = Console.ReadLine();

			while (input.ToLower() != "quit")
			{
				//M.ReadString returns true if after reading 
				//the string, M is in a final state
				if (M.ReadString(input.ToLower()))
				{
					Console.WriteLine("String Accepted");
				}
				else
				{
					Console.WriteLine("String Rejected");
				}
				Console.WriteLine("Input test string, or quit");
				input = Console.ReadLine();
			}			
		}
	}
}
