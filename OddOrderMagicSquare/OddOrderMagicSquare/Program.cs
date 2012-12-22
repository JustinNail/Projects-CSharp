/*
 * Justin Nail
 * Math 420: Windows Software Developement
 * Assignment 1
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OddOrderMagicSquare
{
	class Program
	{
		static void Main(string[] args)//arrg i'm a pirate
		{
			//declare variables
			int n;
			int[,] MagicSquare;

			//do-while for order verification
			do
			{
				n = GetOrder();//GetInt returns -1 if invalid order
			} while (n < 0);//if n>=0, it must be a valid order

			MagicSquare = MakeSquare(n);//method makes the magic square
			PrintSquare(MagicSquare);//method prints the magic square
			Console.ReadKey();//waits for any keyboard input, not just the enter key
		}

		private static void PrintSquare(int[,] MagicSquare)
		{
			//declare variables

			//sums for row, column and diagonal, respectively
			int rsum=0,csum=0,dsum=0;
			//n is the order, GetLength(0) return length of first dimension
			//it's square so it doesn't really matter which dimension i use
			int n=MagicSquare.GetLength(0);
			//SumDigits equals the number of digits the sums will have, for formatting
			int SumDigits = ((((n * n) + 1) / 2) * n).ToString().Length;

			//nested for loops for moving through the 2-d array
			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < n; j++)
				{
					//print the elements of the magic square

					//switch on the number of digits
					//could find no cleaner way of having the flexible formatting
					switch (SumDigits)
					{
							//sum is 2 digits long, so use d2 for formatting
						case 2: Console.Write("{0:d2}  ", MagicSquare[i, j]);
							break;
							//sum is 3 digits long, so use d3 for formatting
						case 3: Console.Write("{0:d3}  ", MagicSquare[i, j]);
							break;
					}

					//i==j means it's on the main diagonal
					if (i == j)
					{
						dsum += MagicSquare[i, j];//add to diagonal sum
					}
					rsum += MagicSquare[i, j];//add each element in row to the row sum
				}

				//print the row sums

				//switch on the number of digits
				//could find no cleaner way of having the flexible formatting
				switch (SumDigits)
				{
						//sum is 2 digits long, so use d2 for formatting
					case 2: Console.WriteLine(" {0:d2}", rsum);
						break;
						//sum is 3 digits long, so use d3 for formatting
					case 3: Console.WriteLine(" {0:d3}", rsum);
						break;
				}
				//reset the row sum at the end of each row, after printing
				rsum = 0;
			}

			Console.WriteLine();//blank line before displaying the column sums

			//inverse of previous nested loops, goes column wise
			for (int j = 0; j < MagicSquare.GetLength(1); j++)
			{
				for (int i = 0; i < MagicSquare.GetLength(0); i++)
				{
					csum += MagicSquare[i, j];//add each element in the column to the column sum
				}

				//print column sums

				//switch on the number of digits
				//could find no cleaner way of having the flexible formatting
				switch (SumDigits)
				{
						//sum is 2 digits long, so use d2 for formatting
					case 2: Console.Write("{0:d2}  ", csum);
						break;
						//sum is 3 digits long, so use d3 for formatting
					case 3: Console.Write("{0:d3}  ", csum);
						break;
				}
				//reset the column sum at end of each column, after printing
				csum = 0;
			}

			//finally, print the diagonal sum

			//switch on the number of digits
			//could find no cleaner way of having the flexible formatting
			switch (SumDigits)
			{
					//sum is 2 digits long, so use d2 for formatting
				case 2: Console.Write(" {0:d2}", dsum);
					break;
					//sum is 3 digits long, so use d3 for formatting
				case 3: Console.Write(" {0:d3}", dsum);
					break;
			}
		}

		private static int[,] MakeSquare(int n)
		{
			int[,] MagicSquare = new int[n, n];//instantiate an nxn array
			
			//equation assumes rows and columns start at 1
			for (int i = 1; i <= n; i++)
			{
				for (int j = 1; j <= n; j++)
				{
					MagicSquare[i-1, j-1]=(n*((i+j-1+(n/2))%n))+((i+(2*j)-2)%n)+1;//equation from wikipedia
				}
			}
			return MagicSquare;
		}
		private static int GetOrder()
		{
			//declare variables
			string sInt;//string form of the integer
			int n;

			//prompt and read from console
			Console.WriteLine("Enter an odd positive integer");
			sInt = Console.ReadLine();

			//try to parse the string to an int
			try
			{
				n = int.Parse(sInt);
			}
			//Parse throws a FormatException if it cannot convert the passed string
			catch (FormatException)
			{
				//print error to console and return -1, meaning it's invalid
				Console.WriteLine("Unable to convert '{0}' to an integer",sInt);
				return -1;
			}
			//if order must be > 1
			if (n <= 1)
			{
				//print error to console and return -1, meaning it's invalid
				Console.WriteLine("Order is too small");
				return -1;
			}
			//assignment specified that 11 was the maximum order
			if (n > 11)
			{
				//print error to console and return -1, meaning it's invalid
				Console.WriteLine("Order is too large");
				return -1;
			}
			//order must be odd
			if (n % 2 == 0)
			{
				//print error to console and return -1, meaning it's invalid
				Console.WriteLine("Order is not odd");
				return -1;
			}
			//if it makes it here, it's valid so return the order
			return n;
		}
	}
}
