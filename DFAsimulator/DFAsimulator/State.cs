using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFAsimulator
{
	class State
	{
		private List<Transition> transitions;//list of every transition from this state
		public bool FinalState { get; set; }//Final state 'flag'

		int id;//state id
		public int ID { get { return id; } }//read-only

		public State(int id)
		{
			//initialize
			this.id = id;
			transitions=new List<Transition>();
		}


		public void addTransition(char symbol, State destination)
		{
			transitions.Add(new Transition(symbol, destination));
		}

		//returns the state to change to based on passed symbol
		//return null if it has no transition for that symbol
		public State changeState (char symbol)
		{
			foreach (Transition t in transitions)
			{
				if (t.Symbol == symbol)
				{
					return t.Destination;
				}
			}
			return null;
		}
	}
}
