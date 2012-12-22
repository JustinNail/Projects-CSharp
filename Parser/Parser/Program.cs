using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace Parser
{
	class Program
	{
		static void Main(string[] args)
		{
			//Check the arguments
			if (args.Length != 1)
			{
				Console.WriteLine("1 Commandline Argument Expected");
				Console.ReadKey();
				return;
			}

			string s = "";

			//try to read in the file
			try
			{
				//begin reading from file
				StreamReader sr = new StreamReader(args[0]);
				
				//read the entire file into a string
				s += sr.ReadToEnd();
			}
			catch (Exception e)
			{
				//display errors to screen
				Console.WriteLine("File could not be read");
				Console.WriteLine(e.Message);
				return;
			}

			//get the tokens
			List<string> tokens = Tokenizer.getTokens(s);

			//If PROGRAM returns true, then it has been parsed correctly
			if (Rules.PROGRAM(ref tokens))
			{
				Console.WriteLine("SUCCESS");
			}
			else
			{
				Console.WriteLine("FAILURE");
			}

			Console.ReadKey();
		}
	}
}
