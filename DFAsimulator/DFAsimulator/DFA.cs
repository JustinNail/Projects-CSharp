using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DFAsimulator
{
	class DFA
	{
		private List<int> Finals;//list of keys for the final states
		private Dictionary<int,State> States;//all of the states in the machine
		private State currentState;//the current state
		
		public DFA()
		{
			//initialize
			Finals=new List<int>();
			States=new Dictionary<int,State>();
			currentState=null;
		}

		public void ReadDFA(string filepath)
		{
			try
			{
				//begin reading from file
				StreamReader sr = new StreamReader(filepath);


				string s = sr.ReadLine();//get first line
				string[] finals = s.Split(' ');//split it at spaces
				
				foreach (string element in finals)
				{
					try//trys to add each element to Finals, as an int
					{
						Finals.Add(int.Parse(element));
					}
					catch (Exception e)
					{
						Console.WriteLine("Could not parse state");
						Console.WriteLine(e.Message);
						return;
					}
				}

				//read the rest of the lines
				while ((s = sr.ReadLine()) != null)
				{
					string[] transition = s.Split(' ');//get the different parts of the transition
					int start;
					int dest;
					
					try
					{
						start = int.Parse(transition[0]);//first element is start state

						//if start state doesn't exsist yet, make it
						if (!(States.ContainsKey(start)))
						{
							State state = new State(start);
							//if start is a final state, set that flag
							if (Finals.Contains(start))
							{
								state.FinalState = true;
							}
							States.Add(start, state);
							
						}	
					}
					catch (Exception e)
					{
						Console.WriteLine("Could not parse state");
						Console.WriteLine(e.Message);
						return;
					}

					char symbol = transition[1][0];//symbol is in the second spot
					try
					{
						dest = int.Parse(transition[2]);//last is destination state

						//if it doesn't exsist yet, make it
						if (!(States.ContainsKey(dest)))
						{
							State state = new State(dest);
							//if it's a final state, set the flag
							if (Finals.Contains(dest))
							{
								state.FinalState = true;
							}
							States.Add(dest, state);
						}	
					}
					catch (Exception e)
					{
						Console.WriteLine("Could not parse state");
						Console.WriteLine(e.Message);
						return;
					}
					//add to the state's transitions
					States[start].addTransition(symbol,States[dest]);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("File could not be read");
				Console.WriteLine(e.Message);
				return;
			}
		}

		public bool ReadString(string input)
		{
			currentState = States[0];//0 is always initial state
			char symbol;

			//read each character in the string and have 
			//currentState change to the appropriate state
			for (int i=0; i < input.Length; i++)
			{
				symbol = input[i];
				State destinationState = currentState.changeState(symbol);
				if (destinationState == null)
				{
					throw new ArgumentNullException();
				}
				Console.WriteLine("{0} + {1} -> {2}", currentState.ID, symbol, destinationState.ID);
				currentState = destinationState;
			}
			//at the end, return whether or not 
			//machine is in final state
			return currentState.FinalState;
		}
	}
}
