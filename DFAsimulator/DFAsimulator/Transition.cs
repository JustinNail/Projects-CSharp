using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFAsimulator
{
	class Transition
	{
		private char symbol;//symbol that id's the transition
		public char Symbol { get { return symbol; } }//read-only

		private State destination;//state to move to
		public State Destination { get { return destination; } }//read-only


		public Transition(char symbol, State destination)
		{
			this.symbol = symbol;
			this.destination = destination;
		}
	}
}
