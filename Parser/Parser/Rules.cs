using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parser
{
	class Rules
	{
		static int Line=1;//Line counter

		//Function to read any EOL's and inc Line counter
		public static void readEOL(ref List<string> tokens)
		{
			if (tokens[0].ToUpper() == "<EOL>")
			{
				Line++;//inc counter
				tokens.RemoveAt(0);//pop off read token
			}
		}

		public static bool PROGRAM(ref List<string> tokens)
		{
			readEOL(ref tokens);//check for EOL's

			if (tokens[0].ToUpper() == "BEGIN")//look for BEGIN token
			{
				tokens.RemoveAt(0);//pop off read token

				if (!STMT_LIST(ref tokens))//if STMT_LIST fails, then parse fails
				{
					return false;
				}
				

				if (tokens[0].ToUpper() == "END")//Look for END token
				{
					return true;//parse successful
				}
				else
				{
					Console.WriteLine("Line {0}: END expected",Line);//Error message
					return false;//fail
				}
			}
			else//BEGIN not found
			{
				Console.WriteLine("LINE {0}: BEGIN expected", Line);//Error message
				return false;//fail
			}
		}

		public static bool STMT_LIST(ref List<string> tokens)
		{
			readEOL(ref tokens);//check for EOL's
			
			if (STMT(ref tokens))//if proper STMT found
			{
				return STMT_LIST_TAIL(ref tokens);//STMT_LIST fails if STMT_LIST_TAIL fails
			}
			else//STMT failed
			{
				Console.WriteLine("LINE {0}: STATEMENT LIST Expected",Line);//Error message
				return false;//STMT_LIST fails
			}
			

		}

		public static bool STMT_LIST_TAIL(ref List<string> tokens)
		{
			readEOL(ref tokens);//check for EOL's
			if(tokens[0]=="")//look for NULL's
			{
				tokens.RemoveAt(0);//pop off read token
				return true;//STMT_LIST_TAIL valid
			}
			else if (STMT(ref tokens))//look for valid STMT
			{
				return STMT_LIST_TAIL(ref tokens);//look for another STMT_LIST_TAIL
			}
			else//NULL or STMT not found
			{
				Console.WriteLine("LINE {0}: STATEMENT Expected",Line);//Error message
				return false;//invalid
			}
		}
		public static bool STMT(ref List<string> tokens)
		{
			readEOL(ref tokens);//check for EOL's
			if (tokens[0].ToUpper() == "READ")//look for READ token
			{
				
				tokens.RemoveAt(0);//pop off read token
				readEOL(ref tokens);//check for EOL's
				if (tokens[0] == "(")//look for (
				{
					tokens.RemoveAt(0);//pop off read token

					if (ID_LIST(ref tokens))//look for valid ID_LIST
					{
						readEOL(ref tokens);//look for EOL's
				
						if (tokens[0] == ")")//look for )
						{
							tokens.RemoveAt(0);//pop off read token

							readEOL(ref tokens);//check for EOL's

							if (tokens[0] == ";")//look for ;
							{
								tokens.RemoveAt(0);//pop off read token
								return true;//get this far, STMT is valid
							}
							else//; not found
							{
								Console.WriteLine("LINE {0}: READ expects ;",Line);//error message
								return false;
							}

						}
						else//) not found
						{
							Console.WriteLine("LINE {0}: READ expects )",Line);//error message
							return false;
						}
					}
					else//ID_LIST wasn't valid
					{
						Console.WriteLine("LINE {0}: READ expects IDENTIFIERS", Line);//error message
						return false;
					}
				}
				else//( wasn't found
				{
					Console.WriteLine("LINE {0}: READ expects ( ",Line);
					return false;
				}
			}
			else if (tokens[0].ToUpper() == "WRITE")//looks for WRITE token
			{
				tokens.RemoveAt(0);//pop off read token
				readEOL(ref tokens);//check for EOL's
				if (tokens[0] == "(")//look for (
				{
					tokens.RemoveAt(0);//pop off read token
					if (EXPR_LIST(ref tokens))//look for valid EXPR_LIST
					{
						readEOL(ref tokens);//check for EOL's
						if (tokens[0] == ")")//look for )
						{
							tokens.RemoveAt(0);//pop off read token

							readEOL(ref tokens);//check for EOL's
							if (tokens[0] == ";")//look for ;
							{
								tokens.RemoveAt(0);//pop off read token
								return true;//STMT valid
							}
							else//; not found
							{
								Console.WriteLine("LINE {0}: WRITE expects ;",Line);//error message
								return false;
							}

						}
						else//) not found
						{
							Console.WriteLine("LINE {0}: WRITE expects )",Line);//error message
							return false;
						}
					}
					else//invalid EXPR_LIST
					{
						Console.WriteLine("LINE {0}: WRITE expects EXPRESSION",Line);//error message
						return false;
					}
				}
				else//( not found
				{
					Console.WriteLine("LINE {0}: WRITE expects (",Line);//error message
					return false;
				}
			}
			else if (IDENT(ref tokens))//look for an IDENT
			{
				readEOL(ref tokens);//check for EOL's
				if (tokens[0] == ":=")//look for :=
				{
					tokens.RemoveAt(0);//pop off read token
					if (EXPR(ref tokens))//look for valid EXPR
					{
						readEOL(ref tokens);//check for EOL's
						if (tokens[0] == ";")//look for ;
						{
							tokens.RemoveAt(0);//pop off read token
							return true;//valid STMT
						}
						else//; not found
						{
							Console.WriteLine("LINE {0}: := expects ;",Line);//Error message
							return false;
						}
					}
					else//invalid  EXPR
					{
						Console.WriteLine("LINE {0}: := expects EXPRESSION", Line);//Error message
						return false;
					}
				}
				else//:= not found
				{
					Console.WriteLine("LINE {0}: := Expected",Line);//error message
					return false;
				}
			}
			else//READ, WRITE, and IDENT all failed
			{
				Console.WriteLine("LINE {0}: Invalid STATEMENT",Line);//error message
				return false;//invalid STMT
			}
		}

		public static bool ID_LIST(ref List<string> tokens)
		{
			readEOL(ref tokens);//check for EOL's
			if (IDENT(ref tokens))//look for valid IDENT
			{
				return ID_LIST_TAIL(ref tokens);//look for valid ID_LIST_TAIL
			}
			else//IDENT not found
			{
				Console.WriteLine("LINE {0}: IDENTIFIER Expected",Line);//error message
				return false;
			}
		}
		public static bool ID_LIST_TAIL(ref List<string> tokens)
		{
			readEOL(ref tokens);//check foe EOL's

			if (tokens[0] == "")//look for NULL's
			{
				tokens.RemoveAt(0);//pop off read token
				return true;
			}
			else if (tokens[0] == ",")//look for ,
			{

				tokens.RemoveAt(0);//pop off read token
				if (IDENT(ref tokens))//look for IDENT
				{
					return (ID_LIST_TAIL(ref tokens));//do it again
				}
				else//IDENT not found
				{
					Console.WriteLine("LINE {0}: IDENTIFIER Expected",Line);//error message
					return false;
				}
			}
			else//NULL nor , found
			{
				Console.WriteLine("LINE {0}: Invalid IDENTIFIER LIST",Line);//error message
				return false;
			}
		}
		public static bool EXPR_LIST(ref List<string> tokens)
		{
			readEOL(ref tokens);//Check for EOL's
			if (EXPR(ref tokens))//look for valid EXPR
			{
				return EXPR_LIST_TAIL(ref tokens);//look for valid EXPR_LIST_TAIL
			}
			else//EXPR not valid
			{
				Console.WriteLine("LINE {0}: EXPRESSION Expected", Line);//error message
				return false;
			}
		}
		public static bool EXPR_LIST_TAIL(ref List<string> tokens)
		{
			readEOL(ref tokens);//check for EOL's
			if (tokens[0] == "")//look for null
			{
				tokens.RemoveAt(0);//pop off read token
				return true;
			}
			else if (tokens[0] == ",")//look for ,
			{
				tokens.RemoveAt(0);//pop off read token
				if (EXPR(ref tokens))//look  for valid EXPR
				{
					return (EXPR_LIST_TAIL(ref tokens));//repeat
				}
				else//EXPR not valid
				{
					Console.WriteLine("LINE {0}: EXPRESSION Expected",Line);//error message
					return false;
				}
			}
			else//null nor , found
			{
				Console.WriteLine("LINE {0}: Invalid EXPRESSION",Line);//error message
				return false;//invalid tail
			}
		}
		public static bool EXPR(ref List<string> tokens)
		{
			readEOL(ref tokens);//check for EOL's
			if (FACTOR(ref tokens))//look for valid FACTOR
			{
				return EXPR_TAIL(ref tokens);//look for valid EXPR_TAIL
			}
			else//FACTOR not valid
			{
				Console.WriteLine("LINE {0}: FACTOR Expected",Line);//error message
				return false;
			}
		}
		public static bool EXPR_TAIL(ref List<string> tokens)
		{
			readEOL(ref tokens);//check for EOL's
			if (tokens[0] == "")//look for NULL's
			{
				tokens.RemoveAt(0);//pop off read token
				return true;
			}
			
			else if (OP(ref tokens))//look for valid OP
			{
				if (FACTOR(ref tokens))//look for valid FACTOR
				{
					return EXPR_TAIL(ref tokens);//Look for another tail
				}
				else//FACTOR not valid
				{
					Console.WriteLine("LINE {0}: FACTOR Expected",Line);//error message
					return false;
				}
			}
			else//OP not valid
			{
				Console.WriteLine("LINE {0}: OPERAND Expected",Line);//error message
				return false;
			}
		}
		public static bool FACTOR(ref List<string> tokens)
		{
			readEOL(ref tokens);//check for EOL's
			int num;//catch for Integer.TryParse

			if (tokens[0] == "(")//look for (
			{
				tokens.RemoveAt(0);//pop off read token
				if (EXPR(ref tokens))//look for valid EXPR
				{
					readEOL(ref tokens);//check for EOL
					if (tokens[0] == ")")//look for )
					{
						tokens.RemoveAt(0);//pop off read token
						return true;//valid FACTOR
					}
					else//) not found
					{
						Console.WriteLine("LINE {0}: ) Expected",Line);//error message
						return false;
					}
				}
				else//EXPR not valid
				{
					Console.WriteLine("LINE {0}: EXPRESSION Expected",Line);//error message
					return false;
				}
			}
			else if (int.TryParse(tokens[0], out num))//look for valid int, num holds parsed int if valid
			{
				tokens.RemoveAt(0);
				return true;//valid FACTOR
			}
			else if (IDENT(ref tokens))//look for valid IDENT
			{
				return true;
			}
			else//no valid FACTOR found
			{
				Console.WriteLine("LINE {0}: Invalid FACTOR",Line);//error message
				return false;
			}
		}
		public static bool OP(ref List<string> tokens)
		{
			readEOL(ref tokens);//check for EOL's

			if (tokens[0] == "+")//look for a +
			{
				tokens.RemoveAt(0);
				return true;
			}
			else if (tokens[0] == "-")//look for a -
			{

				tokens.RemoveAt(0);
				return true;
			}
			else//no other valid OP's
			{
				Console.WriteLine("LINE {0}: Invalid OPERATOR",Line);//error message
				return false;
			}

		}
		public static bool IDENT(ref List<string> tokens)
		{
			readEOL(ref tokens);//check for EOL's
			if (char.IsLetter(tokens[0][0]))//first character is a letter
			{
				foreach (char c in tokens[0])
				{
					//the rest of the characters must be alphanumeric or _
					if (c != '_' && !char.IsLetterOrDigit(c))//if not, invalid IDENT
					{
						Console.WriteLine("LINE {0}: Invalid IDENTIFIER",Line);//error
						return false;
					}
				}
				tokens.RemoveAt(0);//pop off read token
				return true;
			}
			else//must start with letter
			{
				Console.WriteLine("LINE {0}: Invalid IDENTIFIER",Line);//error message
				return false;
			}
		}
	}
  
}
