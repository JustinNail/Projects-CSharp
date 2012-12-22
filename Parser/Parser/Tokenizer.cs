using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parser
{
	class Tokenizer
	{
		//arrays holding the special tokens
		static string[] SpecialTokens = { "BEGIN", "END", "READ", "WRITE", ":=" };
		static string[] Punctuation = { "(", ")", ",", ";", "+", "-" };

		public static List<string> getTokens(string s)
		{
			List<string> tokens = new List<string>();//List to hold the tokens

			string[] split = s.Split();//splits on whitespace, returning an array with the tokens

			//Empty strings left over by .Split mark end of lines
			for (int i = 0; i < split.Count(); i++)
			{
				if (split[i] == "")
				{
					split[i] = "<EOL>";//Turn them into <EOL> tokens for line counting
				}
			}
			foreach (string x in split)
			{
				string temp = x;//lets me modify 'x' via 'temp'

				//string is not a Special Token or Punctuation (or there was no whitespace between tokens)
				if (!SpecialTokens.Contains(temp) && !Punctuation.Contains(temp))
				{

					if (temp.Trim() != "")
					{
						//extract all special tokens "embedded" amongst identifiers and integers
						tokens.AddRange(ExtractTokens(temp));
					}
				}

				//x is a Special Token or Punctuation
				else
				{
					tokens.Add(temp);//doesn't need extraction, just add to token list
				}
			}

			//remove surpuflous "" from the extraction
			for (int i = 0; i < tokens.Count; i++)
			{
				if (tokens[i] == "")
				{
					tokens.RemoveAt(i);
				}
			}

			//re-insert empty strings(NULL's) to know when lists end
			for (int i = 0; i < tokens.Count; i++)
			{
				if (tokens[i] == ")")//in front of every )
				{
					
					tokens.Insert(i, "");
					i++;
				}
				if (tokens[i].ToUpper() == "WRITE")//in front of ,'s in EXPR_LIST's 
				{
					
					int j;
					for (j = i+1; j<tokens.Count && tokens[j] != ";"; j++)
					{
						
						if (tokens[j] == ",")
						{
							tokens.Insert(j, "");
							j++;
						}
						if (tokens[j] == ")")
						{
							
							tokens.Insert(j, "");
							j++;
						}
					}
					tokens.Insert(j-1, "");
					i = j;
				}
				if (tokens[i] == ";" && (tokens[i - 1] != ")" && tokens[i - 1] != ""))
				{
					tokens.Insert(i, "");
					i++;
				}
				if (tokens[i].ToUpper() == "END")
				{
					tokens.Insert(i, "");
					i++;
				}
				if (tokens[i] == ":=")
				{

					int j;
					for (j = i + 1; j < tokens.Count && tokens[j] != ";"; j++) 
					{
						if (tokens[j] == ")")
						{

							tokens.Insert(j, "");
							j++;
						}
					}
					if (tokens[j - 1] == ")")
					{
						tokens.Insert(j, "");
					}
					i = j - 1;
				}
				
			}
			return tokens;
		}

		public static List<string> ExtractTokens(string s)//starts recursive part
		{
			List<string> tokens = new List<string>();
			recExtractTokens(s, ref tokens);//tokens passed by reference

			return tokens;
		}

		public static void recExtractTokens(string s, ref List<string> tokens)//recursive part
		{
			string prefix = "";//everything before a Punctuation
			bool EndOfString = false;
			if (s != "")//stop on empty strings
			{
				string ch = s.Substring(0, 1);//get first character
				s = s.Substring(1);//remove first character

				while (!Punctuation.Contains(ch))//go until first Punctuation
				{
					prefix += ch;//add first character to the prefix
					if (s != "")
					{
						ch = s.Substring(0, 1);//get next character
						s = s.Substring(1);//remove new first character
					}
					else
					{
						EndOfString = true;
						break;
					}
				}
				tokens.Add(prefix);//add everything before Punctuation (an Ident or Numeral)
				if (!EndOfString)//prevents repeated characters
				{
					tokens.Add(ch);//add the Punctuation
				}
				recExtractTokens(s, ref tokens);//do again for rest of string
			}
		}
	}
}
